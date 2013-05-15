namespace DefaultScreen
{
    /// <summary>
    /// Controls the screen font
    /// </summary>
    internal class FontManager
    {
        /// <summary>
        /// The address of the font, 0 for default
        /// </summary>
        private static ushort _address = 0;

        /// <summary>
        /// Get the charater in the font position
        /// </summary>
        /// <param name="id">The position</param>
        /// <returns>The charater in the font at the position</returns>
        internal static uint getChar(ushort id)
        {
            //If using the defualt font get it from there
            if(_address == 0)
                return DefaultFont[id];

            //Read the address to get the font
            uint font = ScreenGui.computer.readMem(_address + id);
            font = (font << 16) + ScreenGui.computer.readMem(_address + id + 1);
            return font;
        }

        /// <summary>
        /// Set the address of the font
        /// </summary>
        /// <param name="newAddress">The new address of the font</param>
        internal static void mapFont(ushort newAddress)
        {
            _address = newAddress;
        }

        /// <summary>
        /// Dump the default font to memory
        /// </summary>
        /// <param name="address">The address of the dump</param>
        internal static void dumpFont(ushort address)
        {
            foreach (var letter in DefaultFont)
            {
                var word1 = (ushort)(letter & 0xFFFF);
                ScreenGui.computer.writeMem(address++, word1);
                var word2 = (ushort)((letter >> 16) & 0xFFFF);
                ScreenGui.computer.writeMem(address++, word1);
            }
        }

        private static readonly uint[] DefaultFont = new uint[]
                                                         {
                                                             2386075319,
                                                             4101319794,
                                                             2407512857,
                                                             1488058757,
                                                             2371108,
                                                             535048,
                                                             2048,
                                                             134744072,
                                                             65280,
                                                             134805504,
                                                             63496,
                                                             3848,
                                                             134745856,
                                                             134807296,
                                                             134805512,
                                                             65288,
                                                             134745864,
                                                             134807304,
                                                             3432592230,
                                                             3429249945,
                                                             2162227454,
                                                             17244031,
                                                             2132739841,
                                                             4277723264,
                                                             11141205,
                                                             2857740885,
                                                             1442818815,
                                                             252645135,
                                                             4042322160,
                                                             4294901760,
                                                             65535,
                                                             4294967295,
                                                             0,
                                                             24320,
                                                             196611,
                                                             4068414,
                                                             3304230,
                                                             4398177,
                                                             1349921078,
                                                             66048,
                                                             4268572,
                                                             1843777,
                                                             1312788,
                                                             531464,
                                                             8256,
                                                             526344,
                                                             16384,
                                                             203872,
                                                             4081982,
                                                             4226882,
                                                             4610402,
                                                             3557666,
                                                             8325135,
                                                             3753255,
                                                             3295550,
                                                             465249,
                                                             3557686,
                                                             4081958,
                                                             9216,
                                                             9280,
                                                             1092752392,
                                                             1315860,
                                                             135537217,
                                                             416002,
                                                             6183230,
                                                             8259966,
                                                             3557759,
                                                             2244926,
                                                             4079999,
                                                             4278655,
                                                             67967,
                                                             8012094,
                                                             8325247,
                                                             4292417,
                                                             4145184,
                                                             7800959,
                                                             4210815,
                                                             8324735,
                                                             8257919,
                                                             4079934,
                                                             395647,
                                                             12468542,
                                                             7735679,
                                                             3295526,
                                                             98049,
                                                             4145215,
                                                             2056223,
                                                             8335487,
                                                             7800951,
                                                             489479,
                                                             4671857,
                                                             4292352,
                                                             6298627,
                                                             8339712,
                                                             131330,
                                                             8421504,
                                                             131328,
                                                             7885860,
                                                             3687551,
                                                             2638904,
                                                             8340536,
                                                             5788728,
                                                             622088,
                                                             3953736,
                                                             7865471,
                                                             4226372,
                                                             4014112,
                                                             7082111,
                                                             4226881,
                                                             8132732,
                                                             7865468,
                                                             3687480,
                                                             529532,
                                                             8131592,
                                                             525436,
                                                             2380872,
                                                             4472324,
                                                             8142908,
                                                             1859612,
                                                             8138876,
                                                             7082092,
                                                             3952716,
                                                             5002340,
                                                             4273672,
                                                             30464,
                                                             538177,
                                                             16908546,
                                                             132354,
                                                         };
    }
}