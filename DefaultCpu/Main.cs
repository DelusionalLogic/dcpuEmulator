using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PluginInterface;

namespace DefaultCpu
{
    public class Main : ICpu
    {
        public Cpu cpu;

        public IPluginHost Host { get; set; }

        public void initialize()
        {
            cpu = new Cpu(Host);
        }

        public void tick()
        {
            cpu.step();
        }

        public ushort[] getRegisterSnapshot()
        {
            return Cpu.register.toArray();
        }

        public ushort[] getSpecialRegisters()
        {
            return cpu.getSpecialRegisters();
        }

        public long getCycles()
        {
            return Cpu.cycles;
        }

        public void interrupt(ushort message)
        {
            cpu.interruptQueue.Enqueue(message);
        }

        public void dispose()
        {
        }

        public void openConfig()
        {
        }

        public string Name
        {
            get { return "DefaultCPU"; }
        }
        public string Description
        {
            get { return "A CPU that is to spec"; }
        }
        public string Author
        {
            get { return "DelusionalLogic"; }
        }
        public string Version
        {
            get { return "0.0(Not Started)"; }
        }
        public bool configPossible
        {
            get { return false; }
        }
    }
}
