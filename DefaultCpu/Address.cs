using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultCpu
{
    public struct Address
    {
        public Type aType { get; set; }

        public ushort address { get; set; }
        public ushort rawValue { get; set; }

        public bool isA { get; set; }

        public ushort value { get; set; }
        public ushort signed
        {
            get { return (ushort) ((value ^ 0xFFFF) + 0x0001); }
        }

        public Address(ushort value, bool isA, bool isSpecial = false) : this()
        {
            this.isA = isA;
            rawValue = value;
            if (isSpecial && !isA)
                return;

            if (value < 0x08)
                aType = Type.Register;
            else if (value < 0x10)
                aType = Type.Ram;
            else if (value < 0x18)
            {
                aType = Type.Ram;
                Cpu.cycles += 1;
            }
            else if (value == 0x18)
                aType = Type.Ram;
            else if (value == 0x19)
                aType = Type.Ram;
            else if (value == 0x1a)
            {
                aType = Type.Ram;
                Cpu.cycles += 1;
            }
            else if (value == 0x1b)
                aType = Type.SP;
            else if (value == 0x1c)
                aType = Type.PC;
            else if (value == 0x1d)
                aType = Type.EX;
            else if (value == 0x1e)
            {
                aType = Type.Ram;
                Cpu.cycles += 1;
            }
            else if (value == 0x1f)
            {
                aType = Type.Ram;
                Cpu.cycles += 1;
            }
            else
                aType = Type.Literal;

            address = getAddress(value);
            this.value = read();
        }

        internal ushort read()
        {
            switch (aType)
            {
                case Type.Register:
                    return Cpu.register.readRegister(address);
                case Type.Ram:
                    return Cpu.computer.readMem(address);
                case Type.PC:
                    return Cpu.PC;
                case Type.SP:
                    return Cpu.SP;
                case Type.EX:
                    return Cpu.EX;
                default:
                    if (address == 0)
                        return 0xFFFF;
                    return (ushort)(address - 1);
            }
        }

        internal void write(ushort value)
        {
            switch (aType)
            {
                case Type.Register:
                    Cpu.register.writeRegister(address, value);
                    break;
                case Type.Ram:
                    Cpu.computer.writeMem(address, value);
                    break;
                case Type.PC:
                    Cpu.nextPC = value;
                    break;
                case Type.SP:
                    Cpu.SP = value;
                    break;
                case Type.EX:
                    Cpu.EX = value;
                    break;
            }
        }

        internal void write(int value)
        {
            write((ushort) (value & 0xFFFF));
        }

        private ushort getAddress(ushort value)
        {
            if (value < 0x08)
                return value;
            if (value < 0x10)
                return Cpu.register.readRegister(value - 0x08);
            if (value < 0x18)
                return (ushort)(Cpu.computer.readMem(Cpu.nextPC++) + Cpu.register.readRegister(value - 0x10));
            if (value == 0x18)
                if (isA)
                    return Cpu.nextSP++;
                else
                    return --Cpu.nextSP;
            if (value == 0x19)
                return Cpu.nextSP;
            if (value == 0x1a)
                return (ushort)(Cpu.computer.readMem(Cpu.nextPC++) + Cpu.nextSP);
            if (value == 0x1b)
                return 0;
            if (value == 0x1c)
                return 0;
            if (value == 0x1d)
                return 0;
            if (value == 0x1e)
                return Cpu.computer.readMem(Cpu.nextPC++);
            if (value == 0x1f)
                return Cpu.nextPC++;

            return (char)(value - 0x20);
        }

        public enum Type
        {
            Register,
            Ram,
            PC,
            SP,
            EX,
            IA,
            Literal,
        }
    }
}
