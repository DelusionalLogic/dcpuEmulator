using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInterface
{
    public interface IPluginHost
    {
        ushort readMem(ushort address);
        void writeMem(int address, ushort value);
    }

    public interface ICpu : IPlugin
    {
        void tick();
    }

    public interface IPlugin
    {
        IPluginHost Host { get; set; }

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

        void initialize();

        void dispose();
    }
}
