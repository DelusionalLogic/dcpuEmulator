using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultCpu
{
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

        public ushort[] toArray()
        {
            return new[] {A, B, C, X, Y, Z, I, J};
        }
    }
}
