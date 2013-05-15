namespace DefaultCpu
{
    /// <summary>
    /// A single cpu instruction
    /// </summary>
    public struct Instruction
    {
        public OPCode opCode { get; set; }
        public Address a { get; set; }
        public Address b { get; set; }

        /// <summary>
        /// Initialize a new instruction
        /// </summary>
        /// <param name="instruction">The hex value</param>
        public Instruction(ushort instruction) : this()
        {
            //Get the opcode (first 5 bits)
            opCode = (OPCode)(instruction & 0x1f);
            //Get the a address (The last 6 bits)
            a = new Address((ushort)(instruction >> 10), true, opCode == OPCode.SPECIAL);
            //Get the b address (The second middle 5 bits)
            b = new Address((ushort)((instruction >> 5) & 0x1f), false, opCode == OPCode.SPECIAL);
        }
    }
}
