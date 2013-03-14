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
            debugger = new DebuggerForm(this);
        }

        public void start()
        {
            debugger.ShowDialog();
        }

        public void tick()
        {
        }

        public ushort[] getRegisterSnapshot()
        {
            return debugger.cpu.register;
        }

        public ushort[] getSpecialRegisters()
        {
            return new[]
                       {
                           debugger.cpu.PC,
                           debugger.cpu.SP,
                           debugger.cpu.EX,
                           debugger.cpu.IA,
                       };
        }

        public long getCycles()
        {
            throw new NotImplementedException();
        }

        public void interrupt(ushort message)
        {
            throw new NotImplementedException();
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
