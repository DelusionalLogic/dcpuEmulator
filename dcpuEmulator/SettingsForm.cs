using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginInterface;

namespace dcpuEmulator
{
    public partial class SettingsForm : Form
    {
        public delegate void SetupCompleteHandler(object sender, EventArgs e);
        public event SetupCompleteHandler setupComplete;

        public void onSetupComplete(EventArgs e)
        {
            SetupCompleteHandler handler = setupComplete;
            if (handler != null) handler(this, e);
        }

        private readonly PluginHandler pluginHandler;

        public string filePath = "";
        public ICpu selectedCpu
        {
            get { return cpuPlugins[cpuBox.SelectedIndex]; }
        }
        public IRam selectedRam
        {
            get { return ramPlugins[ramBox.SelectedIndex]; }
        }

        public List<IHardware> selectedHardware
        {
            get
            {
                return (from int hardwareIndex in hardwareBox.SelectedIndices select hardwareList[hardwareIndex]).ToList();
            }
        }

        public SettingsForm(PluginHandler pluginHandler)
        {
            this.pluginHandler = pluginHandler;
            InitializeComponent();
        }

        private List<ICpu> cpuPlugins;
        private List<IRam> ramPlugins;
        private List<IHardware> hardwareList;  

        private void MainForm_Load(object sender, EventArgs e)
        {
            AdvConsole.Log("SettingsForm loaded");

            cpuPlugins = pluginHandler.loadPluginsInFolder<ICpu>(AppDomain.CurrentDomain.BaseDirectory + "Plugins");
            ramPlugins = pluginHandler.loadPluginsInFolder<IRam>(AppDomain.CurrentDomain.BaseDirectory + "Plugins");
            hardwareList =
                pluginHandler.loadPluginsInFolder<IHardware>(AppDomain.CurrentDomain.BaseDirectory + "Plugins");

            populateCombo(cpuPlugins.ToArray(), ramPlugins.ToArray());

            foreach (var hardware in hardwareList)
            {
                hardwareBox.Items.Add(string.Format("{0} - {1} [{2}]", hardware.Name, hardware.Author, hardware.Version));
            }
        }

        private void populateCombo(ICpu[] cpus, IRam[] rams)
        {
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
        }

        private void browseBut_Click(object sender, EventArgs e)
        {
            if(openBinDia.ShowDialog() == DialogResult.OK)
            {
                filePathBox.Text = openBinDia.FileName;
                filePath = openBinDia.FileName;
            }
        }

        private void okBut_Click(object sender, EventArgs e)
        {
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
