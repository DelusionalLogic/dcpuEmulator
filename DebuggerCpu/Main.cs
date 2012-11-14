using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginInterface;

namespace DebuggerCpu
{
    public class Main : ICpu
    {
        private DebuggerForm debugger;

        public IPluginHost Host { get; set; }

        public void initialize()
        {
        }

        public void start()
        {
            debugger = new DebuggerForm(this);
            debugger.ShowDialog();
        }

        public void step()
        {
        }

        public void dispose()
        {
        }

        public void openConfig()
        {
        }

        public string Name
        {
            get { return "Debugger CPU"; }
        }
        public string Description
        {
            get { return "A CPU with debugger, Outdated"; }
        }
        public string Author
        {
            get { return "DelusionalLogic"; }
        }
        public string Version
        {
            get { return "0.5"; }
        }
        public bool configPossible
        {
            get { return false; }
        }
    }
}
