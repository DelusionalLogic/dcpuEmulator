using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInterface
{
    public interface IPluginHost
    {
        ushort readMem(int address);
        void writeMem(int address, ushort value);

        List<IHardware> getDeviceList();
        void interruptCPU(ushort message);

        void dump(string message);
    }

    public interface ICpu : IPlugin
    {
        void start();
        void step();

        ushort[] getRegisterSnapshot();
        ushort[] getSpecialRegisters();

        void interrupt(ushort message);
    }

    public interface IRam : IPlugin
    {
        ushort readMem(int address);
        void writeMem(int address, ushort value);
    }

    public interface IHardware : IPlugin
    {
        ushort[] interrupt(ushort[] registers);

        uint ID { get; }
        ushort HVersion { get; }
        uint ManufacturerID { get; }
    }

    public interface IPlugin
    {
        IPluginHost Host { get; set; }

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

        bool configPossible { get; }

        void openConfig();

        void initialize();
        void dispose();
    }
}
