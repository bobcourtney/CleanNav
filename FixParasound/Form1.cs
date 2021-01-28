using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SEGYlib;

namespace FixParasound
{
    public partial class Form1 : Form
    {
        string fin;
        SEGYlib.SEGYFile sf;
        string inputSEGYfile;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Enter Parasound file name";
            if(this.openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            fin = this.openFileDialog1.FileName;
            this.button1.Text = fin;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Enter the destination for the corrected SEGY file";
            if (this.folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            // read throught input file and make corrections to trace positions as needed
            sf = new SEGYlib.SEGYFile();
            inputSEGYfile = this.openFileDialog1.FileName;
            sf.Open(this.openFileDialog1.FileName);
            if (!sf.isSEGY())
            {
                sf.Close();
                return;
            }

            string outputFileName = this.folderBrowserDialog1.SelectedPath + System.IO.Path.DirectorySeparatorChar.ToString() + System.IO.Path.GetFileNameWithoutExtension(inputSEGYfile) + "fix.sgy";


            SEGYlib.SEGYFile sf2 = new SEGYlib.SEGYFile();
            sf2.Open(outputFileName);

            sf2.FileHeader = sf.FileHeader.Copy();
            sf2.Write(sf2.FileHeader);

            while (sf.ReadNextTrace())
            {
                SEGYlib.SEGYTrace tr = sf.currentTrace;

                SEGYlib.SEGYTrace newTr = tr.Copy();
                newTr.TraceHeader.scalarToBeAppliedToAllCoordinates *= -1;
                newTr.sourcePositionX = newTr.sourcePositionX / 10;
                newTr.sourcePositionY = newTr.sourcePositionY / 10;

                sf2.Write(newTr);

            }

            sf.Close();
            sf2.Close();

        }

    }
}
