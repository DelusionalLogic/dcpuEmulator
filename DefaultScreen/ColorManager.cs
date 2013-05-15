using System.Drawing;

namespace DefaultScreen
{
    /// <summary>
    /// Manages the color palette
    /// </summary>
    internal class ColorManager
    {
        /// <summary>
        /// The address of the palette, 0 for default
        /// </summary>
        private static ushort _address = 0;

        /// <summary>
        /// Get the color of the id
        /// </summary>
        /// <param name="id">The id of the color in the palet</param>
        /// <returns>The color at the position</returns>
        internal static Color getColor(int id)
        {
            ushort color = _address == 0 ? DefaultColor[id] : ScreenGui.computer.readMem(_address + id);

            return Color.FromArgb(((color >> 8) & 0xF) * 17, ((color >> 4) & 0xF) * 17, (color & 0xF) * 17);
        }

        /// <summary>
        /// Set the palette address
        /// </summary>
        /// <param name="newAddress">The new address</param>
        internal static void mapPalette(ushort newAddress)
        {
            _address = newAddress;
        }

        /// <summary>
        /// Dumps the palette to the memory
        /// </summary>
        /// <param name="address">The address</param>
        internal static void dumpPalette(ushort address)
        {
            foreach (var color in DefaultColor)
            {
                ScreenGui.computer.writeMem(address++, color);
            }
        }

        /// <summary>
        /// The default palette
        /// </summary>
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