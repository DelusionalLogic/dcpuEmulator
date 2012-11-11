using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace dcpuEmulator
{
    public class Computer : IPluginHost
    {
        private IScreen screen;
        private ICpu cpu;
        private IRam ram;

        public void setParts(string binaryPath, IScreen screen, ICpu cpu, IRam ram)
        {
            this.screen = screen;
            this.cpu = cpu;
            this.ram = ram;

            loadFile(binaryPath);
            cpu.start();
        }

        public void loadFile(string fileName)
        {
            using (var sr = new StreamReader(fileName, Encoding.UTF8))
            {
                int addr = 0;
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    string[] words = str.Split(' ');

                    foreach (var word in words)
                    {
                        if(word.Length != 4)
                            continue;
                        ushort num = ushort.Parse(word, NumberStyles.HexNumber);
                        ram.writeMem(addr++, num);
                    }
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

        public void dump(string message)
        {
            AdvConsole.Log(message);
        }
    }
}
