using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebuggerCpu
{
    public partial class DebuggerForm : Form
    {
        private Main comInterface;
        private Cpu cpu;

        public DebuggerForm(Main comInterface)
        {
            this.comInterface = comInterface;
            InitializeComponent();
        }

        private void DebuggerForm_Load(object sender, EventArgs e)
        {
            cpu = new Cpu(comInterface.Host);
            registerTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            registerTable.DataSource = TableFactory.getRegisterTable(cpu);
            registerTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridHelper.showChanged(registerTable);
            updateMemBut.PerformClick();
        }

        private void StepBut_Click(object sender, EventArgs e)
        {
            cpu.tick();
            MemTableHelper.updateTables(registerTable, memoryTable, cpu);
        }

        private void ResetBut_Click(object sender, EventArgs e)
        {
            cpu.reset();
            MemTableHelper.updateTables(registerTable, memoryTable, cpu);
        }

        private void RunBut_Click(object sender, EventArgs e)
        {
            cpu.run();
        }

        private void updateMemBut_Click(object sender, EventArgs e)
        {
            memoryTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            memoryTable.DataSource = null;
            memoryTable.DataSource = TableFactory.getMemoryTable(comInterface.Host);
            memoryTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            memoryTable.ClearSelection();
            memoryTable.Rows[(int)Math.Floor((double)(cpu.PC / 0xF))].Cells[cpu.PC % 0xF].Selected = true;
        }

        private void registerTable_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            ((DataGridView)sender).AutoResizeRow(e.RowIndex);
        }

        private void memoryTable_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            ((DataGridView)sender).AutoResizeRow(e.RowIndex);
        }
    }
}
