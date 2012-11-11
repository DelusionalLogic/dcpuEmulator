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
            get { return "A cpu for debugging"; }
        }
        public string Author
        {
            get { return "DelusionalLogic"; }
        }
        public string Version
        {
            get { return "0.1 (NON WORKING)"; }
        }
        public bool configPossible
        {
            get { return false; }
        }
    }

    public enum Register
    {
        A = 0,
        B,
        C,
        X,
        Y,
        Z,
        I,
        J,
    }

    public enum AddressType
    {
        Register,
        Ram,
        PC,
        SP,
        EX,
        IA,
        Literal,
    }
}
