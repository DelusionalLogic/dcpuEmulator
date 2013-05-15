using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DefaultScreen
{
    /// <summary>
    /// Creates a screen image
    /// </summary>
    internal class ScreenFactory
    {
        /// <summary>
        /// Creates the screen bitmap from a buffer
        /// </summary>
        /// <param name="buffer">The buffer</param>
        /// <returns>Bitmap of the screen</returns>
        public static Bitmap createScreen(Character[] buffer)
        {
            var screen = new Bitmap(128, 96);

            int i = 0;
            //Loop through each charater in the buffer
            foreach (var character in buffer)
            {
                //Convert the index to a screen position
                int x = i%32;
                int y = (int) Math.Floor((double) (i/32));

                //Get the charater
                uint charData = (FontManager.getChar(character.id));

                //Print the charater code 4*8 with the selected color
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        //Get the charater background and foreground color
                        Color color = ColorManager.getColor(character.backColor);
                        if (((charData >> (j*8) + k) & 0x1) == 1)
                            color = ColorManager.getColor(character.color);

                        //Set the pixel at the position
                        screen.SetPixel(x*4 + j, y*8 + k, color);
                    }
                }
                i++;
            }
            //Save the image, useful for debugging
            //screen.Save("bitmap.png", ImageFormat.Png);
            return screen;
        }

        /// <summary>
        /// Creates the screen
        /// </summary>
        /// <param name="characters">The character array</param>
        /// <param name="width">The width of the screen</param>
        /// <param name="height">The height of the screen</param>
        /// <returns>The image to draw on the screen</returns>
        public static Image createScreen(Character[] characters, int width, int height)
        {
            //Create the source and the destination bitmaps
            Bitmap source = createScreen(characters);
            var dest = new Bitmap(width, height);

            using(Graphics g = Graphics.FromImage(dest))
            {
                //Turn off smoothing of pixels, antialiasing and blending
                g.SmoothingMode = SmoothingMode.None;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                //Draw the source on the target
                g.DrawImage(source,new Rectangle(0,0, width, height));
            }
            return dest;
        }
    }
}