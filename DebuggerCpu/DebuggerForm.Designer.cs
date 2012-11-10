namespace DebuggerCpu
{
    partial class DebuggerForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PCRegLabel = new System.Windows.Forms.Label();
            this.JRegLabel = new System.Windows.Forms.Label();
            this.IRegLabel = new System.Windows.Forms.Label();
            this.ZRegLabel = new System.Windows.Forms.Label();
            this.YRegLabel = new System.Windows.Forms.Label();
            this.XRegLabel = new System.Windows.Forms.Label();
            this.CRegLabel = new System.Windows.Forms.Label();
            this.BRegLabel = new System.Windows.Forms.Label();
            this.ARegLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ResetBut = new System.Windows.Forms.Button();
            this.RunBut = new System.Windows.Forms.Button();
            this.StepBut = new System.Windows.Forms.Button();
            this.actionBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PCRegLabel);
            this.groupBox1.Controls.Add(this.JRegLabel);
            this.groupBox1.Controls.Add(this.IRegLabel);
            this.groupBox1.Controls.Add(this.ZRegLabel);
            this.groupBox1.Controls.Add(this.YRegLabel);
            this.groupBox1.Controls.Add(this.XRegLabel);
            this.groupBox1.Controls.Add(this.CRegLabel);
            this.groupBox1.Controls.Add(this.BRegLabel);
            this.groupBox1.Controls.Add(this.ARegLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(121, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registers";
            // 
            // PCRegLabel
            // 
            this.PCRegLabel.AutoSize = true;
            this.PCRegLabel.Location = new System.Drawing.Point(6, 70);
            this.PCRegLabel.Name = "PCRegLabel";
            this.PCRegLabel.Size = new System.Drawing.Size(39, 13);
            this.PCRegLabel.TabIndex = 8;
            this.PCRegLabel.Text = "PC = 0";
            // 
            // JRegLabel
            // 
            this.JRegLabel.AutoSize = true;
            this.JRegLabel.Location = new System.Drawing.Point(82, 29);
            this.JRegLabel.Name = "JRegLabel";
            this.JRegLabel.Size = new System.Drawing.Size(30, 13);
            this.JRegLabel.TabIndex = 7;
            this.JRegLabel.Text = "J = 0";
            // 
            // IRegLabel
            // 
            this.IRegLabel.AutoSize = true;
            this.IRegLabel.Location = new System.Drawing.Point(82, 16);
            this.IRegLabel.Name = "IRegLabel";
            this.IRegLabel.Size = new System.Drawing.Size(28, 13);
            this.IRegLabel.TabIndex = 6;
            this.IRegLabel.Text = "I = 0";
            // 
            // ZRegLabel
            // 
            this.ZRegLabel.AutoSize = true;
            this.ZRegLabel.Location = new System.Drawing.Point(44, 42);
            this.ZRegLabel.Name = "ZRegLabel";
            this.ZRegLabel.Size = new System.Drawing.Size(32, 13);
            this.ZRegLabel.TabIndex = 5;
            this.ZRegLabel.Text = "Z = 0";
            // 
            // YRegLabel
            // 
            this.YRegLabel.AutoSize = true;
            this.YRegLabel.Location = new System.Drawing.Point(44, 29);
            this.YRegLabel.Name = "YRegLabel";
            this.YRegLabel.Size = new System.Drawing.Size(32, 13);
            this.YRegLabel.TabIndex = 4;
            this.YRegLabel.Text = "Y = 0";
            // 
            // XRegLabel
            // 
            this.XRegLabel.AutoSize = true;
            this.XRegLabel.Location = new System.Drawing.Point(44, 16);
            this.XRegLabel.Name = "XRegLabel";
            this.XRegLabel.Size = new System.Drawing.Size(32, 13);
            this.XRegLabel.TabIndex = 3;
            this.XRegLabel.Text = "X = 0";
            // 
            // CRegLabel
            // 
            this.CRegLabel.AutoSize = true;
            this.CRegLabel.Location = new System.Drawing.Point(6, 42);
            this.CRegLabel.Name = "CRegLabel";
            this.CRegLabel.Size = new System.Drawing.Size(32, 13);
            this.CRegLabel.TabIndex = 2;
            this.CRegLabel.Text = "C = 0";
            // 
            // BRegLabel
            // 
            this.BRegLabel.AutoSize = true;
            this.BRegLabel.Location = new System.Drawing.Point(6, 29);
            this.BRegLabel.Name = "BRegLabel";
            this.BRegLabel.Size = new System.Drawing.Size(32, 13);
            this.BRegLabel.TabIndex = 1;
            this.BRegLabel.Text = "B = 0";
            // 
            // ARegLabel
            // 
            this.ARegLabel.AutoSize = true;
            this.ARegLabel.Location = new System.Drawing.Point(6, 16);
            this.ARegLabel.Name = "ARegLabel";
            this.ARegLabel.Size = new System.Drawing.Size(32, 13);
            this.ARegLabel.TabIndex = 0;
            this.ARegLabel.Text = "A = 0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ResetBut);
            this.groupBox2.Controls.Add(this.RunBut);
            this.groupBox2.Controls.Add(this.StepBut);
            this.groupBox2.Location = new System.Drawing.Point(12, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(145, 83);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // ResetBut
            // 
            this.ResetBut.Location = new System.Drawing.Point(6, 48);
            this.ResetBut.Name = "ResetBut";
            this.ResetBut.Size = new System.Drawing.Size(128, 23);
            this.ResetBut.TabIndex = 2;
            this.ResetBut.Text = "Reset";
            this.ResetBut.UseVisualStyleBackColor = true;
            this.ResetBut.Click += new System.EventHandler(this.ResetBut_Click);
            // 
            // RunBut
            // 
            this.RunBut.Location = new System.Drawing.Point(59, 19);
            this.RunBut.Name = "RunBut";
            this.RunBut.Size = new System.Drawing.Size(75, 23);
            this.RunBut.TabIndex = 1;
            this.RunBut.Text = "Run";
            this.RunBut.UseVisualStyleBackColor = true;
            // 
            // StepBut
            // 
            this.StepBut.Location = new System.Drawing.Point(6, 19);
            this.StepBut.Name = "StepBut";
            this.StepBut.Size = new System.Drawing.Size(47, 23);
            this.StepBut.TabIndex = 0;
            this.StepBut.Text = "Step";
            this.StepBut.UseVisualStyleBackColor = true;
            this.StepBut.Click += new System.EventHandler(this.StepBut_Click);
            // 
            // actionBox
            // 
            this.actionBox.FormattingEnabled = true;
            this.actionBox.Location = new System.Drawing.Point(190, 12);
            this.actionBox.Name = "actionBox";
            this.actionBox.Size = new System.Drawing.Size(314, 225);
            this.actionBox.TabIndex = 2;
            // 
            // DebuggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 254);
            this.Controls.Add(this.actionBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DebuggerForm";
            this.Text = "DebuggerForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label ARegLabel;
        private System.Windows.Forms.Label BRegLabel;
        private System.Windows.Forms.Label PCRegLabel;
        private System.Windows.Forms.Label JRegLabel;
        private System.Windows.Forms.Label IRegLabel;
        private System.Windows.Forms.Label ZRegLabel;
        private System.Windows.Forms.Label YRegLabel;
        private System.Windows.Forms.Label XRegLabel;
        private System.Windows.Forms.Label CRegLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ResetBut;
        private System.Windows.Forms.Button RunBut;
        private System.Windows.Forms.Button StepBut;
        public System.Windows.Forms.ListBox actionBox;
    }
}