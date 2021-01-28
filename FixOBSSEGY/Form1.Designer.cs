namespace Demultiplex
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxsxrx = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBoxSwitch = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBoxOffset = new System.Windows.Forms.CheckBox();
            this.checkBoxlldec = new System.Windows.Forms.CheckBox();
            this.checkBoxllsa = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open SEGY Files";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(176, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Process Next";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(17, 230);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(633, 134);
            this.listBox1.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // checkBoxsxrx
            // 
            this.checkBoxsxrx.AutoSize = true;
            this.checkBoxsxrx.Location = new System.Drawing.Point(29, 83);
            this.checkBoxsxrx.Name = "checkBoxsxrx";
            this.checkBoxsxrx.Size = new System.Drawing.Size(90, 17);
            this.checkBoxsxrx.TabIndex = 3;
            this.checkBoxsxrx.Text = "Swap RX/SX";
            this.checkBoxsxrx.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(329, 34);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Process All";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // checkBoxSwitch
            // 
            this.checkBoxSwitch.AutoSize = true;
            this.checkBoxSwitch.Location = new System.Drawing.Point(158, 83);
            this.checkBoxSwitch.Name = "checkBoxSwitch";
            this.checkBoxSwitch.Size = new System.Drawing.Size(114, 17);
            this.checkBoxSwitch.TabIndex = 5;
            this.checkBoxSwitch.Text = "Swap Lat with Lon";
            this.checkBoxSwitch.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(310, 83);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(126, 17);
            this.checkBox2.TabIndex = 6;
            this.checkBox2.Text = "Set Position Multiplier";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(442, 81);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "-1000";
            // 
            // checkBoxOffset
            // 
            this.checkBoxOffset.AutoSize = true;
            this.checkBoxOffset.Location = new System.Drawing.Point(29, 131);
            this.checkBoxOffset.Name = "checkBoxOffset";
            this.checkBoxOffset.Size = new System.Drawing.Size(101, 17);
            this.checkBoxOffset.TabIndex = 8;
            this.checkBoxOffset.Text = "Calculate Offset";
            this.checkBoxOffset.UseVisualStyleBackColor = true;
            // 
            // checkBoxlldec
            // 
            this.checkBoxlldec.AutoSize = true;
            this.checkBoxlldec.Location = new System.Drawing.Point(158, 131);
            this.checkBoxlldec.Name = "checkBoxlldec";
            this.checkBoxlldec.Size = new System.Drawing.Size(132, 17);
            this.checkBoxlldec.TabIndex = 9;
            this.checkBoxlldec.Text = "Lat/Lon Decimal Units";
            this.checkBoxlldec.UseVisualStyleBackColor = true;
            // 
            // checkBoxllsa
            // 
            this.checkBoxllsa.AutoSize = true;
            this.checkBoxllsa.Location = new System.Drawing.Point(304, 131);
            this.checkBoxllsa.Name = "checkBoxllsa";
            this.checkBoxllsa.Size = new System.Drawing.Size(140, 17);
            this.checkBoxllsa.TabIndex = 10;
            this.checkBoxllsa.Text = "Lat/Lon Seconds of Arc";
            this.checkBoxllsa.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 398);
            this.Controls.Add(this.checkBoxllsa);
            this.Controls.Add(this.checkBoxlldec);
            this.Controls.Add(this.checkBoxOffset);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBoxSwitch);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.checkBoxsxrx);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Fix OBS SEGY Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox checkBoxsxrx;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBoxSwitch;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBoxOffset;
        private System.Windows.Forms.CheckBox checkBoxlldec;
        private System.Windows.Forms.CheckBox checkBoxllsa;
    }
}

