using System.Drawing;

namespace DefaultScreen
{
    internal class ColorManager
    {
        private static ushort address = 0;

        internal static Color getColor(int id)
        {
            ushort color;
            if(address == 0)
                color = DefaultColor[id];
            else
            {
                color = ScreenGui.computer.readMem(address + id);
            }

            return Color.FromArgb(((color >> 8) & 0xF) * 17, ((color >> 4) & 0xF) * 17, (color & 0xF) * 17);
        }

        internal static void mapPalette(ushort newAddress)
        {
            address = newAddress;
        }

        internal static void dumpPalette(ushort address)
        {
            foreach (var color in DefaultColor)
            {
                ScreenGui.computer.writeMem(address++, color);
            }
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