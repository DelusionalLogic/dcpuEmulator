using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace DefaultScreen
{
    internal class ScreenFactory
    {
        public static Bitmap createScreen(Character[] buffer)
        {
            var screen = new Bitmap(128, 96);

            int i = 0;
            foreach (var character in buffer)
            {
                int x = i%32;
                int y = (int) Math.Floor((double) (i/32));

                uint charData = (FontManager.getChar(character.id));

                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        Color color = ColorManager.getColor(character.backColor);
                        if (((charData >> (j*8) + k) & 0x1) == 1)
                            color = ColorManager.getColor(character.color);

                        screen.SetPixel(x*4 + j, y*8 + k, color);
                    }
                }
                i++;
            }
            //screen.Save("bitmap.png", ImageFormat.Png);
            return screen;
        }

        public static Image createScreen(Character[] characters, int width, int height)
        {
            Bitmap source = createScreen(characters);
            var dest = new Bitmap(width, height);

            using(Graphics g = Graphics.FromImage(dest))
            {
                g.SmoothingMode = SmoothingMode.None;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.DrawImage(source,new Rectangle(0,0, width, height));
            }
            return dest;
        }
    }
}