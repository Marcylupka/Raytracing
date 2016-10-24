using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Raytracing
{
    public class Picture
    {
        //public int xsize;
        //public int ysize;
        private Bitmap obraz;
        public Bitmap Obraz
        {
            get { return obraz; }
            set { obraz = value; }
        }

        public Picture(int x, int y)
        {
            this.obraz = new Bitmap(x, y, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        public void setPixel(int x, int y, LightIntensity pixel)
        {
            int red, green, blue;
            //konwersja na 0-255
            red = (int)(pixel.R * 255);
            green = (int)(pixel.G * 255);
            blue = (int)(pixel.B * 255);
            if (red > 255) red = 255;
            if (green > 255) green = 255;
            if (blue > 255) blue = 255;
            if (red < 0) red = 0;
            if (green < 0) green = 0;
            if (blue < 0) blue = 0;
            this.obraz.SetPixel(x, y, Color.FromArgb(red, green, blue));
        }
    }
}
