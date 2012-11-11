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
            registerTable.DataSource = TableFactory.getRegisterTable(cpu);
            dataGridHelper.showChanged(registerTable);
            updateMemBut.PerformClick();
        }

        private void StepBut_Click(object sender, EventArgs e)
        {
            cpu.tick();

            registerTable.DataSource = null;
            registerTable.DataSource = TableFactory.getRegisterTable(cpu);

            dataGridHelper.showChanged(registerTable);
            memoryTable.ClearSelection();
            if (memoryTable.ColumnCount == 0xF + 2)
                memoryTable.Rows[(int)Math.Floor((double)(cpu.PC / 0xF))].Cells[cpu.PC % 0xF].Selected = true;
        }

        private void ResetBut_Click(object sender, EventArgs e)
        {
            cpu.reset();

            registerTable.DataSource = null;
            registerTable.DataSource = TableFactory.getRegisterTable(cpu);

            dataGridHelper.showChanged(registerTable);
            memoryTable.ClearSelection();
            if (memoryTable.ColumnCount == 0xF + 2)
                memoryTable.Rows[(int)Math.Floor((double)(cpu.PC / 0xF))].Cells[cpu.PC % 0xF].Selected = true;
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
            memoryTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            memoryTable.ClearSelection();
            memoryTable.Rows[(int)Math.Floor((double)(cpu.PC / 0xF))].Cells[cpu.PC % 0xF].Selected = true;
        }
    }
}
