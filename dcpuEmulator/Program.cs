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
        private static Computer computer;

        [STAThread]
        static void Main(string[] args)
        {
            AdvConsole.Log("Opening SettingsForm"); 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.setupComplete += setupComplete;
            Application.Run(settingsForm);
            AdvConsole.ReadLine();
        }

        public static void setupComplete(object sender, EventArgs e)
        {
            AdvConsole.Log("Setting dialog done");
            AdvConsole.Log(string.Format("Binary path: {0}", ((SettingsForm)sender).filePath));
            //TODO: Add computer init 
        }
    }
}
