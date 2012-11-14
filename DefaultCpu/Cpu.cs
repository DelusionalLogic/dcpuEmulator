using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace DefaultCpu
{
    class Cpu
    {
        internal static IPluginHost computer;

        internal static Register register;
        internal static ushort PC;
        internal static ushort SP;
        internal static ushort EX;
        internal static ushort IA;

        internal static ushort nextSP, nextPC;

        internal bool skipNext;

        public Cpu(IPluginHost computer)
        {
            Cpu.computer = computer;
        }

        public void step()
        {
            var instr = new Instruction(computer.readMem(PC));

            int result = 0;

            if (instr.opCode != 0)
            {
                switch (instr.opCode)
                {
                    case OPCode.SET:
                        result = instr.a.value;
                        instr.b.write(result);
                        break;
                    case OPCode.ADD:
                        result = instr.b.value + instr.a.value;
                        EX = (ushort) ((result < 0xFFFF) ? 1 : 0);
                        instr.b.write(result);
                        break;
                    case OPCode.SUB:
                        result = instr.b.value - instr.a.value;
                        EX = (ushort) (result > 0x0000 ? 0 : 0xFFFF);
                        instr.b.write(result);
                        break;
                    case OPCode.MUL:
                        result = instr.b.value*instr.a.value;
                        EX = (ushort) ((result >> 16) & 0xFFFF);
                        instr.b.write(result);
                        break;
                    case OPCode.MLI:
                        result = instr.b.signed*instr.a.signed;
                        EX = (ushort) ((result >> 16) & 0xFFFF);
                        instr.b.write(result);
                        break;
                    case OPCode.DIV:
                        result = instr.b.value/instr.a.value;
                        EX = (ushort) (((instr.b.value << 16)/instr.a.value) & 0xFFFF);
                        instr.b.write(result);
                        break;
                    case OPCode.DVI:
                        result = instr.b.signed/instr.a.signed;
                        EX = (ushort) (((instr.b.signed << 16)/instr.a.signed) & 0xFFFF);
                        instr.b.write(result);
                        break;
                    case OPCode.MOD:
                        result = instr.a.value == 0 ? 0 : instr.b.value%instr.a.value;
                        instr.b.write(result);
                        break;
                    case OPCode.MDI:
                        result = instr.a.value == 0 ? 0 : instr.b.signed%instr.a.signed;
                        instr.b.write(result);
                        break;
                    case OPCode.AND:
                        result = instr.b.value & instr.a.value;
                        instr.b.write(result);
                        break;
                    case OPCode.BOR:
                        result = instr.b.value | instr.a.value;
                        instr.b.write(result);
                        break;
                    case OPCode.XOR:
                        result = instr.b.value ^ instr.a.value;
                        instr.b.write(result);
                        break;
                    case OPCode.SHR:
                        result = instr.b.value >> instr.a.value;
                        EX = (ushort) (((instr.b.value << 16) >> instr.a.value) & 0xFFFF);
                        instr.b.write(result);
                        break;
                    case OPCode.ASR:
                        result = instr.b.signed >> instr.b.value;
                        EX = (ushort) (((instr.b.signed << 16) >> instr.a.value) & 0xFFFF);
                        instr.b.write(result);
                        break;
                    case OPCode.SHL:
                        result = instr.b.value << instr.a.value;
                        EX = (ushort) (((instr.b.value << instr.a.value) >> 16) & 0xFFFF);
                        instr.b.write(result);
                        break;
                    case OPCode.IFB:
                        skipNext = (instr.b.value & instr.a.value) == 0;
                        break;
                    case OPCode.IFC:
                        skipNext = (instr.b.value & instr.a.value) != 0;
                        break;
                    case OPCode.IFE:
                        skipNext = (instr.b.value != instr.a.value);
                        break;
                    case OPCode.IFN:
                        skipNext = (instr.b.value == instr.a.value);
                        break;
                    case OPCode.IFG:
                        skipNext = (instr.b.value < instr.a.value);
                        break;
                    case OPCode.IFA:
                        skipNext = (instr.b.signed < instr.a.signed);
                        break;
                    case OPCode.IFL:
                        skipNext = (instr.b.value > instr.a.value);
                        break;
                    case OPCode.IFU:
                        skipNext = (instr.b.signed > instr.a.signed);
                        break;
                    case OPCode.ADX:
                        result = instr.b.value + instr.a.value + EX;
                        EX = (ushort) (result < 0 ? 0 : 1);
                        instr.b.write(result);
                        break;
                    case OPCode.SBX:
                        result = instr.b.value - instr.a.value - EX;
                        EX = (ushort) (result > 0 ? 0 : 1);
                        instr.b.write(result);
                        break;
                    case OPCode.STI:
                        result = instr.a.value;
                        register.I++;
                        register.J++;
                        instr.b.write(result);
                        break;
                    case OPCode.STD:
                        result = instr.a.value;
                        register.I--;
                        register.J--;
                        instr.a.write(result);
                        break;
                    default:
                        throw new NotImplementedException("Opcode not supported");
                }
            }
            else
            {
                //Special Opcode
                switch (instr.b.rawValue)
                {
                        
                }
            }
        }

        public struct Register
        {
            public ushort A;
            public ushort B;
            public ushort C;
            public ushort X;
            public ushort Y;
            public ushort Z;
            public ushort I;
            public ushort J;

            public ushort readRegister(int index)
            {
                switch (index)
                {
                    case 0:
                        return A;
                    case 1:
                        return B;
                    case 2:
                        return C;
                    case 3:
                        return X;
                    case 4:
                        return Y;
                    case 5:
                        return Z;
                    case 6:
                        return I;
                    case 7:
                        return J;
                    default:
                        return 0;
                }
            }

            public void writeRegister(int index, ushort value)
            {
                switch (index)
                {
                    case 0:
                        A = value;
                        break;
                    case 1:
                        B = value;
                        break;
                    case 2:
                        C = value;
                        break;
                    case 3:
                        X = value;
                        break;
                    case 4:
                        Y = value;
                        break;
                    case 5:
                        Z = value;
                        break;
                    case 6:
                        I = value;
                        break;
                    case 7:
                        J = value;
                        break;
                }
            }
        }
    }
}
