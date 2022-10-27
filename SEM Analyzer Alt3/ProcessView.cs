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
    public partial class ProcessView : Form
    {
        public static Image Image;
        public ProcessView()
        {
            InitializeComponent();
        }

        private void ProcessView_Load(object sender, EventArgs e)
        {

            PictureBox.Image = Image;
        }
    }
}
