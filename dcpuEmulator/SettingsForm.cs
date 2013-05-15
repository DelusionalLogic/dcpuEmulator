using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PluginInterface;

namespace dcpuEmulator
{
    public partial class SettingsForm : Form
    {
        public delegate void SetupCompleteHandler(object sender, EventArgs e);
        public event SetupCompleteHandler setupComplete;

        private void onSetupComplete(EventArgs e)
        {
            SetupCompleteHandler handler = setupComplete;
            if (handler != null) handler(this, e);
        }

        private readonly PluginHandler pluginHandler;

        /// <summary>
        /// the path to the selected file
        /// </summary>
        public string filePath = "";

        /// <summary>
        /// The selected CPU
        /// </summary>
        public ICpu selectedCpu
        {
            get { return cpuPlugins[cpuBox.SelectedIndex]; }
        }

        /// <summary>
        /// The selected ram module
        /// </summary>
        public IRam selectedRam
        {
            get { return ramPlugins[ramBox.SelectedIndex]; }
        }

        /// <summary>
        /// The selected timer
        /// </summary>
        public ITimer selectedTimer
        {
            get { return timerPlugins[timerBox.SelectedIndex]; }
        }

        /// <summary>
        /// All selected generic hardware
        /// </summary>
        public List<IHardware> selectedHardware
        {
            get
            {
                return (from int hardwareIndex in hardwareBox.CheckedIndices select hardwareList[hardwareIndex]).ToList();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsForm"/> class
        /// </summary>
        /// <param name="pluginHandler">The plugin handler</param>
        public SettingsForm(PluginHandler pluginHandler)
        {
            this.pluginHandler = pluginHandler;
            InitializeComponent();
        }

        private List<ICpu> cpuPlugins;
        private List<IRam> ramPlugins;
        private List<ITimer> timerPlugins; 
        private List<IHardware> hardwareList;  

        private void MainForm_Load(object sender, EventArgs e)
        {
            AdvConsole.Log("SettingsForm loaded");

            //Get the possible plugins for each category
            cpuPlugins = pluginHandler.loadPluginsInFolder<ICpu>(AppDomain.CurrentDomain.BaseDirectory + "Plugins");
            ramPlugins = pluginHandler.loadPluginsInFolder<IRam>(AppDomain.CurrentDomain.BaseDirectory + "Plugins");
            timerPlugins = pluginHandler.loadPluginsInFolder<ITimer>(AppDomain.CurrentDomain.BaseDirectory + "Plugins");
            hardwareList =
                pluginHandler.loadPluginsInFolder<IHardware>(AppDomain.CurrentDomain.BaseDirectory + "Plugins");

            populateCombo(cpuPlugins.ToArray(), ramPlugins.ToArray(), timerPlugins.ToArray());

            foreach (var hardware in hardwareList)
            {
                //Add the generic hardware
                hardwareBox.Items.Add(string.Format("{0} - {1} [{2}]", hardware.Name, hardware.Author, hardware.Version));
            }
        }

        /// <summary>
        /// Populate the comboboxes with the items
        /// </summary>
        /// <param name="cpus">The possible cpus</param>
        /// <param name="rams">The possible ram</param>
        /// <param name="timers">The possible timers</param>
        private void populateCombo(ICpu[] cpus, IRam[] rams, ITimer[] timers)
        {
            //The generic format of the items
            const string listSetup = "{0} - {1} [{2}]";
            foreach (var cpu in cpus)
            {
                cpuBox.Items.Add(string.Format(listSetup, cpu.Name, cpu.Author, cpu.Version));
            }
            cpuBox.SelectedIndex = 0;
            foreach (var ram in rams)
            {
                ramBox.Items.Add(string.Format(listSetup, ram.Name, ram.Author, ram.Version));
            }
            ramBox.SelectedIndex = 0;
            foreach (var timer in timers)
            {
                timerBox.Items.Add(string.Format(listSetup, timer.Name, timer.Author, timer.Version));
            }
            timerBox.SelectedIndex = 0;
        }

        private void browseBut_Click(object sender, EventArgs e)
        {
            //Open a dialog to select the binary
            if(openBinDia.ShowDialog() == DialogResult.OK)
            {
                filePathBox.Text = openBinDia.FileName;
                filePath = openBinDia.FileName;
            }
        }

        private void okBut_Click(object sender, EventArgs e)
        {
            //Hide the dialog, call the callback and close
            Hide();
            onSetupComplete(new EventArgs());
            Close();
        }

        private void cpuBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            configCpuBut.Enabled = selectedCpu.configPossible;
        }

        private void ramBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            configRamBut.Enabled = selectedRam.configPossible;
        }
    }
}
