using System;
using System.Windows.Forms;
using PluginInterface;

namespace DefaultScreen
{
    public partial class ScreenGui : Form
    {
        public static IPluginHost computer;

        private Character[] characters;
        private int memMap = 0;

        public ScreenGui(IPluginHost computer)
        {
            ScreenGui.computer = computer;
            characters = new Character[384]; // Screensize
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i] = new Character(70, 0, 0xE, false);
            }
            ScreenFactory.createScreen(characters);
            InitializeComponent();
        }

        public ushort[] interrupt(ushort[] registers)
        {
            switch (registers[0]) // register A
            {
                case 0x0: //MEM_MAP_SCREEN
                    memMap = registers[1]; //Register B
                    break;
                case 0x1: //MEM_MAP_FONT
                    FontManager.mapFont(registers[1]); //Register B
                    break;
                case 0x2: //MEM_MAP_PALETTE
                    ColorManager.mapPalette(registers[1]); //Register B
                    break;
                case 0x3:
                    //TODO: Add bordercolor and set it here
                    break;
                case 0x4: //MEM_DUMP_FONT
                    FontManager.dumpFont(registers[1]); // Register B
                    break;
                case 0x5: //MEM_DUMP_PALETTE
                    ColorManager.dumpPalette(registers[1]); //Register B
                    break;
            }
            return registers;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            for (int i = memMap; i < memMap + characters.Length; i++)
            {
                characters[i - memMap] = new Character(computer.readMem(i));
            }
            screenBox.Image = ScreenFactory.createScreen(characters, screenBox.Width, screenBox.Height);
        }
    }
}
