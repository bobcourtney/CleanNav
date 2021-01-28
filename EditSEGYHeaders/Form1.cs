using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using SEGYlib;

namespace EditSEGYHeaders
{
    public partial class Form1 : Form
    {
        SEGYFile f;
        PropertyInfo[] propertyInfos;
        bool changeActive;
        int currentTraceNumber;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Open SEGY File";
            if (this.openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            f = new SEGYFile();
            currentTraceNumber = 0;
            if( f.Open(this.openFileDialog1.FileName) == 0 )
            {
                return;
            }
            if (f.FileHeader.isSEGYFileHeaderAscii) checkBoxIsEBCDIC.Checked = false;
            refreshFileheader();
            //read first trace
            f.ReadNextTrace();
            
            refreshTraceHeader();

        }

        private void refreshTraceHeader()
        {
            changeActive = false;

            // binary trace info
            dataGridViewTraceHeader.Rows.Clear();
            if (f.currentTrace.TraceHeader != null)
            {
                propertyInfos = typeof(SEGYTraceHeader).GetProperties();
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    PropertyInfo p = propertyInfos[i];
                    string name = p.Name;
                    string dtype = p.PropertyType.Name;
                    string writeable = Convert.ToString(p.CanWrite);
                    object value = p.GetValue(f.currentTrace.TraceHeader, null);
                    dataGridViewTraceHeader.Rows.Add();
                    dataGridViewTraceHeader.Rows[i].Cells[0].Value = name;
                    dataGridViewTraceHeader.Rows[i].Cells[1].Value = dtype;
                    dataGridViewTraceHeader.Rows[i].Cells[2].Value = writeable;
                    dataGridViewTraceHeader.Rows[i].Cells[3].Value = value;
                }
            }

            // trace header info

            dataGridViewTraceSummary.Rows.Clear();
            if (f.currentTrace.TraceHeader != null)
            {
                propertyInfos = typeof(SEGYTrace).GetProperties();
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    PropertyInfo p = propertyInfos[i];
                    string name = p.Name;
                    string dtype = p.PropertyType.Name;
                    string writeable = Convert.ToString(p.CanWrite);
                    object value = p.GetValue(f.currentTrace, null);
                    dataGridViewTraceSummary.Rows.Add();
                    dataGridViewTraceSummary.Rows[i].Cells[0].Value = name;
                    dataGridViewTraceSummary.Rows[i].Cells[1].Value = dtype;
                    dataGridViewTraceSummary.Rows[i].Cells[2].Value = writeable;
                    dataGridViewTraceSummary.Rows[i].Cells[3].Value = value;
                }
            }

            // refresh trace display
            this.tChart1.AutoRepaint = false;
            this.fastLine1.Clear();
            double[] t = new double[f.currentTrace.Data.Length];
            for (int i = 0; i < t.Length; i++) t[i] = f.currentTrace.TraceHeader.delayRecordingTimeMsec + i * f.currentTrace.TraceHeader.sampleIntervalUsec / 1e3;
            this.fastLine1.Add(t,f.currentTrace.Data);
            this.tChart1.AutoRepaint = true;
            this.tChart1.Refresh();
            System.Windows.Forms.Application.DoEvents();
                changeActive = true;

        }
        private void refreshFileheader()
        {
            changeActive = false;
            dataGridViewFileHeader.Rows.Clear();
            if (f.FileHeader != null)
            {
                propertyInfos = typeof(SEGYFileHeader).GetProperties();
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    PropertyInfo p = propertyInfos[i];
                    string name = p.Name;
                    string dtype = p.PropertyType.Name;
                    string writeable = Convert.ToString(p.CanWrite);
                    object value = p.GetValue(f.FileHeader, null);
                    dataGridViewFileHeader.Rows.Add();
                    dataGridViewFileHeader.Rows[i].Cells[0].Value = name;
                    dataGridViewFileHeader.Rows[i].Cells[1].Value = dtype;
                    dataGridViewFileHeader.Rows[i].Cells[2].Value = writeable;
                    dataGridViewFileHeader.Rows[i].Cells[3].Value = value;
                }
                // file in text header
                this.dataGridViewTextHeader.Rows.Clear();
                for (int i = 0; i < 40; i++)
                {
                    string s = f.FileHeader.GetFileHeaderTextByLine(0, i);
                    dataGridViewTextHeader.Rows.Add();
                    dataGridViewTextHeader.Rows[i].Cells[0].Value = i;
                    dataGridViewTextHeader.Rows[i].Cells[1].Value = s;

                }
            }
            changeActive = true;

        }

        private void dataGridViewFileHeader_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!changeActive) return;
            if ( f == null) return; // file not open
            if (e.ColumnIndex != 3) return; // only edit value
            SEGYFileHeader fh = f.FileHeader; // active object
            int rowid = e.RowIndex;
            propertyInfos = typeof(SEGYFileHeader).GetProperties();
            PropertyInfo p = propertyInfos[rowid];
            object val;
            try
            {
                val = Convert.ChangeType(dataGridViewFileHeader.Rows[rowid].Cells[e.ColumnIndex].Value, p.PropertyType);
            }
            catch ( SystemException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                dataGridViewFileHeader.Rows[rowid].Cells[3].Value = p.GetValue(fh, null);
                return;
            }
            p.SetValue(fh, val, null);
            refreshFileheader();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( f.currentTrace != null)
            {
                f.ReadNextTrace();
                refreshTraceHeader();

            }
        }

        private void dataGridViewTraceHeader_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!changeActive) return;
            if (f == null) return; // file not open
            if (e.ColumnIndex != 4 && e.ColumnIndex != 5)  return; // only edit value set or increment
            SEGYTraceHeader fh = f.currentTrace.TraceHeader; // active object
            int rowid = e.RowIndex;
            propertyInfos = typeof(SEGYTraceHeader).GetProperties();
            PropertyInfo p = propertyInfos[rowid];
            object val;
            try
            {
                val = Convert.ChangeType(dataGridViewTraceHeader.Rows[rowid].Cells[e.ColumnIndex].Value, p.PropertyType);
            }
            catch (SystemException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                dataGridViewTraceHeader.Rows[rowid].Cells[3].Value = p.GetValue(fh, null);
                return;
            }
            p.SetValue(fh, val, null);
            refreshTraceHeader();
        }

        private void dataGridViewTraceSummary_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!changeActive) return;
            if (f == null) return; // file not open
            if (e.ColumnIndex != 3) return; // only edit value
            SEGYTrace fh = f.currentTrace; // active object
            int rowid = e.RowIndex;
            propertyInfos = typeof(SEGYTrace).GetProperties();
            PropertyInfo p = propertyInfos[rowid];
            object val;
            try
            {
                val = Convert.ChangeType(dataGridViewTraceSummary.Rows[rowid].Cells[e.ColumnIndex].Value, p.PropertyType);
            }
            catch (SystemException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                dataGridViewTraceSummary.Rows[rowid].Cells[3].Value = p.GetValue(fh, null);
                return;
            }
            p.SetValue(fh, val, null);
            refreshTraceHeader();
        }

        private void checkBoxIsEBCDIC_CheckedChanged(object sender, EventArgs e)
        {
            if ( checkBoxIsEBCDIC.Checked)
            {
                f.FileHeader.isSEGYFileHeaderAscii = false;
                checkBoxIsEBCDIC.Text = "Header is in EBCDIC format";
            }
            else
            {
                f.FileHeader.isSEGYFileHeaderAscii = true;
                checkBoxIsEBCDIC.Text = "Header is in ASCII format";
            }
            refreshFileheader();

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (f.currentTrace != null)
            {
                f.ReadNextTrace();
                refreshTraceHeader();
            }
        }

        private void dataGridViewTraceHeader_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridViewTraceHeader_DoubleClick(object sender, EventArgs e)
        {
                //f.MoveFilePointerToStartOfTraces();
                ////current row
                //int current_row = this.dataGridViewTraceHeader.CurrentRow.Index;
                //var cells = this.dataGridViewTraceHeader.CurrentRow.Cells;
                //// set or increment
                //// selected property
                //propertyInfos = typeof(SEGYTraceHeader).GetProperties();
                //ArrayList x = new ArrayList();
                //ArrayList y = new ArrayList();
                //PropertyInfo p = propertyInfos[current_row];
                //while ( f.ReadNextTrace() != false )
                //{ 

                //    object value = p.GetValue(f.currentTrace.TraceHeader, null);
                //    x.Add(f.currentTrace.positionOfTraceInFile);
                //    y.Add(value);

                //}
                //int k = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            scanSEGY();

        }

        private void scanSEGY()
        {
            // read all traces; drop trace data contents
            f.MoveFilePointerToStartOfTraces();
            f.Traces = new List<SEGYTrace>();
            int c = 0;
            while (true)
            {
                if (f.ReadNextTrace(false) == false) break;
                f.Traces.Add(f.currentTrace);
                c++;
                if (c % 100 == 0)
                {
                    this.toolStripStatusLabel1.Text = "scanning trace # " + c.ToString();
                    Application.DoEvents();
                }
            }
            this.toolStripStatusLabel1.Text = "";
            f.MoveFilePointerToStartOfTraces();
            f.ReadNextTrace();
            refreshTraceHeader();
            trackBar1.Minimum = 0;
            trackBar1.Maximum = f.Traces.Count - 1;
            trackBar1.TickFrequency = trackBar1.Maximum / 10;
            trackBar1.Value = 0;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {


        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if (f == null) return;
            if (f.Traces == null)
            {
                if (!f.isSEGY()) return;
                scanSEGY();
            }

            int val = this.trackBar1.Value;
            toolStripStatusLabel1.Text = " Current Trace #" + val.ToString();


            f.GoToStartOfTrace(val);
            f.ReadNextTrace();
            refreshTraceHeader();
        }
    }
}
