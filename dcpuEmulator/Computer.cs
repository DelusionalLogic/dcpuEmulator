using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PluginInterface;

namespace dcpuEmulator
{
    public class Computer : IPluginHost
    {
        private ICpu cpu;
        private IRam ram;

        private List<IHardware> hardware; 

        public void setParts(string binaryPath, ICpu cpu, IRam ram, ITimer timing, List<IHardware> hardware)
        {
            this.cpu = cpu;
            this.ram = ram;
            this.hardware = hardware;

            loadFile(binaryPath);
            new Thread((ThreadStart)delegate
                                        {
                                            while (true)
                                            {
                                                string s = cpu.getRegisterSnapshot().Aggregate("", (current, register) => current + (register + "; "));
                                                s += cpu.getSpecialRegisters()[0] + "; ";
                                                AdvConsole.Debug(s);
                                            }
                                        }).Start();
            timing.start();
        }

        public void loadFile(string fileName)
        {
            using (var file = File.Open(fileName, FileMode.Open))
            {
                int address = 0;
                int b;
                while ((b = file.ReadByte()) != -1)
                {
                    ushort memVal = (ushort) ((b << 8) + file.ReadByte());
                    writeMem(address++, memVal);
                }
            }
        }

        public ushort readMem(int address)
        {
            return ram.readMem(address);
        }

        public void writeMem(int address, ushort value)
        {
            ram.writeMem(address, value);
        }

        public List<IHardware> getDeviceList()
        {
            return hardware;
        }

        public void interruptCPU(ushort message)
        {
            cpu.interrupt(message);
        }

        public ICpu getCPU()
        {
            return cpu;
        }

        public void dump(string message)
        {
            AdvConsole.Log(message);
        }
    }
}
