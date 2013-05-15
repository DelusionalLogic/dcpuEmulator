namespace DefaultCpu
{
    /// <summary>
    /// Register manager
    /// </summary>
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

        /// <summary>
        /// Reads a register
        /// </summary>
        /// <param name="index">The index of the register</param>
        /// <returns>Value of the register</returns>
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

        /// <summary>
        /// Writes to a register
        /// </summary>
        /// <param name="index">The index of the register</param>
        /// <param name="value">The value</param>
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

        /// <summary>
        /// Convert the registers to an array
        /// </summary>
        /// <returns>The registers as an array</returns>
        public ushort[] toArray()
        {
            return new[] {A, B, C, X, Y, Z, I, J};
        }

        /// <summary>
        /// Import the registers from an array
        /// </summary>
        /// <param name="registers">The register array to import</param>
        public void fromArray(ushort[] registers)
        {
            A = registers[0]; B = registers[1]; C = registers[2]; X = registers[3]; Y = registers[4]; Z = registers[5]; I = registers[6]; J = registers[7];
        }
    }
}
