using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class LightIntensity
    {
        private float r;
        public float R
        {
            get { return r; }
            set { r = value; }
        }

        private float g;
        public float G
        {
            get { return g; }
            set { g = value; }
        }

        private float b;
        public float B
        {
            get { return b; }
            set { b = value; }
        }

        public LightIntensity(float r, float g, float b)
        {
            if (r >= 0 && r <= 1)
            {
                this.r = r;
            }
            else
            {
                if (r < 0) r = 0;
                if (r > 1) r = 1;
            }
            //throw new Exception("r value out of range");
            if (g >= 0 && g <= 1)
            {
                this.g = g;
            }
            else
            {
                if (g < 0) g = 0;
                if (g > 1) g = 1;
            }
            //throw new Exception("g value out of range");
            if (b >= 0 && b <= 1)
            {
                this.b = b;
            }
            else
            {
                if (b < 0) b = 0;
                if (b > 1) b = 1;
            }
            //throw new Exception("b value out of range");
        }


        public LightIntensity ()
        {
            this.r = 0;
            this.g = 0;
            this.b = 0;
        }

        public LightIntensity (LightIntensity source)
        {
            this.r = source.r;
            this.b = source.b;
            this.g = source.g;
        }

        /*public Vector convertToVector ()
        {
            return (this * 255);
        }*/

        public override string ToString()
        {
            return "(" + r.ToString() + "," + g.ToString() + "," + b.ToString() + ")";
        }

        public void add(LightIntensity v)
        {
            if (this.r + v.R > 1) this.r = 1;
            else this.r += v.R;
            if (this.g + v.G > 1) this.g = 1;
            else this.g += v.G;
            if (this.b + v.B > 1) this.b = 1;
            else this.b += v.B;
        }

        public void sub(LightIntensity v)
        {
            this.r -= v.R;
            this.g -= v.G;
            this.b -= v.B;
        }

        public void div(float f)
        {
            if (f != 0)
            {
                this.r /= f;
                this.g /= f;
                this.b /= f;
            }
            else
                throw new Exception("Can't divide by 0");
        }

        #region Operators

        public static LightIntensity operator *(float scalar, LightIntensity right)
        {
            float g = right.g * scalar;
            if (g < 0) g = 0;
            if (g > 1) g = 1;
            float b = right.b * scalar;
            if (b < 0) b = 0;
            if (b > 1) b = 1;
            float r = right.r * scalar;
            if (r < 0) r = 0;
            if (r > 1) r = 1;
            return new LightIntensity(r, g, b);
        }
        public static LightIntensity operator *(LightIntensity left, float scalar)
        {
            float g = left.g * scalar;
            if (g < 0) g = 0;
            if (g > 1) g = 1;
            float b = left.b * scalar;
            if (b < 0) b = 0;
            if (b > 1) b = 1;
            float r = left.r * scalar;
            if (r < 0) r = 0;
            if (r > 1) r = 1;
            return new LightIntensity(r,g,b);
        }
        public static LightIntensity operator *(LightIntensity left, LightIntensity right)
        {
            float g = left.g * right.g;
            if (g < 0) g = 0;
            if (g > 1) g = 1;
            float b = left.b * right.b;
            if (b < 0) b = 0;
            if (b > 1) b = 1;
            float r = left.r * right.r;
            if (r < 0) r = 0;
            if (r > 1) r = 1;
            return new LightIntensity(r,g,b);
        }
        /*public static LightIntensity operator *(Vector vec, LightIntensity right)
        {
            return new LightIntensity(right.r * vec.X, right.g * vec.Y, right.b * vec.Z);
        }*/
        public static LightIntensity operator +(LightIntensity left, LightIntensity right)
        {
            float g = left.g + right.g;
            if (g < 0) g = 0;
            if (g > 1) g = 1;
            float b = left.b + right.b;
            if (b < 0) b = 0;
            if (b > 1) b = 1;
            float r = left.r + right.r;
            if (r < 0) r = 0;
            if (r > 1) r = 1;
            return new LightIntensity(r, g, b);
        }
        public static LightIntensity operator -(LightIntensity left, LightIntensity right)
        {
            float g = left.g - right.g;
            if (g < 0) g = 0;
            if (g > 1) g = 1;
            float b = left.b - right.b;
            if (b < 0) b = 0;
            if (b > 1) b = 1;
            float r = left.r - right.r;
            if (r < 0) r = 0;
            if (r > 1) r = 1;
            return new LightIntensity(r, g, b);
        }
        public static LightIntensity operator -(LightIntensity left)
        {
            return new LightIntensity(-left.r, -left.g, -left.b);
        }
        public static bool operator ==(LightIntensity left, LightIntensity right)
        {
            return (left.r == right.r && left.g == right.g && left.b == right.b);
        }
        public static bool operator !=(LightIntensity left, LightIntensity right)
        {
            return (left.r != right.r || left.g != right.g || left.b != right.b);
        }

        #endregion Operators
    }
}

