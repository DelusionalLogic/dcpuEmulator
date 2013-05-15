using System;
using System.Collections.Generic;
using PluginInterface;

namespace DefaultCpu
{
    /// <summary>
    /// The cpu emulator
    /// </summary>
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

        /// <summary>
        /// The interrupt queue
        /// </summary>
        internal readonly Queue<ushort> interruptQueue = new Queue<ushort>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Cpu"/> class
        /// </summary>
        /// <param name="computer">The emulator host</param>
        public Cpu(IPluginHost computer)
        {
            Cpu.computer = computer;
        }

        /// <summary>
        /// Executes the next instruction
        /// </summary>
        /// <exception cref="System.NotImplementedException">
        /// Opcode not supported
        /// or
        /// Special Opcode not supported
        /// </exception>
        public void step()
        {
            nextSP = SP;
            nextPC = (ushort) (PC + 1);

            var instr = new Instruction(computer.readMem(PC));

            //If we have a regular OPCode
            if (instr.opCode != OPCode.SPECIAL)
            {
                if(skipNext)
                {
                    skipNext = false;
                    // Skip reading next words //TODO: Get working
                    //if ((instr.b.rawValue >= 0x10 && instr.b.rawValue < 0x18) || instr.b.rawValue == 0x1a || instr.b.rawValue == 0x1e || instr.b.rawValue == 0x1f) // [register + next word], [SP + next word], [next word], next word
                        //nextPC++;
                    //if ((instr.a.rawValue >= 0x10 && instr.a.rawValue < 0x18) || instr.a.rawValue == 0x1a || instr.a.rawValue == 0x1e || instr.a.rawValue == 0x1f) // [register + next word], [SP + next word], [next word], next word
                        //nextPC++;

                    PC = nextPC;

                    //If the opcode is larger than 0x10 and smaller than 0x17, skip the next opcode and add a cycle
                    if (instr.opCode >= (OPCode)0x10 && instr.opCode <= (OPCode)0x17)
                    {
                        skipNext = true;
                        cycles += 1;
                    }
                    return;
                }
                int result = 0;
                //Switch over opcode
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
                        throw new NotImplementedException("Opcode not supported"); //The opcode was not on the list, throw exception
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
                        //TODO: Something about triggering a sofware interrupt or whatever
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
                        try
                        {
                            hardware = computer.getDeviceList()[instr.a.value];
                            register.A = (ushort) (hardware.ID & 0xFFFF);
                            register.B = (ushort) ((hardware.ID >> 16) & 0xFFFF);
                            register.C = hardware.HVersion;
                            register.X = (ushort) (hardware.ManufacturerID & 0xFFFF);
                            register.Y = (ushort) ((hardware.ManufacturerID >> 16) & 0xFFFF);
                            cycles += 4;
                        }catch(ArgumentOutOfRangeException e)
                        {
                            register.A = 0;
                            register.B = 0;
                            register.C = 0;
                            register.X = 0;
                            register.Y = 0;
                            cycles += 4;
                        }
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

            //If theres items in the interruptQueue, and we are not pausing interrupts
            if(interruptQueue.Count != 0 && !pauseInterrupt)
            {
                if(IA != 0)
                {
                    //Stop interrupts
                    pauseInterrupt = true;
                    //Push the current PC
                    push(PC);
                    //Push the current a register
                    push(register.A);
                    //Set the next PC location to IA
                    nextPC = IA;
                    //Get the interrupt message and pop it
                    register.A = interruptQueue.Dequeue();
                }
            }

            PC = nextPC;
            SP = nextSP;
        }

        /// <summary>
        /// Pushes the specified value to the stack
        /// </summary>
        /// <param name="value">The value</param>
        internal void push(ushort value)
        {
            //Push to the stack
            computer.writeMem(--nextSP, value);
        }

        /// <summary>
        /// Pops a value from the stack
        /// </summary>
        /// <returns>The value</returns>
        internal ushort pop()
        {
            //Pop from the stack
            return computer.readMem(nextSP++);
        }

        /// <summary>
        /// Gets the registers
        /// </summary>
        /// <returns>Registers</returns>
        public Register getRegisters()
        {
            return register;
        }

        /// <summary>
        /// Gets the special registers
        /// </summary>
        /// <returns>The special registers</returns>
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
