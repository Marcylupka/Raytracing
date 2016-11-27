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
            this.ColorMap = new LightIntensity[this.width,this.height];
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

                    ColorMap[i, j] = new LightIntensity((float)pixelColor.R / 255f, (float)pixelColor.G / 255f, (float)pixelColor.B / 255f);

                   // this.ColorMap[i, j].R = (float)(pixelColor.R/255);
                   // this.ColorMap[i, j].G = (float)(pixelColor.G/255);
                   // this.ColorMap[i, j].B = (float)(pixelColor.B/255);
                }
            }
        }

        public LightIntensity pixelColorSph (Vector vert, float radius, Vector center)
        {
            //Console.WriteLine("wszedlem");

            LightIntensity result = new LightIntensity(1,1,1);

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
                //Console.WriteLine("case 1");
            }
            else if (Math.Abs(v - 1.0f) < 0.00005f)
            {
                u = 0d;
                //Console.WriteLine("case 2");
            }
            else if (Math.Abs(lp.X - rMULsin) < 0.00005f)
            {
                u = 0d;
                //Console.WriteLine("case 3");
            }
            else if (Math.Abs(lp.X + rMULsin) < 0.00005f)
            {
                u = 0.5d;
                //Console.WriteLine("case 4");
            } else
            {
                try
                {
                    //Math.Acos(lp.X / (double)rMULsin);
                    u = (double)((double)Math.Acos(lp.X / (double)rMULsin)) / (double)(2 * Math.PI);
                    u = u * this.pic.Width;
                    v = v * this.pic.Height;

                    int newu = (int)Math.Round(u);
                    int newv = (int)Math.Round(v);

                    //Console.WriteLine(newu + ", " + newv);
                    //Console.WriteLine(ColorMap[newu, newv]);
                    result = this.ColorMap[newu, newv];
                }
                catch (Exception)
                {
                    u = 0d;
                    u = u * this.pic.Width;
                    v = v * this.pic.Height;

                    int newu = (int)Math.Round(u);
                    int newv = (int)Math.Round(v);

                    //Console.WriteLine(newu + ", " + newv);
                    //Console.WriteLine(ColorMap[newu, newv]);
                    result = this.ColorMap[newu, newv];
                }


                //Console.WriteLine(rMULsin + ", " + lp.X + ", " + Math.Acos(lp.X / (double)rMULsin));
                //u = (double)((double)Math.Acos(lp.X / (double)rMULsin)) / (double)(2 * Math.PI);
                //Console.WriteLine("case 5");
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
            /*
             * // przekształcenie punktu z przestrzeni sfery na przestrzeń tekstury
inline vec2 mapSurfaceToTexture(sphere::i s, vec3::i p) {
  vec3 lp = p - s.center;
 
  float yDIVr = lp.y / s.radius;
  float v = 
    ( abs(yDIVr - 1.0f) < epsilon5 ) ? 0.0f : // v = 0 (górny biegun)
    ( abs(yDIVr + 1.0f) < epsilon5 ) ? 1.0f : // v = 1 (dolny biegun)
    acos(yDIVr) / Pi;                         // bez osobliwości
 
  float rMULsin = s.radius * sin(Pi*v);
  float u = // 1,2 -> biegun // 3,4 -> |x/rMULsin|=1 -> acos nieokreślony
    ( abs(v) < epsilon5 ) ? 0.0f :               // 1) (u,v) = (0.0, 0)
    ( abs(v - 1.0f) < epsilon5 ) ? 0.0f :        // 2) (u,v) = (0.0, 1)
    ( abs(lp.x - rMULsin) < epsilon5 ) ? 0.0f :  // 3) (u,v) = (0.0, v) 
    ( abs(lp.x + rMULsin) < epsilon5 ) ? 0.5f :  // 4) (u,v) = (0.5, v)
    acos(lp.x / rMULsin) / PiMul2;               // bez osobliwości
 
  return vec2(u, v);


              return vec3(
    s.center.x + s.radius * sin(Pi * p.v) * cos(PiMul2 * p.u),
    s.center.y + s.radius * cos(Pi * p.v),
    s.center.z + s.radius * sin(Pi * p.v) * sin(PiMul2 * p.u)
  */


            /*Vector igrek = new Vector(0, 1, 0);
            float angle = vert.dot(igrek);
            int u = (int)((float)(Math.Acos(vert.X / (radius * (float)Math.Sin(angle))))/(float)(2*Math.PI));
            int v = (int)(((float)(Math.Acos(vert.Y / (float)radius))) / ((float)Math.PI));
            LightIntensity result = new LightIntensity();
            result = this.ColorMap[u, v];*/

            return result;
        }

    }
}
