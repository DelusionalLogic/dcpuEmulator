using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace DefaultRam
{
    public class Main : IRam
    {
        private readonly ushort[] memory = new ushort[0x10000];

        private readonly object ramReadLock = new object();
        private readonly object ramWriteLock = new object();

        public IPluginHost Host { get; set; }

        public void openConfig()
        {
        }

        public void initialize()
        {
        }

        public ushort readMem(int address)
        {
            lock (ramReadLock)
            {
                if (address < memory.Length)
                    return memory[address];
                return 0;
            }
        }

        public void writeMem(int address, ushort value)
        {
            lock (ramWriteLock)
            {
                if (address < memory.Length)
                    memory[address] = value;
            }
        }

        public void dispose()
        {
        }

        public string Name
        {
            get { return "Default Ram"; }
        }
        public string Description
        {
            get { return "A normal Ram module following the spec, returns 0x0000 on memeory out of bounds"; }
        }
        public string Author
        {
            get { return "DelusionalLogic"; }
        }
        public string Version
        {
            get { return "0.1"; }
        }
        public bool configPossible
        {
            get { return false; }
        }
    }
}
