using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SEGYlib;

namespace FixSEGYRecordLength
{
    public partial class Form1 : Form
    {
        private int maxTraceLength;
        private int minTime, maxTime;
        private DataTable ellipsoidDT, datumDT;
        private int datum_number;
        private System.Collections.ArrayList l1norms;
        public Form1()
        {
            InitializeComponent();
            // fill data tables
            string ellipsoids = "Ellipsoids.csv";
            string datums = "GeodeticTransformParameters.csv";
            // read ellipsoids
            ellipsoidDT = ReadCSV(ellipsoids);
            datumDT = ReadCSV(datums);

            for (int i = 0; i < datumDT.Rows.Count; i++)
            {
                DataRow r = datumDT.Rows[i];
                string id = r[0].ToString() + " " + r[5].ToString();
                comboBox1.Items.Add(id);
            }

            //use WGS84 by default
            comboBox1.SelectedIndex = comboBox1.Items.Count - 3;

        }

        private void tChart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Enter SEGY files to scan";
            if (this.openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            for (int i = 0; i < this.openFileDialog1.FileNames.Length; i++ )
            {
                string f = this.openFileDialog1.FileNames[i];
                SEGYlib.SEGYFile s = new SEGYFile();
                s.Open(f);
                bool isSegy = s.isSEGY();
                s.Close();
                if (isSegy) listBox1.Items.Add(f);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0 && listBox1.Items.Count >= 1 ) listBox1.SelectedIndex = 0;
            scanFile();
        }

        private void scanFile()
        {
            if (this.listBox1.SelectedIndex < 0) return;
            string f = (string)listBox1.SelectedItem;
            SEGYFile s = new SEGYFile();
            s.Open(f);
            s.Traces = new List<SEGYlib.SEGYTrace>();
            s.ReadFileHeader();
            int c = 0;
            bool flip = true;

            l1norms = new System.Collections.ArrayList();
            while ( s.ReadNextTrace())
            {
                SEGYTrace tr = s.currentTrace;
                // calculate l1 norm of trace enegy
                //double l1 = 0;
                //for (int i = 0; i < tr.TraceData.Data.Length; i++) l1 += Math.Abs(tr.TraceData.Data[i]);
                //l1norms.Add(l1);
                //    tr.TraceData.TraceDataBuffer = null;
                s.Traces.Add(tr);
                c++;
                if ( c%100 == 0 )
                {
                    toolStripStatusLabel1.Text = "Reading " + f + " ";
                    if ( flip )
                    {
                        toolStripStatusLabel1.Text += "\\";
                        flip = false; 
                    } else {
                         toolStripStatusLabel1.Text += "/";
                        flip = true;
                    }
                    Application.DoEvents();
                }
            }
            //s.ReadAllTraceHeaders();
            int tmin = s.Traces[0].TraceHeader.numberOfSamplesInTrace;
            int tmax = tmin;
            int delaymin = s.Traces[0].TraceHeader.delayRecordingTimeMsec;
            int delaymax = Convert.ToInt32(delaymin + s.Traces[0].TraceHeader.numberOfSamplesInTrace*s.Traces[0].TraceHeader.sampleIntervalUsec/1e3);

            for (int i = 0; i < s.Traces.Count; i++)
            {
                int ti = s.Traces[i].TraceHeader.numberOfSamplesInTrace;
                if (ti < tmin) tmin = ti;
                if (ti > tmax) tmax = ti;
                if (s.Traces[i].TraceHeader.delayRecordingTimeMsec < delaymin) delaymin = s.Traces[i].TraceHeader.delayRecordingTimeMsec;
                int tm = Convert.ToInt32(s.Traces[i].TraceHeader.delayRecordingTimeMsec + s.Traces[i].TraceHeader.numberOfSamplesInTrace * s.Traces[i].TraceHeader.sampleIntervalUsec / 1e3);
                if (tm > delaymax) delaymax = tm;

            }
            s.Close();
            this.toolStripStatusLabel1.Text = "Trace length varies from " + tmin.ToString() + " to " + tmax.ToString() ;
            maxTraceLength = tmax;
            minTime = delaymin;
            maxTime = delaymax;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            scanFile();
        }

        private DataTable ReadCSV(string ellipsoids)
        {
            string CSVFilePathName = ellipsoids;
            string[] Lines = File.ReadAllLines(CSVFilePathName);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);
            DataTable dt = new DataTable();
            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToLower(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0); i++)
            {
                Fields = Lines[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                    Row[f] = Fields[f];
                dt.Rows.Add(Row);
            }
            return dt;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            for ( int i =0; i < this.listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                string f = (string)listBox1.Items[i];
                string g = this.folderBrowserDialog1.SelectedPath + System.IO.Path.DirectorySeparatorChar.ToString() + System.IO.Path.GetFileNameWithoutExtension(f) + "_fixed.sgy";
                Application.DoEvents();
                if (checkBox2.Checked) scanFile();
                Application.DoEvents();

                fixTraceLengths(f, g);


            }
            this.toolStripStatusLabel1.Text = "";
        }

        private void fixTraceLengths(string f, string g)
        {


            // setup datum trasformation
            DataRow r = datumDT.Rows[datum_number];

            //find ellipsoid parameters
            int ellipse_number =  ellipsoidDT.Rows.Count-1; // last entry is wgs 84
            for (int i = 0; i < ellipsoidDT.Rows.Count-1; i++)
            {
                if ( String.Compare(r[1].ToString(),ellipsoidDT.Rows[i][0].ToString() ) == 0 )
                {
                    ellipse_number = i;
                    break;
                }
            }

            double a_in = Convert.ToDouble(ellipsoidDT.Rows[ellipse_number][1]);
            double fi_in = Convert.ToDouble(ellipsoidDT.Rows[ellipse_number][2]);
            double dX = Convert.ToDouble(r[2]);
            double dY = Convert.ToDouble(r[3]);
            double dZ = Convert.ToDouble(r[4]);
            double a_out = Convert.ToDouble(ellipsoidDT.Rows[ellipsoidDT.Rows.Count - 1][1]);
            double fi_out = Convert.ToDouble(ellipsoidDT.Rows[ellipsoidDT.Rows.Count - 1][2]);
            NETGeographicLib.Geocentric fw = new NETGeographicLib.Geocentric(a_in,1.0/fi_in);
            NETGeographicLib.Geocentric rw = new NETGeographicLib.Geocentric(a_out,1.0/fi_out);

            SEGYlib.SEGYFile sf = new SEGYFile();
            sf.Open(f);   // open an existing SEGY file

            SEGYlib.SEGYFile sf2 = new SEGYlib.SEGYFile(); // create a new SEGY file
            sf2.Open(g);

            sf2.FileHeader = sf.FileHeader.Copy();  // copy the input trace header
            if( checkBox2.Checked)
            {
  
                sf2.FileHeader.numberOfSamplesPerDataTrace = Convert.ToUInt16((maxTime - minTime) * 1e3 / sf2.FileHeader.sampleIntervalInMicroseconds);
            }
            if(checkBox3.Checked) sf2.FileHeader.SetFileHeader(0,36,"C36 Positions coverted to Lat/Lon : " + DateTime.Now.ToShortDateString());
            sf2.Write(sf2.FileHeader);  // write out the header 
            int c = 0;
            bool flip = true;

            System.IO.StreamWriter tw = null;

            if ( checkBox1.Checked )
            {
                // create a zone report
                string z = System.IO.Path.ChangeExtension(g, ".prj");
                 tw = System.IO.File.CreateText(z);
            }
            while (sf.ReadNextTrace())
            {
                SEGYlib.SEGYTrace tr = sf.currentTrace;
                SEGYlib.SEGYTrace newTr = tr.Copy();
                if ( checkBox2.Checked )
                {
                    // pad top and bottom of trace
                    maxTraceLength = Convert.ToInt32((maxTime - minTime) * 1e3 / tr.TraceHeader.sampleIntervalUsec);
                    // delay offset
                    int delayoffset = (int)((newTr.TraceHeader.delayRecordingTimeMsec - minTime)*1e3/(double) newTr.TraceHeader.sampleIntervalUsec);
                    // pad top of trace
                    newTr.Resize(maxTraceLength, delayoffset);
                    newTr.TraceHeader.delayRecordingTimeMsec = (short) minTime;
                }     


                if ( checkBox1.Checked || checkBoxDatum.Checked || checkBox3.Checked)
                {
                    // switch to UTM
                    if  (tr.isLatLon)
                    {
                        double lon = tr.sourcePositionX;
                        double lat = tr.sourcePositionY;
                        if (checkBoxDatum.Checked)
                        {
                            double h = 0;
                            double X, Y, Z;
                            fw.Forward(lat, lon, 0.0, out X, out Y, out Z);
                            X += dX;
                            Y += dY;
                            Z += dZ;
                            rw.Reverse(X, Y, Z, out lat, out lon, out h);
                            newTr.sourcePositionX = lon;
                            newTr.sourcePositionY = lat;

                        }
                        if (checkBox1.Checked)
                        {
                            // convert to geocentric 
                            double x, y;
                            int setzone, zone;
                            bool north;
                            setzone = Convert.ToInt32(this.numericUpDown1.Value);
                            NETGeographicLib.UTMUPS.Forward(lat, lon, out zone, out north, out x, out y, setzone, false);
                            tw.WriteLine(lat.ToString() + " " + lon.ToString() + " " + x.ToString() + " " + y.ToString() + " " + zone.ToString());

                            newTr.TraceHeader.coordinateUnits = 1;
                            newTr.TraceHeader.scalarToBeAppliedToAllCoordinates = -100;
                            newTr.sourcePositionY = y;
                            newTr.sourcePositionX = x;

                        }
                    }
                    else
                    {
                        int setzone, zone;
                        bool north;
                        double lat, lon;

                        double x = tr.sourcePositionX;
                        double y = tr.sourcePositionY;
                        if ( x > 0 && x < 1000000.0 )
                        { 
                             setzone = Convert.ToInt32(this.numericUpDown1.Value);
                             NETGeographicLib.UTMUPS.Reverse(setzone, true, x, y, out lon, out lat, false);
                        }
                        else
                        {
                            lon = 0;
                            lat = 0;
                        }
                        newTr.TraceHeader.coordinateUnits = 2;
                        newTr.TraceHeader.scalarToBeAppliedToAllCoordinates = -100;
                        newTr.sourcePositionY = lon;
                        newTr.sourcePositionX = lat;
                        if(checkBoxDatum.Checked)
                        {
                            double h = 0;
                            double X, Y, Z;
                            fw.Forward(lat, lon, 0.0, out X, out Y, out Z);
                            X += dX;
                            Y += dY;
                            Z += dZ;
                            rw.Reverse(X, Y, Z, out lat, out lon, out h);
                            newTr.sourcePositionX = lon;
                            newTr.sourcePositionY = lat;

                        }
                    }
                } 



                sf2.Write(newTr);
                c++;
                if (c >= 4949)
                {
                    int ibreak = 0;
                }
                if (c % 100 == 0)
                {
                    toolStripStatusLabel1.Text = "Writing  " + g + " ";
                    if (flip)
                    {
                        toolStripStatusLabel1.Text += "\\";
                        flip = false;
                    }
                    else
                    {
                        toolStripStatusLabel1.Text += "/";
                        flip = true;
                    }
                    Application.DoEvents();
                }
            }

            if( tw != null) tw.Close();
            sf.Close();
            sf2.Close();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            datum_number = comboBox1.SelectedIndex;
            this.toolStripStatusLabel1.Text = comboBox1.Text;
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
