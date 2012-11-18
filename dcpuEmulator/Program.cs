using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginInterface;

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
            var settings = (SettingsForm) sender;
            AdvConsole.Log("Setting dialog complete");
            AdvConsole.Log(string.Format("Binary path: {0}", settings.filePath));
            AdvConsole.Log(string.Format("Cpu Loaded: {0}", settings.selectedCpu.Name));
            AdvConsole.Log(string.Format("Ram Loaded: {0}", settings.selectedRam.Name));
            foreach (var hardware in settings.selectedHardware)
            {
                hardware.initialize();
            }
            settings.selectedCpu.initialize(); settings.selectedRam.initialize();
            computer.setParts(settings.filePath, settings.selectedCpu, settings.selectedRam, settings.selectedHardware);
        }
    }
}
