using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavCleaner
{
    public partial class Form3 : Form
    {
        public Form3(double[] xin)
        {
            InitializeComponent();
            Array.Sort(xin);
            double minx = 0.0;
            double maxx = 20;
            int nbins =20;
            double dx = (maxx - minx)/nbins;
            double[] yy = new double[nbins];
            double[] xx = new double[nbins];
            for (int j = 0; j <  nbins; j++) xx[j] =minx + j*dx;
            for ( int i =0; i <xin.Length; i++)
            {
                int bin = (int) Math.Round(xin[i] / dx);
                if (bin < 0) bin = 0;
                if (bin >= nbins) bin = nbins - 1;
                yy[bin] = yy[bin] + 1;

            }
            this.line1.Add(xx, yy);

        }

        private void tChart1_Click(object sender, EventArgs e)
        {

        }
    }
}
