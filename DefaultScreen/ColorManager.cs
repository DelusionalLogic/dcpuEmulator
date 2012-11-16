using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultScreen
{
    internal class ColorManager
    {
        internal static Color getColor(int id)
        {
            ushort color = DefaultColor[id];

            return Color.FromArgb(((color >> 8) & 0xF) * 17, ((color >> 4) & 0xF) * 17, (color & 0xF) * 17);
        }

        private static readonly ushort[] DefaultColor = new ushort[]
                                    {
                                        0x0000,
                                        0x000A,
                                        0x00A0,
                                        0x00AA,
                                        0x0A00,
                                        0x0A0A,
                                        0x0A50,
                                        0x0AAA,
                                        0x0555,
                                        0x055F,
                                        0x05F5,
                                        0x05FF,
                                        0x0F55,
                                        0x0F5F,
                                        0x0FF5,
                                        0x0FFF,
                                    };
    }
}