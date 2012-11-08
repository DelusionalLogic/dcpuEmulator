using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dcpuEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            AdvConsole.Log("Opening MainForm"); 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            AdvConsole.ReadLine();
        }
    }
}
