using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebuggerCpu
{
    class MemTableHelper
    {
        public static DataGridViewRow lastRow;
        public static DataGridViewCell lastCell;

        public static void updateTables(DataGridView registerTable, DataGridView memoryTable, Cpu cpu)
        {
            registerTable.DataSource = null;
            registerTable.DataSource = TableFactory.getRegisterTable(cpu);

            dataGridHelper.showChanged(registerTable);
            if (memoryTable.ColumnCount == 0xF + 2)
            {
                memoryTable.CurrentCell = memoryTable.Rows[(int)Math.Floor((double)(cpu.PC / 0x10))].Cells[(cpu.PC % 0x10) + 1];
                memoryTable.ClearSelection();
            }
            memoryTable.Rows[(int)Math.Floor((double)(cpu.PC / 0x10))].Cells[(cpu.PC % 0x10) + 1].Style.BackColor = Color.Yellow;
            if (lastCell != null) lastCell.Style = lastRow.DefaultCellStyle;
            lastRow = memoryTable.Rows[(int) Math.Floor((double) (cpu.PC/0x10))];
            lastCell = lastRow.Cells[(cpu.PC % 0x10) + 1];
        }
    }
}
