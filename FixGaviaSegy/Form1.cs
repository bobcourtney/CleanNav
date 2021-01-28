using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SEGYlib;

namespace FixGaviaSegy
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
            var sf = new SEGYFile();
            foreach ( var f in openFileDialog1.FileNames )
            {
                sf.Open(f);
                if( sf.isSEGY()) listBox1.Items.Add(f);
                sf.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach ( var i in listBox1.Items )
            {
                String f = (string)i;
                string f0 = System.IO.Path.GetDirectoryName(f) + System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileNameWithoutExtension(f) + "_corr.sgy";
                if (File.Exists(f0)) File.Delete(f0);

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
                int count = 0;
                toolStripStatusLabel1.Text = "Writing " + f0 + " : ";
                while ( s0.ReadNextTrace())
                {
                    SEGYTrace t = s0.currentTrace.Copy() ; // seismic data
                    if (t.TraceHeader.traceIdentificationCode != 1) continue;
                    // find water depth of source in meters
                    var srcDepth = Convert.ToDouble(t.TraceHeader.sourceDepthBelowSurface);
                    var fac = t.TraceHeader.scalarForAllElevationsAndDepths;
                    if ( fac <  0)
                    {
                        srcDepth /= -fac;
                    } else if ( fac > 0)
                    {
                        srcDepth *= fac;
                    }
                    // calculate the corresponding time delay in seconds
                    var twt = 2 * srcDepth / 1500; // assume vw = 1500 m/s
                    var twtmsec = twt * 1e3;
                    t.TraceHeader.delayRecordingTimeMsec = Convert.ToInt16(Math.Floor(twtmsec));
                    var remainder = twtmsec - t.TraceHeader.delayRecordingTimeMsec;
                    // corresponding number of samples to pad trace for fractional delay times
                    var nsamp = Convert.ToInt32(remainder * 1e3 / t.TraceHeader.sampleIntervalUsec);
                    var data = t.TraceData.Data;
                    // mute start of trace
                    if (this.checkBox1.Checked)
                    {
                        int mutedelay = Convert.ToInt32( Convert.ToDouble(this.numericUpDown1.Value) * 1e3 / Convert.ToDouble(t.TraceHeader.sampleIntervalUsec));
                        for (int ii = 0; ii < mutedelay; ii++)
                        {
                            data[ii] = 0;
                        }
                    }
                    var datanew = new double[data.Length]; // should be initializes to zero 
                    Array.Copy(data, 0, datanew, nsamp, data.Length - nsamp);
                    t.TraceData.Data = datanew;
                    s1.Write(t);
                    count++;
                    if( count%100 == 0)
                    {
                        toolStripStatusLabel2.Text = count.ToString();
                        Application.DoEvents();
                    }


                }
                s0.Close();
                s1.Close();

            }
            toolStripStatusLabel1.Text ="."; toolStripStatusLabel2.Text = ".";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
