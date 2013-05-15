using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using PluginInterface;

namespace dcpuEmulator
{
    /// <summary>
    /// The defalt host computer container.
    /// </summary>
    public class Computer : IPluginHost
    {
        private ICpu cpu;
        private IRam ram;

        private List<IHardware> hardware; 

        /// <summary>
        /// Sets the currently used parts and starts the computer
        /// </summary>
        /// <param name="binaryPath">Path to the binary to execute</param>
        /// <param name="cpu">The selected cpu module</param>
        /// <param name="ram">The selected ram module</param>
        /// <param name="timing">The selected timer</param>
        /// <param name="hardware">The selected generic hardware</param>
        public void setParts(string binaryPath, ICpu cpu, IRam ram, ITimer timing, List<IHardware> hardware)
        {
            this.cpu = cpu;
            this.ram = ram;
            this.hardware = hardware;

            loadFile(binaryPath);
            //Start a new thread which prints the registers to the console.
            new Thread((ThreadStart)delegate
                                        {
                                            while (true)
                                            {
                                                //Convert the current cpu status to a string
                                                string s = cpu.getRegisterSnapshot().Aggregate("", (current, register) => current + (register + "; "));
                                                //Add the PC special register to the string
                                                s += cpu.getSpecialRegisters()[0] + "; ";
                                                AdvConsole.Debug(s);
                                            }
                                        }).Start();
            timing.start();
        }

        /// <summary>
        /// Load a binary file into the memory
        /// </summary>
        /// <param name="fileName">The path to the binary</param>
        public void loadFile(string fileName)
        {
            using (var file = File.Open(fileName, FileMode.Open))
            {
                int address = 0;
                int b;
                //Loop untill the end of the file
                while ((b = file.ReadByte()) != -1)
                {
                    //Read 2 bytes and bitshift the first one 8 to the left
                    var memVal = (ushort) ((b << 8) + file.ReadByte());
                    writeMem(address++, memVal);
                }
            }
        }

        /// <summary>
        /// Read a specific memory address
        /// </summary>
        /// <param name="address">The address to read</param>
        /// <returns>The 16 value at the address</returns>
        public ushort readMem(int address)
        {
            return ram.readMem(address);
        }

        /// <summary>
        /// Write a value to the memory
        /// </summary>
        /// <param name="address">The address to write to</param>
        /// <param name="value">The value to write</param>
        public void writeMem(int address, ushort value)
        {
            ram.writeMem(address, value);
        }

        /// <summary>
        /// Get a list of the active generic devices
        /// </summary>
        /// <returns>A lost of the generic devices</returns>
        public List<IHardware> getDeviceList()
        {
            return hardware;
        }

        /// <summary>
        /// Interrupt the cpu with a message
        /// </summary>
        /// <param name="message">Message to send to the cpu</param>
        public void interruptCPU(ushort message)
        {
            cpu.interrupt(message);
        }

        /// <summary>
        /// Get the current CPU
        /// </summary>
        /// <returns>The current CPU</returns>
        public ICpu getCPU()
        {
            return cpu;
        }

        /// <summary>
        /// Write a message to the console
        /// </summary>
        /// <param name="message">The message to write</param>
        public void dump(string message)
        {
            AdvConsole.Log(message);
        }
    }
}
