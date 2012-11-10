using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PluginInterface;

namespace DefaultCpu
{
    public class Main : ICpu
    {
        public IPluginHost Host { get; set; }

        public ushort[] register;
        public ushort PC;
        public ushort SP;
        public ushort EX;
        public ushort IA;

        private ushort tSP, tPC;

        private bool skipping;

        public void initialize()
        {
            register = new ushort[8];
            PC = SP = EX = IA = 0;
            skipping = false;
        }

        public void start()
        {
            new Thread(loop).Start();
        }

        private void loop()
        {
            while (true)
            {
                tick();
            }
        }

        private void tick()
        {
            tSP = SP;
            tPC = (ushort) (PC + 1);

            ushort instr = Host.readMem(PC); 
            
            int opcode = instr & 0x1f;
            ushort b = (ushort)((instr >> 5) & 0x1f);
            ushort a = (ushort)(instr >> 10);

            if (opcode != 0)
		{
			if (skipping)
			{
				// Skip reading next words 
				if ((b >= 0x10 && b < 0x18) || b == 0x1a || b == 0x1e || b == 0x1f) // [register + next word], [SP + next word], [next word], next word
					tPC++;
				if ((a >= 0x10 && a < 0x18) || a == 0x1a || a == 0x1e || a == 0x1f) // [register + next word], [SP + next word], [next word], next word
					tPC++;

				PC = tPC;

				if (opcode < 0x10 || opcode > 0x17) // Keep skipping over conditionals
					skipping = false;
				return;
			}

			// Work out address type and location before instruction
			// These will only modify tSP or tPC, not the actual SP or PC
			// until after the instruction completes. 
		    AddressType aType = getType(a), bType = getType(b);
            ushort aAddr = addressA(a), bAddr = addressB(b);

			// Grab the actual values
            ushort aVal = read(aType, aAddr), bVal = read(bType, bAddr);

			int res = 0; // Result

			switch (opcode)
			{
				case 0x01: // SET
					res = aVal;
					break;
				case 0x02: // ADD
					res = bVal + aVal;
                    EX = (res < 0xffff) ? (ushort)0 : (ushort)1;
					break;
				case 0x03: // SUB
					res = bVal - aVal;
                    EX = (ushort) ((res > 0) ? 0 : (ushort)0xffff);
					break;
				case 0x04: // MUL
					res = bVal * aVal;
                    EX = (ushort)(res >> 16);
					break;
				case 0x05: // MLI
					res = toSigned(bVal) * toSigned(aVal);
                    EX = (ushort)(res >> 16);
					break;
				case 0x06: // DIV
					res = bVal / aVal;
                    EX = (ushort)(((bVal << 16) / aVal) & 0xffff);
					break;
				case 0x07: // DVI
					res = toSigned(bVal) / toSigned(aVal);
                    EX = (ushort)(((toSigned(bVal) << 16) / toSigned(aVal)) & 0xffff);
					break;
				case 0x08: // MOD
					if (aVal == 0)
						res = 0;
					else
						res = bVal % aVal;
					break;
				case 0x09: // MDI
					if (aVal == 0)
						res = 0;
					else
						res = toSigned(bVal) % toSigned(aVal);
					break;
				case 0x0a: // AND
					res = bVal & aVal;
					break;
				case 0x0b: // BOR
					res = bVal | aVal;
					break;
				case 0x0c: // XOR
					res = bVal ^ aVal;
					break;
				case 0x0d: // SHR
					res = bVal >> a;
					EX = (ushort)(((bVal << 16) >> a) & 0xffff);
					break;
				case 0x0e: // ASR
					res = bVal >> a;
                    EX = (ushort)(((bVal << 16) >> a) & 0xffff);
					break;
				case 0x0f: // SHL
					res = bVal << a;
                    EX = (ushort)(((bVal << aVal) >> 16) & 0xfff);
					break;
				case 0x10: // IFB
					skipping = (bVal & aVal) != 0;
					break;
				case 0x11: // IFC
					skipping = (bVal & aVal) == 0;
					break;
				case 0x12: // IFE
					skipping = (bVal == aVal);
					break;
				case 0x13: // IFN
					skipping = (bVal != aVal);
					break;
				case 0x14: // IFG
					skipping = (bVal > aVal);
					break;
				case 0x15: // IFA
					skipping = (toSigned(bVal) > toSigned(aVal));
					break;
				case 0x16: // IFL
					skipping = (bVal < aVal);
					break;
				case 0x17: // IFU
					skipping = (toSigned(bVal) < toSigned(aVal));
					break;
				case 0x18: // -
				case 0x19: // -
				case 0x1a: // ADX
					res = bVal + aVal + EX;
                    EX = (ushort)((res < 0) ? 0 : 1);
					break;
				case 0x1b: // SBX
					res = bVal - aVal - EX;
                    EX = (ushort)((res > 0) ? 0 : 1);
					break;
				case 0x1c: // -
				case 0x1d: // -
				case 0x1e: // STI
					res = aVal;
					register[(int) Register.I]++;
					register[(int) Register.J]++;
					break;
				case 0x1f: // STD
					res = aVal;
					register[(int) Register.I]--;
					register[(int) Register.J]--;
					break;
			}

            write(bType, bAddr, (ushort)(res & 0xffff));
		}
		else
            {
                if (skipping)
                {
                    // Skip reading next word
                    if ((a >= 0x10 && a < 0x18) || a == 0x1a || a == 0x1e || a == 0x1f)
                        // [register + next word], [SP + next word], [next word], next word
                        tPC++;

                    skipping = false;
                    PC = tPC;
                }

                switch (b) // b becomes the special opcode
                {
                    case 0x00: // Reserved
                    case 0x01: // JSR
                        // Get jump address
                        ushort jmp = read(getType(a), addressA(a));

                        // Push PC
                        Host.writeMem(--tSP, tPC);

                        // Set PC to jump address (after increment)
                        tPC = jmp;
                        break;
                    case 0x02: // -
                    case 0x03: // -
                    case 0x04: // -
                    case 0x05: // -
                    case 0x06: // -
                    case 0x07: // -
                    case 0x08: // INT
                    case 0x09: // IAG
                    case 0x0a: // IAS
                    case 0x0b: // RFI
                    case 0x0c: // IAQ
                    case 0x0d: // -
                    case 0x0e: // -
                    case 0x0f: // -
                    case 0x10: // HWN
                    case 0x11: // HWQ
                    case 0x12: // HWI
                    case 0x13: // -
                    case 0x14: // -
                    case 0x15: // -
                    case 0x16: // -
                    case 0x17: // -
                    case 0x18: // -
                    case 0x19: // -
                    case 0x1a: // -
                    case 0x1b: // -
                    case 0x1c: // -
                    case 0x1d: // -
                    case 0x1e: // -
                    case 0x1f: // -
                        break;
                }
            }
            Host.dump(string.Format("A = {0}, B = {1}, C = {2}, X = {3}, Y = {4}, Z = {5}, I = {6}, J = {7}", register[(int)Register.A], register[(int)Register.B], register[(int)Register.C], register[(int)Register.X], register[(int)Register.Y], register[(int)Register.Z], register[(int)Register.I], register[(int)Register.J]));
        }

        private short toSigned(ushort value)
        {
            return (short) value;
        }

        private ushort read(AddressType type, ushort address)
        {
            switch (type)
            {
                case AddressType.Register:
                    return register[address];
                case AddressType.Ram:
                    return Host.readMem(address);
                case AddressType.PC:
                    return PC;
                case AddressType.SP:
                    return SP;
                case AddressType.EX:
                    return EX;
                default:
                    if (address == 0)
                        return 0xFFFF;
                    return (ushort)(address - 1);
            }
        }

        private void write(AddressType type, ushort address, ushort word)
        {
            switch (type)
            {
                case AddressType.Register:
                    register[address] = word;
                    break;
                case AddressType.Ram:
                    Host.writeMem(address, word);
                    break;
                case AddressType.PC:
                    PC = word;
                    break;
                case AddressType.SP:
                    SP = word;
                    break;
                case AddressType.EX:
                    EX = word;
                    break;
            }
        }

        private AddressType getType(ushort value)
        {
            if (value < 0x08)
                return AddressType.Register;
            if (value < 0x10)
                return AddressType.Ram;
            if (value < 0x18)
                return AddressType.Ram;
            if (value == 0x18)
                return AddressType.Ram;
            if (value == 0x19)
                return AddressType.Ram;
            if (value == 0x1a)
                return AddressType.Ram;
            if (value == 0x1b)
                return AddressType.SP;
            if (value == 0x1c)
                return AddressType.PC;
            if (value == 0x1d)
                return AddressType.EX;
            if (value == 0x1e)
                return AddressType.Ram;
            if (value == 0x1f)
                return AddressType.Ram;
            return AddressType.Literal;
        }

        private ushort addressA(ushort value)
        {
            if (value < 0x08)
                return value;
            if (value < 0x10)
                return register[value - 0x08];
            if (value < 0x18)
                return (ushort)(Host.readMem(tPC++) + register[value - 0x10]);
            if (value == 0x18)
                return tSP++;
            if (value == 0x19)
                return tSP;
            if (value == 0x1a)
                return (ushort)(Host.readMem(tPC++) + tSP);
            if (value == 0x1b)
                return 0;
            if (value == 0x1c)
                return 0;
            if (value == 0x1d)
                return 0;
            if (value == 0x1e)
                return Host.readMem(tPC++);
            if (value == 0x1f)
                return tPC++;

            return (char)(value - 0x20);
        }

        private ushort addressB(ushort value)
        {
            if (value < 0x08)
                return value;
            if (value < 0x10)
                return register[value - 0x08];
            if (value < 0x18)
                return (ushort)(Host.readMem(tPC++) + register[value - 0x10]);
            if (value == 0x18)
                return --tSP;
            if (value == 0x19)
                return tSP;
            if (value == 0x1a)
                return (ushort)(Host.readMem(tPC++) + tSP);
            if (value == 0x1b)
                return 0;
            if (value == 0x1c)
                return 0;
            if (value == 0x1d)
                return 0;
            if (value == 0x1e)
                return Host.readMem(tPC++);
            if (value == 0x1f)
                return tPC++;

            return (char)(value - 0x20);
        }

        public void dispose()
        {
        }

        public void openConfig()
        {
        }

        public string Name
        {
            get { return "ADCPU port"; }
        }
        public string Description
        {
            get { return "A port of the android version (Made in java)"; }
        }
        public string Author
        {
            get { return "sticksoft"; }
        }
        public string Version
        {
            get { return "0.1 (NON WORKING)"; }
        }
        public bool configPossible
        {
            get { return false; }
        }
    }

    public enum Register
    {
        A = 0,
        B,
        C,
        X,
        Y,
        Z,
        I,
        J,
    }

    public enum AddressType
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
