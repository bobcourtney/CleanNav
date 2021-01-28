using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
//using System.Drawing;
using NETGeographicLib;
using SEGYlib;
using System.Diagnostics.Eventing.Reader;

namespace NavCleaner
{
    public partial class Form1 : Form
    {
        private string filename;
        private SortedList data;
        private SortedList dataDeleted;
        private SortedList navSegments;
        private SortedList navSegmentsDeleted;
        double[] sx;
        double[] sy;
        double[] ts;
        double[] xs;
        double[] ys;
        bool zoomx, zoomy;
        point lastpoint;
        DataSet ds;
        DataTable dt;
        SEGYlib.SEGYFile sf;
        string inputSEGYfile;
        double referenceMeridian;
        SortedList expeditionYears;
        double[] millisecondsBetweenShots;
        double[] millisecondsCorrectionsToShotTime;
        bool drawing;
        List<DouglasPeucker.Point> PointsPruned;
        double rawDistanceAlongTrack, dpDistanceAlongTrack;
        bool mergenavFormat;

        public Form1()
        {
            InitializeComponent();


            ds = new DataSet();
            dt = null;
            // load from expedition database in background
            this.backgroundWorker2.RunWorkerAsync();
          
            referenceMeridian = -400;
        }

        private void LoadExpeditionDatabase()
        {
            this.oracleConnection1.ConnectionString = "user id=" + "coreview"
                                                     + ";password=" + "coreview"
                                              + ";data source=" + "(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)"
                                              + "(HOST=" + "gdratl.ess.nrcan.gc.ca"
                                              + ")(PORT=" + "1521"
                                              + "))(CONNECT_DATA="
                                              + "(SERVICE_NAME=" + "gdratl.ess.nrcan.gc.ca"
                                              + ")))";

            try
            {
                // load ED expeds
                this.oracleConnection1.Open();
                this.oracleCommand1.Connection = this.oracleConnection1;
                ds = new DataSet();
                this.oracleCommand1.CommandText = "SELECT  EXPED_CD, EXPED_YEAR FROM CORE.EXPEDITION WHERE (EXPED_CD IN (SELECT DISTINCT EXPED_CD FROM CORE.SEISMIC_PARAMETER)) ORDER BY EXPED_YEAR, EXPED_CD";
                this.oracleDataAdapter1.SelectCommand = this.oracleCommand1;
                this.oracleDataAdapter1.Fill(ds, "Cruise List");
                expeditionYears = new SortedList();
                dt = ds.Tables["Cruise List"];

            }

            catch (SystemException e)
            {
                System.Windows.Forms.MessageBox.Show("NRCan Expedition Database is not online");
            }
        }

        public void calculateSpeeds()
        {
            // load first segment
            int n = Convert.ToInt32(numericUpDown1.Value);
            data = (SortedList)navSegments.GetByIndex(n);
            point p0 = (point)data.GetByIndex(0);
            double x0 = p0.xm();
            double y0 = p0.ym();
            int zone = p0.zone;
            double t0 = p0.thr();

            // calculate speed
            sx = new double[data.Count];
            sy = new double[data.Count];
            sx = new double[data.Count];
            xs = new double[data.Count];
            ts = new double[data.Count];
            ys = new double[data.Count];
            xs[0] = p0.xm();
            ys[0] = p0.ym();
            ts[0] = p0.tsec();
            for (int i = 1; i < data.Count; i++)
            {
                point p = (point)data.GetByIndex(i);
                point pm = p;
                if (i > 0) pm = (point)data.GetByIndex(i - 1);
                p.zone = zone;
                pm.zone = zone;
                xs[i] = p.xm();
                ys[i] = p.ym();
                ts[i] = p.tsec();
                sx[i] = 1e3*(p.xm() - pm.xm()) / (p.tsec() - pm.tsec()); // time is now in msec
                sy[i] = 1e3*(p.ym() - pm.ym()) / (p.tsec() - pm.tsec());

            }
            sx[0] = sx[1];
            sy[0] = sy[1];


        }

        // display latlon
        private void displayLatLon()
        {
            this.tChart1.AutoRepaint = false;
            this.fastLineLatLonBox.Clear();
            this.fastLineLatLonBox.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.None;
            this.pointsLatLonBox.Clear();
            this.fastLineOtherLines.Clear();
            this.fastLineOtherLines.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.None;
            this.fastLineOtherLines.YValues.Order = Steema.TeeChart.Styles.ValueListOrder.None;
            this.fastLineOtherLines.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.DoNotPaint;


            int n = Convert.ToInt32(numericUpDown1.Value);

            for (int j = 0; j < navSegments.Count; j++)
            {
                if (j == n) continue; //skip current segment
                data = (SortedList)navSegments.GetByIndex(j);
                for (int i = 0; i < data.Count; i++)
                {
                    point p = (point)data.GetByIndex(i);



                    fastLineOtherLines.Add(p.x, p.y);
                    if ( i == data.Count - 1 )
                    {
                        int k = fastLineOtherLines.XValues.Count -1;
                        fastLineOtherLines.SetNull(k);
                    }

                }

            }

            data = (SortedList)navSegments.GetByIndex(n);
            dataDeleted = (SortedList)navSegmentsDeleted.GetByIndex(n);
            this.fastLineLatLonBox.LinePen.Color = Color.Blue;
            for ( int i = 0; i < data.Count; i++ )
            {
                point p = (point)data.GetByIndex(i);

                this.fastLineLatLonBox.Add(p.x, p.y);
            }
            for (int i = 0; i < dataDeleted.Count; i++)
            {
                point p = (point)dataDeleted.GetByIndex(i);

                this.pointsLatLonBox.Add(p.x, p.y);
            }

            this.tChart1.AutoRepaint = true;
            this.tChart1.Refresh();


        }



        // this routine find the bracketing times of a desired time point 

        private void findBoundingTimes( long inputCodedTime, int segment,  out long bottomBoundedTime, out long topBoundedTime)
        {
            // use bisection method
            int n = segment; // current segmant
            data = (SortedList)navSegments.GetByIndex(n); // current "good" points data block
            int np = data.Count;
            if ( inputCodedTime <= ((point) data.GetByIndex(0)).t )
            {
                // extrapolate from 1st two point in series
                bottomBoundedTime = ((point) data.GetByIndex(0)).t;
                topBoundedTime = ((point) data.GetByIndex(1)).t;
                return;
            }
            if ( inputCodedTime >= ((point) data.GetByIndex(np-1)).t )
            {
                // extrapolate from 1st two point in series
                bottomBoundedTime = ((point) data.GetByIndex(np-2)).t;
                topBoundedTime = ((point) data.GetByIndex(np-1)).t;
                return;
            }

            int nbottom = 0;
            int ntop = np-1;
            double ts = point.codedTimeIntoSeconds(inputCodedTime);

            while ( ntop - nbottom > 1 )
            {


                int nprediction = (int)(nbottom + (int) ( ts -((point) data.GetByIndex(nbottom)).tsec() )*1.0*(ntop - nbottom)/(((point) data.GetByIndex(ntop)).tsec()-((point) data.GetByIndex(nbottom)).tsec() ));

                if ( ((point) data.GetByIndex(nprediction)).tsec()  > ts )
                {
                    ntop = nprediction;

                } else {
                    if ( nbottom == nprediction)
                    {
                        ntop = nbottom + 1;
                        while (((point)data.GetByIndex(ntop)).tsec() < ts && ntop < data.Count)
                        {
                            ntop++;
                            nbottom++;
                        }
                        break;
                    }
                    else { 
                     nbottom = nprediction;
                    }
                }
            }

            topBoundedTime = ((point)data.GetByIndex(ntop)).t;
            bottomBoundedTime =((point) data.GetByIndex(nbottom)).t ;


        }
        // display latlon
        private void displayEditBox()
        {
            if (data == null) return;
            this.tChart3.AutoRepaint = false;
            if( this.nearestPoint1 != null ) this.nearestPoint1.Active = false;
            if (this.nearestPoint2 != null) this.nearestPoint2.Active = false;
            this.fastLineEditBox.Clear();
            this.fastLine1.Clear(); this.fastLine2.Clear(); this.fastLine3.Clear();
            this.fastLineEditBox.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            this.pointsEditBox.Clear();

            int n = Convert.ToInt32(numericUpDown1.Value);

            data = (SortedList)navSegments.GetByIndex(n);
            dataDeleted = (SortedList)navSegmentsDeleted.GetByIndex(n);
            //int zone = ((point)data.GetByIndex(0)).zone;

            for (int i = 0; i < data.Count; i++)
            {
                point p = (point)data.GetByIndex(i);
               // p.zone = zone;
                double d = 0; ;
                if(radioButtonLat.Checked)
                {
                    d = p.x;
                    tChart3.Axes.Left.Title.Text = "Lat";
                } 
                else if (radioButtonLon.Checked)
                {
                    d = p.y;
                    tChart3.Axes.Left.Title.Text = "Long";
                }
                else if (radioButtonUTME.Checked)
                {
                    d = p.xm();
                    tChart3.Axes.Left.Title.Text = "Easting";
                }
                else if (radioButtonUTMN.Checked)
                {
                    d = p.ym();
                    tChart3.Axes.Left.Title.Text = "Northing";
                }
                else if (radioButtonVX.Checked)
                {
                    if (data.Count <= 1) break;
                    if( i > 0)
                    {
                        point pm =  (point)data.GetByIndex(i-1);
                        d = 1e3 * (p.ym() - pm.ym()) / (p.tsec() - pm.tsec());
                    } else {
                        point pm =  (point)data.GetByIndex(i+1);
                        d = 1e3 * (pm.ym() - p.ym()) / (pm.tsec() - p.tsec());
                    }
                    tChart3.Axes.Left.Title.Text = "Vx (m/s)";
                }
                else if (radioButtonVY.Checked)
                {
                    if (data.Count <= 1) break;
                    if (i > 0)
                    {
                        point pm = (point)data.GetByIndex(i - 1);
                        //pm.zone = zone;
                        d = 1e3 * (p.xm() - pm.xm()) / (p.tsec() - pm.tsec());
                    }
                    else
                    {
                        point pm = (point)data.GetByIndex(i + 1);
                        //pm.zone = zone;
                        d = 1e3*(pm.xm() - p.xm()) / (pm.tsec() - p.tsec());
                    }
                    tChart3.Axes.Left.Title.Text = "Vy (m/s)";
                }
                else if (radioButtonV.Checked)
                {
                    if (data.Count <= 1) break;
                    if (i > 0)
                    {
                        point pm = (point)data.GetByIndex(i - 1);
                        //pm.zone = zone;
                        double dx = (p.xm() - pm.xm());
                        double dy = (p.ym() - pm.ym());
                        d = 1e3*Math.Sqrt(dx * dx + dy * dy) / (p.tsec() - pm.tsec());

                    }
                    else
                    {
                        point pm = (point)data.GetByIndex(i + 1);
                        //pm.zone = zone;
                        double dx = (p.xm() - pm.xm());
                        double dy = (p.ym() - pm.ym());
                        d = -Math.Sqrt(dx * dx + dy * dy) / (p.tsec() - pm.tsec());
                    }
                    tChart3.Axes.Left.Title.Text = "V (m/s)";
                }
                this.fastLineEditBox.Add(p.thr(), d);
            }
            for (int i = 0; i < dataDeleted.Count; i++)
            {
                point p = (point)dataDeleted.GetByIndex(i);
                double d = 0;
                if (radioButtonLat.Checked)
                {
                    d = p.x;
                }
                else if (radioButtonLon.Checked)
                {
                    d = p.y;
                }
                else if (radioButtonUTME.Checked)
                {
                    d = p.xm();
                }
                else if (radioButtonUTMN.Checked)
                {
                    d = p.ym();
                }
                else if (radioButtonVX.Checked)
                {

                }
                else if (radioButtonVY.Checked)
                {

                }
                this.pointsEditBox.Add(p.thr(), d);
            }

            this.tChart3.AutoRepaint = true;
            drawing = false;
            this.tChart3.Refresh();
            while (!drawing)
            {
                System.Threading.Thread.Sleep(100);
            }
           

            if (this.radioButtonDEL.Checked)
            {
                ActivateNearestPoint1();
            }
            else
            {
                ActivateNearestPoint2();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if ( numericUpDownExpedYear.Value < 1901 )
            {
                System.Windows.Forms.MessageBox.Show("Set the expedition year and expedition name below before the navigation is read");
                return;
            }
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.filename = this.openFileDialog1.FileName;
                System.IO.TextReader tr = System.IO.File.OpenText(this.filename);
                this.Text = "NavTool : Active File is " + System.IO.Path.GetFileName(this.filename);
                char[] delimiterChars = { ' ', ',', ':', '\t' };
                data = new SortedList();
                dataDeleted = new SortedList();
                navSegments = new SortedList();
                referenceMeridian = -400.0;

                int year = (int)numericUpDownExpedYear.Value;

                int count = 0;
                while (true)
                {
                    string ln = tr.ReadLine();
                    count++;
                    if ((count % 100) == 0)
                    {
                        // update tools strip progress bar
                        if (count >= (int)this.numericUpDownMaxDataSize.Value) count = 0;
                        this.toolStripProgressBar1.Value = 100 * count / (int)this.numericUpDownMaxDataSize.Value;
                        Application.DoEvents();

                    }
                    if (ln == null)
                    {
                        if (data.Count > 0)
                        {
                            point pi = (point)data[data.GetKey(0)];
                            navSegments.Add(pi.t, data); // add last working segment
                        }
                        break;

                    }
                    string[] fields = ln.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length != 3 && fields.Length != 7)
                    {
                        // coule be start of survey or line break in survey
                        if (data.Count == 0 || radioButton1.Checked) continue; // start od survey file
                        // use starting time as index
                        point pi = (point)data[data.GetKey(0)];
                        navSegments.Add(pi.t, data);
                        data = new SortedList();
                        continue;
                    }
                    long t;
                    bool ok;
                    double x, y;
                    if (fields.Length == 3)
                    { 
                        ok = long.TryParse(fields[0], out t);
                        t *= 1000; // convert to msec
                        if (!ok) continue;
                        ok = double.TryParse(fields[1], out y);
                        if (!ok) continue;
                        ok = double.TryParse(fields[2], out x);
                        if (!ok) continue;
                        mergenavFormat = false; // AGC format
                    } else
                    {
                        // mergenav format yr day hr min ss lat loh
                        var day = Convert.ToInt32(fields[1]);
                        var hr = Convert.ToInt32(fields[2]);
                        var min  = Convert.ToInt32(fields[3]);
                        var sec  = Convert.ToInt32(fields[4]);
                        t = sec + 100 * (min + 100 * (hr + 100 * day));
                        t *= 1000;
                        ok = double.TryParse(fields[5], out y);
                        if (!ok) continue;
                        ok = double.TryParse(fields[6], out x);
                        if (!ok) continue;
                        mergenavFormat = true; // AGC format
                    }

                    if (referenceMeridian < -360) referenceMeridian =x;
                    point p = new point(t, x,y, this.checkBox1.Checked, referenceMeridian);
                    p.year = year;

                    if (!data.ContainsKey(p.t))
                    {
                        data.Add(p.t, p);
                    }

                }
                this.toolStripProgressBar1.Value = 0;
                Application.DoEvents();

                // need to check for unmarked segments breaks;
                for (int i = navSegments.Count - 1; i >= 0; i--)
                {
                    // check each of the segments for breaks
                    data = (SortedList)navSegments.GetByIndex(i);
                    if (data.Count == 1) continue;
                    long key = (long)navSegments.GetKey(i);

                    long[] ti = new long[data.Count];
                    for (int j = 0; j < data.Count; j++)
                    {
                        ti[j] = ((point)data.GetByIndex(j)).tsec();
                    }
                    long[] diffs = new long[data.Count - 1];
                    for (int j = 0; j < data.Count - 1; j++)
                    {
                        diffs[j] = ti[j + 1] - ti[j];
                    }
                    long[] diffsCopy = new long[data.Count - 1];
                    Array.Copy(diffs, diffsCopy, data.Count - 1);

                    Array.Sort(diffs);
                    int n2 = data.Count / 2;
                    long median =((point)data.GetByIndex(0)).tsec();
                    if ( n2 > 0 )
                    {
                        median = diffs[n2-1];
                    }
                    // line breaks 
                    long timeBreak = 100 * median;
                    if (timeBreak < 60000) timeBreak = 60000; // 1 minute minimum timebreak;

                    // number of time breaks
                    if (diffs[diffs.Length - 1] < timeBreak) continue;
                    SortedList data2 = new SortedList();
                    point p = (point)data.GetByIndex(0);
                    data2.Add(p.t, p);
                    navSegments.Remove(key); // remove segment from list

                    for (int j = 0; j < data.Count - 1; j++)
                    {
                        p = (point)data.GetByIndex(j + 1);
                        if (diffsCopy[j] >timeBreak)
                        {
                            long key2 = (long)data2.GetKey(0);
                            navSegments.Add(key2, data2);
                            data2 = new SortedList();

                        }
                        data2.Add(p.t, p);
                    }
                    if (data2.Count > 0)
                    {
                        long key2 = (long)data2.GetKey(0);
                        navSegments.Add(key2, data2);
                    }

                }

                int datasize = ((SortedList)navSegments.GetByIndex(0)).Count;

                for (int i = 1; i < navSegments.Count-1; i++ )
                {
                    if ( datasize < ((SortedList)navSegments.GetByIndex(i)).Count  ) datasize =  ((SortedList)navSegments.GetByIndex(i)).Count;
                }
                
                if ( this.numericUpDownMaxDataSize.Value != 0 )
                {
                    // work from the bottom of the stack upwards
                    for ( int i = navSegments.Count - 1; i >= 0 ; i--)
                    {
                        data = (SortedList)(navSegments.GetByIndex(i));
                        long key = (long)navSegments.GetKey(i);
                        if ( data.Count > this.numericUpDownMaxDataSize.Value)
                        {
                            //number of sets
                            int nsets = (int) Math.Ceiling(((double)(data.Count))/ (double)this.numericUpDownMaxDataSize.Value);
                            int setsize = (int)((double)(data.Count/nsets) + nsets);
                            navSegments.Remove(key); // remove index
                            int c = 0;
                            for ( int j = 0; j < nsets ; j++)
                            {
                                SortedList datanew = new SortedList();
                                for ( int k = 0; k < setsize; k++)
                                {
                                    if (c > data.Count - 1) break;
                                    point p = (point)data.GetByIndex(c);
                                    datanew.Add(p.t, p);
                                    c++;
                                }
                                navSegments.Add(((point)datanew.GetByIndex(0)).t, datanew);
                            }
                        }
                    }
                }
                this.toolStripStatusLabel1.Text = "Maximum Datasize = " + datasize.ToString();
                    // reduce segment size

                    navSegmentsDeleted = new SortedList();
                for (int i = 0; i < navSegments.Count; i++ )
                {
                    dataDeleted = new SortedList();
                    object key = navSegments.GetKey(i);
                    navSegmentsDeleted.Add(key, dataDeleted);
                }



                this.label1.Text = "Number of Segments is " + navSegments.Count.ToString();
                this.label3.Text = " of " + navSegments.Count.ToString() + " segments";
                this.numericUpDown1.Value = 0;
                this.numericUpDown1.Maximum = navSegments.Count - 1;

                calculateSpeeds();
                //displayArrays(ts, sx, sy, ts[0], 0.0, 0.0);
                displayLatLon();
                // display edit box
                displayEditBox();

                // zoom to limits
                this.tChart3.Zoom.Undo();
                this.tChart1.Zoom.Undo();


            }


        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dt == null) return;

            if (this.comboBox1.Items.Count > 0 && this.comboBox1.Text.Length > 3 )
            {

                data = new SortedList();
                dataDeleted = new SortedList();
                navSegments = new SortedList();

                string exped = this.comboBox1.Text;
                Cursor.Current = Cursors.WaitCursor;
                UseWaitCursor = true;
                Application.DoEvents();
                this.oracleCommand1.Connection = this.oracleConnection1;
                this.oracleCommand1.CommandText = "SELECT SEISMIC_GMT, SEISMIC_LAT, SEISMIC_LONG,SEISMIC_SEC FROM CORE.SEISMIC_NAV WHERE (EXPED_CD = '" + exped + "') ORDER BY SEISMIC_GMT, SEISMIC_SEC";
                this.oracleDataAdapter1.SelectCommand = this.oracleCommand1;
                ds = new DataSet();
                this.backgroundWorker1.RunWorkerAsync();
                while (this.backgroundWorker1.IsBusy)
                {
                    System.Threading.Thread.Sleep(200);
                    Application.DoEvents();
                }
                this.Text = "NavTool : Active Expedition is " + exped;
                Cursor.Current = Cursors.Default;
                UseWaitCursor = false;


                dt = ds.Tables["Navigation"];

                referenceMeridian = -400.0;
                int year = (int) expeditionYears[exped];

                this.textBoxExpedID.Text = exped;
                this.numericUpDownExpedYear.Value = year;

                for (int i = 0; i < dt.Rows.Count; i++ )
                {
                    object[] ia = dt.Rows[i].ItemArray;

                    long t;
                    double x;
                    double y;

                    y = Convert.ToDouble(ia[1]);
                    x = Convert.ToDouble(ia[2]);
                    t = 1000*(Convert.ToInt64(ia[0])*100 + Convert.ToInt64(ia[3]));

                    if (referenceMeridian < -360) referenceMeridian = x;
                    point p = new point(t, x, y, this.checkBox1.Checked, referenceMeridian);
                    p.year = year;


                    if (!data.ContainsKey(p.t))
                    {
                        data.Add(p.t, p);
                    }

                }
                point pi = (point)data[data.GetKey(0)];
                navSegments.Add(pi.t, data); // add last working segment


                // need to check for unmarked segments breaks;
                for (int i = navSegments.Count - 1; i >= 0; i--)
                {
                    // check each of the segments for breaks
                    data = (SortedList)navSegments.GetByIndex(i);
                    long key = (long)navSegments.GetKey(i);

                    long[] ti = new long[data.Count];
                    for (int j = 0; j < data.Count; j++)
                    {
                        ti[j] = ((point)data.GetByIndex(j)).tsec();
                    }
                    long[] diffs = new long[data.Count - 1];
                    for (int j = 0; j < data.Count - 1; j++)
                    {
                        diffs[j] = ti[j + 1] - ti[j];
                    }
                    long[] diffsCopy = new long[data.Count - 1];
                    Array.Copy(diffs, diffsCopy, data.Count - 1);

                    Array.Sort(diffs);
                    int n2 = data.Count / 2;
                    long median = diffs[n2];
                    // line breaks 
                    long timeBreak = 100 * median;
                    if (timeBreak < 60000) timeBreak = 60000; // 1 minute minimum timebreak;

                    // number of time breaks
                    if (diffs[diffs.Length - 1] < timeBreak) continue;
                    SortedList data2 = new SortedList();
                    point p = (point)data.GetByIndex(0);
                    data2.Add(p.t, p);
                    navSegments.Remove(key); // remove segment from list

                    for (int j = 0; j < data.Count - 1; j++)
                    {
                        if (diffsCopy[j] < timeBreak)
                        {
                            p = (point)data.GetByIndex(j + 1);

                            data2.Add(p.t, p);
                        }
                        else
                        {
                            long key2 = (long)data2.GetKey(0);
                            navSegments.Add(key2, data2);
                            data2 = new SortedList();
                        }
                    }
                    if (data2.Count > 0)
                    {
                        long key2 = (long)data2.GetKey(0);
                        navSegments.Add(key2, data2);
                    }

                }

                navSegmentsDeleted = new SortedList();
                for (int i = 0; i < navSegments.Count; i++)
                {
                    dataDeleted = new SortedList();
                    object key = navSegments.GetKey(i);
                    navSegmentsDeleted.Add(key, dataDeleted);
                }



                this.label1.Text = "Number of Segments is " + navSegments.Count.ToString();
                this.label3.Text = " of " + navSegments.Count.ToString() + " segments";
                this.numericUpDown1.Value = 0;
                this.numericUpDown1.Maximum = navSegments.Count - 1;

                calculateSpeeds();
                //displayArrays(ts, sx, sy, ts[0], 0.0, 0.0);
                displayLatLon();
                // display edit box
                displayEditBox();
                Application.DoEvents();
                // zoom to limits
                this.tChart3.Zoom.Undo();
                this.tChart1.Zoom.Undo();


            }


        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)

        {
            // zoom to limits
            this.tChart3.Zoom.Undo();
            this.tChart1.Zoom.Undo();
            this.nearestPoint1.Active = false;
            this.nearestPoint2.Active = false;
            this.pointsCurrent.Clear();
            this.fastlineLineFit.Clear();
            displayLatLon();
            displayEditBox();
            this.toolStripStatusLabel1.Text = "Current Data Size = " + data.Count.ToString();



        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonLon_CheckedChanged(object sender, EventArgs e)
        {
            this.tChart3.Zoom.Undo();
            tChart1.Zoom.Undo();
            displayEditBox();
        }

        private void editor1_Click(object sender, EventArgs e)
        {

        }

        private void tChart3_KeyPress(object sender, KeyPressEventArgs e)
        {
            int k = Convert.ToInt32(e.KeyChar);
            if ( k == 120 )
            {
                //unzoom x 
                this.tChart3.Zoom.Undo();
                this.tChart1.Zoom.Undo();
            }
            else
            {

                 DeleteorAddPoint(k);
            }
        }

        private void DeleteorAddPoint(int k)
        {
            point p0 = null;
            point p = null;

            long originalKey = (long)navSegments.GetKey(Convert.ToInt32(this.numericUpDown1.Value));

            if (this.radioButtonDEL.Checked || k == 100)
            {
                int i = this.nearestPoint1.Point;
                if (i < 0 || i > data.Count - 1) return;
                p = (point)data.GetByIndex(i);
                p0 = (point)data.GetByIndex(0);
                data.RemoveAt(i);
                dataDeleted.Add(p.t, p);



            }
            else
            {
                // undo deleted point
                int i = this.nearestPoint2.Point;
                if (i < 0 || i > dataDeleted.Count - 1) return;
                p = (point)dataDeleted.GetByIndex(i);
                p0 = (point)data.GetByIndex(0);
                dataDeleted.RemoveAt(i);
                data.Add(p.t, p);
            }

            // original index key

            navSegments.Remove(originalKey);
            navSegments.Add(originalKey, data);

            if(navSegmentsDeleted.ContainsKey(originalKey))  navSegmentsDeleted.Remove(originalKey);
            navSegmentsDeleted.Add(originalKey, dataDeleted);

            displayEditBox();

            // update current point 
            this.pointsCurrent.Clear();
            this.pointsCurrent.Add(p.x, p.y);
            displayLatLon();
        }

        private void tChart3_Click(object sender, EventArgs e)
        {
            point p0 = null;
            point p = null;

            if (this.radioButtonDEL.Checked )
            {
                if (data == null) return;
                if (data.Count <= 0) return;
                // delete point
                int i = this.nearestPoint1.Point;
                if (i < 0) return;
                if (i < 0 || i > data.Count - 1) return;
                p = (point)data.GetByIndex(i);

            }
            else
            {
                // undo deleted point
                if (dataDeleted == null) return;
                if (dataDeleted.Count <= 0) return;
                int i = this.nearestPoint2.Point;
                if (i < 0) return;
                if (i < 0 || i > dataDeleted.Count - 1) return;
                p = (point)dataDeleted.GetByIndex(i);

            }

            // update current point 
            this.pointsCurrent.Clear();
            this.pointsCurrent.Add(p.x, p.y);
            displayLatLon();
            this.toolStripStatusLabel1.Text = p.t.ToString() + " " + p.x.ToString() + " " + p.y.ToString();
        }

        private void ActivateNearestPoint2()
        {
            this.nearestPoint2 = new Steema.TeeChart.Tools.NearestPoint(tChart3.Chart);
            this.nearestPoint2.Active = false;
            this.nearestPoint2.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nearestPoint2.Brush.Visible = false;
            this.nearestPoint2.DrawLine = false;
            this.nearestPoint2.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.nearestPoint2.Pen.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.nearestPoint2.Series = this.pointsEditBox;
            this.nearestPoint2.Size = 4;
            this.nearestPoint2.Active = true;
        }

        private void ActivateNearestPoint1()
        {
            this.nearestPoint1 = new Steema.TeeChart.Tools.NearestPoint(tChart3.Chart);
            this.nearestPoint1.Active = false;
            this.nearestPoint1.Brush.Visible = false;
            this.nearestPoint1.DrawLine = false;
            this.nearestPoint1.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.nearestPoint1.Pen.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.nearestPoint1.Series = this.fastLineEditBox;
            this.nearestPoint1.Size = 4;
            this.nearestPoint1.Active = true;
        }

        private void radioButtonDEL_CheckedChanged(object sender, EventArgs e)
        {
            if( radioButtonDEL.Checked)
            {
                if ( this.nearestPoint1 != null ) this.nearestPoint1.Active = true;
                if (this.nearestPoint2 != null) this.nearestPoint2.Active = false;
            }
            else
            {
                if (this.nearestPoint1 != null) this.nearestPoint1.Active = false;
                if (this.nearestPoint2 != null) this.nearestPoint2.Active = true;
            }
        }

        private void tChart1_DoubleClick(object sender, EventArgs e)
        {
            this.tChart1.Zoom.Undo();
            this.tChart3.Zoom.Undo();
        }

        private void tChart1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int k = Convert.ToInt32(e.KeyChar);
                if (k == 120)
                {
                    //unzoom x 
                    this.tChart3.Zoom.Undo();
                    this.tChart1.Zoom.Undo();
                }
        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.oracleDataAdapter1.Fill(ds, "Navigation");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MedianCalculate();
        }

        private void MedianCalculate()
        {
            if (!this.radioButtonV.Checked) return;
            this.tChart1.AutoRepaint = false;
            int width = Convert.ToInt32(this.numericUpDownClippingWidth.Value);
            double[] median = new double[width];
            this.fastLine1.Clear(); this.fastLine2.Clear(); this.fastLine3.Clear();
            for (int i = 0; i < data.Count - 1; i++)
            {
                point pi = (point)data.GetByIndex(i);
                for (int j = -width / 2; j <= width / 2; j++)
                {
                    int k = Math.Abs(j - i);
                    if (k >= data.Count - 1) k = data.Count - 2 - (k - data.Count);
                    double d;
                    point p = (point)data.GetByIndex(k);
                    if (k > 0)
                    {
                        point pm = (point)data.GetByIndex(k - 1);
                        double dx = (p.xm() - pm.xm());
                        double dy = (p.ym() - pm.ym());
                        d = 1e3*Math.Sqrt(dx * dx + dy * dy) / (p.tsec() - pm.tsec());

                    }
                    else
                    {
                        point pm = (point)data.GetByIndex(k + 1);
                        double dx = (p.xm() - pm.xm());
                        double dy = (p.ym() - pm.ym());
                        d = -1e3*Math.Sqrt(dx * dx + dy * dy) / (p.tsec() - pm.tsec());
                    }
                    median[j + width / 2] = d;
                }
                Array.Sort(median);
                double med = median[width / 2];
                this.fastLine1.Add(pi.thr(), med);
                double offset = Convert.ToDouble(this.numericUpDown2.Value);
                double ym = med - offset;
                if (ym < 0) ym = 0;
                double yx = med + offset;
                this.fastLine2.Add(pi.thr(), ym);
                this.fastLine3.Add(pi.thr(), yx);

            }
            this.tChart1.AutoRepaint = true;
            this.tChart1.Refresh();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (!this.radioButtonV.Checked) return;
            if( this.fastLine1.XValues.Count <= 0 ) return;

            this.tChart1.AutoRepaint = false;
            int width = Convert.ToInt32(this.numericUpDownClippingWidth.Value);
            this.fastLine2.Clear(); this.fastLine3.Clear();
            for (int i = 0; i < this.fastLine1.XValues.Count; i++)
            {
                double med = this.fastLine1.YValues[i]; 
                double offset = Convert.ToDouble(this.numericUpDown2.Value);
                double ym = med - offset;
                if (ym < 0) ym = 0;
                double yx = med + offset;
                this.fastLine2.Add( this.fastLine1.XValues[i], ym);
                this.fastLine3.Add( this.fastLine1.XValues[i], yx);

            }
            this.tChart1.AutoRepaint = true;
            this.tChart1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!this.radioButtonV.Checked) return;
            if (this.fastLine1.XValues.Count <= 0) return;

            for ( int i = data.Count - 2; i >= 0 ; i--)
            {
                point p = (point)data.GetByIndex(i);
                if ( fastLineEditBox.YValues[i] >= fastLine2.YValues[i] &&  fastLineEditBox.YValues[i] <= fastLine3.YValues[i] )
                {
                    //double diff = p.thr() - fastLineEditBox.XValues[i];
                    // keep the point
                }
                else
                {
                    data.RemoveAt(i);
                    dataDeleted.Add(p.t, p);
                }
            }

            // redisplay edit box
            this.displayEditBox();
            //redisplay lat box
            this.displayLatLon();
            //recalculate deviattion
            this.MedianCalculate();
        }

        private void numericUpDownClippingWidth_ValueChanged(object sender, EventArgs e)
        {
            MedianCalculate();
        }

        private void button8_Click(object sender, EventArgs e)
        {


                //string exped = this.comboBox1.Text;
                //Cursor.Current = Cursors.WaitCursor;
                //UseWaitCursor = true;
                //Application.DoEvents();
                //this.oracleCommand1.Connection = this.oracleConnection1;
                //this.oracleCommand1.CommandText = "SELECT SEISMIC_GMT, SEISMIC_LAT, SEISMIC_LONG,SEISMIC_SEC FROM CORE.SEISMIC_NAV WHERE (EXPED_CD = '" + exped + "') ORDER BY SEISMIC_GMT, SEISMIC_SEC";
                //this.oracleDataAdapter1.SelectCommand = this.oracleCommand1;
                //ds = new DataSet();
                //this.backgroundWorker1.RunWorkerAsync();
                //while (this.backgroundWorker1.IsBusy)
                //{
                //    System.Threading.Thread.Sleep(200);
                //    Application.DoEvents();
                //}

                //Cursor.Current = Cursors.Default;
                //UseWaitCursor = false;


                //dt = ds.Tables["Navigation"];

                this.openFileDialog1.Title = "Open SEGY Files";
                this.openFileDialog1.FileName = null;
                if (this.openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                data = new SortedList();
                dataDeleted = new SortedList();
                navSegments = new SortedList();
                this.Text = "NavTool : Active SEGY File is " + System.IO.Path.GetFileName(this.openFileDialog1.FileName);

                this.sf  = new SEGYlib.SEGYFile();
                inputSEGYfile = this.openFileDialog1.FileName;
                sf.Open(this.openFileDialog1.FileName);
                if( !sf.isSEGY()  )
                {
                    sf.Close();
                    return;
                }

                sf.MoveFilePointerToStartOfTraces();
                sf.Traces  = new List<SEGYlib.SEGYTrace>();
                int c = 0;
                this.toolStripStatusLabel1.Text = "Reading SEGY File";
                bool time_coded = true;
                while (sf.ReadNextTrace(true))
                {
                    SEGYlib.SEGYTrace tr = sf.currentTrace;
                   if (tr.timeTracedRecorded.Ticks == 0) time_coded = false; // no time code or error in time code

                    //tr.TraceData.TraceDataBuffer = null; // dump the trace data

                    sf.Traces.Add(tr);


                    c++;
                    if( (c % 100 ) == 0 )
                    {
                        if (toolStripProgressBar1.Value == 100) toolStripProgressBar1.Value = 0;
                        this.toolStripProgressBar1.Value += 1;
                        Application.DoEvents();
                    }
                }
                if (!time_coded)
                {
                    // create pseudo-time  assuming a ground speed of 2.5 m/s / 4,8 nauts
                    DateTime tstart = new DateTime(1900, 1, 1, 0, 0, 0);
                    // calculate median shot separation
                    double[] dist = new double[sf.Traces.Count - 1];
                    double d;
                    NETGeographicLib.Geodesic gd = new Geodesic();
                    for ( int i = 1; i < sf.Traces.Count ; i++)
                    {
                       SEGYTrace t1 = sf.Traces[i];
                       SEGYTrace t0 = sf.Traces[i - 1];
                        gd.Inverse(t0.sourcePositionY,t0.sourcePositionX,t1.sourcePositionY, t1.sourcePositionX, out d);
                        dist[i-1] = d;
                    }
                    Array.Sort(dist);
                    double median_shot_spacing = dist[dist.Length / 2];
                    double time_between_shots = median_shot_spacing / 2.5; // assuming a speed of 2.5 m/s
                    // create time in file
                    sf.Traces[0].timeTracedRecorded = tstart;
                    for ( int i = 0; i <sf.Traces.Count; i++)
                    {
                        tstart += TimeSpan.FromSeconds(time_between_shots);
                        SEGYTraceHeader h = sf.Traces[i].TraceHeader;
                        h.yearDataRecorded  = (ushort ) tstart.Year;
                        h.dayOfYear = (ushort)tstart.DayOfYear;
                        h.hourOfDay= (ushort)tstart.Hour;
                        h.minuteOfHour = (ushort)tstart.Minute;
                        h.secondOfMinute= (ushort)tstart.Second;
                        h.lagTimeBMsec = (short)tstart.Millisecond;
                    }

                }

                this.toolStripStatusLabel1.Text = "Done";
                toolStripProgressBar1.Value = 0;
                Application.DoEvents();
                sf.Close();


                // set year 
                this.numericUpDownExpedYear.Value = sf.Traces[0].timeTracedRecorded.Year;
                this.textBoxExpedID.Text = System.IO.Path.GetFileNameWithoutExtension(inputSEGYfile);

                if ( this.checkBoxFixMsec.Checked)
                {
                    for ( int i = 0; i < sf.Traces.Count; i++)
                    {
                        sf.Traces[i].TraceHeader.lagTimeBMsec = (short)sf.Traces[i].TraceHeader.timeBasis;
                    }
                }

                // check for msec field in data
                int numberOfRepeatedTimesValues = 0;
                for (int i = 1; i < sf.Traces.Count; i++ )
                {
                    if (sf.Traces[i].timeTracedRecorded.Millisecond  == sf.Traces[i - 1].timeTracedRecorded.Millisecond)
                    {
                        numberOfRepeatedTimesValues++;
                    }
                }
                if (!this.checkBoxCreateMillisecondField.Checked && numberOfRepeatedTimesValues > 10 )
                {
                     MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                    if ( System.Windows.Forms.MessageBox.Show("Millisecond field probably not set ; Do you want to calculate the msec field?","Potential Error", buttons) == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.checkBoxCreateMillisecondField.Checked = true;
                    }
                }

                    if (this.checkBoxCreateMillisecondField.Checked)
                    {
                        CorrectForMissingMillisecondField();
                    } 

                
                this.referenceMeridian = -4000;
                for (int i = 0; i < sf.Traces.Count; i++ )
                {

                    if (this.checkBoxFixMsec.Checked) sf.Traces[i].FixMsecField();

                    double x;
                    double y;
                    if ( this.checkBoxUseGroupLocations.Checked)
                    {
                        x = sf.Traces[i].groupPositionXGSCDIG;
                        y = sf.Traces[i].groupPositionYGSCDIG;

                    }
                    else
                    {
                        x = sf.Traces[i].sourcePositionX;
                        y = sf.Traces[i].sourcePositionY;

                    }
                    int t = 0;
                    if (referenceMeridian < -360) referenceMeridian = x;
                    point p = new point(t, x, y, this.checkBox1.Checked, referenceMeridian);
                    p.settime(sf.Traces[i].timeTracedRecorded); // ovverwrite default time
                    p.traceNumber = i;


                    if (!data.ContainsKey(p.t))
                    {
                        data.Add(p.t, p);
                    }
                    else
                    {
                        int stophere = 1;
                    }

                }
                point pi = (point)data[data.GetKey(0)];
                navSegments.Add(pi.t, data); // add last working segment


                // need to check for unmarked segments breaks;
                for (int i = navSegments.Count - 1; i >= 0; i--)
                {
                    // check each of the segments for breaks
                    data = (SortedList)navSegments.GetByIndex(i);
                    long key = (long)navSegments.GetKey(i);

                    long[] ti = new long[data.Count];
                    for (int j = 0; j < data.Count; j++)
                    {
                        ti[j] = ((point)data.GetByIndex(j)).tsec();
                    }
                    long[] diffs = new long[data.Count - 1];
                    for (int j = 0; j < data.Count - 1; j++)
                    {
                        diffs[j] = ti[j + 1] - ti[j];
                    }
                    long[] diffsCopy = new long[data.Count - 1];
                    Array.Copy(diffs, diffsCopy, data.Count-1);

                    Array.Sort(diffs);
                    int n2 = data.Count / 2;
                    long median = diffs[n2];
                    // line breaks 
                    long timeBreak = 10 * median;
                    if (timeBreak < 60000) timeBreak = 60000; // 1 minute minimum timebreak;

                    // number of time breaks
                    if (diffs[diffs.Length - 1] < timeBreak) continue;
                    SortedList data2 = new SortedList();
                    point p = (point)data.GetByIndex(0);
                    data2.Add(p.t, p);
                    navSegments.Remove(key); // remove segment from list

                    for (int j = 0; j < data.Count - 1; j++)
                    {
                        if (diffsCopy[j] < timeBreak)
                        {
                            p = (point)data.GetByIndex(j + 1);

                            data2.Add(p.t, p);
                        }
                        else
                        {
                            if ( data2.Count > 0 )
                            { 
                                long key2 = (long)data2.GetKey(0);
                                navSegments.Add(key2, data2);
                                data2 = new SortedList();
                            }
                        }
                    }
                    if (data2.Count > 0)
                    {
                        long key2 = (long)data2.GetKey(0);
                        navSegments.Add(key2, data2);
                    }

                }

                // make an estimate of median shot spacing
                ArrayList ar = new ArrayList();
                for (int i = 0; i < navSegments.Count; i++)
                {
                    data = (SortedList)navSegments.GetByIndex(i);
                    for ( int j = 1; j < data.Count; j++)
                    {
                        point p1 = (point)data.GetByIndex(j - 1);
                        point p2 = (point)data.GetByIndex(j);
                        ar.Add(Math.Sqrt(Math.Pow(p2.xm() - p1.xm(), 2) + Math.Pow(p2.ym() - p1.ym(), 2)));
                    }
                }
                ar.Sort();
                double medianShotSeparation = (double)ar[ar.Count / 2];
                this.numericUpDownShotSpacingAlongTrack.Value = (decimal)Math.Round(medianShotSeparation);

                navSegmentsDeleted = new SortedList();
                for (int i = 0; i < navSegments.Count; i++)
                {
                    dataDeleted = new SortedList();
                    object key = navSegments.GetKey(i);
                    navSegmentsDeleted.Add(key, dataDeleted);
                }



                this.label1.Text = "Number of Segments is " + navSegments.Count.ToString();
                this.label3.Text = " of " + navSegments.Count.ToString() + " segments";
                this.numericUpDown1.Value = 0;
                this.numericUpDown1.Maximum = navSegments.Count -1 ;

                calculateSpeeds();
                //displayArrays(ts, sx, sy, ts[0], 0.0, 0.0);
                displayLatLon();
                // display edit box
                displayEditBox();
                Application.DoEvents();

                // zoom to limits
                this.tChart3.Zoom.Undo();
                this.tChart1.Zoom.Undo();


        }

        private void CorrectForMissingMillisecondField()
        {
            for (int i = 0; i < sf.Traces.Count; i++)
            {
                // set all the milliseconds field to zero
                sf.Traces[i].TraceHeader.lagTimeBMsec = 0;
                sf.Traces[i].TraceHeader.lagTimeAMsec = 0;
            }

            //create a list of number of milliseconds between shots
            millisecondsBetweenShots = new double[sf.Traces.Count];
            millisecondsCorrectionsToShotTime = new double[sf.Traces.Count];
            int m = 10;

            for (int i = 0; i < sf.Traces.Count - m; i++)
            {
                millisecondsBetweenShots[i] = (sf.Traces[i + m].timeTracedRecorded - sf.Traces[i].timeTracedRecorded).TotalMilliseconds / m;
            }
            int c = 0;
            this.toolStripStatusLabel1.Text = "Recreating Millisecond field";
            for (int i = 0; i < sf.Traces.Count - m; i++)
            {
                c++;
                if ((c % 100) == 0)
                {
                    if (toolStripProgressBar1.Value == 100) toolStripProgressBar1.Value = 0;
                    this.toolStripProgressBar1.Value += 1;
                    Application.DoEvents();
                }

                double diffOld;
                double[] diff = new double[10];
                int ic = 0;
                bool gotit = false;
                int millisecondIncrement = 100;
                for (int shotTimeAfterSecondMarkMsec = 0; shotTimeAfterSecondMarkMsec < 1000; shotTimeAfterSecondMarkMsec += millisecondIncrement ) // shot offset from start of second .1 sec accuracy
                {
                    diff[ic]  =0;
                    // predict following shot times using shot interval
                    DateTime predictedShotTimeBase = sf.Traces[i].timeTracedRecorded; // assume millisecond field has been set to zero
                    for (int k = 1; k < m; k++) // test prediction over the next 10 traces
                    {
                        int recordedShotTimeDifference = (int)(sf.Traces[i + k].timeTracedRecorded - sf.Traces[i].timeTracedRecorded).TotalMilliseconds;

                        DateTime predictedShotTime = sf.Traces[i].timeTracedRecorded.AddMilliseconds(shotTimeAfterSecondMarkMsec + k * millisecondsBetweenShots[i]);
                        predictedShotTime = predictedShotTime.AddMilliseconds(-predictedShotTime.Millisecond); // strip off msec field

                        int predictedShotTimeDifference = 1000 * (int)Math.Round((predictedShotTime - predictedShotTimeBase).TotalSeconds); // msec quantize to 1000 msec increments

                        diff[ic] += Math.Abs(predictedShotTimeDifference - recordedShotTimeDifference);


                    }

                    if (diff[ic] < 1 )
                    {
                        gotit = true;
                        millisecondsCorrectionsToShotTime[i] = shotTimeAfterSecondMarkMsec + millisecondIncrement/2; // empirical evidence that the best fit minimum is 100 msec wide
                        if (i == sf.Traces.Count - m - 1)
                        {
                            //after last trace that can look ahead m traces
                            for (int kk = i + 1; kk < sf.Traces.Count; kk++)
                            {
                                DateTime predictedShotTime = sf.Traces[i].timeTracedRecorded.AddMilliseconds(shotTimeAfterSecondMarkMsec + kk * millisecondsBetweenShots[i]);
                                millisecondsCorrectionsToShotTime[i] = predictedShotTime.Millisecond;
                            }
                        }
                        break;

                    }
                    ic++;
                }
                if( !gotit )
                {
                    // did not find a zero mininum ; guess the correct from the minimum
                    double dmin = diff.Min();
                    // find the position of the minimum
                    for ( int j = 0; j < diff.Length; j++)
                    {
                        if ( diff[j] == dmin)
                        {
                            millisecondsCorrectionsToShotTime[i] = j*millisecondIncrement + millisecondIncrement / 2; // empirical evidence that the best fit minimum is 100 msec wide
                            if (i == sf.Traces.Count - m - 1)
                            {
                                //after last trace that can look ahead m traces
                                for (int kk = i + 1; kk < sf.Traces.Count; kk++)
                                {
                                    DateTime predictedShotTime = sf.Traces[i].timeTracedRecorded.AddMilliseconds(j * millisecondIncrement + kk * millisecondsBetweenShots[i]);
                                    millisecondsCorrectionsToShotTime[i] = predictedShotTime.Millisecond;
                                }
                            }
                            break;
                        }
                    }


                }


            }
            this.toolStripStatusLabel1.Text = "Done";
            toolStripProgressBar1.Value = 0;

            for (int i = 0; i < sf.Traces.Count; i++)
            {
                sf.Traces[i].TraceHeader.lagTimeBMsec = (short)millisecondsCorrectionsToShotTime[i];
                if (this.checkBoxFixMsec.Checked) sf.Traces[i].TraceHeader.timeBasis = (ushort)millisecondsCorrectionsToShotTime[i];
            }
        }

        private void tChart1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            // load first segment
            ExtractLineNtimes();
        }

        private void ExtractLine()
        {
            int n = Convert.ToInt32(numericUpDown1.Value);
            data = (SortedList)navSegments.GetByIndex(n);
            point p0 = (point)data.GetByIndex(0);
            List<DouglasPeucker.Point> Points = new List<DouglasPeucker.Point>();
            //List<DouglasPeucker.Point> PointsPruned;

            for (int i = 0; i < data.Count; i++)
            {
                point pi = (point)data.GetByIndex(i);
                DouglasPeucker.Point p = new DouglasPeucker.Point(pi.xm(), pi.ym(), pi.tDateTime, pi.traceNumber, true);
                Points.Add(p);
            }
            double xtol = (double)this.numericUpDownSubsampleTrackMeters.Value;
            PointsPruned = DouglasPeucker.DP.DouglasPeuckerReduction(Points, xtol);

            this.fastlineLineFit.Active = true;
            this.fastlineLineFit.Visible = true;
            this.fastlineLineFit.XValues.Clear();
            this.fastlineLineFit.YValues.Clear();
            this.fastlineLineFit.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.None;
            this.fastlineLineFit.YValues.Order = Steema.TeeChart.Styles.ValueListOrder.None;
            Application.DoEvents();
            point pw = new point(p0.t, p0.x, p0.y, this.checkBox1.Checked, 0);


            for (int j = 0; j < PointsPruned.Count; j++)
            {
                DouglasPeucker.Point p = PointsPruned[j];

                pw.tDateTime = p.T; // convert date&time to coded dddhhhmmmsss
                p0 = (point)data[pw.t];
                this.fastlineLineFit.Add(p0.x, p0.y);
            }

            // calculate metrics 
            rawDistanceAlongTrack = 0;
            for ( int i = 1; i < data.Count; i++ )
            {
                double dx = ((point)data.GetByIndex(i)).xm() -((point)data.GetByIndex(i-1)).xm();
                double dy =  ((point)data.GetByIndex(i)).ym() -((point)data.GetByIndex(i-1)).ym();
                rawDistanceAlongTrack += Math.Sqrt(dx * dx + dy * dy);
            }
            dpDistanceAlongTrack = 0;
            for (int i =1; i < PointsPruned.Count; i++)
            {

                DouglasPeucker.Point p = PointsPruned[i-1];
                pw.tDateTime = p.T; // convert date&time to coded dddhhhmmmsss
                point p1 = (point)data[pw.t];

                p = PointsPruned[i];
                pw.tDateTime = p.T; // convert date&time to coded dddhhhmmmsss
                point p2  = (point)data[pw.t];

                double dx =p2.xm() - p1.xm();
                double dy = p2.ym() - p1.ym();
                dpDistanceAlongTrack += Math.Sqrt(dx * dx + dy * dy);
            }
            this.toolStripStatusLabel1.Text = "Stats for DP xtol = " + xtol.ToString() + "m  " + (rawDistanceAlongTrack/dpDistanceAlongTrack).ToString();

            this.tChart1.Refresh();
        }
        private void ExtractLineNtimes()
        {
            // save original 
            double saveValue = (double)this.numericUpDownSubsampleTrackMeters.Value;
            ArrayList xx = new ArrayList();
            ArrayList yy = new ArrayList();
            ArrayList ynp = new ArrayList();
            for ( double x = 0.5; x < saveValue ; x = 1.2*x)
            {
                this.numericUpDownSubsampleTrackMeters.Value = (decimal)x;
                ExtractLine();
                xx.Add(x);
                yy.Add(100*(rawDistanceAlongTrack/ dpDistanceAlongTrack - 1));
                ynp.Add(((double)this.PointsPruned.Count)/data.Count);
            }
            this.numericUpDownSubsampleTrackMeters.Value = (decimal)saveValue;
            ExtractLine();
            xx.Add(saveValue);
            yy.Add(100*(rawDistanceAlongTrack / dpDistanceAlongTrack -1));
            ynp.Add(((double)this.PointsPruned.Count)/data.Count);

            double[] xxx = (double[])  xx.ToArray(typeof(double));

            double[] yyy = (double[])yy.ToArray(typeof(double));
            double[] yynp  = (double[])ynp.ToArray(typeof(double));


            Form2 F = new Form2(xxx, yyy, yynp);
            F.Visible = true;
            


        }
        private void button7_Click(object sender, EventArgs e)
        {

            this.folderBrowserDialog1.Description = "Enter the destination for the NAV corrected SEGY file";
            if ( this.folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            // read throught input file and make corrections to trace positions as needed
            sf = new SEGYlib.SEGYFile();
            inputSEGYfile = this.openFileDialog1.FileName;
            sf.Open(this.openFileDialog1.FileName);
            if( !sf.isSEGY()  )
            {
                sf.Close();
                return;
            }

            string outputFileName = this.folderBrowserDialog1.SelectedPath + System.IO.Path.DirectorySeparatorChar.ToString()+ System.IO.Path.GetFileNameWithoutExtension(inputSEGYfile)+"cln.sgy"; 
          
            
            SEGYlib.SEGYFile sf2  = new SEGYlib.SEGYFile();
            sf2.Open(outputFileName);

            sf2.FileHeader = sf.FileHeader.Copy();
            sf2.Write(sf2.FileHeader);

            int currentseg = 0;
            int nseg = navSegments.Count;
            long bottomBoundedTime,topBoundedTime;

            int c = 0;
            this.toolStripStatusLabel1.Text = "Writing SEGY File";


            while ( sf.ReadNextTrace())
            {
                SEGYlib.SEGYTrace tr = sf.currentTrace;
                if (tr.timeTracedRecorded.Ticks == 0) continue; // no time code or error in time code

                long timecode = tr.codedTime;

                data = (SortedList)navSegments.GetByIndex(currentseg);
                while ( timecode > ((point)data.GetByIndex(data.Count - 1)).t && currentseg < nseg ) 
                {   
                    currentseg ++;  // move the segment number up 
                    if (currentseg == nseg) break;
                    data = (SortedList)navSegments.GetByIndex(currentseg);
                }

                if (currentseg == nseg)
                {
                    break;
                }

                SEGYlib.SEGYTrace newTr = tr.Copy();
                findBoundingTimes( timecode, currentseg, out bottomBoundedTime, out topBoundedTime);
                point p = ((point)data[bottomBoundedTime]).interpolatedPoint((point)data[bottomBoundedTime], (point)data[topBoundedTime], timecode);

                if ( p.x <= 360.0 && p.x >= -360.0 && p.y >= -90.0 && p.y <= 90.0 )
                {
                    newTr.TraceHeader.scalarToBeAppliedToAllCoordinates = -1000;
                    newTr.TraceHeader.coordinateUnits = 2; // seconds
                } else {
                    newTr.TraceHeader.scalarToBeAppliedToAllCoordinates = -100; // cm accuracy
                    newTr.TraceHeader.coordinateUnits = 1; // utm 
                }
                newTr.sourcePositionX = p.x;
                newTr.sourcePositionY = p.y;

                if ( this.checkBoxCreateMillisecondField.Checked)
                {
                    newTr.TraceHeader.lagTimeBMsec = (short) millisecondsCorrectionsToShotTime[c];
                    newTr.TraceHeader.timeBasis = (ushort)millisecondsCorrectionsToShotTime[c];

                }
                sf2.Write(newTr);
                c++;
                if ((c % 100) == 0)
                {
                    if (toolStripProgressBar1.Value == 100) toolStripProgressBar1.Value = 0;
                    this.toolStripProgressBar1.Value += 1;
                    Application.DoEvents();
                }
                 
            }
            this.toolStripStatusLabel1.Text = "Done";
            toolStripProgressBar1.Value = 0;
            Application.DoEvents();
            sf.Close();
            sf2.Close();

        }

        private void tChart1_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

            this.openFileDialog1.Title = "Enter the name(s) of the input SEGY files";
            this.openFileDialog1.FileName = null;
            if (this.openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            this.folderBrowserDialog1.Description = "Enter the destination for the NAV-merged SEGY files";
            if (this.folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            for (int kk = 0; kk < this.openFileDialog1.FileNames.Length; kk++)
            {
                // read throught input file and make corrections to trace positions as needed
                sf = new SEGYlib.SEGYFile();
                inputSEGYfile = this.openFileDialog1.FileNames[kk];
                sf.Open(this.openFileDialog1.FileNames[kk]);
                if (!sf.isSEGY())
                {
                    sf.Close();
                    continue;
                }
                string outputFileName = System.IO.Path.GetDirectoryName(inputSEGYfile) + System.IO.Path.DirectorySeparatorChar.ToString() + System.IO.Path.GetFileNameWithoutExtension(inputSEGYfile) + "nav.sgy";
                this.toolStripStatusLabel1.Text = "Processing " + outputFileName;
                System.Windows.Forms.Application.DoEvents();

                SEGYlib.SEGYFile sf2 = new SEGYlib.SEGYFile();
                sf2.Open(outputFileName);

                sf2.FileHeader = sf.FileHeader.Copy();
                sf2.Write(sf2.FileHeader);

                int currentseg = 0;
                int nseg = navSegments.Count;
                long bottomBoundedTime, topBoundedTime;

                while (sf.ReadNextTrace())
                {
                    SEGYlib.SEGYTrace tr = sf.currentTrace;
                    long timecode = tr.codedTime;

                    data = (SortedList)navSegments.GetByIndex(currentseg);
                    while (timecode > ((point)data.GetByIndex(data.Count - 1)).t && currentseg < nseg)
                    {
                        currentseg++;  // move the segment number up 
                        data = (SortedList)navSegments.GetByIndex(currentseg);
                    }

                    SEGYlib.SEGYTrace newTr = tr.Copy();
                    findBoundingTimes(timecode, currentseg, out bottomBoundedTime, out topBoundedTime);
                    point p = ((point)data[bottomBoundedTime]).interpolatedPoint((point)data[bottomBoundedTime], (point)data[topBoundedTime], timecode);
                    newTr.TraceHeader.coordinateUnits = 2; // enter in coordinate system here - choose arcsconds by default
                    if (newTr.TraceHeader.scalarToBeAppliedToAllCoordinates == 0) newTr.TraceHeader.scalarToBeAppliedToAllCoordinates = -1000; // 0.001 second accuracy or 0.001 m accuracy
                    if (p.x > 360.0 || p.x < -360.0 || p.y < -90.0 || p.y > 90.0) newTr.TraceHeader.coordinateUnits = 1;
                    newTr.sourcePositionX = p.x;
                    newTr.sourcePositionY = p.y;
                    sf2.Write(newTr);



                }

                sf.Close();
                sf2.Close();
            }
            this.toolStripStatusLabel1.Text = "Done";
            System.Windows.Forms.Application.DoEvents();
        }

        private void checkBoxUseGroupLocations_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tChart1_Click_1(object sender, EventArgs e)
        {

        }

        private void tChart3_AfterDraw(object sender, Steema.TeeChart.Drawing.Graphics3D g)
        {
            drawing = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Enter the folder for the output clean navigation file";
            if(folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)return;

            string expedcd = this.textBoxExpedID.Text;
            filename = expedcd + "_cln.nav";
            filename = folderBrowserDialog1.SelectedPath + System.IO.Path.DirectorySeparatorChar + filename;

            System.IO.TextWriter tr = System.IO.File.CreateText(filename);


            // write out the navigation in ASCII Format

            for ( int i  = 0; i < navSegments.Count; i++)
            {
                data = (SortedList)navSegments.GetByIndex(i);
                tr.WriteLine(expedcd + " " + data.Count.ToString());
                string lastt = "";
                for ( int j = 0; j < data.Count; j++)
                {
                    point p = (point)data.GetByIndex(j);
                    string t = p.t.ToString();
                    t = t.Substring(0, t.Length - 3); //trim off msec field
                    string x = p.x.ToString("F8");
                    string y = p.y.ToString("F8");
                    if( String.Compare( lastt, t) != 0 ) tr.WriteLine(t + "   " + y + "   " + x);
                    lastt = t;
                }
            }
            tr.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //load trace sheadear - make a sorted list that allows indexing 
            SortedList traces = new SortedList();
            int number_of_droppped_traces = 0;
            for (int i = 0; i < sf.Traces.Count; i++ )
            {
                SEGYlib.SEGYTrace tr = sf.Traces[i];
                if (tr.timeTracedRecorded.Ticks == 0) continue; // no time code or error in time code
                long j = tr.codedTime;
                if (!traces.Contains(j))
                {
                    traces.Add(j, tr);
                }
                else
                {
                    // drop trace
                    number_of_droppped_traces++;
                }
            }


            SEGYlib.SEGYTrace trClosest;

            // open the input file for random access
            SEGYlib.SEGYFile sf3 = new SEGYFile();
            sf3.Open(inputSEGYfile);

            // open a new file for writing
            SEGYlib.SEGYFile sf4 = new SEGYFile();
            string C = System.IO.Path.GetDirectoryName(inputSEGYfile);
            string basename = System.IO.Path.GetFileNameWithoutExtension(inputSEGYfile);
            string newName = System.IO.Path.GetDirectoryName(inputSEGYfile) + System.IO.Path.DirectorySeparatorChar.ToString() +basename + "_SC"+this.numericUpDownShotSpacingAlongTrack.Value.ToString()+".sgy";
            if (System.IO.File.Exists(newName)) System.IO.File.Delete(newName); // delete the old file if it exists
            sf4.Open(newName);
            sf4.FileHeader = sf3.FileHeader.Copy();
            sf4.Write(sf3.FileHeader);

            int currentTraceInFile = 1;

            for (int i = 0; i <= this.numericUpDown1.Maximum; i++)
            {
                this.numericUpDown1.Value = (decimal)i; // go sequentially through ssegments
                int currentseg = i;
                data = (SortedList)navSegments.GetByIndex(currentseg);
                Application.DoEvents();
                ExtractLine(); //extract the Douglas-Pecker best fit line to segment
                Application.DoEvents();

                // cyle through point pairs in DP line segments
                long  bottomBoundedTime, topBoundedTime;
                double doff = 0; // carry over offset btween pruned points

                for (int j = 1; j < PointsPruned.Count; j++)
                {
                    DouglasPeucker.Point P1 = PointsPruned[j - 1];
                    DouglasPeucker.Point P2 = PointsPruned[j];
                    
                    // find start and end trace corresponding to the end points
                    long starttime = P1.codedTime;
                    long endtime = P2.codedTime;

                    // distance between points
                    point p1 = (point)data[starttime];
                    int startingTraceIndex = traces.IndexOfKey(starttime);

                    point p2 = (point)data[endtime];
                    int endTraceIndex = traces.IndexOfKey(endtime);

                    double distance = Math.Sqrt(Math.Pow(p2.xm() - p1.xm(), 2) + Math.Pow(p2.ym() - p1.ym(), 2));
                    double deltaT = (p2.tDateTime - p1.tDateTime).TotalSeconds;
                    double speed = distance / deltaT;

                    // equation of line connecting p1 to p2
                    double dy = p2.ym() - p1.ym();
                    double dx = p2.xm() - p1.xm();
                    double b = 0; // y intercept
                    double slope = 0;
                    double tol = 1e-6;
                    double fac = 10.0;
                    if ( Math.Abs(dx) > tol)  // make sure line is not vertical
                    {
                        slope = dy / dx;
                        b = p1.ym() - slope * p1.xm(); // y = mx + b 
                    }

                    double slopePerpendicular ;
                    double bPerpendicular;

                    SortedList sortedByDistanceFromP1 = new SortedList();
                    SortedList sortedByDistanceFromPk = new SortedList();

                    // find the perpendicular offset of the traces in between the two times from the two points p1 and p2 ;
                    for (int k = startingTraceIndex ; k < endTraceIndex; k++ )
                    {
                         long timecode = (long) traces.GetKey(k);
                         findBoundingTimes( timecode, currentseg, out bottomBoundedTime, out topBoundedTime);

                         // interpolated position of sounding
                         point pk = ((point)data[bottomBoundedTime]).interpolatedPoint((point)data[bottomBoundedTime], (point)data[topBoundedTime], timecode);
                         pk.year = p1.year;

                         // equation of perpendicular line though pk to the DP line

                         bPerpendicular = 0;
                         slopePerpendicular = 0;
                         if ( Math.Abs(dy) > tol)
                         {
                             slopePerpendicular  =  -dx/dy; // slope of  is -1/m 
                             bPerpendicular = pk.ym() - slopePerpendicular * pk.xm(); // passes through pk

                         }

                         double xi, yi;
                         //double err;
                         //err = 0;
                         // case 1 -  defined slopes
                         if ( Math.Abs(dx) > tol && Math.Abs(dy) > tol )
                         {
                             // point on line and perpendicualr
                              xi = (bPerpendicular-b) * dx * dy / (dy * dy + dx * dx);
                              yi = slope * xi + b;

                              //check
                              //err = yi -( slopePerpendicular * xi + bPerpendicular);
                         }
                         else
                         {
                             // horizonal or vertical lines
                             if ( Math.Abs(dx) < tol )
                             {
                                 yi = pk.ym();
                                 xi = p1.xm();
                             }
                             else
                             {
                                 yi = p1.ym();
                                 xi = pk.xm();
                             }

                         }

                         // distance from point, pk, to line
                         double distanceAlongPerpendicular = Math.Sqrt(Math.Pow(pk.xm() - xi, 2) + Math.Pow(pk.ym() - yi, 2));
                         // express in nearest dm
                         int idistanceAlongPerpendicular = (int)Math.Round(fac*distanceAlongPerpendicular);

                         double distanceFromP1 = Math.Sqrt(Math.Pow(p1.xm() - xi, 2) + Math.Pow(p1.ym() - yi, 2));
                         int idistanceFromP1 = (int)Math.Round(fac*distanceFromP1);

                        // do a double sorted list by distance from starting point and distance from perpendicualr 
                         if (!sortedByDistanceFromP1.Contains(idistanceFromP1))
                         {
                             SortedList byDistancePerpendicular = new SortedList();
                             sortedByDistanceFromP1.Add(idistanceFromP1, byDistancePerpendicular);
                         }

                         SortedList sl = (SortedList)sortedByDistanceFromP1[idistanceFromP1];
                         if ( !sl.Contains(idistanceAlongPerpendicular) ) sl.Add(idistanceAlongPerpendicular, traces[timecode]);

                  
 
                      }

                        double d; 
                        int ikey = 0;
                        int id;
                        for ( d = doff ; d < distance; d += (double)this.numericUpDownShotSpacingAlongTrack.Value)
                        {
                            // find the trace at a specified deistance along track in the sorted array
                            id  = (int) ( fac * d ) ;
                            while ( id >= (int)sortedByDistanceFromP1.GetKey(ikey))
                            {
                                ikey++;
                                if (ikey >= sortedByDistanceFromP1.Count)
                                {
                                    ikey--;
                                    break;
                                }
                            }

                            if ( ikey < sortedByDistanceFromP1.Count - 1)
                            {
                                //bracket
                                if ( d - ((int)sortedByDistanceFromP1.GetKey(ikey-1)) <  ((int)sortedByDistanceFromP1.GetKey(ikey)) - d )
                                {
                                    //closer to previous
                                    trClosest = (SEGYTrace)(((SortedList)(sortedByDistanceFromP1.GetByIndex(ikey - 1))).GetByIndex(0));
                                }
                                else
                                {
                                    trClosest = (SEGYTrace)(((SortedList)(sortedByDistanceFromP1.GetByIndex(ikey))).GetByIndex(0));
                                }
                            }
                            else
                            {
                                // use last trace in series
                                trClosest = (SEGYTrace)(((SortedList)(sortedByDistanceFromP1.GetByIndex(ikey))).GetByIndex(0));
                            }

                            // re-read trace
                            // go to poistion of trace in file 
                            // read the whole trace again
                            // reset the trace date
                            sf3.MoveToStreamPosition(trClosest.positionOfTraceInFile);
                            sf3.ReadNextTrace();
                            SEGYTrace newtr = sf3.currentTrace.Copy();

                            newtr.TraceHeader.traceSequenceNumberWithinFile = (uint) currentTraceInFile;
                            currentTraceInFile++;

                            // find the staight line interpolated time
                            DateTime ti = P1.T.AddMilliseconds((P2.T - P1.T).TotalMilliseconds * d / distance);
                            newtr.timeTracedRecorded = ti;
                            // reset the trace position
                            if ( newtr.TraceHeader.coordinateUnits == 4 ||  newtr.TraceHeader.coordinateUnits == 3 )
                            {
                                newtr.TraceHeader.coordinateUnits = 2; // use seconds of arc
                                newtr.TraceHeader.scalarToBeAppliedToAllCoordinates = -1000; // gives sub 3 cm accuracy
                            }

                            // interpolated position of sounding
                            point pk = p1.interpolatedPoint(p1, p2, newtr.codedTime);
                            pk.year = p1.year;
                            //newtr.sourcePositionX = p1.x + (p2.x - p1.x) * d / distance;
                            //newtr.sourcePositionY = p1.y + (p2.y - p1.y) * d / distance;

                            newtr.sourcePositionX = pk.x;
                            newtr.sourcePositionY = pk.y;

                            // write the trace
                            // dumm the trace data contents
                            sf4.Write(newtr);



                        }

                        doff = ( distance  - d ) + (double)this.numericUpDownShotSpacingAlongTrack.Value; // starting point on next pair of DP points

                        

                }


            }
            sf4.Close();
            sf3.Close();
            
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            this.LoadExpeditionDatabase();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (dt == null) return; // no data table downloaded

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.comboBox1.Items.Add((string)dt.Rows[i].ItemArray[0]);
                expeditionYears.Add((string)dt.Rows[i].ItemArray[0], Convert.ToInt32((string)dt.Rows[i].ItemArray[1]));
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ExtractLine();

            double[] length = new double[PointsPruned.Count - 1];

           for ( int i = 1; i <  PointsPruned.Count; i++ )
           {
            DouglasPeucker.Point pm = (DouglasPeucker.Point)PointsPruned[i-1];
            DouglasPeucker.Point p =  (DouglasPeucker.Point)PointsPruned[i];
            length[i-1] = Math.Sqrt(Math.Pow(p.X - pm.X, 2) + Math.Pow(p.Y - pm.Y, 2));
            //length[i - 1] = length[i-1]/(p.T - pm.T).TotalSeconds;
           }
           Form3 f3 = new Form3(length);
           f3.Visible = true;
        }

        private void numericUpDownMaxDataSize_ValueChanged(object sender, EventArgs e)
        {
                
        }



    }
}
