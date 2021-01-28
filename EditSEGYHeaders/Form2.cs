using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EditSEGYHeaders
{
    public partial class Form2 : Form
    {
        public Form2(string name, double[] x, double[] y )
        {
            InitializeComponent();
            this.Name = name;
            this.tChart1.Axes.Bottom.Title.Text = "Trace Number";
            this.tChart1.Axes.Left.Title.Text = "Value";
            this.tChart1.AutoRepaint = false;
            this.fastLine1.Clear();
            this.fastLine1.Add(x, y);
            this.tChart1.AutoRepaint = true;
            this.tChart1.Refresh();

        }

        private void tChart1_Click(object sender, EventArgs e)
        {

        }
    }
}
