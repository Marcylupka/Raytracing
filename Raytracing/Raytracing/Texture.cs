using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kuc_Ray
{
    public class Texture
    {

        private Bitmap pic;
        public Bitmap Pic
        {
            get { return pic; }
            set { pic = value; }
        }

        private String path;
        public String Path
        {
            get { return path; }
            set { path = value; }
        }

        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public LightIntensity[,] ColorMap;

        public Texture()
        {
            this.width = 200;
            this.height = 200;
            this.ColorMap = new LightIntensity[this.width, this.height];
        }

        public Texture(int w, int h)
        {
            this.width = w;
            this.height = h;
            this.ColorMap = new LightIntensity[this.width, this.height];
        }

        public Texture(int w, int h, LightIntensity[,] colmap)
        {
            this.width = w;
            this.height = h;
            this.ColorMap = colmap;
        }

        public Texture(String p)
        {
            this.path = p;
            this.pic = new Bitmap(this.path, true);
            this.width = this.pic.Width;
            this.height = this.pic.Height;
            this.ColorMap = new LightIntensity[this.pic.Width, this.pic.Height];
            for (int i = 0; i < this.pic.Width; i++)
            {
                for (int j = 0; j < this.pic.Height; j++)
                {
                    Color pixelColor = this.pic.GetPixel(i, j);

                    this.ColorMap[i, j] = new LightIntensity((float)pixelColor.R / 255f, (float)pixelColor.G / 255f, (float)pixelColor.B / 255f);
                }
            }
        }

        public LightIntensity pixelColorSph(Vector vert, float radius, Vector center)
        {
            //Console.WriteLine("wszedlem");

            LightIntensity result = new LightIntensity(0, 0, 0);
            
            Vector lp = vert - center;
            double yDIVr = (double)lp.Y / (double)radius;
            double v = 0;
            if (Math.Abs(yDIVr - 1.0f) < 0.00005f) v = 0f;
            else if (Math.Abs(yDIVr + 1.0f) < 0.00005f) v = 1f;
            else
            {
                if (yDIVr < -1f) yDIVr = -1f;
                else if (yDIVr > 1f) yDIVr = 1f;
                v = (double)Math.Acos(yDIVr) / (double)Math.PI;
            }
            double rMULsin = radius * (double)Math.Sin((double)Math.PI * v);
            double u = 0;
            if (Math.Abs(v) < 0.00005f)
            {
                u = 0d;
            }
            else if (Math.Abs(v - 1.0f) < 0.00005f)
            {
                u = 0d;
            }
            else if (Math.Abs(lp.X - rMULsin) < 0.00005f)
            {
                u = 0d;
            }
            else if (Math.Abs(lp.X + rMULsin) < 0.00005f)
            {
                u = 0.5d;
            }
            else
            {
                double newCos = lp.X / (double)rMULsin;

                if (newCos < -1d)
                {
                    newCos = -1d;
                }
                else if (newCos > 1d)
                {
                    newCos = 1d;
                }

                u = (double)((double)Math.Acos(newCos)) / (double)(2 * Math.PI);
                u = u * this.pic.Width;
                v = v * this.pic.Height;

                int newu = (int)Math.Round(u);
                int newv = (int)Math.Round(v);

                if (newu > this.width)
                {
                    newu -= this.width;
                }
                else if (newu < 0)
                {
                    newu += this.width;
                } else if (newv > this.height)
                {
                    newv -= this.height;
                }
                else if (newv < 0)
                {
                    newv += this.height;
                }
                
                result = this.ColorMap[newu, newv];
            }
            
            /*
        double TWOPI = Math.PI * 2;
        double INV_TWOPI = 1 / (Math.PI * 2);

            int[] coords = new int[2];
            Vector d = (vert - center);
            d.normalize();

            double thetha = Math.Acos(d.Y);

            double phi = Math.Atan2(d.X, d.Z);

            if (phi < 0.0d)
            {
                phi += TWOPI;
            }

            var u = phi * INV_TWOPI;
            var v = 1.0 - thetha * INV_TWOPI;

            coords[0] = (int)((this.width - 1) * v);
            coords[1] = (int)((this.height - 1) * u);

            result = this.ColorMap[coords[0], coords[1]];
            */
            return result;
        }

        
        public LightIntensity pixelColorPl(Vector vert, Vector normal)
        {
            LightIntensity result = new LightIntensity(0, 0, 0);

            int u = (int)Math.Round((vert.X + 1) / 2);
            int v = (int)Math.Round((vert.Z + 1) / 2);

            int pixelX = (this.width - 1) * u;
            int pixelY = this.height - (int)((this.height - 1) * v) - 1;

            while (pixelX >= this.width)
            {
                pixelX -= this.width;
            }
            while (pixelY >= this.height)
            {
                pixelY -= this.height;
            }
            while (pixelX < 0)
            {
                pixelX += this.width;
            }
            while (pixelY < 0)
            {
                pixelY += this.height;
            }

            result = this.ColorMap[pixelX, pixelY];

            return result;
        }
    }
}
