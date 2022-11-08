using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEM_Analyzer_Alt3
{
    public partial class Graph_Form : Form
    {
        public static double[] x,y;
        public static string Legend;
        public static string Title;
        public static string XLabel;
        public static string YLabel;

        public Graph_Form()
        {
            InitializeComponent();
        }

        private void SaveGraph_Button_Click(object sender, EventArgs e)
        {
            GraphControl.SaveAsBitmap();
        }

        private void Form2_Open(object sender, EventArgs e)
        {
            // import values 
            var curve1= GraphControl.GraphPane.AddCurve(Legend, x, y, Color.Red);
            curve1.Line.IsAntiAlias = true;
            curve1.Symbol.IsVisible = false;

            // set title
            GraphControl.GraphPane.Title.Text = Title;
            GraphControl.GraphPane.XAxis.Title.Text = XLabel;
            GraphControl.GraphPane.YAxis.Title.Text = YLabel;

            // render and autoscale
            GraphControl.GraphPane.YAxis.ResetAutoScale(GraphControl.GraphPane, CreateGraphics());
            GraphControl.GraphPane.XAxis.ResetAutoScale(GraphControl.GraphPane, CreateGraphics());
            GraphControl.Refresh();

        }
    }
}
