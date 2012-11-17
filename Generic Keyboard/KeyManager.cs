using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Generic_Keyboard
{
    class KeyManager
    {
        private static List<Keys> keysDown = new List<Keys>();
        internal static globalKeyboardHook ghk = new globalKeyboardHook();

        internal static bool isDown(Keys key)
        {
            if (keysDown.Contains(key))
                return true;
            return false;
        }

        internal static void installHooks()
        {
            ghk.hook();
            for (var i = (int) Keys.A; i <= (int) Keys.Z; i++ )
                ghk.HookedKeys.Add((Keys) i);

            ghk.HookedKeys.Add(Keys.Back);
            ghk.HookedKeys.Add(Keys.Return);
            ghk.HookedKeys.Add(Keys.Insert);
            ghk.HookedKeys.Add(Keys.Delete);

            ghk.HookedKeys.Add(Keys.Up);
            ghk.HookedKeys.Add(Keys.Down);
            ghk.HookedKeys.Add(Keys.Left);
            ghk.HookedKeys.Add(Keys.Right);

            ghk.HookedKeys.Add(Keys.Shift);
            ghk.HookedKeys.Add(Keys.Control);

            ghk.KeyDown += onKeyDown;
            ghk.KeyUp += onKeyUp;
        }

        private static void onKeyDown(object sender, KeyEventArgs e)
        {
            Keyboard.computer.dump(e.KeyCode.ToString());
            keysDown.Add(e.KeyCode);
            switch (e.KeyCode)
            {
                case Keys.Back:
                    Keyboard.keyBuffer.Enqueue(0x10);
                    break;
                    case Keys.Return:
                    Keyboard.keyBuffer.Enqueue(0x11);
                    break;
                    case Keys.Insert:
                    Keyboard.keyBuffer.Enqueue(0x12);
                    break;
                    case Keys.Delete:
                    Keyboard.keyBuffer.Enqueue(0x13);
                    break;
                case Keys.Up:
                    Keyboard.keyBuffer.Enqueue(0x80);
                    break;
                    case Keys.Down:
                    Keyboard.keyBuffer.Enqueue(0x81);
                    break;
                case Keys.Left:
                    Keyboard.keyBuffer.Enqueue(0x82);
                    break;
                    case Keys.Right:
                    Keyboard.keyBuffer.Enqueue(0x83);
                    break;
                case Keys.Shift:
                    Keyboard.keyBuffer.Enqueue(0x90);
                    break;
                    case Keys.ControlKey:
                    Keyboard.keyBuffer.Enqueue(0x91);
                    break;
                default:
                    if(e.KeyCode >= Keys.A && Keys.Z >= e.KeyCode)
                        Keyboard.keyBuffer.Enqueue((ushort) e.KeyCode);
                    else
                        Keyboard.computer.dump(e.KeyCode.ToString());
                    break;
            }
            Keyboard.computer.interruptCPU(Keyboard.message);
        }

        public static void onKeyUp(object sender, KeyEventArgs e)
        {
            keysDown.Remove(e.KeyCode);
            Keyboard.computer.interruptCPU(Keyboard.message);
        }
    }
}
