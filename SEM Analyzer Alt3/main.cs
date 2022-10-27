using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace SEM_Analyzer_Alt3
{
    public partial class Form1 : Form
    {
        protected bool validData;
        string path;
        protected Image image;
        Emgu.CV.Image<Bgr, Byte> RawImage;
        Emgu.CV.Image<Bgr, byte> BitmapROI;
        Emgu.CV.Image<Gray, byte> BinaryROI;
        protected Thread getImageThread;

        //ReportData
        double MeanArea = 0;
        int ObjectAmount = 0;
        double StdDev = 0;
        double[] FilteredArea;

        //settings
        int RedThreshold, GreenThreshold, BlueThreshold;
        double MinArea, MaxArea;
        bool EnabledAnalysis = false;
        int LineThickness = 1;
        Rectangle ROIRect = new Rectangle();
        Point ROIp1;
        bool SelectingROI = false;
        double UndoZoomLevel = 1;
        int UndoHorzScroll = 0;
        int UndoVertScroll = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RedThreshold = RedThreshold_TrackBar.Value;
            GreenThreshold = GreenThreshold_TrackBar.Value;
            BlueThreshold = BlueThreshold_TrackBar.Value;
            MinArea = Convert.ToDouble(MinArea_TextBox.Text);
            MaxArea = Convert.ToDouble(MaxArea_TextBox.Text);
            LineThickness = LineThickness_TrackBar.Value;

            RedThreshold_Label.Text = "Grey";
            GreenThreshold_TrackBar.Visible = false;
            BlueThreshold_TrackBar.Visible = false;
            GreenThreshold_Label.Visible = false;
            BlueThreshold_Label.Visible = false;

            DragIcon_PictureBox.BringToFront();
            DragIcon_Label.BringToFront();
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            string filename;
            validData = GetFilename(out filename, e);
            if (validData)
            {
                path = filename;
                getImageThread = new Thread(new ThreadStart(LoadImage));
                getImageThread.Start();
                e.Effect = DragDropEffects.Copy;
                DragIcon_PictureBox.SendToBack();
                DragIcon_Label.SendToBack();
            }
            else
                e.Effect = DragDropEffects.None;
        }
        private bool GetFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;
            filename = String.Empty;
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp") || (ext == ".tif"))
                        {
                            ret = true;
                            Path_Textbox.Text = filename;
                        }
                    }
                }
            }
            return ret;
        }
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (validData)
            {
                while (getImageThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(0);
                }
            }
        }
        protected void LoadImage()
        {
            // import raw image from file directory
            RawImage = new Image<Bgr, Byte>(path);

            // create a copy and crop image
            BitmapROI = RawImage.Clone();
            BitmapROI.ROI = ROIRect;

            // eval offset from ROI to global for shifting localized contour image in ROI
            Point ROIOffset = new Point();
            ROIOffset.X = ROIRect.X;
            ROIOffset.Y = ROIRect.Y;

            if (EnabledAnalysis)
            {
                if (Invert_CheckBox.Checked)
                {
                    BitmapROI = ~BitmapROI;
                }
                // binarization
                if (Grayscale_CheckBox.Checked)
                {
                    BitmapROI = BitmapROI.ThresholdBinary(new Bgr(RedThreshold, RedThreshold, RedThreshold), new Bgr(255, 255, 255));
                }
                else
                {
                    BitmapROI = BitmapROI.ThresholdBinary(new Bgr(BlueThreshold, GreenThreshold, RedThreshold), new Bgr(255, 255, 255));
                }

                // find contour
                BinaryROI = BitmapROI.Convert<Gray, Byte>();

                VectorOfVectorOfPoint Contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(BinaryROI, Contours, null, RetrType.List, ChainApproxMethod.ChainApproxNone);

                // eval contour area 
                ObjectAmount = 0;
                double LargestArea = 0;
                int LargestInd = 0;
                double SmallestArea= 2147483647;
                int SmallestInd = 0;
                Array.Resize(ref FilteredArea, Contours.Size);
                for (int i = 0; i < Contours.Size; i++)
                {
                    double Area = CvInvoke.ContourArea(Contours[i], false);
                    if (Area > MinArea && Area < MaxArea)
                    {
                        CvInvoke.DrawContours(RawImage, Contours, i, new MCvScalar(0, 255, 0, 255), LineThickness, LineType.EightConnected, null, 2147483647, ROIOffset);

                        // rebuild area array to exclude filtered values
                        FilteredArea[ObjectAmount] = Area;

                        ObjectAmount++;
                        if(Area> LargestArea)
                        {
                            LargestArea = Area;
                            LargestInd = i;
                        }
                        if (Area < SmallestArea)
                        {
                            SmallestArea = Area;
                            SmallestInd = i;
                        }
                    }
                }
                CvInvoke.DrawContours(RawImage, Contours, LargestInd, new MCvScalar(0, 0, 255, 255), LineThickness, LineType.EightConnected, null, 2147483647, ROIOffset);
                CvInvoke.DrawContours(RawImage, Contours, SmallestInd, new MCvScalar(255, 0, 0, 255), LineThickness, LineType.EightConnected, null, 2147483647, ROIOffset);


                if (ObjectAmount > 0)
                {
                    // trim array from excessive empty elements
                    Array.Resize(ref FilteredArea, ObjectAmount);

                    // find avg
                    MeanArea = FilteredArea.Average();

                    // find std dev
                    StdDev = Stddev(FilteredArea);
                }
                else
                {
                    Array.Resize(ref FilteredArea, 1);
                    FilteredArea[0] = 0;
                    MeanArea = 0;
                    StdDev = 0;
                }
            }
            Main_panAndZoomPictureBox.Image = RawImage.ToBitmap();

        }
        void UpdateReport()
        {
            // print report
            Report_RichTextBox.Text = "----------------------------------" +
                                       "\nReport:\n" +
                                       "Objects= " + ObjectAmount.ToString() + "\n" +
                                       "Smallest Area= " + FilteredArea.Min().ToString() + "\n" +
                                       "Largest Area= " + FilteredArea.Max().ToString() + "\n" +
                                       "Mean Area= " + MeanArea.ToString() + "\n" +
                                       "Area std dev= " + StdDev.ToString() + "\n" +
                                       "----------------------------------" +
                                       "\nConfiguration:\n" +
                                       "Threshold(R/Grey): " + RedThreshold.ToString() + "\n" +
                                       "Threshold(G): " + GreenThreshold.ToString() + "\n" +
                                       "Threshold(B): " + BlueThreshold.ToString() + "\n" +
                                       "Max Area Filter: " + MaxArea.ToString() + "\n" +
                                       "Min Area Filter: " + MinArea.ToString() + "\n"+
                                       "Inverted: "+ Invert_CheckBox.Checked.ToString()+"\n";
        }
        double Stddev(double[] Values)
        {
            double avg = Values.Average();
            double sum = 0;
            for (int i = 1; i < Values.Length; i++)
            {
                sum = sum + Math.Pow((Values[i] - avg), 2);
            }
            return Math.Sqrt(sum / Values.Length);
        }
 

        private void RedThreshold_TrackBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                RedThreshold = RedThreshold_TrackBar.Value;
                LoadImage();
                UpdateReport();
            }
            catch { }
        }

        private void MinArea_TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MinArea = Convert.ToDouble(MinArea_TextBox.Text);
                LoadImage();
                UpdateReport();
            }
            catch { }
        }

        private void MaxArea_TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MaxArea = Convert.ToDouble(MaxArea_TextBox.Text);
                LoadImage();
                UpdateReport();
            }
            catch { }
        }

        private void ExportAsCSV_Button_Click(object sender, EventArgs e)
        {

        }
        private void Enabled_Button_Click(object sender, EventArgs e)
        {
            if (Enabled_Button.Checked)
            {
                Enabled_Button.CheckState = CheckState.Unchecked;
                try
                {
                    EnabledAnalysis = false;
                    LoadImage();
                    UpdateReport();
                }
                catch { }
            }
            else
            {
                Enabled_Button.CheckState = CheckState.Checked;
                try
                {
                    EnabledAnalysis = true;
                    LoadImage();
                    UpdateReport();
                }
                catch { }
            }
        }
        private void LineThickness_TrackBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                LineThickness = LineThickness_TrackBar.Value;
                LoadImage();
                UpdateReport();
            }catch { }
        }


        private void distrubutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prepare data
            var ans = KernelDensityEstimation(FilteredArea, 50, 1000);

            // show plot
            Graph_Form graph_Form = new Graph_Form();
            Graph_Form.Title = "Area Distrubution";
            Graph_Form.x = ans.Item1;
            Graph_Form.y = ans.Item2;
            Graph_Form.XLabel = "Area";
            Graph_Form.YLabel = "Density";
            graph_Form.ShowDialog();
        }

        private void Grayscale_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Grayscale_CheckBox.Checked)
            {
                RedThreshold_Label.Text = "Grey";
                GreenThreshold_TrackBar.Visible = false;
                BlueThreshold_TrackBar.Visible = false;
                GreenThreshold_Label.Visible = false;
                BlueThreshold_Label.Visible = false;
            }
            else
            {
                RedThreshold_Label.Text = "Red";
                GreenThreshold_TrackBar.Visible = true;
                BlueThreshold_TrackBar.Visible = true;
                GreenThreshold_Label.Visible = true;
                BlueThreshold_Label.Visible = true;
            }
            if (validData)
            {
                LoadImage();
                UpdateReport();
            }
        }

        private void SelectROI_Button_Click(object sender, EventArgs e)
        {
            if(SelectROI_Button.Checked)
            {
                SelectROI_Button.CheckState = CheckState.Unchecked;
                SelectROI_Button.Checked = false;
            }
            else
            {
                SelectROI_Button.CheckState = CheckState.Checked;
                SelectROI_Button.Checked = true;
            }
        }
        private void SelectROI_PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if(SelectROI_Button.Checked)
            {
                //find p2 of the ROI bounding box, offsetted by imagebox scrollbox
                int horz = Main_panAndZoomPictureBox.HorizontalScrollBar.Value;
                int vert = Main_panAndZoomPictureBox.VerticalScrollBar.Value;

                ROIp1.X = (int)(((double)e.X) / Main_panAndZoomPictureBox.ZoomScale)+horz;
                ROIp1.Y = (int)(((double)e.Y) / Main_panAndZoomPictureBox.ZoomScale)+vert;
                SelectingROI = true;

                // record imagebox built in zoom/pan so that it can be undo when the mouse is released (can't disable pan/zoom)
                UndoZoomLevel = Main_panAndZoomPictureBox.ZoomScale;
                UndoHorzScroll = horz;
                UndoVertScroll = vert;
            }
        }
        private void SelectROI_PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectROI_Button.Checked)
            {
                SelectingROI = false;

                // undo autozoom and pan
                Point point = new Point(UndoHorzScroll, UndoVertScroll);
                Main_panAndZoomPictureBox.SetZoomScale(UndoZoomLevel, point);
                Main_panAndZoomPictureBox.HorizontalScrollBar.Value = UndoHorzScroll;
                Main_panAndZoomPictureBox.VerticalScrollBar.Value = UndoVertScroll;

                // unpop button
                SelectROI_Button.CheckState = CheckState.Unchecked;
                SelectROI_Button.Checked = false;
            }
        }
        private void SelectROI_PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (SelectROI_Button.Checked&&SelectingROI)
            {
                int horz = Main_panAndZoomPictureBox.HorizontalScrollBar.Value;
                int vert = Main_panAndZoomPictureBox.VerticalScrollBar.Value;

                Point ROIp2 = new Point(0,0);
                ROIp2.X = (int)(((double)e.X) / Main_panAndZoomPictureBox.ZoomScale) + horz;
                ROIp2.Y = (int)(((double)e.Y) / Main_panAndZoomPictureBox.ZoomScale) + vert;
                

                ROIRect = Main_panAndZoomPictureBox.GetRectangle(ROIp1, ROIp2);
                Refresh();
                try
                {
                    LoadImage();
                    UpdateReport();
                }
                catch { }
            }
        }
        private void SelectROI_PictureBox_Draw(object sender, PaintEventArgs e)
        {
            // draw yellow box
            Pen pen = new Pen(Color.Yellow,1);
            e.Graphics.DrawRectangle(pen, ROIRect);
        }

        private void SelectRuler_Button_Click(object sender, EventArgs e)
        {

        }

        private void Invert_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadImage();
                UpdateReport();
            }
            catch { }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void Help_Button_Click(object sender, EventArgs e)
        {

        }
        private void ShowRawImg_Button_Click(object sender, EventArgs e)
        {
            // show cropped image (recropping to not interrupt process)
            Image<Bgr, Byte> temp = new Image<Bgr, Byte>(path);
            temp.ROI = ROIRect;
            ProcessView ProcessView = new ProcessView();
            ProcessView.Image = temp.ToBitmap();

            ProcessView.ShowDialog();
        }

        private void BinarizationVew_Button_Click(object sender, EventArgs e)
        {
            // show binary process
            ProcessView ProcessView = new ProcessView();
            ProcessView.Image = BinaryROI.ToBitmap();

            ProcessView.ShowDialog();
        }



        private void ContourView_Button_Click(object sender, EventArgs e)
        {
            // show only contour
            Emgu.CV.Image<Bgr, byte> temp = new Image<Bgr, byte>(BinaryROI.Width, BinaryROI.Height);
            temp = ~temp;

            // find contour
            BinaryROI = BitmapROI.Convert<Gray, Byte>();
            VectorOfVectorOfPoint Contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(BinaryROI, Contours, null, RetrType.List, ChainApproxMethod.ChainApproxNone);

            // eval contour area 
            Array.Resize(ref FilteredArea, Contours.Size);
            double LargestArea = 0;
            int LargestInd = 0;
            double SmallestArea = 0;
            int SmallestInd = 0;
            for (int i = 0; i < Contours.Size; i++)
            {
                double Area = CvInvoke.ContourArea(Contours[i], false);
                if (Area > MinArea && Area < MaxArea)
                {
                    CvInvoke.DrawContours(temp, Contours, i, new MCvScalar(0, 255, 0, 255), LineThickness);
                    if (Area > LargestArea)
                    {
                        LargestArea = Area;
                        LargestInd = i;
                    }
                    if (Area < SmallestArea)
                    {
                        SmallestArea = Area;
                        SmallestInd = i;
                    }
                }
            }
            CvInvoke.DrawContours(temp, Contours, LargestInd, new MCvScalar(0, 0, 255, 255), LineThickness);
            CvInvoke.DrawContours(temp, Contours, SmallestInd, new MCvScalar(255, 0, 0, 255), LineThickness);

            // pass img to next winform
            ProcessView ProcessView = new ProcessView();
            ProcessView.Image = temp.ToBitmap();
            ProcessView.ShowDialog();
        }

        private void UndoZoom_Button_Click(object sender, EventArgs e)
        {
            Point temp = new Point(0,0);
            Main_panAndZoomPictureBox.SetZoomScale(1, temp);
        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

        }

        private void plotDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DiscardLargest_Button_Click(object sender, EventArgs e)
        {
            try
            {
                MaxArea_TextBox.Text=FilteredArea.Max().ToString();
                MaxArea = FilteredArea.Max();
            }
            catch
            {
                MessageBox.Show("no object to discard!");
            }
        }

        private void DiscardSmallest_Button_Click(object sender, EventArgs e)
        {
            try
            {
                MinArea_TextBox.Text = FilteredArea.Min().ToString();
                MinArea = FilteredArea.Min();
            }
            catch
            {
                MessageBox.Show("no object to discard!");
            }
        }

        private void GreenThreshold_TrackBar_Scroll(object sender, EventArgs e)
        {
            GreenThreshold = GreenThreshold_TrackBar.Value;
            LoadImage();
            UpdateReport();
        }

        private void BlueThreshold_TrackBar_Scroll(object sender, EventArgs e)
        {
            BlueThreshold = BlueThreshold_TrackBar.Value;
            LoadImage();
            UpdateReport();
        }
        public static Tuple<double[], double[]> KernelDensityEstimation(double[] data, double sigma, int nsteps)
        {
            // probability density function (PDF) signal analysis
            // Works like ksdensity in mathlab. 
            // KDE performs kernel density estimation (KDE)on one - dimensional data
            // http://en.wikipedia.org/wiki/Kernel_density_estimation

            // Input:	-data: input data, one-dimensional
            //          -sigma: bandwidth(sometimes called "h")
            //          -nsteps: optional number of abscis points.If nsteps is an
            //          array, the abscis points will be taken directly from it. (default 100)
            // Output:	-x: equispaced abscis points
            //          -y: estimates of p(x)

            // This function is part of the Kernel Methods Toolbox(KMBOX) for MATLAB. 
            // http://sourceforge.net/p/kmbox
            // Converted to C# code by ksandric

            double[,] result = new double[nsteps, 2];
            double[] x = new double[nsteps], y = new double[nsteps];

            double MAX = data.Max(), MIN = data.Min();
            int N = data.Length; // number of data points


            // Like MATLAB linspace(MIN, MAX, nsteps);
            x[0] = MIN;
            for (int i = 1; i < nsteps; i++)
            {
                x[i] = x[i - 1] + ((MAX - MIN) / nsteps);
            }

            // kernel density estimation
            double c = 1.0 / (Math.Sqrt(2 * Math.PI * sigma * sigma));
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < nsteps; j++)
                {
                    y[j] = y[j] + 1.0 / N * c * Math.Exp(-(data[i] - x[j]) * (data[i] - x[j]) / (2 * sigma * sigma));
                }
            }

            return new Tuple<double[], double[]>(x, y);
        }
    }
}
