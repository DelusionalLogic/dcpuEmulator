namespace dcpuEmulator
{
    partial class SettingsForm
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
            this.filePathBox = new System.Windows.Forms.TextBox();
            this.browseBut = new System.Windows.Forms.Button();
            this.openBinDia = new System.Windows.Forms.OpenFileDialog();
            this.okBut = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.configCpuBut = new System.Windows.Forms.Button();
            this.cpuBox = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.configRamBut = new System.Windows.Forms.Button();
            this.ramBox = new System.Windows.Forms.ComboBox();
            this.hardwareBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.configTimerBut = new System.Windows.Forms.Button();
            this.timerBox = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // filePathBox
            // 
            this.filePathBox.Location = new System.Drawing.Point(12, 12);
            this.filePathBox.Name = "filePathBox";
            this.filePathBox.Size = new System.Drawing.Size(407, 20);
            this.filePathBox.TabIndex = 1;
            // 
            // browseBut
            // 
            this.browseBut.Location = new System.Drawing.Point(425, 12);
            this.browseBut.Name = "browseBut";
            this.browseBut.Size = new System.Drawing.Size(75, 20);
            this.browseBut.TabIndex = 2;
            this.browseBut.Text = "Browse...";
            this.browseBut.UseVisualStyleBackColor = true;
            this.browseBut.Click += new System.EventHandler(this.browseBut_Click);
            // 
            // openBinDia
            // 
            this.openBinDia.FileName = "program.bin";
            this.openBinDia.Filter = "Binary|*.bin";
            this.openBinDia.Title = "Please select binary";
            // 
            // okBut
            // 
            this.okBut.Location = new System.Drawing.Point(425, 404);
            this.okBut.Name = "okBut";
            this.okBut.Size = new System.Drawing.Size(75, 23);
            this.okBut.TabIndex = 3;
            this.okBut.Text = "OK";
            this.okBut.UseVisualStyleBackColor = true;
            this.okBut.Click += new System.EventHandler(this.okBut_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.configCpuBut);
            this.groupBox2.Controls.Add(this.cpuBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(488, 49);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cpu";
            // 
            // configCpuBut
            // 
            this.configCpuBut.Location = new System.Drawing.Point(413, 19);
            this.configCpuBut.Name = "configCpuBut";
            this.configCpuBut.Size = new System.Drawing.Size(69, 21);
            this.configCpuBut.TabIndex = 6;
            this.configCpuBut.Text = "Config...";
            this.configCpuBut.UseVisualStyleBackColor = true;
            // 
            // cpuBox
            // 
            this.cpuBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cpuBox.FormattingEnabled = true;
            this.cpuBox.Location = new System.Drawing.Point(6, 19);
            this.cpuBox.Name = "cpuBox";
            this.cpuBox.Size = new System.Drawing.Size(401, 21);
            this.cpuBox.TabIndex = 4;
            this.cpuBox.SelectedIndexChanged += new System.EventHandler(this.cpuBox_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.configRamBut);
            this.groupBox3.Controls.Add(this.ramBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(488, 49);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ram";
            // 
            // configRamBut
            // 
            this.configRamBut.Location = new System.Drawing.Point(413, 18);
            this.configRamBut.Name = "configRamBut";
            this.configRamBut.Size = new System.Drawing.Size(69, 21);
            this.configRamBut.TabIndex = 7;
            this.configRamBut.Text = "Config...";
            this.configRamBut.UseVisualStyleBackColor = true;
            // 
            // ramBox
            // 
            this.ramBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ramBox.FormattingEnabled = true;
            this.ramBox.Location = new System.Drawing.Point(6, 19);
            this.ramBox.Name = "ramBox";
            this.ramBox.Size = new System.Drawing.Size(401, 21);
            this.ramBox.TabIndex = 4;
            this.ramBox.SelectedIndexChanged += new System.EventHandler(this.ramBox_SelectedIndexChanged);
            // 
            // hardwareBox
            // 
            this.hardwareBox.FormattingEnabled = true;
            this.hardwareBox.Location = new System.Drawing.Point(12, 203);
            this.hardwareBox.Name = "hardwareBox";
            this.hardwareBox.Size = new System.Drawing.Size(488, 169);
            this.hardwareBox.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.configTimerBut);
            this.groupBox1.Controls.Add(this.timerBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 49);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timer";
            // 
            // configTimerBut
            // 
            this.configTimerBut.Location = new System.Drawing.Point(413, 19);
            this.configTimerBut.Name = "configTimerBut";
            this.configTimerBut.Size = new System.Drawing.Size(69, 21);
            this.configTimerBut.TabIndex = 6;
            this.configTimerBut.Text = "Config...";
            this.configTimerBut.UseVisualStyleBackColor = true;
            // 
            // timerBox
            // 
            this.timerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timerBox.FormattingEnabled = true;
            this.timerBox.Location = new System.Drawing.Point(6, 19);
            this.timerBox.Name = "timerBox";
            this.timerBox.Size = new System.Drawing.Size(401, 21);
            this.timerBox.TabIndex = 4;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 439);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.hardwareBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.okBut);
            this.Controls.Add(this.browseBut);
            this.Controls.Add(this.filePathBox);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filePathBox;
        private System.Windows.Forms.Button browseBut;
        private System.Windows.Forms.OpenFileDialog openBinDia;
        private System.Windows.Forms.Button okBut;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cpuBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox ramBox;
        private System.Windows.Forms.Button configCpuBut;
        private System.Windows.Forms.Button configRamBut;
        private System.Windows.Forms.CheckedListBox hardwareBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button configTimerBut;
        private System.Windows.Forms.ComboBox timerBox;
    }
}