namespace SEM_Analyzer_Alt3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Report_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.DragIcon_PictureBox = new System.Windows.Forms.PictureBox();
            this.DragIcon_Label = new System.Windows.Forms.Label();
            this.Path_Textbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SelectROI_Button = new System.Windows.Forms.ToolStripButton();
            this.SelectRuler_Button = new System.Windows.Forms.ToolStripButton();
            this.SelectOCR_Button = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Enabled_Button = new System.Windows.Forms.ToolStripButton();
            this.UnZoom_button = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ResetRegion_Button = new System.Windows.Forms.ToolStripButton();
            this.SetScalingBar_Button = new System.Windows.Forms.ToolStripButton();
            this.ScaledUnit_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.AutoLength_Button = new System.Windows.Forms.ToolStripButton();
            this.Length_Label = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Main_panAndZoomPictureBox = new Emgu.CV.UI.PanAndZoomPictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportContour_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportArea_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.plotDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.areaDistrubutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorHistogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawImageInROIToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.binarizationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contourToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DiscardSmallest_Button = new System.Windows.Forms.Button();
            this.DiscardLargest_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MaxArea_TextBox = new System.Windows.Forms.TextBox();
            this.MinArea_TextBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.HighlightLS_CheckBox = new System.Windows.Forms.CheckBox();
            this.LineThickness_TrackBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Binarization_Tooltip = new System.Windows.Forms.RichTextBox();
            this.Invert_CheckBox = new System.Windows.Forms.CheckBox();
            this.BlueThreshold_Label = new System.Windows.Forms.Label();
            this.GreenThreshold_Label = new System.Windows.Forms.Label();
            this.RedThreshold_Label = new System.Windows.Forms.Label();
            this.Grayscale_CheckBox = new System.Windows.Forms.CheckBox();
            this.BlueThreshold_TrackBar = new System.Windows.Forms.TrackBar();
            this.GreenThreshold_TrackBar = new System.Windows.Forms.TrackBar();
            this.RedThreshold_TrackBar = new System.Windows.Forms.TrackBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SaveConfig_Button = new System.Windows.Forms.Button();
            this.LoadConfig_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DragIcon_PictureBox)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Main_panAndZoomPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LineThickness_TrackBar)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlueThreshold_TrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenThreshold_TrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedThreshold_TrackBar)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Report_RichTextBox
            // 
            this.Report_RichTextBox.Location = new System.Drawing.Point(1101, 52);
            this.Report_RichTextBox.Name = "Report_RichTextBox";
            this.Report_RichTextBox.Size = new System.Drawing.Size(239, 502);
            this.Report_RichTextBox.TabIndex = 2;
            this.Report_RichTextBox.Text = "";
            // 
            // DragIcon_PictureBox
            // 
            this.DragIcon_PictureBox.Image = ((System.Drawing.Image)(resources.GetObject("DragIcon_PictureBox.Image")));
            this.DragIcon_PictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("DragIcon_PictureBox.InitialImage")));
            this.DragIcon_PictureBox.Location = new System.Drawing.Point(479, 339);
            this.DragIcon_PictureBox.Name = "DragIcon_PictureBox";
            this.DragIcon_PictureBox.Size = new System.Drawing.Size(126, 122);
            this.DragIcon_PictureBox.TabIndex = 5;
            this.DragIcon_PictureBox.TabStop = false;
            // 
            // DragIcon_Label
            // 
            this.DragIcon_Label.AutoSize = true;
            this.DragIcon_Label.Location = new System.Drawing.Point(469, 464);
            this.DragIcon_Label.Name = "DragIcon_Label";
            this.DragIcon_Label.Size = new System.Drawing.Size(142, 12);
            this.DragIcon_Label.TabIndex = 6;
            this.DragIcon_Label.Text = "Drag TIF/JPG/JPEG files here";
            // 
            // Path_Textbox
            // 
            this.Path_Textbox.Location = new System.Drawing.Point(867, 1);
            this.Path_Textbox.Name = "Path_Textbox";
            this.Path_Textbox.ReadOnly = true;
            this.Path_Textbox.Size = new System.Drawing.Size(477, 22);
            this.Path_Textbox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(835, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Path:";
            // 
            // SelectROI_Button
            // 
            this.SelectROI_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectROI_Button.Image = ((System.Drawing.Image)(resources.GetObject("SelectROI_Button.Image")));
            this.SelectROI_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectROI_Button.Name = "SelectROI_Button";
            this.SelectROI_Button.Size = new System.Drawing.Size(23, 22);
            this.SelectROI_Button.Text = "Set ROI";
            this.SelectROI_Button.ToolTipText = "Set ROI Region";
            this.SelectROI_Button.Click += new System.EventHandler(this.SelectROI_Button_Click);
            // 
            // SelectRuler_Button
            // 
            this.SelectRuler_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectRuler_Button.Image = ((System.Drawing.Image)(resources.GetObject("SelectRuler_Button.Image")));
            this.SelectRuler_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectRuler_Button.Name = "SelectRuler_Button";
            this.SelectRuler_Button.Size = new System.Drawing.Size(23, 22);
            this.SelectRuler_Button.Text = "Measure distance between 2 points";
            this.SelectRuler_Button.Click += new System.EventHandler(this.SelectRuler_Button_Click);
            // 
            // SelectOCR_Button
            // 
            this.SelectOCR_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectOCR_Button.Image = ((System.Drawing.Image)(resources.GetObject("SelectOCR_Button.Image")));
            this.SelectOCR_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectOCR_Button.Name = "SelectOCR_Button";
            this.SelectOCR_Button.Size = new System.Drawing.Size(23, 22);
            this.SelectOCR_Button.Text = "Set Unit Region";
            this.SelectOCR_Button.Click += new System.EventHandler(this.SelectOCR_Button_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Enabled_Button,
            this.UnZoom_button,
            this.toolStripSeparator2,
            this.ResetRegion_Button,
            this.SelectROI_Button,
            this.SetScalingBar_Button,
            this.SelectOCR_Button,
            this.ScaledUnit_TextBox,
            this.toolStripSeparator1,
            this.AutoLength_Button,
            this.SelectRuler_Button,
            this.Length_Label,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1352, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Enabled_Button
            // 
            this.Enabled_Button.Image = ((System.Drawing.Image)(resources.GetObject("Enabled_Button.Image")));
            this.Enabled_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Enabled_Button.Name = "Enabled_Button";
            this.Enabled_Button.Size = new System.Drawing.Size(62, 22);
            this.Enabled_Button.Text = "Enable";
            this.Enabled_Button.ToolTipText = "Enable Analysis";
            this.Enabled_Button.Click += new System.EventHandler(this.Enabled_Button_Click);
            // 
            // UnZoom_button
            // 
            this.UnZoom_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UnZoom_button.Image = ((System.Drawing.Image)(resources.GetObject("UnZoom_button.Image")));
            this.UnZoom_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UnZoom_button.Name = "UnZoom_button";
            this.UnZoom_button.Size = new System.Drawing.Size(23, 22);
            this.UnZoom_button.Text = "Reset Zoom";
            this.UnZoom_button.Click += new System.EventHandler(this.UndoZoom_Button_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ResetRegion_Button
            // 
            this.ResetRegion_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ResetRegion_Button.Image = ((System.Drawing.Image)(resources.GetObject("ResetRegion_Button.Image")));
            this.ResetRegion_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ResetRegion_Button.Name = "ResetRegion_Button";
            this.ResetRegion_Button.Size = new System.Drawing.Size(23, 22);
            this.ResetRegion_Button.Text = "Reset Region and ROIs";
            this.ResetRegion_Button.Click += new System.EventHandler(this.ResetRegion_Button_Click);
            // 
            // SetScalingBar_Button
            // 
            this.SetScalingBar_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SetScalingBar_Button.Image = ((System.Drawing.Image)(resources.GetObject("SetScalingBar_Button.Image")));
            this.SetScalingBar_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SetScalingBar_Button.Name = "SetScalingBar_Button";
            this.SetScalingBar_Button.Size = new System.Drawing.Size(23, 22);
            this.SetScalingBar_Button.Text = "Set Scalebar Region";
            this.SetScalingBar_Button.Click += new System.EventHandler(this.SetScalingBar_Button_Click);
            // 
            // ScaledUnit_TextBox
            // 
            this.ScaledUnit_TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ScaledUnit_TextBox.Name = "ScaledUnit_TextBox";
            this.ScaledUnit_TextBox.Size = new System.Drawing.Size(50, 25);
            this.ScaledUnit_TextBox.Text = "? px";
            this.ScaledUnit_TextBox.TextChanged += new System.EventHandler(this.ScaledUnit_TextBox_Changed);
            // 
            // AutoLength_Button
            // 
            this.AutoLength_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AutoLength_Button.Image = ((System.Drawing.Image)(resources.GetObject("AutoLength_Button.Image")));
            this.AutoLength_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AutoLength_Button.Name = "AutoLength_Button";
            this.AutoLength_Button.Size = new System.Drawing.Size(23, 22);
            this.AutoLength_Button.Text = "Automatically measure Length/Width bounding of all detectable objects in ROI";
            this.AutoLength_Button.Click += new System.EventHandler(this.AutoLength_Button_Click);
            // 
            // Length_Label
            // 
            this.Length_Label.Name = "Length_Label";
            this.Length_Label.Size = new System.Drawing.Size(64, 22);
            this.Length_Label.Text = "Length = 0";
            this.Length_Label.ToolTipText = "length of the current ruler measurement";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // Main_panAndZoomPictureBox
            // 
            this.Main_panAndZoomPictureBox.Location = new System.Drawing.Point(12, 52);
            this.Main_panAndZoomPictureBox.Name = "Main_panAndZoomPictureBox";
            this.Main_panAndZoomPictureBox.Size = new System.Drawing.Size(1083, 752);
            this.Main_panAndZoomPictureBox.TabIndex = 10;
            this.Main_panAndZoomPictureBox.TabStop = false;
            this.Main_panAndZoomPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Draw);
            this.Main_panAndZoomPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            this.Main_panAndZoomPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseMove);
            this.Main_panAndZoomPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem,
            this.plotDataToolStripMenuItem,
            this.processToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1352, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportContour_Button,
            this.ExportArea_Button});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.filesToolStripMenuItem.Text = "Export";
            // 
            // ExportContour_Button
            // 
            this.ExportContour_Button.Name = "ExportContour_Button";
            this.ExportContour_Button.Size = new System.Drawing.Size(155, 22);
            this.ExportContour_Button.Text = "Export Contour";
            // 
            // ExportArea_Button
            // 
            this.ExportArea_Button.Name = "ExportArea_Button";
            this.ExportArea_Button.Size = new System.Drawing.Size(155, 22);
            this.ExportArea_Button.Text = "Export Area";
            this.ExportArea_Button.Click += new System.EventHandler(this.ExportArea_Button_Click);
            // 
            // plotDataToolStripMenuItem
            // 
            this.plotDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.areaDistrubutionToolStripMenuItem,
            this.colorHistogramToolStripMenuItem});
            this.plotDataToolStripMenuItem.Name = "plotDataToolStripMenuItem";
            this.plotDataToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.plotDataToolStripMenuItem.Text = "Plot Data";
            this.plotDataToolStripMenuItem.Click += new System.EventHandler(this.plotDataToolStripMenuItem_Click);
            // 
            // areaDistrubutionToolStripMenuItem
            // 
            this.areaDistrubutionToolStripMenuItem.Name = "areaDistrubutionToolStripMenuItem";
            this.areaDistrubutionToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.areaDistrubutionToolStripMenuItem.Text = "Area Distrubution";
            this.areaDistrubutionToolStripMenuItem.Click += new System.EventHandler(this.distrubutionToolStripMenuItem_Click);
            // 
            // colorHistogramToolStripMenuItem
            // 
            this.colorHistogramToolStripMenuItem.Name = "colorHistogramToolStripMenuItem";
            this.colorHistogramToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.colorHistogramToolStripMenuItem.Text = "Color Histogram";
            this.colorHistogramToolStripMenuItem.Click += new System.EventHandler(this.colorHistogramToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rawImageInROIToolStripMenuItem1,
            this.binarizationToolStripMenuItem1,
            this.contourToolStripMenuItem1});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // rawImageInROIToolStripMenuItem1
            // 
            this.rawImageInROIToolStripMenuItem1.Name = "rawImageInROIToolStripMenuItem1";
            this.rawImageInROIToolStripMenuItem1.Size = new System.Drawing.Size(167, 22);
            this.rawImageInROIToolStripMenuItem1.Text = "Raw Image in ROI";
            this.rawImageInROIToolStripMenuItem1.Click += new System.EventHandler(this.ShowRawImg_Button_Click);
            // 
            // binarizationToolStripMenuItem1
            // 
            this.binarizationToolStripMenuItem1.Name = "binarizationToolStripMenuItem1";
            this.binarizationToolStripMenuItem1.Size = new System.Drawing.Size(167, 22);
            this.binarizationToolStripMenuItem1.Text = "Binarization";
            this.binarizationToolStripMenuItem1.Click += new System.EventHandler(this.BinarizationVew_Button_Click);
            // 
            // contourToolStripMenuItem1
            // 
            this.contourToolStripMenuItem1.Name = "contourToolStripMenuItem1";
            this.contourToolStripMenuItem1.Size = new System.Drawing.Size(167, 22);
            this.contourToolStripMenuItem1.Text = "Contour";
            this.contourToolStripMenuItem1.Click += new System.EventHandler(this.ContourView_Button_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage3.Controls.Add(this.DiscardSmallest_Button);
            this.tabPage3.Controls.Add(this.DiscardLargest_Button);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.MaxArea_TextBox);
            this.tabPage3.Controls.Add(this.MinArea_TextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(231, 222);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Filters";
            // 
            // DiscardSmallest_Button
            // 
            this.DiscardSmallest_Button.Location = new System.Drawing.Point(88, 102);
            this.DiscardSmallest_Button.Name = "DiscardSmallest_Button";
            this.DiscardSmallest_Button.Size = new System.Drawing.Size(140, 23);
            this.DiscardSmallest_Button.TabIndex = 6;
            this.DiscardSmallest_Button.Text = "Discard Smallest Object";
            this.DiscardSmallest_Button.UseVisualStyleBackColor = true;
            this.DiscardSmallest_Button.Click += new System.EventHandler(this.DiscardSmallest_Button_Click);
            // 
            // DiscardLargest_Button
            // 
            this.DiscardLargest_Button.Location = new System.Drawing.Point(88, 73);
            this.DiscardLargest_Button.Name = "DiscardLargest_Button";
            this.DiscardLargest_Button.Size = new System.Drawing.Size(140, 23);
            this.DiscardLargest_Button.TabIndex = 5;
            this.DiscardLargest_Button.Text = "Discard Largest Object";
            this.DiscardLargest_Button.UseVisualStyleBackColor = true;
            this.DiscardLargest_Button.Click += new System.EventHandler(this.DiscardLargest_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Max Area:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Min Area:";
            // 
            // MaxArea_TextBox
            // 
            this.MaxArea_TextBox.Location = new System.Drawing.Point(88, 45);
            this.MaxArea_TextBox.Name = "MaxArea_TextBox";
            this.MaxArea_TextBox.Size = new System.Drawing.Size(140, 22);
            this.MaxArea_TextBox.TabIndex = 1;
            this.MaxArea_TextBox.Text = "100000";
            this.MaxArea_TextBox.TextChanged += new System.EventHandler(this.MaxArea_TextBox_TextChanged);
            // 
            // MinArea_TextBox
            // 
            this.MinArea_TextBox.Location = new System.Drawing.Point(88, 17);
            this.MinArea_TextBox.Name = "MinArea_TextBox";
            this.MinArea_TextBox.Size = new System.Drawing.Size(140, 22);
            this.MinArea_TextBox.TabIndex = 0;
            this.MinArea_TextBox.Text = "0";
            this.MinArea_TextBox.TextChanged += new System.EventHandler(this.MinArea_TextBox_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(231, 222);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Contour";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.HighlightLS_CheckBox);
            this.groupBox1.Controls.Add(this.LineThickness_TrackBar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visualization";
            // 
            // HighlightLS_CheckBox
            // 
            this.HighlightLS_CheckBox.AutoSize = true;
            this.HighlightLS_CheckBox.Checked = true;
            this.HighlightLS_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HighlightLS_CheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.HighlightLS_CheckBox.Location = new System.Drawing.Point(63, 57);
            this.HighlightLS_CheckBox.Name = "HighlightLS_CheckBox";
            this.HighlightLS_CheckBox.Size = new System.Drawing.Size(152, 16);
            this.HighlightLS_CheckBox.TabIndex = 3;
            this.HighlightLS_CheckBox.Text = "Highlight Largest / Smallest";
            this.HighlightLS_CheckBox.UseVisualStyleBackColor = true;
            this.HighlightLS_CheckBox.CheckedChanged += new System.EventHandler(this.HighlightLS_CheckBox_CheckedChanged);
            // 
            // LineThickness_TrackBar
            // 
            this.LineThickness_TrackBar.LargeChange = 1;
            this.LineThickness_TrackBar.Location = new System.Drawing.Point(98, 21);
            this.LineThickness_TrackBar.Maximum = 5;
            this.LineThickness_TrackBar.Minimum = 1;
            this.LineThickness_TrackBar.Name = "LineThickness_TrackBar";
            this.LineThickness_TrackBar.Size = new System.Drawing.Size(121, 45);
            this.LineThickness_TrackBar.TabIndex = 2;
            this.LineThickness_TrackBar.Value = 2;
            this.LineThickness_TrackBar.Scroll += new System.EventHandler(this.LineThickness_TrackBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(14, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "Line Thickness:";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage1.Controls.Add(this.Binarization_Tooltip);
            this.tabPage1.Controls.Add(this.Invert_CheckBox);
            this.tabPage1.Controls.Add(this.BlueThreshold_Label);
            this.tabPage1.Controls.Add(this.GreenThreshold_Label);
            this.tabPage1.Controls.Add(this.RedThreshold_Label);
            this.tabPage1.Controls.Add(this.Grayscale_CheckBox);
            this.tabPage1.Controls.Add(this.BlueThreshold_TrackBar);
            this.tabPage1.Controls.Add(this.GreenThreshold_TrackBar);
            this.tabPage1.Controls.Add(this.RedThreshold_TrackBar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(231, 222);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Threshold";
            // 
            // Binarization_Tooltip
            // 
            this.Binarization_Tooltip.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Binarization_Tooltip.Location = new System.Drawing.Point(6, 150);
            this.Binarization_Tooltip.Name = "Binarization_Tooltip";
            this.Binarization_Tooltip.ReadOnly = true;
            this.Binarization_Tooltip.Size = new System.Drawing.Size(216, 66);
            this.Binarization_Tooltip.TabIndex = 13;
            this.Binarization_Tooltip.Text = "Binarization threshold, set any value above or below threshold to 0 or 255";
            // 
            // Invert_CheckBox
            // 
            this.Invert_CheckBox.AutoSize = true;
            this.Invert_CheckBox.Location = new System.Drawing.Point(52, 6);
            this.Invert_CheckBox.Name = "Invert_CheckBox";
            this.Invert_CheckBox.Size = new System.Drawing.Size(52, 16);
            this.Invert_CheckBox.TabIndex = 7;
            this.Invert_CheckBox.Text = "Invert";
            this.Invert_CheckBox.UseVisualStyleBackColor = true;
            this.Invert_CheckBox.CheckedChanged += new System.EventHandler(this.Invert_CheckBox_CheckedChanged);
            // 
            // BlueThreshold_Label
            // 
            this.BlueThreshold_Label.AutoSize = true;
            this.BlueThreshold_Label.Location = new System.Drawing.Point(19, 102);
            this.BlueThreshold_Label.Name = "BlueThreshold_Label";
            this.BlueThreshold_Label.Size = new System.Drawing.Size(30, 12);
            this.BlueThreshold_Label.TabIndex = 6;
            this.BlueThreshold_Label.Text = "Blue:";
            // 
            // GreenThreshold_Label
            // 
            this.GreenThreshold_Label.AutoSize = true;
            this.GreenThreshold_Label.Location = new System.Drawing.Point(15, 68);
            this.GreenThreshold_Label.Name = "GreenThreshold_Label";
            this.GreenThreshold_Label.Size = new System.Drawing.Size(36, 12);
            this.GreenThreshold_Label.TabIndex = 5;
            this.GreenThreshold_Label.Text = "Green:";
            // 
            // RedThreshold_Label
            // 
            this.RedThreshold_Label.AutoSize = true;
            this.RedThreshold_Label.Location = new System.Drawing.Point(21, 33);
            this.RedThreshold_Label.Name = "RedThreshold_Label";
            this.RedThreshold_Label.Size = new System.Drawing.Size(27, 12);
            this.RedThreshold_Label.TabIndex = 4;
            this.RedThreshold_Label.Text = "Red:";
            // 
            // Grayscale_CheckBox
            // 
            this.Grayscale_CheckBox.AutoSize = true;
            this.Grayscale_CheckBox.Checked = true;
            this.Grayscale_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Grayscale_CheckBox.Location = new System.Drawing.Point(156, 6);
            this.Grayscale_CheckBox.Name = "Grayscale_CheckBox";
            this.Grayscale_CheckBox.Size = new System.Drawing.Size(69, 16);
            this.Grayscale_CheckBox.TabIndex = 3;
            this.Grayscale_CheckBox.Text = "Grayscale";
            this.Grayscale_CheckBox.UseVisualStyleBackColor = true;
            this.Grayscale_CheckBox.CheckedChanged += new System.EventHandler(this.Grayscale_CheckBox_CheckedChanged);
            // 
            // BlueThreshold_TrackBar
            // 
            this.BlueThreshold_TrackBar.LargeChange = 20;
            this.BlueThreshold_TrackBar.Location = new System.Drawing.Point(47, 99);
            this.BlueThreshold_TrackBar.Maximum = 255;
            this.BlueThreshold_TrackBar.Name = "BlueThreshold_TrackBar";
            this.BlueThreshold_TrackBar.Size = new System.Drawing.Size(175, 45);
            this.BlueThreshold_TrackBar.TabIndex = 2;
            this.BlueThreshold_TrackBar.Value = 125;
            this.BlueThreshold_TrackBar.Scroll += new System.EventHandler(this.BlueThreshold_TrackBar_Scroll);
            // 
            // GreenThreshold_TrackBar
            // 
            this.GreenThreshold_TrackBar.LargeChange = 20;
            this.GreenThreshold_TrackBar.Location = new System.Drawing.Point(47, 65);
            this.GreenThreshold_TrackBar.Maximum = 255;
            this.GreenThreshold_TrackBar.Name = "GreenThreshold_TrackBar";
            this.GreenThreshold_TrackBar.Size = new System.Drawing.Size(175, 45);
            this.GreenThreshold_TrackBar.TabIndex = 1;
            this.GreenThreshold_TrackBar.Value = 125;
            this.GreenThreshold_TrackBar.Scroll += new System.EventHandler(this.GreenThreshold_TrackBar_Scroll);
            // 
            // RedThreshold_TrackBar
            // 
            this.RedThreshold_TrackBar.LargeChange = 20;
            this.RedThreshold_TrackBar.Location = new System.Drawing.Point(46, 28);
            this.RedThreshold_TrackBar.Maximum = 255;
            this.RedThreshold_TrackBar.Name = "RedThreshold_TrackBar";
            this.RedThreshold_TrackBar.Size = new System.Drawing.Size(178, 45);
            this.RedThreshold_TrackBar.TabIndex = 0;
            this.RedThreshold_TrackBar.Value = 125;
            this.RedThreshold_TrackBar.Scroll += new System.EventHandler(this.RedThreshold_TrackBar_Scroll);
            // 
            // tabControl1
            // 
            this.tabControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(1101, 560);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(239, 248);
            this.tabControl1.TabIndex = 3;
            // 
            // SaveConfig_Button
            // 
            this.SaveConfig_Button.Location = new System.Drawing.Point(1261, 557);
            this.SaveConfig_Button.Name = "SaveConfig_Button";
            this.SaveConfig_Button.Size = new System.Drawing.Size(38, 23);
            this.SaveConfig_Button.TabIndex = 14;
            this.SaveConfig_Button.Text = "Save";
            this.SaveConfig_Button.UseVisualStyleBackColor = true;
            this.SaveConfig_Button.Click += new System.EventHandler(this.SaveConfig_Button_Click);
            // 
            // LoadConfig_Button
            // 
            this.LoadConfig_Button.Location = new System.Drawing.Point(1297, 557);
            this.LoadConfig_Button.Name = "LoadConfig_Button";
            this.LoadConfig_Button.Size = new System.Drawing.Size(39, 23);
            this.LoadConfig_Button.TabIndex = 15;
            this.LoadConfig_Button.Text = "Load";
            this.LoadConfig_Button.UseVisualStyleBackColor = true;
            this.LoadConfig_Button.Click += new System.EventHandler(this.LoadConfig_Button_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1352, 820);
            this.Controls.Add(this.LoadConfig_Button);
            this.Controls.Add(this.SaveConfig_Button);
            this.Controls.Add(this.Main_panAndZoomPictureBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Path_Textbox);
            this.Controls.Add(this.DragIcon_Label);
            this.Controls.Add(this.DragIcon_PictureBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.Report_RichTextBox);
            this.Controls.Add(this.tabControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SEM Analyser - koland #x";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.DragIcon_PictureBox)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Main_panAndZoomPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LineThickness_TrackBar)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlueThreshold_TrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenThreshold_TrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedThreshold_TrackBar)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox Report_RichTextBox;
        private System.Windows.Forms.PictureBox DragIcon_PictureBox;
        private System.Windows.Forms.Label DragIcon_Label;
        private System.Windows.Forms.TextBox Path_Textbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton SelectROI_Button;
        private System.Windows.Forms.ToolStripButton SelectRuler_Button;
        private System.Windows.Forms.ToolStripButton SelectOCR_Button;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton Enabled_Button;
        private Emgu.CV.UI.PanAndZoomPictureBox Main_panAndZoomPictureBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plotDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem areaDistrubutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawImageInROIToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binarizationToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem contourToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ExportContour_Button;
        private System.Windows.Forms.ToolStripMenuItem colorHistogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton UnZoom_button;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button DiscardLargest_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MaxArea_TextBox;
        private System.Windows.Forms.TextBox MinArea_TextBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar LineThickness_TrackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox Invert_CheckBox;
        private System.Windows.Forms.Label BlueThreshold_Label;
        private System.Windows.Forms.Label GreenThreshold_Label;
        private System.Windows.Forms.Label RedThreshold_Label;
        private System.Windows.Forms.CheckBox Grayscale_CheckBox;
        private System.Windows.Forms.TrackBar BlueThreshold_TrackBar;
        private System.Windows.Forms.TrackBar GreenThreshold_TrackBar;
        private System.Windows.Forms.TrackBar RedThreshold_TrackBar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button DiscardSmallest_Button;
        private System.Windows.Forms.RichTextBox Binarization_Tooltip;
        private System.Windows.Forms.CheckBox HighlightLS_CheckBox;
        private System.Windows.Forms.ToolStripLabel Length_Label;
        private System.Windows.Forms.ToolStripMenuItem ExportArea_Button;
        private System.Windows.Forms.ToolStripButton AutoLength_Button;
        private System.Windows.Forms.ToolStripButton SetScalingBar_Button;
        private System.Windows.Forms.ToolStripTextBox ScaledUnit_TextBox;
        private System.Windows.Forms.ToolStripButton ResetRegion_Button;
        private System.Windows.Forms.Button SaveConfig_Button;
        private System.Windows.Forms.Button LoadConfig_Button;
    }
}

