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
                string f0 = System.IO.Path.GetDirectoryName(f) + System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileNameWithoutExtension(f) + "_corr.sgy";

                SEGYFile s0 = new SEGYFile();
                if (s0.Open(f) == 0)
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
                var fh = s0.FileHeader.Copy();
                fh.SetFileHeader(0, 39, "C39 File processed by FixOBSSEGY Sept/2019");
                s1.Write(fh);
                while (s0.ReadNextTrace())
                {
                    if (s0.currentTrace.TraceHeader.numberOfSamplesInTrace < 1000 || s0.currentTrace.TraceHeader.numberOfSamplesInTrace > 60000)
                    {
                        break;
                    }
                    if (s0.currentTrace.timeTracedRecorded < new DateTime(1950,1,1) )
                    {
                        break;
                    }
                    SEGYTrace t = s0.currentTrace.Copy();
                    if ( checkBoxSwitch.Checked )
                    {
                        var s = t.TraceHeader.sourceCoordinateX;
                        t.TraceHeader.sourceCoordinateX = t.TraceHeader.sourceCoordinateY;
                        t.TraceHeader.sourceCoordinateY = s;
                        s = t.TraceHeader.groupCoordinateX;
                        t.TraceHeader.groupCoordinateX = t.TraceHeader.groupCoordinateY;
                        t.TraceHeader.groupCoordinateY = s;

                    }
                    if (checkBoxlldec.Checked) t.TraceHeader.coordinateUnits = 3;
                    if (checkBoxllsa.Checked) t.TraceHeader.coordinateUnits = 2;
                    s1.Write(t);

                }
                s0.Close();
                s1.Close();
            }
        }
    }
}
