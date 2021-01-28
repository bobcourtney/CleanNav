using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SEGYlib;

namespace SEGYRev1ToRev0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Load SEGY rev 1 files";
            if (this.openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            for ( int i = 0; i < this.openFileDialog1.FileNames.Length; i++)
            {
                string f = this.openFileDialog1.FileNames[i];
                SEGYlib.SEGYFile s = new SEGYFile();
                s.Open(f);
                if( s.isSEGY())
                {
                    listBox1.Items.Add(f);
                }
                s.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Enter the destination for the corrected SEGY file";
            if (this.folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;


            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                int c = 0;
                // read throught input file and make corrections to trace positions as needed
                SEGYlib.SEGYFile sf = new SEGYlib.SEGYFile();
                string inputSEGYfile = (string)listBox1.Items[i];
                sf.Open(this.openFileDialog1.FileName);

                string outputFileName = this.folderBrowserDialog1.SelectedPath + System.IO.Path.DirectorySeparatorChar.ToString() + System.IO.Path.GetFileNameWithoutExtension(inputSEGYfile) + "rev0.sgy";
                this.toolStripStatusLabel1.Text = "Writing " + outputFileName;
                Application.DoEvents();

                SEGYlib.SEGYFile sf2 = new SEGYlib.SEGYFile();
                sf2.Open(outputFileName);

                sf2.FileHeader = sf.FileHeader.Copy();

                sf2.FileHeader.segyFormatRevisionNumber = 0; // convert to version 0 
                sf2.FileHeader.numberOfExtendedTextualFileHeaderRecordsFollowing = 0;
                if (sf2.FileHeader.ExtendedTextHeader.Count > 1) sf2.FileHeader.ExtendedTextHeader.RemoveRange(1, sf2.FileHeader.ExtendedTextHeader.Count - 1);

                sf2.Write(sf2.FileHeader);

                while (sf.ReadNextTrace())
                {
                    SEGYlib.SEGYTrace tr = sf.currentTrace;

                    SEGYlib.SEGYTrace newTr = tr.Copy();

                    sf2.Write(newTr);
                    c++;
                    if ( (c%100) == 0 )
                    {
                        toolStripStatusLabel2.Text += ".";
                        if (toolStripStatusLabel2.Text.Length > 50) toolStripStatusLabel2.Text = ".";
                        Application.DoEvents();
                    }

                }

                sf.Close();
                sf2.Close();
            }
            toolStripStatusLabel2.Text = "";
            this.toolStripStatusLabel1.Text = "Done";
            Application.DoEvents();

        }
    }
}
