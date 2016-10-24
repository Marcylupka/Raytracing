using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing
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
            } else throw new Exception("r value out of range");
            if (g >= 0 && g <= 1)
            {
                this.g = g;
            }
            else throw new Exception("g value out of range");
            if (b >= 0 && b <= 1)
            {
                this.b = b;
            }
            else throw new Exception("b value out of range");
        }

        public LightIntensity ()
        {
            this.r = 0;
            this.g = 0;
            this.b = 0;
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
            this.r += v.R;
            this.g += v.G;
            this.b += v.B;
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

        public static Vector operator *(float scalar, LightIntensity right)
        {
            return new Vector(right.r * scalar, right.g * scalar, right.b * scalar);
        }
        public static Vector operator *(LightIntensity left, float scalar)
        {
            return new Vector(left.r * scalar, left.g * scalar, left.b * scalar);
        }
        public static Vector operator *(LightIntensity left, LightIntensity right)
        {
            return new Vector(left.r * right.r, left.g * right.g, left.b * right.b);
        }
        public static Vector operator +(LightIntensity left, LightIntensity right)
        {
            return new Vector(left.r + right.r, left.g + right.g, left.b + right.b);
        }
        public static Vector operator -(LightIntensity left, LightIntensity right)
        {
            return new Vector(left.r - right.r, left.g - right.g, left.b - right.b);
        }
        public static Vector operator -(LightIntensity left)
        {
            return new Vector(-left.r, -left.g, -left.b);
        }
        public static bool operator ==(LightIntensity left, LightIntensity right)
        {
            return (left.r == right.r && left.g == right.g && left.b == right.b);
        }
        public static bool operator !=(LightIntensity left, LightIntensity right)
        {
            return (left.r != right.r || left.g != right.g || left.b != right.b);
        }
    }
}

