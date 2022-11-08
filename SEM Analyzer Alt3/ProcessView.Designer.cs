namespace SEM_Analyzer_Alt3
{
    partial class ProcessView
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
            this.PictureBox = new Emgu.CV.UI.PanAndZoomPictureBox();
            this.SaveImage_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(12, 12);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(918, 644);
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            // 
            // SaveImage_Button
            // 
            this.SaveImage_Button.Location = new System.Drawing.Point(822, 609);
            this.SaveImage_Button.Name = "SaveImage_Button";
            this.SaveImage_Button.Size = new System.Drawing.Size(75, 23);
            this.SaveImage_Button.TabIndex = 1;
            this.SaveImage_Button.Text = "Save Image";
            this.SaveImage_Button.UseVisualStyleBackColor = true;
            this.SaveImage_Button.Click += new System.EventHandler(this.SaveImage_Button_Click);
            // 
            // ProcessView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 668);
            this.Controls.Add(this.SaveImage_Button);
            this.Controls.Add(this.PictureBox);
            this.Name = "ProcessView";
            this.Text = "ProcessView";
            this.Load += new System.EventHandler(this.ProcessView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.PanAndZoomPictureBox PictureBox;
        private System.Windows.Forms.Button SaveImage_Button;
    }
}