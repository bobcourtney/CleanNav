using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SEGYlib;

namespace Test2
{
    public partial class Form1 : Form
    {
        private SEGYFile sf;

        public Form1()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.tChart1.AutoRepaint = false;
            this.fastLine1.Clear();
            this.fastLine1.Add(sf.Traces[Convert.ToInt32(this.numericUpDown1.Value)].Data);
            this.tChart1.AutoRepaint = true;
            this.tChart1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            sf = new SEGYFile();
            sf.Open(this.openFileDialog1.FileName);
            sf.ReadAllTraces();
            int stophere = 1;
            this.fastLine1.Add(sf.Traces[0].Data);
            this.numericUpDown1.Maximum = Convert.ToDecimal(sf.Traces.Count - 1);
        }
    }
}
