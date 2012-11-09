﻿using System;
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
            get { return screenPlugins[screenBox.SelectedIndex]; }
        }
        public ICpu selectedCpu
        {
            get { return cpuPlugins[cpuBox.SelectedIndex]; }
        }
        public IRam selectedRam
        {
            get { return ramPlugins[ramBox.SelectedIndex]; }
        }

        public SettingsForm(PluginHandler pluginHandler)
        {
            this.pluginHandler = pluginHandler;
            InitializeComponent();
        }

        private List<IScreen> screenPlugins;
        private List<ICpu> cpuPlugins;
        private List<IRam> ramPlugins; 

        private void MainForm_Load(object sender, EventArgs e)
        {
            AdvConsole.Log("SettingsForm loaded");

            screenPlugins = pluginHandler.loadPluginsInFolder<IScreen>(AppDomain.CurrentDomain.BaseDirectory + "Screen");
            cpuPlugins = pluginHandler.loadPluginsInFolder<ICpu>(AppDomain.CurrentDomain.BaseDirectory + "Cpu");
            ramPlugins = pluginHandler.loadPluginsInFolder<IRam>(AppDomain.CurrentDomain.BaseDirectory + "Ram");

            populateCombo(screenPlugins.ToArray(), cpuPlugins.ToArray(), ramPlugins.ToArray());
        }

        private void populateCombo(IScreen[] screens, ICpu[] cpus, IRam[] rams)
        {
            const string listSetup = "{0} - {1} [{2}]";
            foreach (var screen in screens)
            {
                screenBox.Items.Add(string.Format(listSetup, screen.Name, screen.Author, screen.Version));
            }
            foreach (var cpu in cpus)
            {
                cpuBox.Items.Add(string.Format(listSetup, cpu.Name, cpu.Author, cpu.Version));
            }
            foreach (var ram in rams)
            {
                ramBox.Items.Add(string.Format(listSetup, ram.Name, ram.Author, ram.Version));
            }
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