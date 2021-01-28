using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SEGYlib;

namespace FixLOSSohm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "open input SEGY files";
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            listBox1.Items.Clear();
            foreach ( var f in openFileDialog1.FileNames )
            {
                listBox1.Items.Add(f);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach ( var i in listBox1.Items )
            {
                String f = (string)i;
                string f0 = System.IO.Path.GetDirectoryName(f) + System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileNameWithoutExtension(f) + "_corr.sgy";

                SEGYFile s0 = new SEGYFile();
                if( s0.Open(f) == 0)
                {
                    break;
                }
                SEGYFile s1 = new SEGYFile();
                if (s1.Open(f0) == 0)
                {
                    s0.Close();
                    break;
                }

                s0.ReadFileHeader();
                s1.Write(s0.FileHeader.Copy());
                while ( s0.ReadNextTrace())
                {
                    SEGYTrace t = s0.currentTrace.Copy() ;
                    short waterDepthMsec = t.TraceHeader.delayRecordingTimeMsec;
                    t.TraceHeader.delayRecordingTimeMsec = 0;
                    //t.TraceHeader.waterDepthAtSource = (int)waterDepthMsec;
                    s1.Write(t);

                }
                s0.Close();
                s1.Close();

            }
        }
    }
}
