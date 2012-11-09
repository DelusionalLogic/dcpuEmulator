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

        private PluginHandler pluginHandler;

        public string filePath = "";
        public IScreen selectedScreen
        {
            get { return (IScreen)screenBox.SelectedItem; }
        }
        public ICpu selectedCpu
        {
            get { return (ICpu)cpuBox.SelectedItem; }
        }
        public IRam selectedRam
        {
            get { return (IRam)ramBox.SelectedItem; }
        }

        public SettingsForm(PluginHandler pluginHandler)
        {
            this.pluginHandler = pluginHandler;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AdvConsole.Log("SettingsForm loaded");

            screenBox.Items.AddRange(pluginHandler.loadPluginsInFolder<IScreen>(AppDomain.CurrentDomain.BaseDirectory + "Screen").ToArray());
            cpuBox.Items.AddRange(pluginHandler.loadPluginsInFolder<ICpu>(AppDomain.CurrentDomain.BaseDirectory + "Cpu").ToArray());
            ramBox.Items.AddRange(pluginHandler.loadPluginsInFolder<IRam>(AppDomain.CurrentDomain.BaseDirectory + "Ram").ToArray());
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
            onSetupComplete(new EventArgs());
            this.Close();
        }
    }
}
