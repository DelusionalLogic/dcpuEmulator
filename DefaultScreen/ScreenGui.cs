using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PluginInterface;

namespace DefaultScreen
{
    public partial class ScreenGui : Form
    {
        private readonly IPluginHost computer;

        private Character[] characters;
        private int memMap = 0;

        public ScreenGui(IPluginHost computer)
        {
            this.computer = computer;
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
                    int newAdd = registers[1]; // register B
                    memMap = newAdd;
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
