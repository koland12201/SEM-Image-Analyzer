using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Reflection;
using System.Configuration;
using System.Globalization;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.OCR;

using CsvHelper;

namespace SEM_Analyzer_Alt3
{

    public partial class Form1 : Form
    {
        protected bool validData;
        string path;
        protected Image image;
        Emgu.CV.Image<Bgr, Byte> PictureBoxImage;
        Emgu.CV.Image<Bgr, Byte> RawImage;
        Emgu.CV.Image<Bgr, byte> BitmapROI;
        Emgu.CV.Image<Gray, byte> BinaryROI;
        protected Thread getImageThread;

        //ReportData
        double MeanArea = 0;
        int ObjectAmount = 0;
        double StdDev = 0;
        double[] FilteredArea;
        RotatedRect[] BoundingBoxes = new RotatedRect[1];
        double RulerLength = 0;
        List<Point> RulerPoints = new List<Point>();
        VectorOfVectorOfPoint Contours = new VectorOfVectorOfPoint();

        //image properties
        int ScaleBarPx = 0;
        double ImageZoomLevel = 100;
        double PixelSize = 1;
        string MeasUnit = "px";

        //settings
        int RedThreshold, GreenThreshold, BlueThreshold;
        double MinArea, MaxArea;
        bool EnabledAnalysis = false;
        int LineThickness = 1;
        Rectangle ROIRect = new Rectangle();
        Rectangle ScaleBarRect = new Rectangle();
        Rectangle OCRRect = new Rectangle();
        Point ROIp1;
        bool SelectingROI = false;
        bool SelectingScalingBar = false;
        bool SelectingOCRRegion = false;
        double UndoZoomLevel = 1;
        int UndoHorzScroll = 0;
        int UndoVertScroll = 0;

        public Form1()
        {
            InitializeComponent();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string displayableVersion = $"{version.Build}";
            this.Text = "SEM Analyzer - koland #" + displayableVersion;

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
                // import raw image from file directory
                RawImage = new Image<Bgr, Byte>(path);
                getImageThread = new Thread(new ThreadStart(LoadImage));
                getImageThread.Start();
                e.Effect = DragDropEffects.Copy;
                DragIcon_PictureBox.SendToBack();
                DragIcon_Label.SendToBack();
                
                LoadImage();
                if(!OCRRect.IsEmpty)
                {
                    UpdateUnit();
                }
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
            try
            {
                // create a copy and crop image
                PictureBoxImage = RawImage.Clone();
                BitmapROI = RawImage.Clone();             
                BitmapROI.ROI = ROIRect;
            }
            catch
            {
                MessageBox.Show("Import an image first!");
            }

            try
            {
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
                        BinaryROI = BitmapROI.ThresholdBinary(new Bgr(RedThreshold, RedThreshold, RedThreshold), new Bgr(255, 255, 255)).Convert<Gray, Byte>();
                    }
                    else
                    {
                        BinaryROI = BitmapROI.ThresholdBinary(new Bgr(BlueThreshold, GreenThreshold, RedThreshold), new Bgr(255, 255, 255)).Convert<Gray, Byte>();
                    }

                    // find contour
                    CvInvoke.FindContours(BinaryROI, Contours, null, RetrType.List, ChainApproxMethod.ChainApproxNone);

                    // eval contour area 
                    ObjectAmount = 0;
                    double LargestArea = 0;
                    int LargestInd = 0;
                    double SmallestArea = 2147483647;
                    int SmallestInd = 0;
                    Array.Resize(ref FilteredArea, Contours.Size); //not a good practice but it works
                    if(AutoLength_Button.Checked)
                    {
                        Array.Resize(ref BoundingBoxes, Contours.Size);
                    }

                    for (int i = 0; i < Contours.Size; i++)
                    {
                        double Area = CvInvoke.ContourArea(Contours[i], false);
                        
                        if (Area > MinArea && Area < MaxArea)
                        {
                            CvInvoke.DrawContours(PictureBoxImage, Contours, i, new MCvScalar(0, 255, 0, 255), LineThickness, LineType.EightConnected, null, 2147483647, ROIOffset);

                            // rebuild area array to exclude filtered values
                            FilteredArea[ObjectAmount] = Area*PixelSize;

                            ObjectAmount++;
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

                            //calculate bounding box for each contour
                            if (AutoLength_Button.Checked)
                            {
                                // locate bounding boxes and record findings (so it can be drawn later
                                BoundingBoxes[ObjectAmount] = CvInvoke.MinAreaRect(Contours[i]);
                                
                            }

                        }
                    }

                    if (HighlightLS_CheckBox.Checked)
                    {
                        CvInvoke.DrawContours(PictureBoxImage, Contours, LargestInd, new MCvScalar(0, 0, 255, 255), LineThickness, LineType.EightConnected, null, 2147483647, ROIOffset);
                        CvInvoke.DrawContours(PictureBoxImage, Contours, SmallestInd, new MCvScalar(255, 0, 0, 255), LineThickness, LineType.EightConnected, null, 2147483647, ROIOffset);
                    }

                    if (ObjectAmount > 0)
                    {
                        // trim array from excessive empty elements
                        Array.Resize(ref FilteredArea, ObjectAmount);
                        if (AutoLength_Button.Checked)
                        {
                            Array.Resize(ref BoundingBoxes, ObjectAmount);
                        }

                        // find avg
                        MeanArea = FilteredArea.Average();

                        // find std dev
                        StdDev = Stddev(FilteredArea);

                        // draw found bounding boxes to picturebox
                        if (AutoLength_Button.Checked)
                        {
                            for(int i=0; i < ObjectAmount; i++)
                            {
                                //draw bounding boxes
                                PointF[] BoxPoints = BoundingBoxes[i].GetVertices();
                                for (int j = 0; j < BoxPoints.Length; j++)
                                {
                                    CvInvoke.Line(PictureBoxImage, new Point((int)BoxPoints[j].X + ROIOffset.X, (int)BoxPoints[j].Y + ROIOffset.Y), new Point((int)BoxPoints[(j + 1) % 4].X + ROIOffset.X, (int)BoxPoints[(j + 1) % 4].Y + ROIOffset.Y), new MCvScalar(0, 0, 255, 255), 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        Array.Resize(ref FilteredArea, 1);
                        FilteredArea[0] = 0;
                        MeanArea = 0;
                        StdDev = 0;
                    }
                    UpdateReport();
                }
                if (validData)
                {
                    Main_panAndZoomPictureBox.Image = PictureBoxImage.ToBitmap();
                }
            }
            catch
            {

            }

        }
        void UpdateReport()
        {
            // print report
            Report_RichTextBox.Text = "----------------------------------" +
                                       "\nReport (summary):\n" +
                                       "Objects = " + ObjectAmount.ToString() + "\n" +
                                       "Smallest obj = " + FilteredArea.Min().ToString() + " " + MeasUnit + "^2\n" +
                                       "Largest obj = " + FilteredArea.Max().ToString() + " " + MeasUnit + "^2\n" +
                                       "Mean Area= " + MeanArea.ToString() + " " + MeasUnit + "^2\n" +
                                       "Area std dev= " + StdDev.ToString() + "\n" +
                                       "----------------------------------" +
                                       "\nData Extraction:\n" +
                                       "ROI X: " + ROIRect.X.ToString() + "\n" +
                                       "ROI Y: " + ROIRect.Y.ToString() + "\n" +
                                       "ROI Height: " + ROIRect.Height.ToString() + "\n" +
                                       "ROI Width: " + ROIRect.Width.ToString() + "\n"+
                                       "Scale Bar lengh in Pixels: " + ScaleBarPx.ToString() + "\n" +
                                       "Scale Bar Real Length: " + ImageZoomLevel.ToString() + "\n" +
                                       "Pixel Size in Real Units: " + PixelSize.ToString() + "\n" +
                                       "Unit:  " + MeasUnit + "\n" +
                                       "----------------------------------" +
                                       "\nConfiguration:\n" +
                                       "Threshold(R/Grey): " + RedThreshold.ToString() + "\n" +
                                       "Threshold(G): " + GreenThreshold.ToString() + "\n" +
                                       "Threshold(B): " + BlueThreshold.ToString() + "\n" +
                                       "Max Area Filter: " + MaxArea.ToString() + "\n" +
                                       "Min Area Filter: " + MinArea.ToString() + "\n" +
                                       "Inverted: " + Invert_CheckBox.Checked.ToString() + "\n";

        }
        void UpdateUnit()
        {
            // Run OCR to detect unit
            Image<Gray, byte> OCRImage = RawImage.ThresholdBinary(new Bgr(255 / 2, 255 / 2, 255 / 2), new Bgr(255, 255, 255)).Convert<Gray, Byte>();
            OCRImage.ROI = OCRRect;
            ScaledUnit_TextBox.Text = OCR(OCRImage);
        }

        void UpdatePxScaling()
        {
            ImageZoomLevel = Convert.ToDouble(ScaledUnit_TextBox.Text.Split(' ')[0].Trim());
            MeasUnit = ScaledUnit_TextBox.Text.Split(' ')[1].Trim();
            PixelSize = ImageZoomLevel / ScaleBarPx;
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
            RedThreshold = RedThreshold_TrackBar.Value;
            LoadImage();
        }

        private void MinArea_TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MinArea = Convert.ToDouble(MinArea_TextBox.Text);
            }
            catch
            {
            }
            LoadImage();
        }

        private void MaxArea_TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MaxArea = Convert.ToDouble(MaxArea_TextBox.Text);
            }
            catch { }
            LoadImage();
        }

        private void Enabled_Button_Click(object sender, EventArgs e)
        {
            if (Enabled_Button.Checked)
            {
                Enabled_Button.CheckState = CheckState.Unchecked;
                EnabledAnalysis = false;
            }
            else
            {
                Enabled_Button.CheckState = CheckState.Checked;
                EnabledAnalysis = true;
            }
            LoadImage();
        }
        private void LineThickness_TrackBar_Scroll(object sender, EventArgs e)
        {
            LineThickness = LineThickness_TrackBar.Value;
            LoadImage();
        }


        private void distributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prepare data
            var ans = KernelDensityEstimation(FilteredArea, Convert.ToDouble(KDESigma_TextBox.Text), 1000);

            // show plot
            Graph_Form graph_Form = new Graph_Form();
            Graph_Form.Title = "Area Distribution";
            Graph_Form.x = ans.Item1;
            Graph_Form.y = ans.Item2;
            Graph_Form.XLabel = "Area ["+MeasUnit+"^2]";
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
            LoadImage();
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

                //uncheck other toggle buttons
                SelectRuler_Button.CheckState = CheckState.Unchecked;
                SelectRuler_Button.Checked = false;
                SetScalingBar_Button.CheckState = CheckState.Unchecked;
                SetScalingBar_Button.Checked = false;
                SelectOCR_Button.CheckState = CheckState.Unchecked;
                SelectOCR_Button.Checked = false;
            }
        }
        private void SetScalingBar_Button_Click(object sender, EventArgs e)
        {
            if (SetScalingBar_Button.Checked)
            {
                SetScalingBar_Button.CheckState = CheckState.Unchecked;
                SetScalingBar_Button.Checked = false;
            }
            else
            {
                SetScalingBar_Button.CheckState = CheckState.Checked;
                SetScalingBar_Button.Checked = true;

                //uncheck other toggle buttons
                SelectRuler_Button.CheckState = CheckState.Unchecked;
                SelectRuler_Button.Checked = false;
                SelectROI_Button.CheckState = CheckState.Unchecked;
                SelectROI_Button.Checked = false;
                SelectOCR_Button.CheckState = CheckState.Unchecked;
                SelectOCR_Button.Checked = false;
            }
        }
        private void SelectOCR_Button_Click(object sender, EventArgs e)
        {
            if (SelectOCR_Button.Checked)
            {
                SelectOCR_Button.CheckState = CheckState.Unchecked;
                SelectOCR_Button.Checked = false;
            }
            else
            {
                SelectOCR_Button.CheckState = CheckState.Checked;
                SelectOCR_Button.Checked = true;

                //uncheck other toggle buttons
                SelectRuler_Button.CheckState = CheckState.Unchecked;
                SelectRuler_Button.Checked = false;
                SelectROI_Button.CheckState = CheckState.Unchecked;
                SelectROI_Button.Checked = false;
                SetScalingBar_Button.CheckState = CheckState.Unchecked;
                SetScalingBar_Button.Checked = false;
            }
        }
        private void SelectRuler_Button_Click(object sender, EventArgs e)
        {
            if (SelectRuler_Button.Checked)
            {
                SelectRuler_Button.CheckState = CheckState.Unchecked;
                SelectRuler_Button.Checked = false;

                //clear lines
                RulerPoints.Clear();
                LoadImage();
            }
            else
            {
                SelectRuler_Button.CheckState = CheckState.Checked;
                SelectRuler_Button.Checked = true;
                RulerPoints.Clear();

                //uncheck other toggle buttons
                SelectROI_Button.CheckState = CheckState.Unchecked;
                SelectROI_Button.Checked = false;
                SetScalingBar_Button.CheckState = CheckState.Unchecked;
                SetScalingBar_Button.Checked = false;
                SelectOCR_Button.CheckState = CheckState.Unchecked;
                SelectOCR_Button.Checked = false;
            }
        }
        private void AutoLength_Button_Click(object sender, EventArgs e)
        {
            if (AutoLength_Button.Checked)
            {
                AutoLength_Button.CheckState = CheckState.Unchecked;
                AutoLength_Button.Checked = false;
                LoadImage();
            }
            else
            {
                AutoLength_Button.CheckState = CheckState.Checked;
                AutoLength_Button.Checked = true;
                LoadImage();
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            //find clicked point on the zoom/pannable picbox, offsetted by imagebox scroll and zoom level
            int horz = Main_panAndZoomPictureBox.HorizontalScrollBar.Value;
            int vert = Main_panAndZoomPictureBox.VerticalScrollBar.Value;
            int MouseAtx = (int)(((double)e.X) / Main_panAndZoomPictureBox.ZoomScale) + horz;
            int MouseAty = (int)(((double)e.Y) / Main_panAndZoomPictureBox.ZoomScale) + vert;
            Point MouseAtPoint = new Point(MouseAtx, MouseAty);

            if (SelectROI_Button.Checked)
            {
                ROIp1 = MouseAtPoint;
                SelectingROI = true;

                // record imagebox built in zoom/pan so that it can be undo when the mouse is released (can't disable pan/zoom)
                UndoZoomLevel = Main_panAndZoomPictureBox.ZoomScale;
                UndoHorzScroll = horz;
                UndoVertScroll = vert;
            }

            if (SetScalingBar_Button.Checked)
            {
                ROIp1 = MouseAtPoint;
                SelectingScalingBar = true;

                // record imagebox built in zoom/pan so that it can be undo when the mouse is released (can't disable pan/zoom)
                UndoZoomLevel = Main_panAndZoomPictureBox.ZoomScale;
                UndoHorzScroll = horz;
                UndoVertScroll = vert;
            }

            if (SelectRuler_Button.Checked && e.Button == MouseButtons.Left)
            {
                RulerPoints.Add(MouseAtPoint);
                Main_panAndZoomPictureBox.Invalidate();
                RulerLength=LengthInPx(RulerPoints);
                Length_Label.Text = "Length = "+ Math.Round(RulerLength,2).ToString() +" " + MeasUnit;

                // record imagebox built in zoom/pan so that it can be undo when the mouse is released (can't disable pan/zoom)
                UndoZoomLevel = Main_panAndZoomPictureBox.ZoomScale;
                UndoHorzScroll = horz;
                UndoVertScroll = vert;
            }
            if (SelectOCR_Button.Checked)
            {
                ROIp1 = MouseAtPoint;
                SelectingOCRRegion = true;

                // record imagebox built in zoom/pan so that it can be undo when the mouse is released (can't disable pan/zoom)
                UndoZoomLevel = Main_panAndZoomPictureBox.ZoomScale;
                UndoHorzScroll = horz;
                UndoVertScroll = vert;
            }
        }
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
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
            // count scaling bar length on mouse up
            if (SetScalingBar_Button.Checked)
            {
                SelectingScalingBar = false;

                // undo autozoom and pan
                Point point = new Point(UndoHorzScroll, UndoVertScroll);
                Main_panAndZoomPictureBox.SetZoomScale(UndoZoomLevel, point);
                Main_panAndZoomPictureBox.HorizontalScrollBar.Value = UndoHorzScroll;
                Main_panAndZoomPictureBox.VerticalScrollBar.Value = UndoVertScroll;

                // unpop button
                SetScalingBar_Button.CheckState = CheckState.Unchecked;
                SetScalingBar_Button.Checked = false;

                // update length based on length region
                FindPixelLength();

                // if no OCR reigon is defined, use px as unit (can be overwritten)
                if (OCRRect.IsEmpty)
                {
                    ScaledUnit_TextBox.Text=ScaleBarPx+" px";
                }
                LoadImage();
                UpdatePxScaling();
            }
            if (SelectOCR_Button.Checked)
            {
                SelectingOCRRegion = false;

                // undo autozoom and pan
                Point point = new Point(UndoHorzScroll, UndoVertScroll);
                Main_panAndZoomPictureBox.SetZoomScale(UndoZoomLevel, point);
                Main_panAndZoomPictureBox.HorizontalScrollBar.Value = UndoHorzScroll;
                Main_panAndZoomPictureBox.VerticalScrollBar.Value = UndoVertScroll;

                // unpop button
                SelectOCR_Button.CheckState = CheckState.Unchecked;
                SelectOCR_Button.Checked = false;
                UpdateUnit();
                UpdatePxScaling();
                LoadImage();
            }
        }
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
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
                LoadImage();
            }
            if (SetScalingBar_Button.Checked&&SelectingScalingBar)
            {
                int horz = Main_panAndZoomPictureBox.HorizontalScrollBar.Value;
                int vert = Main_panAndZoomPictureBox.VerticalScrollBar.Value;

                Point ROIp2 = new Point(0, 0);
                ROIp2.X = (int)(((double)e.X) / Main_panAndZoomPictureBox.ZoomScale) + horz;
                ROIp2.Y = (int)(((double)e.Y) / Main_panAndZoomPictureBox.ZoomScale) + vert;

                ScaleBarRect = Main_panAndZoomPictureBox.GetRectangle(ROIp1, ROIp2);
                Refresh();
                LoadImage();
            }
            if (SelectOCR_Button.Checked && SelectingOCRRegion)
            {
                int horz = Main_panAndZoomPictureBox.HorizontalScrollBar.Value;
                int vert = Main_panAndZoomPictureBox.VerticalScrollBar.Value;

                Point ROIp2 = new Point(0, 0);
                ROIp2.X = (int)(((double)e.X) / Main_panAndZoomPictureBox.ZoomScale) + horz;
                ROIp2.Y = (int)(((double)e.Y) / Main_panAndZoomPictureBox.ZoomScale) + vert;

                OCRRect = Main_panAndZoomPictureBox.GetRectangle(ROIp1, ROIp2);
                Refresh();
                LoadImage();
            }
        }
        private void PictureBox_Draw(object sender, PaintEventArgs e)
        {
            // draw yellow box for ROI
            Pen pen = new Pen(Color.Yellow,1);
            e.Graphics.DrawRectangle(pen, ROIRect);

            // draw purple box for length
            pen.Color = Color.BlueViolet;
            e.Graphics.DrawRectangle(pen, ScaleBarRect);

            // draw purple box for length
            pen.Color = Color.Violet;
            e.Graphics.DrawRectangle(pen, OCRRect);


            if (SelectRuler_Button.Checked)
            {
                if (RulerPoints.Count > 1) e.Graphics.DrawLines(Pens.Red, RulerPoints.ToArray());
            }      
            if (AutoLength_Button.Checked && EnabledAnalysis)
            {
                for (int i = 0; i < ObjectAmount; i++)
                {
                    Font drawFont = new Font("Arial", 3);
                    SolidBrush drawBrush = new SolidBrush(Color.Blue);
                    PointF drawPoint = new PointF(BoundingBoxes[i].Center.X+ROIRect.X- BoundingBoxes[i].Size.Width/2, BoundingBoxes[i].Center.Y+ ROIRect.Y-3);
                    e.Graphics.DrawString("L:" + Math.Round(BoundingBoxes[i].Size.Height*PixelSize, 1)+",W:"+ Math.Round(BoundingBoxes[i].Size.Width * PixelSize, 1) + " (" + MeasUnit + ")", drawFont, drawBrush, drawPoint);
                }
            }

        }

        private void Invert_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadImage();
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
            // Generate white background
            Emgu.CV.Image<Bgr, byte> temp = new Image<Bgr, byte>(BinaryROI.Width, BinaryROI.Height);
            temp = ~temp; //invert black to white background

            // eval contour area 
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

            if (HighlightLS_CheckBox.Checked)
            {
                CvInvoke.DrawContours(temp, Contours, LargestInd, new MCvScalar(0, 0, 255, 255), LineThickness);
                CvInvoke.DrawContours(temp, Contours, SmallestInd, new MCvScalar(255, 0, 0, 255), LineThickness);
            }

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

        private void plotDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DiscardLargest_Button_Click(object sender, EventArgs e)
        {
            try
            {
                MaxArea_TextBox.Text=(FilteredArea.Max()/PixelSize).ToString();
                MaxArea = FilteredArea.Max() / PixelSize;
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
                MinArea_TextBox.Text = (FilteredArea.Min() / PixelSize).ToString();
                MinArea = FilteredArea.Min() / PixelSize;
            }
            catch
            {
                MessageBox.Show("no object to discard!");
            }
        }

        private void HighlightLS_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void colorHistogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Histogram_Form HistogramView = new Histogram_Form();
            Histogram_Form.Image = BitmapROI;
            HistogramView.ShowDialog();
        }

        private void GreenThreshold_TrackBar_Scroll(object sender, EventArgs e)
        {
            GreenThreshold = GreenThreshold_TrackBar.Value;
            LoadImage();
        }

        private void ExportArea_Button_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "|*.csv";
            saveFileDialog1.Title = "Export Area data";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                var records = new List<ImageResult>();
                for (int i = 0; i < FilteredArea.Length; i++)
                {
                    records.Add(new ImageResult { Area = FilteredArea[i] });
                }
                var writer = new StreamWriter(fs);
                var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                {
                    csv.WriteRecords(records);
                }
                writer.Close();
            }
        }
        private void ExportContour_Button_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "|*.txt";
            saveFileDialog1.Title = "Export Contour data";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
               
                var writer = new StreamWriter(fs);
                for (int i = 0; i < Contours.Size; i++)
                {
                    double Area = CvInvoke.ContourArea(Contours[i], false);
                    if (Area > MinArea && Area < MaxArea)
                    {
                        for (int i2 = 0; i2 < Contours[i].ToArray().Length; i2++)
                        {
                            string test = "[" + Contours[i].ToArray()[i2].X.ToString() + "," + Contours[i].ToArray()[i2].Y.ToString() + "]";
                            writer.WriteLine(test);
                        }
                        writer.WriteLine("/");
                    }
                }
                writer.Close();
            }
        }

        private void BlueThreshold_TrackBar_Scroll(object sender, EventArgs e)
        {
            BlueThreshold = BlueThreshold_TrackBar.Value;
            LoadImage();
        }

        private void ScaledUnit_TextBox_Changed(object sender, EventArgs e)
        {
            try
            {
                UpdatePxScaling();
            }
            catch
            {
            }
            LoadImage();
        }

        private void ResetRegion_Button_Click(object sender, EventArgs e)
        {
            // set all rec to 0    
            ROIRect = new Rectangle();
            OCRRect = new Rectangle();
            ScaleBarRect  =new Rectangle();

            // reset unit
            ScaledUnit_TextBox.Text = "? px";
            PixelSize = 1;

            LoadImage();
        }
        private void SaveConfig_Button_Click(object sender, EventArgs e)
        {
            // save regions
            AddUpdateAppSettings("ROIRect.X", ROIRect.X.ToString());
            AddUpdateAppSettings("ROIRect.Y", ROIRect.Y.ToString());
            AddUpdateAppSettings("ROIRect.Width", ROIRect.Width.ToString());
            AddUpdateAppSettings("ROIRect.Height", ROIRect.Height.ToString());

            AddUpdateAppSettings("OCRRect.X", OCRRect.X.ToString());
            AddUpdateAppSettings("OCRRect.Y", OCRRect.Y.ToString());
            AddUpdateAppSettings("OCRRect.Width", OCRRect.Width.ToString());
            AddUpdateAppSettings("OCRRect.Height", OCRRect.Height.ToString());

            AddUpdateAppSettings("ScaleBarRect.X", ScaleBarRect.X.ToString());
            AddUpdateAppSettings("ScaleBarRect.Y", ScaleBarRect.Y.ToString());
            AddUpdateAppSettings("ScaleBarRect.Width", ScaleBarRect.Width.ToString());
            AddUpdateAppSettings("ScaleBarRect.Height", ScaleBarRect.Height.ToString());

            // save threshold page
            AddUpdateAppSettings("RedThreshold_TrackBar.Value", RedThreshold_TrackBar.Value.ToString());
            AddUpdateAppSettings("GreenThreshold_TrackBar.Value", GreenThreshold_TrackBar.Value.ToString());
            AddUpdateAppSettings("BlueThreshold_TrackBar.Value", BlueThreshold_TrackBar.Value.ToString());

            AddUpdateAppSettings("Invert_CheckBox.Checked", Invert_CheckBox.Checked.ToString());
            AddUpdateAppSettings("Grayscale_CheckBox.Checked", Grayscale_CheckBox.Checked.ToString());

            // contour page
            AddUpdateAppSettings("LineThickness_TrackBar.Value", LineThickness_TrackBar.Value.ToString());
            AddUpdateAppSettings("HighlightLS_CheckBox.Checked", HighlightLS_CheckBox.Checked.ToString());

            // Filter page
            AddUpdateAppSettings("MinArea_TextBox.Text", MinArea_TextBox.Text);
            AddUpdateAppSettings("MaxArea_TextBox.Text", MaxArea_TextBox.Text);


        }
        private void LoadConfig_Button_Click(object sender, EventArgs e)
        {
            // load Regions
            ROIRect.X =Convert.ToInt32(ReadSetting("ROIRect.X"));
            ROIRect.Y = Convert.ToInt32(ReadSetting("ROIRect.Y"));
            ROIRect.Width = Convert.ToInt32(ReadSetting("ROIRect.Width"));
            ROIRect.Height = Convert.ToInt32(ReadSetting("ROIRect.Height"));

            OCRRect.X = Convert.ToInt32(ReadSetting("OCRRect.X"));
            OCRRect.Y = Convert.ToInt32(ReadSetting("OCRRect.Y"));
            OCRRect.Width = Convert.ToInt32(ReadSetting("OCRRect.Width"));
            OCRRect.Height = Convert.ToInt32(ReadSetting("OCRRect.Height"));

            ScaleBarRect.X = Convert.ToInt32(ReadSetting("ScaleBarRect.X"));
            ScaleBarRect.Y = Convert.ToInt32(ReadSetting("ScaleBarRect.Y"));
            ScaleBarRect.Width = Convert.ToInt32(ReadSetting("ScaleBarRect.Width"));
            ScaleBarRect.Height = Convert.ToInt32(ReadSetting("ScaleBarRect.Height"));

            // load threshold page
            RedThreshold_TrackBar.Value = Convert.ToInt32(ReadSetting("RedThreshold_TrackBar.Value"));
            GreenThreshold_TrackBar.Value = Convert.ToInt32(ReadSetting("GreenThreshold_TrackBar.Value"));
            BlueThreshold_TrackBar.Value = Convert.ToInt32(ReadSetting("BlueThreshold_TrackBar.Value"));

            Invert_CheckBox.Checked = Convert.ToBoolean(ReadSetting("Invert_CheckBox.Checked"));
            Grayscale_CheckBox.Checked = Convert.ToBoolean(ReadSetting("Grayscale_CheckBox.Checked"));

            // load contour page
            LineThickness_TrackBar.Value = Convert.ToInt32(ReadSetting("LineThickness_TrackBar.Value"));
            HighlightLS_CheckBox.Checked = Convert.ToBoolean(ReadSetting("HighlightLS_CheckBox.Checked"));

            // filter page
            MinArea_TextBox.Text = ReadSetting("MinArea_TextBox.Text");
            MaxArea_TextBox.Text = ReadSetting("MaxArea_TextBox.Text");

            
            FindPixelLength();
            UpdateUnit();
            UpdatePxScaling();
            LoadImage();
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void FindPixelLength()
        {
            // find px length
            Image<Bgr, byte> ScalebarImage = RawImage.Clone();
            ScalebarImage.ROI = ScaleBarRect;
            Image<Gray, byte> tempGrey = ScalebarImage.ThresholdBinary(new Bgr(125, 125, 125), new Bgr(255, 255, 255)).Convert<Gray, Byte>();
            int FirstPx = 0;
            int LastPx = 0;

            for (int ix = 0; ix < tempGrey.Size.Width; ix++)
            {
                if (tempGrey.Data[tempGrey.Size.Height / 2, ix, 0] <= 255 / 2)
                {
                    if (FirstPx == 1)
                    {
                        FirstPx = ix;
                    }
                    else
                    {
                        LastPx = ix;
                    }
                }
            }
            ScaleBarPx = LastPx - FirstPx;
        }
        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (System.Configuration.ConfigurationException)
            {
                System.Windows.Forms.MessageBox.Show("Error reading app settings");
                return "Not Found";
            }
        }
        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                System.Windows.Forms.MessageBox.Show("Error writing app settings");
            }
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
        double LengthInPx(List<Point> points)
        {
            if (!(points.Count > 1)) return 0;

            double len = 0;
            for (int i = 1; i < points.Count; i++)
            {
                len += Math.Sqrt((points[i - 1].X - points[i].X) * (points[i - 1].X - points[i].X)
                            + (points[i - 1].Y - points[i].Y) * (points[i - 1].Y - points[i].Y));
            }
            return len;
        }

        string OCR(Image<Gray,byte> Img)
        {
            string path = Path.GetDirectoryName(Path.GetFullPath("eng.traineddata"))+"\\";
            string lang = Path.GetFileNameWithoutExtension("eng.traineddata").Split('.')[0];

            Emgu.CV.OCR.Tesseract OCREngine = new Tesseract(path, lang, OcrEngineMode.Default);
            OCREngine.SetImage(Img);
            return OCREngine.GetUTF8Text();
 
        }
    }
    public class ImageResult
    {
        public double Area { get; set; }
    }
    public class ContourResult
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
