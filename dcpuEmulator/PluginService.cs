using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace dcpuEmulator
{
    class PluginService : IPluginHost
    {
        public ushort readMem(int address)
        {
            throw new NotImplementedException();
        }

        public void writeMem(int address, ushort value)
        {
            throw new NotImplementedException();
        }
    }
}
