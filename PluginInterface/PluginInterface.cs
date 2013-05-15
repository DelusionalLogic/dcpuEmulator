using System.Collections.Generic;

namespace PluginInterface
{
    public interface IPluginHost
    {
        ushort readMem(int address);
        void writeMem(int address, ushort value);

        List<IHardware> getDeviceList();
        void interruptCPU(ushort message);

        ICpu getCPU();

        void dump(string message);
    }

    public interface ICpu : IPlugin
    {
        void tick();

        ushort[] getRegisterSnapshot();
        ushort[] getSpecialRegisters();
        long getCycles();

        void interrupt(ushort message);
    }

    public interface IRam : IPlugin
    {
        ushort readMem(int address);
        void writeMem(int address, ushort value);
    }

    public interface ITimer : IPlugin
    {
        void start();
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
