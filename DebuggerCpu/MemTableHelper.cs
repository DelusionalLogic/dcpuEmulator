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

        public static void updateTables(DataGridView registerTable, DataGridView memoryTable, Cpu cpu, Main comInterface)
        {
            registerTable.DataSource = null;
            registerTable.RowTemplate.Height = 16;
            registerTable.DataSource = TableFactory.getRegisterTable(cpu);

            dataGridHelper.showChanged(registerTable);

            memoryTable.DataSource = null;
            memoryTable.RowTemplate.Height = 16;
            memoryTable.DataSource = TableFactory.getMemoryTable(comInterface.Host);

            int curRow = (int) Math.Floor((double) (cpu.PC/0x10)), curCell = (cpu.PC%0x10) + 1;

            if (memoryTable.ColumnCount == 0x10 + 1)
            {
                memoryTable.CurrentCell = memoryTable.Rows[curRow].Cells[curCell];
                memoryTable.ClearSelection();
            }
            memoryTable.Rows[curRow].Cells[curCell].Style.BackColor = Color.Yellow;
            if (lastCell != null) lastCell.Style = lastRow.DefaultCellStyle;
            lastRow = memoryTable.Rows[curRow];
            lastCell = lastRow.Cells[curCell];
        }
    }
}
