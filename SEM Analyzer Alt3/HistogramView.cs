using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;


namespace SEM_Analyzer_Alt3
{
    public partial class Histogram_Form : Form
    {
        public static Emgu.CV.Image<Bgr, Byte> Image;
        public Histogram_Form()
        {
            InitializeComponent();
        }

        private void Histogram_Form_Load(object sender, EventArgs e)
        {

            histogramBox.ClearHistogram();
            histogramBox.GenerateHistograms(Image, 256);
            histogramBox.Refresh();
        }
    }
}
