using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DebuggerCpu
{
    public partial class DebuggerForm : Form
    {
        private Main cpu;
        public DebuggerForm(Main cpu)
        {
            this.cpu = cpu;
            InitializeComponent();
        }

        private void StepBut_Click(object sender, EventArgs e)
        {
            cpu.tick();
            ARegLabel.Text = cpu.register[(int) Register.A].ToString();
            BRegLabel.Text = cpu.register[(int) Register.B].ToString();
            CRegLabel.Text = cpu.register[(int) Register.C].ToString();

            XRegLabel.Text = cpu.register[(int) Register.X].ToString();
            YRegLabel.Text = cpu.register[(int) Register.Y].ToString();
            ZRegLabel.Text = cpu.register[(int) Register.Z].ToString();

            IRegLabel.Text = cpu.register[(int) Register.I].ToString();
            JRegLabel.Text = cpu.register[(int) Register.J].ToString();

            PCRegLabel.Text = cpu.PC.ToString();
        }

        private void ResetBut_Click(object sender, EventArgs e)
        {

        }
    }
}
