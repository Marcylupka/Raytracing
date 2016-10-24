using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Raytracing
{
    public class Sampler
    {
        private int minimum;
        public int Minimum
        {
            get { return minimum; }
            set { minimum = value; }
        }

        private int maximum;
        public int Maximum
        {
            get { return maximum; }
            set { maximum = value; }
        }

        private float contrast;
        public float Contrast
        {
            get { return contrast; }
            set { contrast = value; }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public Sampler()
        {
            this.minimum = 1;
            this.maximum = 11;
            this.contrast = 0.2f;
            this.count = 1;
        }

        public Sampler(int min, int max, float con, int cou)
        {
            this.minimum = min;
            this.maximum = max;
            this.contrast = con;
            this.count = cou;
        }

        public Sampler(int min, int max, float con)
        {
            this.minimum = min;
            this.maximum = max;
            this.contrast = con;
            this.count = 1;
        }

        public Boolean contrastFunc(LightIntensity a, LightIntensity b, LightIntensity c, LightIntensity d)
        {
            if (((Math.Abs(a.R - b.R)) < this.contrast) && ((Math.Abs(a.G - b.G)) < this.contrast) && ((Math.Abs(a.B - b.B)) < this.contrast) && ((Math.Abs(a.R - c.R)) < this.contrast) && ((Math.Abs(a.G - c.G)) < this.contrast) && ((Math.Abs(a.B - c.B)) < this.contrast) &&
                ((Math.Abs(a.R - d.R)) < this.contrast) && ((Math.Abs(d.G - b.G)) < this.contrast) && ((Math.Abs(b.B - c.B)) < this.contrast) && ((Math.Abs(b.R - c.R)) < this.contrast) && ((Math.Abs(b.G - c.G)) < this.contrast) && ((Math.Abs(b.B - c.B)) < this.contrast) &&
                ((Math.Abs(b.R - d.R)) < this.contrast) && ((Math.Abs(b.G - d.G)) < this.contrast) && ((Math.Abs(b.B - d.B)) < this.contrast) && ((Math.Abs(c.R - d.R)) < this.contrast) && ((Math.Abs(c.G - d.G)) < this.contrast) && ((Math.Abs(c.B - d.B)) < this.contrast))
            {
                return true;
            }
            else return false;
        }

        public Boolean adaptiveSampling(LightIntensity pixA, LightIntensity pixB, LightIntensity pixC, LightIntensity pixD, LightIntensity pixE, ref int iter, ref LightIntensity res)
        {
            if (this.contrastFunc(pixA, pixB, pixC, pixD))
            {
                //LightIntensity res = new LightIntensity();
                res.R += (float)((pixA.R + pixB.R + pixC.R + pixD.R + pixE.R * 4f) / 8f / Math.Pow(4, (double)iter));
                res.G += (float)((pixA.G + pixB.G + pixC.G + pixD.G + pixE.G * 4f) / 8f / Math.Pow(4, (double)iter));
                res.B += (float)((pixA.B + pixB.B + pixC.B + pixD.B + pixE.B * 4f) / 8f / Math.Pow(4, (double)iter));
                this.count += 5;
                iter++;
                return true;
            }
            else if (this.count < this.maximum)
            {
                LightIntensity res1 = new LightIntensity();
                res1.R = (float)((pixA.R + pixB.R + pixC.R + pixD.R + pixE.R * 3f) / 8f / Math.Pow(4, (double)iter));
                res1.G = (float)((pixA.G + pixB.G + pixC.G + pixD.G + pixE.G * 3f) / 8f / Math.Pow(4, (double)iter));
                res1.B = (float)((pixA.B + pixB.B + pixC.B + pixD.B + pixE.B * 3f) / 8f / Math.Pow(4, (double)iter));
                res.R += res1.R;
                res.G += res1.G;
                res.B += res1.B;
                this.count += 3;
                iter++;
                return false;
            }
            else
            {
                res.R += (float)((pixA.R + pixB.R + pixC.R + pixD.R + pixE.R * 4f) / 8f / Math.Pow(4, (double)iter));
                res.G += (float)((pixA.G + pixB.G + pixC.G + pixD.G + pixE.G * 4f) / 8f / Math.Pow(4, (double)iter));
                res.B += (float)((pixA.B + pixB.B + pixC.B + pixD.B + pixE.B * 4f) / 8f / Math.Pow(4, (double)iter));
                this.count += 5;
                iter++;
                return true;
            }
        }
    }
}
