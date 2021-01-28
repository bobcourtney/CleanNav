namespace NavCleaner
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.tChart1 = new Steema.TeeChart.TChart();
            this.points1 = new Steema.TeeChart.Styles.Points();
            this.tChart2 = new Steema.TeeChart.TChart();
            this.points2 = new Steema.TeeChart.Styles.Points();
            this.SuspendLayout();
            // 
            // tChart1
            // 
            this.tChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.tChart1.Axes.Bottom.Logarithmic = true;
            // 
            // 
            // 
            this.tChart1.Axes.Bottom.Title.Caption = "Cross-Track Tolerance (m)";
            this.tChart1.Axes.Bottom.Title.Lines = new string[] {
        "Cross-Track Tolerance (m)"};
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart1.Axes.Left.Labels.Font.Size = 6;
            this.tChart1.Axes.Left.Labels.Font.SizeFloat = 6F;
            this.tChart1.Axes.Left.Labels.ValueFormat = "0.00000";
            // 
            // 
            // 
            this.tChart1.Axes.Left.Title.Caption = "Length Ratio - 1 (%)";
            this.tChart1.Axes.Left.Title.Lines = new string[] {
        "Length Ratio - 1 (%)"};
            // 
            // 
            // 
            this.tChart1.Header.Lines = new string[] {
        ""};
            // 
            // 
            // 
            this.tChart1.Legend.Visible = false;
            this.tChart1.Location = new System.Drawing.Point(0, 2);
            this.tChart1.Name = "tChart1";
            this.tChart1.Series.Add(this.points1);
            this.tChart1.Size = new System.Drawing.Size(893, 276);
            this.tChart1.TabIndex = 0;
            this.tChart1.Click += new System.EventHandler(this.tChart1_Click);
            // 
            // points1
            // 
            this.points1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.points1.ColorEach = false;
            // 
            // 
            // 
            this.points1.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(61)))), ((int)(((byte)(98)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.points1.Pointer.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            // 
            // 
            // 
            this.points1.Pointer.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.points1.Pointer.Draw3D = false;
            this.points1.Pointer.HorizSize = 2;
            this.points1.Pointer.SizeDouble = 0D;
            this.points1.Pointer.SizeUnits = Steema.TeeChart.Styles.PointerSizeUnits.Pixels;
            this.points1.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.Cross;
            this.points1.Pointer.VertSize = 2;
            this.points1.Title = "points1";
            // 
            // 
            // 
            this.points1.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // tChart2
            // 
            this.tChart2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.tChart2.Aspect.View3D = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart2.Axes.Bottom.Logarithmic = true;
            // 
            // 
            // 
            this.tChart2.Axes.Bottom.Title.Caption = "Cross-trace Tolerance (m)";
            this.tChart2.Axes.Bottom.Title.Lines = new string[] {
        "Cross-trace Tolerance (m)"};
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart2.Axes.Left.Labels.Font.Size = 6;
            this.tChart2.Axes.Left.Labels.Font.SizeFloat = 6F;
            this.tChart2.Axes.Left.Labels.ValueFormat = ".0000";
            // 
            // 
            // 
            this.tChart2.Axes.Left.Title.Caption = "Cost Function";
            this.tChart2.Axes.Left.Title.Lines = new string[] {
        "Cost Function"};
            this.tChart2.Axes.Left.Title.TextAlign = System.Drawing.StringAlignment.Far;
            this.tChart2.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // 
            // 
            this.tChart2.Header.Lines = new string[] {
        ""};
            // 
            // 
            // 
            this.tChart2.Legend.Visible = false;
            this.tChart2.Location = new System.Drawing.Point(0, 284);
            this.tChart2.Name = "tChart2";
            this.tChart2.Series.Add(this.points2);
            this.tChart2.Size = new System.Drawing.Size(893, 250);
            this.tChart2.TabIndex = 1;
            this.tChart2.Click += new System.EventHandler(this.tChart2_Click);
            // 
            // points2
            // 
            this.points2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.points2.ColorEach = false;
            // 
            // 
            // 
            this.points2.LinePen.Color = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(61)))), ((int)(((byte)(98)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.points2.Pointer.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            // 
            // 
            // 
            this.points2.Pointer.Brush.Gradient.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(102)))), ((int)(((byte)(163)))));
            this.points2.Pointer.Draw3D = false;
            this.points2.Pointer.HorizSize = 2;
            this.points2.Pointer.SizeDouble = 0D;
            this.points2.Pointer.SizeUnits = Steema.TeeChart.Styles.PointerSizeUnits.Pixels;
            this.points2.Pointer.Style = Steema.TeeChart.Styles.PointerStyles.DiagCross;
            this.points2.Pointer.VertSize = 2;
            this.points2.Title = "points1";
            // 
            // 
            // 
            this.points2.XValues.DataMember = "X";
            this.points2.XValues.Order = Steema.TeeChart.Styles.ValueListOrder.Ascending;
            // 
            // 
            // 
            this.points2.YValues.DataMember = "Y";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 546);
            this.Controls.Add(this.tChart2);
            this.Controls.Add(this.tChart1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Steema.TeeChart.TChart tChart1;
        private Steema.TeeChart.Styles.Points points1;
        private Steema.TeeChart.TChart tChart2;
        private Steema.TeeChart.Styles.Points points2;
    }
}