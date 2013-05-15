using System;
using System.Windows.Forms;

namespace dcpuEmulator
{
    /// <summary>
    /// Default entrypoint
    /// </summary>
    class Program
    {
        private static Computer _computer;

        private static PluginHandler _pluginHandler;

        /// <summary>
        /// Porgram entrypoint
        /// </summary>
        /// <param name="args">Program arguments</param>
        [STAThread]
        static void Main(string[] args)
        {
            _computer = new Computer();
            _pluginHandler = new PluginHandler(_computer);

            AdvConsole.Log("Opening SettingsForm"); 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SettingsForm settingsForm = new SettingsForm(_pluginHandler);
            settingsForm.setupComplete += setupComplete;
            Application.Run(settingsForm);
            AdvConsole.ReadLine();
        }

        //Called when the settings are completed
        public static void setupComplete(object sender, EventArgs e)
        {
            //Print the selected settings to the console
            var settings = (SettingsForm) sender;
            AdvConsole.Log("Setting dialog complete");
            AdvConsole.Log(string.Format("Binary path: {0}", settings.filePath));
            AdvConsole.Log(string.Format("Cpu Loaded: {0}", settings.selectedCpu.Name));
            AdvConsole.Log(string.Format("Ram Loaded: {0}", settings.selectedRam.Name));

            //Initialize all generic hardware
            foreach (var hardware in settings.selectedHardware)
            {
                hardware.initialize();
            }
            //Initialize the cpu and the ram
            settings.selectedCpu.initialize(); settings.selectedRam.initialize();
            _computer.setParts(settings.filePath, settings.selectedCpu, settings.selectedRam, settings.selectedTimer, settings.selectedHardware);
        }
    }
}
