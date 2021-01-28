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
    public partial class Form2 : Form
    {
        public Form2(double[] x, double[] y, double[] ynp)
        {
            InitializeComponent();
            this.tChart1.AutoRepaint = false;
            this.points1.Clear();
            this.points1.Add(x, y);
            this.tChart1.AutoRepaint = true;
            this.tChart1.Refresh();
            for (int i = 0; i < ynp.Length; i++)
            {
                ynp[i] = ynp[i] * y[i];
            }
            this.tChart2.AutoRepaint = false;
            this.points2.Clear();
            this.points2.Add(x, ynp);
            this.tChart2.AutoRepaint = true;
            this.tChart2.Refresh();
        }

        private void tChart1_Click(object sender, EventArgs e)
        {
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void tChart2_Click(object sender, EventArgs e)
        {

        }
    }
}
