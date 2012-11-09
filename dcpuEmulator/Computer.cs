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
    class Computer
    {
        private readonly IScreen screen;
        private readonly ICpu cpu;
        private readonly IRam ram;

        public Computer(IScreen screen, ICpu cpu, IRam ram)
        {
            this.screen = screen;
            this.cpu = cpu;
            this.ram = ram;
        }

        public void loadFile(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                int addr = 0;
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    string[] words = str.Split(' ');

                    foreach (var word in words)
                    {
                        ushort num = ushort.Parse(word, NumberStyles.HexNumber);
                        ram.writeMem(addr++, num);
                    }
                }
            }
        }
    }
}
