using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public string filePath = "";

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AdvConsole.Log("SettingsForm loaded");
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
