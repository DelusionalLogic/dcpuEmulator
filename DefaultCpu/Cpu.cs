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

        internal static ushort[] register = new ushort[8];
        internal static ushort PC;
        internal static ushort SP;
        internal static ushort EX;
        internal static ushort IA;

        internal static ushort nextSP, nextPC;

        internal bool skipExec;

        public Cpu(IPluginHost computer)
        {
            Cpu.computer = computer;
        }

        public void step()
        {
            var instr = new Instruction(computer.readMem(PC));

            int result = 0;
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
                    EX = (ushort)(((instr.b.signed << 16) >> instr.a.value) & 0xFFFF);
                    instr.b.write(result);
                    break;
            }
        }
    }
}
