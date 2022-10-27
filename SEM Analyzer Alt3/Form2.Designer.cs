namespace SEM_Analyzer_Alt3
{
    partial class Graph_Form
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
            this.components = new System.ComponentModel.Container();
            this.GraphControl = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // GraphControl
            // 
            this.GraphControl.Location = new System.Drawing.Point(12, 12);
            this.GraphControl.Name = "GraphControl";
            this.GraphControl.ScrollGrace = 0D;
            this.GraphControl.ScrollMaxX = 0D;
            this.GraphControl.ScrollMaxY = 0D;
            this.GraphControl.ScrollMaxY2 = 0D;
            this.GraphControl.ScrollMinX = 0D;
            this.GraphControl.ScrollMinY = 0D;
            this.GraphControl.ScrollMinY2 = 0D;
            this.GraphControl.Size = new System.Drawing.Size(762, 603);
            this.GraphControl.TabIndex = 0;
            // 
            // Graph_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 627);
            this.Controls.Add(this.GraphControl);
            this.Name = "Graph_Form";
            this.Text = "Graph";
            this.Load += new System.EventHandler(this.Form2_Open);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl GraphControl;
    }
}