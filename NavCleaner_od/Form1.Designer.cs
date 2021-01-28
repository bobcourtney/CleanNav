namespace NavCleaner
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tChart3 = new Steema.TeeChart.TChart();
            this.fastLineEditBox = new Steema.TeeChart.Styles.FastLine();
            this.pointsEditBox = new Steema.TeeChart.Styles.Points();
            this.fastLine1 = new Steema.TeeChart.Styles.FastLine();
            this.fastLine2 = new Steema.TeeChart.Styles.FastLine();
            this.fastLine3 = new Steema.TeeChart.Styles.FastLine();
            this.nearestPoint1 = new Steema.TeeChart.Tools.NearestPoint();
            this.nearestPoint2 = new Steema.TeeChart.Tools.NearestPoint();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonV = new System.Windows.Forms.RadioButton();
            this.radioButtonVY = new System.Windows.Forms.RadioButton();
            this.radioButtonVX = new System.Windows.Forms.RadioButton();
            this.radioButtonUTMN = new System.Windows.Forms.RadioButton();
            this.radioButtonUTME = new System.Windows.Forms.RadioButton();
            this.radioButtonLat = new System.Windows.Forms.RadioButton();
            this.radioButtonLon = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.editor1 = new Steema.TeeChart.Editor(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButtonUNDO = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButtonDEL = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDownSubsampleTrackMeters = new System.Windows.Forms.NumericUpDown();
            this.button9 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.checkBoxFixMsec = new System.Windows.Forms.CheckBox();
            this.checkBoxUseGroupLocations = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateMillisecondField = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxExpedID = new System.Windows.Forms.TextBox();
            this.numericUpDownExpedYear = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownShotSpacingAlongTrack = new System.Windows.Forms.NumericUpDown();
            this.button12 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.numericUpDownClippingWidth = new System.Windows.Forms.NumericUpDown();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.button4 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDownMaxDataSize = new System.Windows.Forms.NumericUpDown();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tChart1 = new Steema.TeeChart.TChart();
            this.fastLineLatLonBox = new Steema.TeeChart.Styles.FastLine();
            this.pointsLatLonBox = new Steema.TeeChart.Styles.Points();
            this.fastLineOtherLines = new Steema.TeeChart.Styles.FastLine();
            this.fastlineLineFit = new Steema.TeeChart.Styles.FastLine();
            this.pointsCurrent = new Steema.TeeChart.Styles.Points();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            //this.oracleConnection1 = new Oracle.ManagedDataAccess.Client.OracleConnection();
            //this.oracleDataAdapter1 = new Oracle.ManagedDataAccess.Client.OracleDataAdapter();
            //this.oracleCommand1 = new Oracle.ManagedDataAccess.Client.OracleCommand();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubsampleTrackMeters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExpedYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShotSpacingAlongTrack)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClippingWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDataSize)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tChart3
            // 
            // 
            // 
            // 
            this.tChart3.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart3.Axes.Bottom.Title.Caption = "Time (Julian Days)";
            this.tChart3.Axes.Bottom.Title.Lines = new string[] {
        "Time (Julian Days)"};
            // 
            // 
            // 
            this.tChart3.Axes.Left.FixedLabelSize = false;
            // 
            // 
            // 
            this.tChart3.Axes.Left.Title.Caption = "Speed (m/s)";
            this.tChart3.Axes.Left.Title.Lines = new string[] {
        "Speed (m/s)"};
            this.tChart3.Cursor = System.Windows.Forms.Cursors.Default;
            this.tChart3.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.tChart3.Header.Lines = new string[] {
        "Edit Box"};
            // 
            // 
            // 
            this.tChart3.Legend.Visible = false;
            this.tChart3.Location = new System.Drawing.Point(0, 0);
            this.tChart3.Name = "tChart3";
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart3.Panel.Brush.Gradient.Visible = false;
            this.tChart3.Panel.MarginLeft = 10D;
            this.tChart3.Series.Add(this.fastLineEditBox);
            this.tChart3.Series.Add(this.pointsEditBox);
            this.tChart3.Series.Add(this.fastLine1);
            this.tChart3.Series.Add(this.fastLine2);
            this.tChart3.Series.Add(this.fastLine3);
            this.tChart3.Size = new System.Drawing.Size(784, 461);
            this.tChart3.TabIndex = 2;
            this.tChart3.Tools.Add(this.nearestPoint1);
            this.tChart3.Tools.Add(this.nearestPoint2);
            this.tChart3.AfterDraw += new Steema.TeeChart.PaintChartEventHandler(this.tChart3_AfterDraw);
            this.tChart3.Click += new System.EventHandler(this.tChart3_Click);
            this.tChart3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tChart3_KeyPress);
            // 
            // fastLineEditBox
            // 
            this.fastLineEditBox.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.fastLineEditBox.ColorEach = false;
            // 
            // 
            // 
            this.fastLineEditBox.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.fastLineEditBox.Marks.Shadow.Visible = false;
            this.fastLineEditBox.Title = "fastLineEditBox";
            this.fastLineEditBox.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.Ignore;
            // 
            // 
            // 
            this.fastLineEditBox.XValues.DataMember = "X";
            this.fastLineEditBox.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLineEditBox.YValues.DataMember = "Y";
            // 
            // pointsEditBox
            // 
            this.pointsEditBox.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pointsEditBox.ColorEach = false;
            // 
            // 
            // 
            this.pointsEditBox.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.pointsEditBox.Pointer.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.pointsEditBox.Pointer.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(53)))));
            this.pointsEditBox.Pointer.HorizSize = 1;
            this.pointsEditBox.Pointer.SizeDouble = 0D;
            this.pointsEditBox.Pointer.SizeUnits = Steema.TeeChart.Styles.PointerSizeUnits.Pixels;
            this.pointsEditBox.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.DiagCross;
            this.pointsEditBox.Pointer.VertSize = 1;
            this.pointsEditBox.Title = "pointsEditDeletedPoints";
            // 
            // 
            // 
            this.pointsEditBox.XValues.DataMember = "X";
            this.pointsEditBox.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.pointsEditBox.YValues.DataMember = "Y";
            // 
            // fastLine1
            // 
            this.fastLine1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(53)))));
            this.fastLine1.ColorEach = false;
            this.fastLine1.DrawAllPoints = false;
            // 
            // 
            // 
            this.fastLine1.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.fastLine1.Title = "fastLine2";
            this.fastLine1.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.Ignore;
            // 
            // 
            // 
            this.fastLine1.XValues.DataMember = "X";
            this.fastLine1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine1.YValues.DataMember = "Y";
            // 
            // fastLine2
            // 
            this.fastLine2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(76)))), ((int)(((byte)(20)))));
            this.fastLine2.ColorEach = false;
            this.fastLine2.DrawAllPoints = false;
            // 
            // 
            // 
            this.fastLine2.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(76)))), ((int)(((byte)(20)))));
            this.fastLine2.Title = "fastLine3";
            this.fastLine2.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.Ignore;
            // 
            // 
            // 
            this.fastLine2.XValues.DataMember = "X";
            this.fastLine2.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine2.YValues.DataMember = "Y";
            // 
            // fastLine3
            // 
            this.fastLine3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(151)))), ((int)(((byte)(168)))));
            this.fastLine3.ColorEach = false;
            this.fastLine3.DrawAllPoints = false;
            // 
            // 
            // 
            this.fastLine3.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(151)))), ((int)(((byte)(168)))));
            this.fastLine3.Title = "fastLine4";
            this.fastLine3.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.Ignore;
            // 
            // 
            // 
            this.fastLine3.XValues.DataMember = "X";
            this.fastLine3.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLine3.YValues.DataMember = "Y";
            // 
            // nearestPoint1
            // 
            // 
            // 
            // 
            this.nearestPoint1.Brush.Visible = false;
            this.nearestPoint1.DrawLine = false;
            // 
            // 
            // 
            this.nearestPoint1.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.nearestPoint1.Pen.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.nearestPoint1.Series = this.fastLineEditBox;
            this.nearestPoint1.Size = 4;
            // 
            // nearestPoint2
            // 
            // 
            // 
            // 
            this.nearestPoint2.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nearestPoint2.Brush.Visible = false;
            this.nearestPoint2.DrawLine = false;
            // 
            // 
            // 
            this.nearestPoint2.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.nearestPoint2.Pen.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.nearestPoint2.Series = this.pointsEditBox;
            this.nearestPoint2.Size = 5;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(13, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Read";
            this.toolTip1.SetToolTip(this.button1, "Read in ASCII navigation file");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "of n segments";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Current Segment";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Number of Segments is unset";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(102, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Write";
            this.toolTip1.SetToolTip(this.button2, "Write out ASCII  editted points");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(16, 59);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown1.TabIndex = 13;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.radioButtonV);
            this.panel1.Controls.Add(this.radioButtonVY);
            this.panel1.Controls.Add(this.radioButtonVX);
            this.panel1.Controls.Add(this.radioButtonUTMN);
            this.panel1.Controls.Add(this.radioButtonUTME);
            this.panel1.Controls.Add(this.radioButtonLat);
            this.panel1.Controls.Add(this.radioButtonLon);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(726, 609);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 124);
            this.panel1.TabIndex = 14;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // radioButtonV
            // 
            this.radioButtonV.AutoSize = true;
            this.radioButtonV.Checked = true;
            this.radioButtonV.Location = new System.Drawing.Point(129, 80);
            this.radioButtonV.Name = "radioButtonV";
            this.radioButtonV.Size = new System.Drawing.Size(56, 17);
            this.radioButtonV.TabIndex = 22;
            this.radioButtonV.TabStop = true;
            this.radioButtonV.Text = "Speed";
            this.radioButtonV.UseVisualStyleBackColor = true;
            this.radioButtonV.CheckedChanged += new System.EventHandler(this.radioButtonLon_CheckedChanged);
            // 
            // radioButtonVY
            // 
            this.radioButtonVY.AutoSize = true;
            this.radioButtonVY.Location = new System.Drawing.Point(129, 59);
            this.radioButtonVY.Name = "radioButtonVY";
            this.radioButtonVY.Size = new System.Drawing.Size(99, 17);
            this.radioButtonVY.TabIndex = 21;
            this.radioButtonVY.Text = "Speed Northing";
            this.radioButtonVY.UseVisualStyleBackColor = true;
            this.radioButtonVY.CheckedChanged += new System.EventHandler(this.radioButtonLon_CheckedChanged);
            // 
            // radioButtonVX
            // 
            this.radioButtonVX.AutoSize = true;
            this.radioButtonVX.Location = new System.Drawing.Point(129, 38);
            this.radioButtonVX.Name = "radioButtonVX";
            this.radioButtonVX.Size = new System.Drawing.Size(94, 17);
            this.radioButtonVX.TabIndex = 20;
            this.radioButtonVX.Text = "Speed Easting";
            this.radioButtonVX.UseVisualStyleBackColor = true;
            this.radioButtonVX.CheckedChanged += new System.EventHandler(this.radioButtonLon_CheckedChanged);
            // 
            // radioButtonUTMN
            // 
            this.radioButtonUTMN.AutoSize = true;
            this.radioButtonUTMN.Location = new System.Drawing.Point(16, 101);
            this.radioButtonUTMN.Name = "radioButtonUTMN";
            this.radioButtonUTMN.Size = new System.Drawing.Size(92, 17);
            this.radioButtonUTMN.TabIndex = 19;
            this.radioButtonUTMN.Text = "UTM Northing";
            this.radioButtonUTMN.UseVisualStyleBackColor = true;
            this.radioButtonUTMN.CheckedChanged += new System.EventHandler(this.radioButtonLon_CheckedChanged);
            // 
            // radioButtonUTME
            // 
            this.radioButtonUTME.AutoSize = true;
            this.radioButtonUTME.Location = new System.Drawing.Point(16, 80);
            this.radioButtonUTME.Name = "radioButtonUTME";
            this.radioButtonUTME.Size = new System.Drawing.Size(87, 17);
            this.radioButtonUTME.TabIndex = 18;
            this.radioButtonUTME.Text = "UTM Easting";
            this.radioButtonUTME.UseVisualStyleBackColor = true;
            this.radioButtonUTME.CheckedChanged += new System.EventHandler(this.radioButtonLon_CheckedChanged);
            // 
            // radioButtonLat
            // 
            this.radioButtonLat.AutoSize = true;
            this.radioButtonLat.Location = new System.Drawing.Point(16, 59);
            this.radioButtonLat.Name = "radioButtonLat";
            this.radioButtonLat.Size = new System.Drawing.Size(63, 17);
            this.radioButtonLat.TabIndex = 17;
            this.radioButtonLat.Text = "Latitude";
            this.radioButtonLat.UseVisualStyleBackColor = true;
            this.radioButtonLat.CheckedChanged += new System.EventHandler(this.radioButtonLon_CheckedChanged);
            // 
            // radioButtonLon
            // 
            this.radioButtonLon.AutoSize = true;
            this.radioButtonLon.Location = new System.Drawing.Point(16, 38);
            this.radioButtonLon.Name = "radioButtonLon";
            this.radioButtonLon.Size = new System.Drawing.Size(72, 17);
            this.radioButtonLon.TabIndex = 16;
            this.radioButtonLon.Text = "Longitude";
            this.radioButtonLon.UseVisualStyleBackColor = true;
            this.radioButtonLon.CheckedChanged += new System.EventHandler(this.radioButtonLon_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Clean Option ";
            // 
            // editor1
            // 
            this.editor1.HighLightTabs = false;
            this.editor1.Location = new System.Drawing.Point(0, 0);
            this.editor1.Name = "editor1";
            this.editor1.Options = null;
            this.editor1.TabIndex = 0;
            this.editor1.Click += new System.EventHandler(this.editor1_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.radioButtonUNDO);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.radioButtonDEL);
            this.panel2.Location = new System.Drawing.Point(996, 481);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(199, 122);
            this.panel2.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Press \"x\" to zoom to limits";
            // 
            // radioButtonUNDO
            // 
            this.radioButtonUNDO.AutoSize = true;
            this.radioButtonUNDO.Location = new System.Drawing.Point(18, 56);
            this.radioButtonUNDO.Name = "radioButtonUNDO";
            this.radioButtonUNDO.Size = new System.Drawing.Size(85, 17);
            this.radioButtonUNDO.TabIndex = 23;
            this.radioButtonUNDO.Text = "Undo Delete";
            this.radioButtonUNDO.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Key Press Action";
            // 
            // radioButtonDEL
            // 
            this.radioButtonDEL.AutoSize = true;
            this.radioButtonDEL.Checked = true;
            this.radioButtonDEL.Location = new System.Drawing.Point(18, 35);
            this.radioButtonDEL.Name = "radioButtonDEL";
            this.radioButtonDEL.Size = new System.Drawing.Size(56, 17);
            this.radioButtonDEL.TabIndex = 22;
            this.radioButtonDEL.TabStop = true;
            this.radioButtonDEL.Text = "Delete";
            this.radioButtonDEL.UseVisualStyleBackColor = true;
            this.radioButtonDEL.CheckedChanged += new System.EventHandler(this.radioButtonDEL_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(13, 131);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "Read ED";
            this.toolTip1.SetToolTip(this.button3, "Read Data from ED");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(115, 29);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 27;
            this.button7.Text = "SEGY Out";
            this.toolTip1.SetToolTip(this.button7, "Write out Nav Edits in SEGY File");
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(13, 29);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 26;
            this.button8.Text = "SEGY In";
            this.toolTip1.SetToolTip(this.button8, "Read in new SEGY file");
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1043, 733);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(151, 17);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Use polar (UPS) projection";
            this.toolTip1.SetToolTip(this.checkBox1, "Otherwise use Lambert Conformal for Canada");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // numericUpDownSubsampleTrackMeters
            // 
            this.numericUpDownSubsampleTrackMeters.DecimalPlaces = 1;
            this.numericUpDownSubsampleTrackMeters.Location = new System.Drawing.Point(115, 166);
            this.numericUpDownSubsampleTrackMeters.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSubsampleTrackMeters.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownSubsampleTrackMeters.Name = "numericUpDownSubsampleTrackMeters";
            this.numericUpDownSubsampleTrackMeters.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownSubsampleTrackMeters.TabIndex = 29;
            this.toolTip1.SetToolTip(this.numericUpDownSubsampleTrackMeters, "Cross-line deviation limit (m)");
            this.numericUpDownSubsampleTrackMeters.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(13, 165);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(98, 20);
            this.button9.TabIndex = 28;
            this.button9.Text = "Rate-Distortion";
            this.toolTip1.SetToolTip(this.button9, "Estimate Rate-Distortion Curve");
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(13, 215);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(98, 20);
            this.button11.TabIndex = 30;
            this.button11.Text = "Project SEGY ";
            this.toolTip1.SetToolTip(this.button11, "Project the Input SEGY onto Subsampled Line");
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // checkBoxFixMsec
            // 
            this.checkBoxFixMsec.AutoSize = true;
            this.checkBoxFixMsec.Location = new System.Drawing.Point(13, 64);
            this.checkBoxFixMsec.Name = "checkBoxFixMsec";
            this.checkBoxFixMsec.Size = new System.Drawing.Size(115, 17);
            this.checkBoxFixMsec.TabIndex = 31;
            this.checkBoxFixMsec.Text = "Get  and set msec ";
            this.toolTip1.SetToolTip(this.checkBoxFixMsec, "GSCDIG saved msec as ushort 166-167 in \r\nthe SGEY trace header. This checked will" +
        " copy this value to\r\nLag time B");
            this.checkBoxFixMsec.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseGroupLocations
            // 
            this.checkBoxUseGroupLocations.AutoSize = true;
            this.checkBoxUseGroupLocations.Location = new System.Drawing.Point(13, 87);
            this.checkBoxUseGroupLocations.Name = "checkBoxUseGroupLocations";
            this.checkBoxUseGroupLocations.Size = new System.Drawing.Size(138, 17);
            this.checkBoxUseGroupLocations.TabIndex = 32;
            this.checkBoxUseGroupLocations.Text = "Use gx and gx positions";
            this.toolTip1.SetToolTip(this.checkBoxUseGroupLocations, resources.GetString("checkBoxUseGroupLocations.ToolTip"));
            this.checkBoxUseGroupLocations.UseVisualStyleBackColor = true;
            this.checkBoxUseGroupLocations.CheckedChanged += new System.EventHandler(this.checkBoxUseGroupLocations_CheckedChanged);
            // 
            // checkBoxCreateMillisecondField
            // 
            this.checkBoxCreateMillisecondField.AutoSize = true;
            this.checkBoxCreateMillisecondField.Location = new System.Drawing.Point(13, 110);
            this.checkBoxCreateMillisecondField.Name = "checkBoxCreateMillisecondField";
            this.checkBoxCreateMillisecondField.Size = new System.Drawing.Size(107, 17);
            this.checkBoxCreateMillisecondField.TabIndex = 34;
            this.checkBoxCreateMillisecondField.Text = "Create msec field";
            this.toolTip1.SetToolTip(this.checkBoxCreateMillisecondField, "Some SEGYs have no msec data; the program will guess at the correct value");
            this.checkBoxCreateMillisecondField.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(102, 135);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(101, 21);
            this.comboBox1.TabIndex = 17;
            this.toolTip1.SetToolTip(this.comboBox1, "Expedition List");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "ASCII Navigation Data";
            this.toolTip1.SetToolTip(this.label9, "Sample Format \r\ndddhhmmss decimal_lat decimal_lon\r\n");
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(49, 113);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "ED Navigation Data";
            this.toolTip1.SetToolTip(this.label12, "Sample Format \r\ndddhhmmss decimal_lat decimal_lon\r\n");
            // 
            // textBoxExpedID
            // 
            this.textBoxExpedID.Location = new System.Drawing.Point(13, 59);
            this.textBoxExpedID.Name = "textBoxExpedID";
            this.textBoxExpedID.Size = new System.Drawing.Size(190, 20);
            this.textBoxExpedID.TabIndex = 22;
            this.textBoxExpedID.Text = "Exped_ID";
            this.toolTip1.SetToolTip(this.textBoxExpedID, "Enter expedtion ID");
            // 
            // numericUpDownExpedYear
            // 
            this.numericUpDownExpedYear.Increment = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericUpDownExpedYear.Location = new System.Drawing.Point(16, 81);
            this.numericUpDownExpedYear.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericUpDownExpedYear.Name = "numericUpDownExpedYear";
            this.numericUpDownExpedYear.Size = new System.Drawing.Size(72, 20);
            this.numericUpDownExpedYear.TabIndex = 23;
            this.toolTip1.SetToolTip(this.numericUpDownExpedYear, "Enter Expedition Year (YYYY)");
            this.numericUpDownExpedYear.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            // 
            // numericUpDownShotSpacingAlongTrack
            // 
            this.numericUpDownShotSpacingAlongTrack.DecimalPlaces = 2;
            this.numericUpDownShotSpacingAlongTrack.Location = new System.Drawing.Point(117, 215);
            this.numericUpDownShotSpacingAlongTrack.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownShotSpacingAlongTrack.Name = "numericUpDownShotSpacingAlongTrack";
            this.numericUpDownShotSpacingAlongTrack.Size = new System.Drawing.Size(76, 20);
            this.numericUpDownShotSpacingAlongTrack.TabIndex = 35;
            this.toolTip1.SetToolTip(this.numericUpDownShotSpacingAlongTrack, "Shot spacing along interpolated track (m)");
            this.numericUpDownShotSpacingAlongTrack.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button12
            // 
            this.button12.Enabled = false;
            this.button12.Location = new System.Drawing.Point(13, 189);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(98, 20);
            this.button12.TabIndex = 36;
            this.button12.Text = "Analyze Outliers";
            this.toolTip1.SetToolTip(this.button12, "Apply Drucker-Prager Line Thinng");
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Visible = false;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button10.Location = new System.Drawing.Point(13, 202);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(190, 23);
            this.button10.TabIndex = 19;
            this.button10.Text = "Merge Nav into SEGY";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.numericUpDownClippingWidth);
            this.panel3.Controls.Add(this.button6);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.numericUpDown2);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(996, 609);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(199, 124);
            this.panel3.TabIndex = 25;
            // 
            // numericUpDownClippingWidth
            // 
            this.numericUpDownClippingWidth.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownClippingWidth.Location = new System.Drawing.Point(102, 36);
            this.numericUpDownClippingWidth.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownClippingWidth.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownClippingWidth.Name = "numericUpDownClippingWidth";
            this.numericUpDownClippingWidth.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownClippingWidth.TabIndex = 28;
            this.numericUpDownClippingWidth.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.numericUpDownClippingWidth.ValueChanged += new System.EventHandler(this.numericUpDownClippingWidth_ValueChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(99, 87);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 27;
            this.button6.Text = "Undo";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(18, 87);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 26;
            this.button5.Text = "Apply";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(99, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Adjust Threshold";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 2;
            this.numericUpDown2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown2.Location = new System.Drawing.Point(18, 62);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(75, 20);
            this.numericUpDown2.TabIndex = 24;
            this.numericUpDown2.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(18, 33);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 23;
            this.button4.Text = "Calculate Clipping Threshold";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(171, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Median Clipping (speed mode only)";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.button12);
            this.panel4.Controls.Add(this.numericUpDownShotSpacingAlongTrack);
            this.panel4.Controls.Add(this.checkBoxCreateMillisecondField);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.checkBoxUseGroupLocations);
            this.panel4.Controls.Add(this.checkBoxFixMsec);
            this.panel4.Controls.Add(this.button11);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.numericUpDownSubsampleTrackMeters);
            this.panel4.Controls.Add(this.button9);
            this.panel4.Controls.Add(this.button7);
            this.panel4.Controls.Add(this.button8);
            this.panel4.Location = new System.Drawing.Point(254, 479);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(208, 247);
            this.panel4.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(45, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Project  SEGY Data";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(66, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "SEGY Data";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.radioButton1);
            this.panel5.Controls.Add(this.numericUpDownExpedYear);
            this.panel5.Controls.Add(this.textBoxExpedID);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.button10);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.comboBox1);
            this.panel5.Controls.Add(this.button3);
            this.panel5.Controls.Add(this.button2);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Location = new System.Drawing.Point(17, 479);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(208, 241);
            this.panel5.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(44, 180);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Merge Nav into SEGY";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.numericUpDownMaxDataSize);
            this.panel6.Controls.Add(this.numericUpDown1);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Location = new System.Drawing.Point(726, 481);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(253, 122);
            this.panel6.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(108, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(140, 13);
            this.label14.TabIndex = 15;
            this.label14.Text = "Set Max Data Segment Size";
            // 
            // numericUpDownMaxDataSize
            // 
            this.numericUpDownMaxDataSize.Location = new System.Drawing.Point(16, 92);
            this.numericUpDownMaxDataSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownMaxDataSize.Name = "numericUpDownMaxDataSize";
            this.numericUpDownMaxDataSize.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownMaxDataSize.TabIndex = 14;
            this.numericUpDownMaxDataSize.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownMaxDataSize.ValueChanged += new System.EventHandler(this.numericUpDownMaxDataSize_ValueChanged);
            // 
            // tChart1
            // 
            // 
            // 
            // 
            this.tChart1.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Axes.Bottom.Title.Caption = "Lon";
            this.tChart1.Axes.Bottom.Title.Lines = new string[] {
        "Lon"};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Axes.Left.Title.Caption = "Lat";
            this.tChart1.Axes.Left.Title.Lines = new string[] {
        "Lat"};
            this.tChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.tChart1.Header.Lines = new string[] {
        "Position",
        ""};
            // 
            // 
            // 
            this.tChart1.Legend.Visible = false;
            this.tChart1.Location = new System.Drawing.Point(0, 0);
            this.tChart1.Name = "tChart1";
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Panel.Brush.Gradient.Visible = false;
            this.tChart1.Panel.MarginLeft = 2D;
            this.tChart1.Series.Add(this.fastLineLatLonBox);
            this.tChart1.Series.Add(this.pointsLatLonBox);
            this.tChart1.Series.Add(this.fastLineOtherLines);
            this.tChart1.Series.Add(this.fastlineLineFit);
            this.tChart1.Series.Add(this.pointsCurrent);
            this.tChart1.Size = new System.Drawing.Size(394, 461);
            this.tChart1.TabIndex = 32;
            this.tChart1.Click += new System.EventHandler(this.tChart1_Click_1);
            this.tChart1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tChart1_KeyPress);
            // 
            // fastLineLatLonBox
            // 
            this.fastLineLatLonBox.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.fastLineLatLonBox.ColorEach = false;
            // 
            // 
            // 
            this.fastLineLatLonBox.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.fastLineLatLonBox.Title = "fastLineLatLonBox";
            this.fastLineLatLonBox.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.Ignore;
            // 
            // 
            // 
            this.fastLineLatLonBox.XValues.DataMember = "X";
            this.fastLineLatLonBox.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLineLatLonBox.YValues.DataMember = "Y";
            // 
            // pointsLatLonBox
            // 
            this.pointsLatLonBox.Color = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pointsLatLonBox.ColorEach = false;
            // 
            // 
            // 
            this.pointsLatLonBox.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.pointsLatLonBox.Pointer.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // 
            // 
            this.pointsLatLonBox.Pointer.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(53)))));
            this.pointsLatLonBox.Pointer.Brush.Gradient.Transparency = 50;
            this.pointsLatLonBox.Pointer.Brush.Visible = false;
            this.pointsLatLonBox.Pointer.Dark3D = false;
            this.pointsLatLonBox.Pointer.Draw3D = false;
            this.pointsLatLonBox.Pointer.HorizSize = 1;
            this.pointsLatLonBox.Pointer.InflateMargins = false;
            // 
            // 
            // 
            this.pointsLatLonBox.Pointer.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pointsLatLonBox.Pointer.SizeDouble = 0D;
            this.pointsLatLonBox.Pointer.SizeUnits = Steema.TeeChart.Styles.PointerSizeUnits.Pixels;
            this.pointsLatLonBox.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.DiagCross;
            this.pointsLatLonBox.Pointer.VertSize = 1;
            this.pointsLatLonBox.Title = "pointsLatLonBox";
            // 
            // 
            // 
            this.pointsLatLonBox.XValues.DataMember = "X";
            this.pointsLatLonBox.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.pointsLatLonBox.YValues.DataMember = "Y";
            // 
            // fastLineOtherLines
            // 
            this.fastLineOtherLines.Color = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(76)))), ((int)(((byte)(20)))));
            this.fastLineOtherLines.ColorEach = false;
            this.fastLineOtherLines.DrawAllPoints = false;
            // 
            // 
            // 
            this.fastLineOtherLines.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.fastLineOtherLines.Title = "fastLineOtherLines";
            this.fastLineOtherLines.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.Ignore;
            // 
            // 
            // 
            this.fastLineOtherLines.XValues.DataMember = "X";
            this.fastLineOtherLines.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastLineOtherLines.YValues.DataMember = "Y";
            // 
            // fastlineLineFit
            // 
            this.fastlineLineFit.Color = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(151)))), ((int)(((byte)(168)))));
            this.fastlineLineFit.ColorEach = false;
            this.fastlineLineFit.DrawAllPoints = false;
            // 
            // 
            // 
            this.fastlineLineFit.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.fastlineLineFit.Title = "fastlineLineFit";
            this.fastlineLineFit.TreatNulls = Steema.TeeChart.Styles.TreatNullsStyle.Ignore;
            // 
            // 
            // 
            this.fastlineLineFit.XValues.DataMember = "X";
            this.fastlineLineFit.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.fastlineLineFit.YValues.DataMember = "Y";
            // 
            // pointsCurrent
            // 
            this.pointsCurrent.Color = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(53)))));
            this.pointsCurrent.ColorEach = false;
            // 
            // 
            // 
            this.pointsCurrent.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.pointsCurrent.Pointer.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(53)))));
            // 
            // 
            // 
            this.pointsCurrent.Pointer.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(53)))));
            this.pointsCurrent.Pointer.Brush.Visible = false;
            this.pointsCurrent.Pointer.Dark3D = false;
            this.pointsCurrent.Pointer.Draw3D = false;
            this.pointsCurrent.Pointer.HorizSize = 3;
            this.pointsCurrent.Pointer.InflateMargins = false;
            // 
            // 
            // 
            this.pointsCurrent.Pointer.Pen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pointsCurrent.Pointer.SizeDouble = 0D;
            this.pointsCurrent.Pointer.SizeUnits = Steema.TeeChart.Styles.PointerSizeUnits.Pixels;
            this.pointsCurrent.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Circle;
            this.pointsCurrent.Pointer.VertSize = 3;
            this.pointsCurrent.Title = "pointsCurrent";
            // 
            // 
            // 
            this.pointsCurrent.XValues.DataMember = "X";
            this.pointsCurrent.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.pointsCurrent.YValues.DataMember = "Y";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 735);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1206, 22);
            this.statusStrip1.TabIndex = 33;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Status";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tChart1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tChart3);
            this.splitContainer1.Size = new System.Drawing.Size(1182, 461);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 34;
            // 
            // oracleCommand1
            // 
            //this.oracleCommand1.Transaction = null;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(102, 81);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(62, 17);
            this.radioButton1.TabIndex = 24;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Re-Split";
            this.toolTip1.SetToolTip(this.radioButton1, "Use this to recombine into 1 line on read and re-split into lines");
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 757);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "NavCleaner";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubsampleTrackMeters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExpedYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShotSpacingAlongTrack)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownClippingWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDataSize)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Steema.TeeChart.TChart tChart3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private Steema.TeeChart.Styles.FastLine fastLineEditBox;
        private Steema.TeeChart.Styles.Points pointsEditBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonVY;
        private System.Windows.Forms.RadioButton radioButtonVX;
        private System.Windows.Forms.RadioButton radioButtonUTMN;
        private System.Windows.Forms.RadioButton radioButtonUTME;
        private System.Windows.Forms.RadioButton radioButtonLat;
        private System.Windows.Forms.RadioButton radioButtonLon;
        private System.Windows.Forms.Label label4;
        private Steema.TeeChart.Editor editor1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonUNDO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButtonDEL;
        private Steema.TeeChart.Tools.NearestPoint nearestPoint1;
        private Steema.TeeChart.Tools.NearestPoint nearestPoint2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RadioButton radioButtonV;
        private System.Windows.Forms.ComboBox comboBox1;
        //private Oracle.DataAccess.Client.OracleCommand oracleCommand1;
        //private Oracle.DataAccess.Client.OracleDataAdapter oracleDataAdapter1;
        //private Oracle.DataAccess.Client.OracleConnection oracleConnection1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown numericUpDownClippingWidth;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label8;
        private Steema.TeeChart.Styles.FastLine fastLine1;
        private Steema.TeeChart.Styles.FastLine fastLine2;
        private Steema.TeeChart.Styles.FastLine fastLine3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.NumericUpDown numericUpDownSubsampleTrackMeters;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.CheckBox checkBoxUseGroupLocations;
        private System.Windows.Forms.CheckBox checkBoxFixMsec;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private Steema.TeeChart.TChart tChart1;
        private System.Windows.Forms.CheckBox checkBoxCreateMillisecondField;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Steema.TeeChart.Styles.FastLine fastLineLatLonBox;
        private Steema.TeeChart.Styles.Points pointsLatLonBox;
        private Steema.TeeChart.Styles.FastLine fastLineOtherLines;
        private Steema.TeeChart.Styles.FastLine fastlineLineFit;
        private Steema.TeeChart.Styles.Points pointsCurrent;
        private System.Windows.Forms.TextBox textBoxExpedID;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDownExpedYear;
        private System.Windows.Forms.NumericUpDown numericUpDownShotSpacingAlongTrack;
        //private Oracle.ManagedDataAccess.Client.OracleConnection oracleConnection1;
        //private Oracle.ManagedDataAccess.Client.OracleDataAdapter oracleDataAdapter1;
        //private Oracle.ManagedDataAccess.Client.OracleCommand oracleCommand1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxDataSize;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

