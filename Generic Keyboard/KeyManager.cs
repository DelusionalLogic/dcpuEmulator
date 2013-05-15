using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Generic_Keyboard
{
    class KeyManager
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        private static List<Keys> lastDown = new List<Keys>();

        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2
        }

        private static KeyStates getKeyState(Keys key)
        {
            var state = KeyStates.None;

            short retVal = GetKeyState((int)key);

            if ((retVal & 0x8000) == 0x8000)
                state |= KeyStates.Down;

            if ((retVal & 1) == 1)
                state |= KeyStates.Toggled;

            return state;
        }

        internal static bool isDown(Keys key)
        {
            return KeyStates.Down == (getKeyState(key) & KeyStates.Down);
        }

        internal static void installHooks()
        {
            int ticks = 0;
            new Thread((ThreadStart)delegate
            {
                while (true)
                {
                    DateTime dateTime = new DateTime();
                    if (((dateTime.Ticks / 10) * 10) / (ticks++ + 1) > 100000)
                        Thread.Sleep(1);
                    checkKeys();
                }
            }).Start();
        }

        private static void checkKeys()
        {
            for (int i = 65; i <= 90; i++)
            {
                Keys key = (Keys) i;
                if (isDown(key))
                {
                    if (!lastDown.Contains(key))
                    {
                        onKeyDown(key);
                        lastDown.Add(key);
                    }
                }
                else
                {
                    if (lastDown.Contains(key))
                    {
                        onKeyUp(key);
                        lastDown.Remove(key);
                    }
                }



            }
        }

        private static void onKeyDown(Keys key)
        {
            Keyboard.computer.dump(key.ToString());
            Keyboard.keyBuffer.Enqueue((ushort) key);
        }

        public static void onKeyUp(Keys key)
        {
        }
    }
}
