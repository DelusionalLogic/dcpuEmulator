using System;
using System.Collections.Generic;
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

        public void setParts(string binaryPath, ICpu cpu, IRam ram, List<IHardware> hardware)
        {
            this.cpu = cpu;
            this.ram = ram;
            this.hardware = hardware;

            loadFile(binaryPath);
            new Thread((ThreadStart)delegate
            {
                while (true)
                {
                    Thread.Sleep(1);
                    cpu.step();
                }
            }).Start();
            new Thread((ThreadStart)delegate
                                        {
                                            while (true)
                                            {
                                                string s = cpu.getRegisterSnapshot().Aggregate("", (current, register) => current + (register + "; "));
                                                s += cpu.getSpecialRegisters()[0] + "; " + cpu.getSpecialRegisters()[2] + "; ";
                                                s += cpu.getCycles();
                                                AdvConsole.Debug(s);
                                            }
                                        }).Start();
            cpu.start();
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

        public void dump(string message)
        {
            AdvConsole.Log(message);
        }
    }
}
