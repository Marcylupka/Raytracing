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
            //Console.WriteLine(ColorMap.Length);
            for (int i = 0; i < this.pic.Width; i++)
            {
                for (int j = 0; j < this.pic.Height; j++)
                {
                    //ColorMap[i, j] = new LightIntensity(0, 0, 0);
                    //Console.WriteLine("wartosc mapy " + ColorMap[i, j]);
                    //this.pic.GetPixel(i,j).
                    Color pixelColor = this.pic.GetPixel(i, j);

                    this.ColorMap[i, j] = new LightIntensity((float)pixelColor.R / 255f, (float)pixelColor.G / 255f, (float)pixelColor.B / 255f);

                    // this.ColorMap[i, j].R = (float)(pixelColor.R/255);
                    // this.ColorMap[i, j].G = (float)(pixelColor.G/255);
                    // this.ColorMap[i, j].B = (float)(pixelColor.B/255);
                }
            }
        }

        public LightIntensity pixelColorSph(Vector vert, float radius, Vector center)
        {
            //Console.WriteLine("wszedlem");

            LightIntensity result = new LightIntensity(0, 0, 0);

            //float u = 0.5f + Math.Atan(2*)
            Vector move = new Vector(0, 0, 0) - center;
            //vert = vert + move;

            Vector lp = vert - center;
            double yDIVr = (double)lp.Y / (double)radius;
            double v = 0;
            if (Math.Abs(yDIVr - 1.0f) < 0.00005f) v = 0f;
            else if (Math.Abs(yDIVr + 1.0f) < 0.00005f) v = 1f;
            else v = (double)Math.Acos(yDIVr) / (double)Math.PI;
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
                //try
                //{
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
                //u = (double)((double)Math.Acos(lp.X / (double)rMULsin)) / (double)(2 * Math.PI);
                u = u * this.pic.Width;
                v = v * this.pic.Height;

                int newu = (int)Math.Round(u);
                int newv = (int)Math.Round(v);

                result = this.ColorMap[newu, newv];
                //}
                /*catch (Exception)
                {
                    u = 0d;
                    u = u * this.pic.Width;
                    v = v * this.pic.Height;

                    int newu = (int)Math.Round(u);
                    int newv = (int)Math.Round(v);

                    //Console.WriteLine(newu + ", " + newv);
                    //Console.WriteLine(ColorMap[newu, newv]);
                    result = this.ColorMap[newu, newv];
                }*/


                //Console.WriteLine(rMULsin + ", " + lp.X + ", " + Math.Acos(lp.X / (double)rMULsin));
                //u = (double)((double)Math.Acos(lp.X / (double)rMULsin)) / (double)(2 * Math.PI);
            }
            /*
            u = u * this.pic.Width;
            v = v * this.pic.Height;

            int newu = (int)Math.Round(u);
            int newv = (int)Math.Round(v);

            //Console.WriteLine(newu + ", " + newv);
            //Console.WriteLine(ColorMap[newu, newv]);
            result = this.ColorMap[newu, newv];
            */


            /*Vector igrek = new Vector(0, 1, 0);
            float angle = vert.dot(igrek);
            int u = (int)((float)(Math.Acos(vert.X / (radius * (float)Math.Sin(angle))))/(float)(2*Math.PI));
            int v = (int)(((float)(Math.Acos(vert.Y / (float)radius))) / ((float)Math.PI));
            LightIntensity result = new LightIntensity();
            result = this.ColorMap[u, v];*/

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
        /*
        public LightIntensity pixelColorPl(Vector vert)
        {
            LightIntensity result = new LightIntensity(0, 0, 0);

            float u, v;
            u = ((vert.X + 1.0f) / 2.0f);
            v = ((vert.Y + 1.0f) / 2.0f);

            int column = (int)Math.Round((this.width - 1f) * u);
            int row = (int)Math.Round((this.height - 1.0f) * v) - 1;

            result = this.ColorMap[column, row];

            return result;
        }
        */
    }
}
