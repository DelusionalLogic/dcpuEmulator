using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace DebuggerCpu
{
    class TableFactory
    {
        public static DataTable getRegisterTable(Cpu cpu)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Register");
            table.Columns.Add("Hex Value");
            table.Columns.Add("Value");

            string[] registers =
                new[]{
                    "A",
                    "B",
                    "C",
                    "X",
                    "Y",
                    "Z",
                    "I",
                    "J",
                };

            for (int i = 0; i < registers.Length; i++)
            {
                string register = registers[i];
                Register reg;
                Enum.TryParse(register, false, out reg);
                ushort value = cpu.register[(int) reg];

                table.Rows.Add();
                table.Rows[i][0] = string.Format("{0}", register);
                table.Rows[i][1] = string.Format("0x{0:X4}", value);
                table.Rows[i][2] = string.Format("{0}", value);
            }

            table.Rows.Add();
            table.Rows[registers.Length][0] = string.Format("{0}", "PC");
            table.Rows[registers.Length][1] = string.Format("0x{0:X4}", cpu.PC);
            table.Rows[registers.Length][2] = string.Format("{0}", cpu.PC);
            return table;
        }

        public static DataTable getMemoryTable(IPluginHost host)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Addresses");
            for (int i = 1; i < 0xF + 2; i++)
            {
                table.Columns.Add(string.Format("0x{0:X1}", i - 1));
            }
            for (int i = 0; i < 0x10000; i += 0xF)
            {
                var row = new string[0xF + 2];
                row[0] = string.Format("0x{0:X4}", i);
                for (int j = 0; j < 0xF + 1; j++)
                {
                    string value = string.Format("{0:X4}", host.readMem(i + j));
                    row[j + 1] = value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
