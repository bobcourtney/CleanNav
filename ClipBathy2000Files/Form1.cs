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

namespace ClipBathy2000Files
{
    public partial class Form1 : Form
    {
        SEGYFile fseg;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            openFileDialog1.Title = "Enter raw bathy2000 file(s)";
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            foreach (var f in openFileDialog1.FileNames)
            {
                listBox1.Items.Add(f);
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach ( string  f in listBox1.Items )
            {
                fseg = new SEGYFile();
                fseg.Open(f);
                fseg.ReadFileHeader();


                SEGYFile fn = new SEGYFile();
                string fnn = f.Replace(".sgy", "_clipped.sgy");
                //create new filename 
                if (System.IO.File.Exists(fnn)) System.IO.File.Delete(fnn);
                fn.Open(fnn);
                fn.FileHeader = fseg.FileHeader.Copy();
                fn.Write(fn.FileHeader);
               
                while ( fseg.ReadNextTrace())
                {
                    if ( fseg.currentTrace.TraceHeader.yearDataRecorded != 2001 || fseg.currentTrace.TraceHeader.dayOfYear > 366 )
                    {
                        int i = 0;
                        break;
                    }
                    // set 1140 thru 1222 to zero in trace block ( las 82 entries )
                    byte[] buff = fseg.currentTrace.TraceData.TraceDataBuffer;
                    // since 4 byte words
                    for (int i = buff.Length - 82 * 4; i < buff.Length; i++) buff[i] = 0;
                    fn.Write(fseg.currentTrace);
                }

                fn.Close();
                fseg.Close();
            }
        }
    }
}
