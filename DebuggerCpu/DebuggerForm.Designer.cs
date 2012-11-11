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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.updateMemBut = new System.Windows.Forms.Button();
            this.ResetBut = new System.Windows.Forms.Button();
            this.RunBut = new System.Windows.Forms.Button();
            this.StepBut = new System.Windows.Forms.Button();
            this.registerTable = new System.Windows.Forms.DataGridView();
            this.memoryTable = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registerTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoryTable)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.updateMemBut);
            this.groupBox2.Controls.Add(this.ResetBut);
            this.groupBox2.Controls.Add(this.RunBut);
            this.groupBox2.Controls.Add(this.StepBut);
            this.groupBox2.Location = new System.Drawing.Point(825, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(145, 83);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // updateMemBut
            // 
            this.updateMemBut.Location = new System.Drawing.Point(87, 48);
            this.updateMemBut.Name = "updateMemBut";
            this.updateMemBut.Size = new System.Drawing.Size(47, 23);
            this.updateMemBut.TabIndex = 3;
            this.updateMemBut.Text = "Mem";
            this.updateMemBut.UseVisualStyleBackColor = true;
            this.updateMemBut.Click += new System.EventHandler(this.updateMemBut_Click);
            // 
            // ResetBut
            // 
            this.ResetBut.Location = new System.Drawing.Point(6, 48);
            this.ResetBut.Name = "ResetBut";
            this.ResetBut.Size = new System.Drawing.Size(75, 23);
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
            this.RunBut.Click += new System.EventHandler(this.RunBut_Click);
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
            // registerTable
            // 
            this.registerTable.AllowUserToAddRows = false;
            this.registerTable.AllowUserToDeleteRows = false;
            this.registerTable.AllowUserToResizeColumns = false;
            this.registerTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.registerTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.registerTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.registerTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.registerTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.registerTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.registerTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.registerTable.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lucida Console", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.registerTable.DefaultCellStyle = dataGridViewCellStyle2;
            this.registerTable.Location = new System.Drawing.Point(825, 12);
            this.registerTable.Name = "registerTable";
            this.registerTable.ReadOnly = true;
            this.registerTable.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.registerTable.RowHeadersVisible = false;
            this.registerTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.registerTable.Size = new System.Drawing.Size(266, 181);
            this.registerTable.TabIndex = 0;
            // 
            // memoryTable
            // 
            this.memoryTable.AllowUserToAddRows = false;
            this.memoryTable.AllowUserToDeleteRows = false;
            this.memoryTable.AllowUserToResizeColumns = false;
            this.memoryTable.AllowUserToResizeRows = false;
            this.memoryTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.memoryTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Lucida Console", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.memoryTable.DefaultCellStyle = dataGridViewCellStyle3;
            this.memoryTable.Location = new System.Drawing.Point(12, 12);
            this.memoryTable.Name = "memoryTable";
            this.memoryTable.RowHeadersVisible = false;
            this.memoryTable.Size = new System.Drawing.Size(807, 605);
            this.memoryTable.TabIndex = 2;
            // 
            // DebuggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 629);
            this.Controls.Add(this.memoryTable);
            this.Controls.Add(this.registerTable);
            this.Controls.Add(this.groupBox2);
            this.Name = "DebuggerForm";
            this.Text = "DebuggerForm";
            this.Load += new System.EventHandler(this.DebuggerForm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.registerTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoryTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ResetBut;
        private System.Windows.Forms.Button RunBut;
        private System.Windows.Forms.Button StepBut;
        private System.Windows.Forms.DataGridView registerTable;
        private System.Windows.Forms.DataGridView memoryTable;
        private System.Windows.Forms.Button updateMemBut;
    }
}