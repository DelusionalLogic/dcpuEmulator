using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PluginInterface;

namespace DefaultCpu
{
    public class Cpu
    {
        internal static IPluginHost computer;

        internal static Register register;
        internal static ushort PC;
        internal static ushort SP;
        internal static ushort EX;
        internal static ushort IA;
        internal static long cycles;

        internal static ushort nextSP, nextPC;

        internal bool skipNext, pauseInterrupt;

        internal readonly Queue<ushort> interruptQueue = new Queue<ushort>(); 

        public Cpu(IPluginHost computer)
        {
            Cpu.computer = computer;
        }

        public void step()
        {
            nextSP = SP;
            nextPC = (ushort) (PC + 1);

            var instr = new Instruction(computer.readMem(PC));

            int result = 0;

            if (instr.opCode != OPCode.SPECIAL)
            {
                if(skipNext)
                {
                    skipNext = false;
                    // Skip reading next words 
                    //if ((instr.b.rawValue >= 0x10 && instr.b.rawValue < 0x18) || instr.b.rawValue == 0x1a || instr.b.rawValue == 0x1e || instr.b.rawValue == 0x1f) // [register + next word], [SP + next word], [next word], next word
                        //nextPC++;
                    //if ((instr.a.rawValue >= 0x10 && instr.a.rawValue < 0x18) || instr.a.rawValue == 0x1a || instr.a.rawValue == 0x1e || instr.a.rawValue == 0x1f) // [register + next word], [SP + next word], [next word], next word
                        //nextPC++;

                    PC = nextPC;

                    if (instr.opCode >= (OPCode)0x10 && instr.opCode <= (OPCode)0x17)
                    {
                        skipNext = true;
                        cycles += 1;
                    }
                    return;
                }
                switch (instr.opCode)
                {
                    case OPCode.SET:
                        result = instr.a.value;
                        instr.b.write(result);
                        cycles += 1;
                        break;
                    case OPCode.ADD:
                        result = instr.b.value + instr.a.value;
                        EX = (ushort) ((result < 0xFFFF) ? 1 : 0);
                        instr.b.write(result);
                        cycles += 2;
                        break;
                    case OPCode.SUB:
                        result = instr.b.value - instr.a.value;
                        EX = (ushort) (result > 0x0000 ? 0 : 0xFFFF);
                        instr.b.write(result);
                        cycles += 2;
                        break;
                    case OPCode.MUL:
                        result = instr.b.value*instr.a.value;
                        EX = (ushort) ((result >> 16) & 0xFFFF);
                        instr.b.write(result);
                        cycles += 2;
                        break;
                    case OPCode.MLI:
                        result = instr.b.signed*instr.a.signed;
                        EX = (ushort) ((result >> 16) & 0xFFFF);
                        instr.b.write(result);
                        cycles += 2;
                        break;
                    case OPCode.DIV:
                        result = instr.b.value/instr.a.value;
                        EX = (ushort) (((instr.b.value << 16)/instr.a.value) & 0xFFFF);
                        instr.b.write(result);
                        cycles += 3;
                        break;
                    case OPCode.DVI:
                        result = instr.b.signed/instr.a.signed;
                        EX = (ushort) (((instr.b.signed << 16)/instr.a.signed) & 0xFFFF);
                        instr.b.write(result);
                        cycles += 3;
                        break;
                    case OPCode.MOD:
                        result = instr.a.value == 0 ? 0 : instr.b.value%instr.a.value;
                        instr.b.write(result);
                        cycles += 3;
                        break;
                    case OPCode.MDI:
                        result = instr.a.value == 0 ? 0 : instr.b.signed%instr.a.signed;
                        instr.b.write(result);
                        cycles += 3;
                        break;
                    case OPCode.AND:
                        result = instr.b.value & instr.a.value;
                        instr.b.write(result);
                        cycles += 1;
                        break;
                    case OPCode.BOR:
                        result = instr.b.value | instr.a.value;
                        instr.b.write(result);
                        cycles += 1;
                        break;
                    case OPCode.XOR:
                        result = instr.b.value ^ instr.a.value;
                        instr.b.write(result);
                        cycles += 1;
                        break;
                    case OPCode.SHR:
                        result = instr.b.value >> instr.a.value;
                        EX = (ushort) (((instr.b.value << 16) >> instr.a.value) & 0xFFFF);
                        instr.b.write(result);
                        cycles += 1;
                        break;
                    case OPCode.ASR:
                        result = instr.b.signed >> instr.b.value;
                        EX = (ushort) (((instr.b.signed << 16) >> instr.a.value) & 0xFFFF);
                        instr.b.write(result);
                        cycles += 1;
                        break;
                    case OPCode.SHL:
                        result = instr.b.value << instr.a.value;
                        EX = (ushort) (((instr.b.value << instr.a.value) >> 16) & 0xFFFF);
                        instr.b.write(result);
                        cycles += 1;
                        break;
                    case OPCode.IFB:
                        skipNext = (instr.b.value & instr.a.value) == 0;
                        cycles += 2;
                        break;
                    case OPCode.IFC:
                        skipNext = (instr.b.value & instr.a.value) != 0;
                        cycles += 2;
                        break;
                    case OPCode.IFE:
                        skipNext = (instr.b.value != instr.a.value);
                        cycles += 2;
                        break;
                    case OPCode.IFN:
                        skipNext = (instr.b.value == instr.a.value);
                        cycles += 2;
                        break;
                    case OPCode.IFG:
                        skipNext = (instr.b.value < instr.a.value);
                        cycles += 2;
                        break;
                    case OPCode.IFA:
                        skipNext = (instr.b.signed < instr.a.signed);
                        cycles += 2;
                        break;
                    case OPCode.IFL:
                        skipNext = (instr.b.value > instr.a.value);
                        cycles += 2;
                        break;
                    case OPCode.IFU:
                        skipNext = (instr.b.signed > instr.a.signed);
                        cycles += 2;
                        break;
                    case OPCode.ADX:
                        result = instr.b.value + instr.a.value + EX;
                        EX = (ushort) (result < 0 ? 0 : 1);
                        instr.b.write(result);
                        cycles += 3;
                        break;
                    case OPCode.SBX:
                        result = instr.b.value - instr.a.value - EX;
                        EX = (ushort) (result > 0 ? 0 : 1);
                        instr.b.write(result);
                        cycles += 3;
                        break;
                    case OPCode.STI:
                        result = instr.a.value;
                        register.I++;
                        register.J++;
                        instr.b.write(result);
                        cycles += 2;
                        break;
                    case OPCode.STD:
                        result = instr.a.value;
                        register.I--;
                        register.J--;
                        instr.a.write(result);
                        cycles += 2;
                        break;
                    default:
                        throw new NotImplementedException("Opcode not supported");
                }
            }
            else
            {
                //Special Opcode
                IHardware hardware;
                switch ((SpecialOPCode)instr.b.rawValue)
                {
                    case SpecialOPCode.JSR:
                        ushort jmpLoc = instr.a.value;
                        push(nextPC);

                        nextPC = jmpLoc;
                        cycles += 3;
                        break;
                    case SpecialOPCode.INT:
                        //Something about triggering a sofware interrupt or whatever
                        cycles += 4;
                        break;
                    case SpecialOPCode.IAG:
                        instr.a.write(IA);
                        cycles += 1;
                        break;
                    case SpecialOPCode.IAS:
                        IA = instr.a.value;
                        cycles += 1;
                        break;
                    case SpecialOPCode.RFI:
                        pauseInterrupt = false;
                        instr.a.write(pop());
                        nextPC = pop();
                        cycles += 3;
                        break;
                    case SpecialOPCode.IAQ:
                        pauseInterrupt = instr.a.value != 0;
                        cycles += 2;
                        break;
                    case SpecialOPCode.HWN:
                        instr.a.write(computer.getDeviceList().Count);
                        cycles += 2;
                        break;
                    case SpecialOPCode.HWQ:
                        hardware = computer.getDeviceList()[instr.a.value];
                        register.A = (ushort) (hardware.ID & 0xFFFF);
                        register.B = (ushort) ((hardware.ID >> 16) & 0xFFFF);
                        register.C = hardware.HVersion;
                        register.X = (ushort) (hardware.ManufacturerID & 0xFFFF);
                        register.Y = (ushort) ((hardware.ManufacturerID >> 16) & 0xFFFF);
                        cycles += 4;
                        break;
                    case SpecialOPCode.HWI:
                        hardware = computer.getDeviceList()[instr.a.value];
                        register.fromArray(hardware.interrupt(register.toArray()));
                        cycles += 4;
                        break;
                    default:
                        throw new NotImplementedException("Special Opcode not supported");
                }
            }

            if(interruptQueue.Count != 0 && !pauseInterrupt)
            {
                if(IA != 0)
                {
                    pauseInterrupt = true;
                    push(PC);
                    push(register.A);
                    nextPC = IA;
                    register.A = interruptQueue.Dequeue();
                }
            }

            PC = nextPC;
            SP = nextSP;
        }

        internal void push(ushort value)
        {
            computer.writeMem(--nextSP, value);
        }

        internal ushort pop()
        {
            return computer.readMem(nextSP++);
        }

        public Register getRegisters()
        {
            return register;
        }

        public ushort[] getSpecialRegisters()
        {
            return new[]
                       {
                           PC,
                           SP,
                           EX,
                           IA,
                       };
        }

    }
}
