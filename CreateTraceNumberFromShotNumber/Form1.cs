using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SEGYlib;

namespace CreateTraceNumberFromShotNumber
{

    public partial class Form1 : Form
    {
        string filename, SEGYfilename;
        SortedList sl;
        SEGYlib.SEGYFile SEGY;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Enter Name of Shotpoint File";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // read line until we get header line
                this.filename = this.openFileDialog1.FileName;
                System.IO.TextReader tr = System.IO.File.OpenText(this.filename);
                string[] fields = null;
                int count = 0;
               // System.IO.TextWriter tr2 = null;
                sl = new SortedList();
                while (true)
                {
                    string ln = tr.ReadLine();
                    count++;
                    if (ln == null)
                    {
                        break;
                    }
                    if (ln.Contains("LINE"))
                    {
                        // header line
                        fields = ln.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
                        //if (tr2 != null) tr2.Close();
                        //tr2 = System.IO.File.CreateText(fields[3]+"_"+fields[1]+"_"+fields[2]+".txt");
                    }
                    else
                    {
                        //data line
                        //tr2.WriteLine(ln);
                        fields = ln.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
                        fix f = new fix(Convert.ToInt32( fields[0]) , Convert.ToDouble( fields[2] ) , Convert.ToDouble( fields[1]  ));
                        sl.Add(f.t, f);
                    }



                }
                //if (tr2 != null) tr2.Close();
                label1.Text = this.filename + " loaded ; " + sl.Count.ToString() + " shotpoints";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Enter Name of SEGY File";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (SEGY != null) SEGY.Close();

                SEGY = new SEGYFile();
                if ( SEGY.Open(openFileDialog1.FileName) == 0 )
                {
                    label2.Text = " Could not opn " + openFileDialog1.FileName;
                    return;
                }
                SEGYfilename = openFileDialog1.FileName;
                SEGY.ReadAllTraceHeaders();
                label2.Text = SEGYfilename + " Loaded; " + SEGY.NumberOfTracesInBuffer.ToString() + " traces loaded";
            }
        }

    }
    public class fix
    {
        public int t;
        public double x;
        public double y;

        public fix(int tt, double xx, double yy)
        {
            t = tt;
            x = xx;
            y = yy;

        }
    }
}

