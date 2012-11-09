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

        private static PluginHandler pluginHandler;

        [STAThread]
        static void Main(string[] args)
        {
            pluginHandler = new PluginHandler();

            AdvConsole.Log("Opening SettingsForm"); 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SettingsForm settingsForm = new SettingsForm(pluginHandler);
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
