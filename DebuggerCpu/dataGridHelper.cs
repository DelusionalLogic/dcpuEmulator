using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebuggerCpu
{
    class dataGridHelper
    {
        private static string[] cellVal;

        public static void showChanged(DataGridView dataGrid)
        {
            if(cellVal == null)
                cellVal = new string[dataGrid.RowCount * dataGrid.ColumnCount];
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                DataGridViewRow row = dataGrid.Rows[i];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    DataGridViewCell cell = row.Cells[j];
                    int index = i * row.Cells.Count + j;
                    if (cellVal[index] != (string)cell.Value)
                    {
                        cell.Style.BackColor = Color.Red;
                    }
                    else
                        cell.Style.BackColor = row.DefaultCellStyle.BackColor;
                    cellVal[index] = (string)cell.Value;
                }
            }
        }
    }
}
