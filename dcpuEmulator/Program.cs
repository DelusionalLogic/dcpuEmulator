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
            computer = new Computer();
            pluginHandler = new PluginHandler(computer);

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
            SettingsForm settings = (SettingsForm) sender;
            AdvConsole.Log("Setting dialog complete");
            AdvConsole.Log(string.Format("Binary path: {0}", settings.filePath));
            AdvConsole.Log(string.Format("Screen Loaded: {0}", settings.selectedScreen.Name));
            AdvConsole.Log(string.Format("Cpu Loaded: {0}", settings.selectedCpu.Name));
            AdvConsole.Log(string.Format("Ram Loaded: {0}", settings.selectedRam.Name));
            computer.setParts(settings.filePath, settings.selectedScreen, settings.selectedCpu, settings.selectedRam);
        }
    }
}
