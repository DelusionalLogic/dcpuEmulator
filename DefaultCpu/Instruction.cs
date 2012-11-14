using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultCpu
{
    public struct Instruction
    {
        public OPCode opCode { get; set; }
        public Address a { get; set; }
        public Address b { get; set; }

        public Instruction(ushort instruction) : this()
        {
            opCode = (OPCode)(instruction & 0x1f);
            b = new Address((ushort)((instruction >> 5) & 0x1f), false);
            a = new Address((ushort)(instruction >> 10), true);
        }
    }
}
