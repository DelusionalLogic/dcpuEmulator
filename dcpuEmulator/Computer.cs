﻿using System;
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
        private IScreen screen;
        private ICpu cpu;
        private IRam ram;

        public void setParts(string binaryPath, IScreen screen, ICpu cpu, IRam ram)
        {
            this.screen = screen;
            this.cpu = cpu;
            this.ram = ram;

            loadFile(binaryPath);
            new Thread((ThreadStart)delegate
            {
                while (true)
                {
                    string s = "";
                    foreach (ushort register in cpu.getRegisterSnapshot())
                    {
                        s += register + "; ";
                    }
                    s += cpu.getSpecialRegisters()[0];
                    AdvConsole.Debug(s);
                    cpu.step();
                }
            }).Start();
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

        public List<IHardware> getDeviceList()
        {
            throw new NotImplementedException();
        }

        public ushort[] interrupt(ushort[] registers)
        {
            throw new NotImplementedException();
        }

        public void interruptCPU(ushort message)
        {
            throw new NotImplementedException();
        }

        public void dump(string message)
        {
            AdvConsole.Log(message);
        }
    }
}
