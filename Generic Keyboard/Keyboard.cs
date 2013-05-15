using System.Collections.Generic;
using PluginInterface;

namespace Generic_Keyboard
{
    class Keyboard
    {
        internal static IPluginHost computer;

        internal static Queue<ushort> keyBuffer = new Queue<ushort>();
        internal static ushort message = 0x0;

        public Keyboard(IPluginHost computer)
        {
            Keyboard.computer = computer;
            KeyManager.installHooks();
        }

        public ushort[] interrupt(ushort[] registers)
        {
            switch (registers[0]) //Register A
            {
                case 0x0: //CLEAR_BUFFER
                    keyBuffer.Clear();
                    break;
                case 0x1: //GETKEY
                    if (keyBuffer.Count > 0)
                        registers[2] = keyBuffer.Dequeue(); //Register C
                    else
                        registers[2] = 0; //Register C
                    break;
            }
            return registers;
        }
    }
}
