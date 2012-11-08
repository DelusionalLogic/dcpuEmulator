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
            this.label1 = new System.Windows.Forms.Label();
            this.filePathBox = new System.Windows.Forms.TextBox();
            this.browseBut = new System.Windows.Forms.Button();
            this.openBinDia = new System.Windows.Forms.OpenFileDialog();
            this.okBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This will do something at some point!";
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
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 439);
            this.Controls.Add(this.okBut);
            this.Controls.Add(this.browseBut);
            this.Controls.Add(this.filePathBox);
            this.Controls.Add(this.label1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filePathBox;
        private System.Windows.Forms.Button browseBut;
        private System.Windows.Forms.OpenFileDialog openBinDia;
        private System.Windows.Forms.Button okBut;
    }
}