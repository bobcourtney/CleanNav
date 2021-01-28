using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SEGYlib;


namespace Demultiplex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            SEGYlib.SEGYFile sf  = new SEGYlib.SEGYFile();
            foreach ( var f in openFileDialog1.FileNames ) 
            {
                if ( sf.Open(f) == 1 )
                {
                    if (sf.isSEGY()) listBox1.Items.Add(f);
                }
                sf.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach ( var i in listBox1.Items )
            {
                String f = (string)i;
                string f0 = System.IO.Path.GetDirectoryName(f) + System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileNameWithoutExtension(f) + ".gps";

                SEGYFile s0 = new SEGYFile();
                if (s0.Open(f) == 0)
                {
                    break;
                }
                var s1 = File.CreateText(f0);
                if (s1 == null)
                {
                    break;
                }

                s0.ReadFileHeader();

                while (s0.ReadNextTrace())
                {
                    SEGYTrace t = s0.currentTrace;
                    if (t.TraceHeader.traceNumberWithinOriginalFieldRecord == 1)
                    {
                        s1.WriteLine(t.timeTracedRecorded.ToString()," ", t.sourcePositionX.ToString()," ", t.sourcePositionY.ToString());
                    }
                        

                }
                s0.Close();
                s1.Close();
            }
        }

        private void checkBoxSwitch_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
