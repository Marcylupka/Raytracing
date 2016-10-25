using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class Vector
    {
        private float x;
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float y;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        private float z;
        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        /*
        private LightIntensity color;
        public LightIntensity Color
        {
            get { return color; }
            set { color = value; }
        }
        */

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector()
        {
            this.x = 1;
            this.y = 1;
            this.z = 1;
        }

        /*
         * public Vector operator +(Vector v1, Vector v2)
        {
            this.x = v2.X - v1.X;
            this.y = v2.Y - v1.Y;
            this.z = v2.Z - v1.Z;
        }
        */

        public Vector(Vector v)
        {
            this.x = v.X;
            this.y = v.Y;
            this.z = v.Z;
        }

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + "," + z.ToString() + ")";
        }

        public void normalize()
        {
            float n = this.length;
            if (n != 0)
            {
                this.div(n);
            }
        }

        public Vector normalizeProduct()
        {
            Vector newV = new Vector(this.x, this.y, this.z);
            float n = this.length;
            try
            {
                newV.div(n);
            }
            catch (Exception)
            {
                Console.WriteLine("null vector, couldn't normalize");
            }
            return newV;
            /* if (n != 0)
             {
                 newV.div(n);
                 return newV;
             }
             else
                 //throw new Exception(throw new Exception("null vector, couldn't normalize"));
                 return newV;
                 */
            //??
        }

        public float length
        {
            get { return ((float)Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow(this.z, 2))); }
        }

        public float lengthSquared
        {
            get { return ((float)(Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow(this.z, 2))); }
        }

        public float dot(Vector v)
        {
            return (this.x * v.x + this.y * v.y + this.z * v.z);
        }

        public Vector cross(Vector v)
        {
            return new Vector(this.y * v.z - this.z * v.y, this.z * v.x - this.x * v.z, this.x * v.y - this.y * v.x);
        }

        public void negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
        }

        public void add(Vector v)
        {
            this.x += v.X;
            this.y += v.Y;
            this.z += v.Z;
        }

        public void sub(Vector v)
        {
            this.x -= v.X;
            this.y -= v.Y;
            this.z -= v.Z;
        }

        public void div(float f)
        {
            if (f != 0)
            {
                this.x /= f;
                this.y /= f;
                this.z /= f;
            }
            else
                throw new Exception("Can't divide by 0");
        }

        public void mag(float f)
        {
            this.x *= f;
            this.y *= f;
            this.z *= f;
        }

        #region Operators

        public static Vector operator *(float scalar, Vector right)
        {
            return new Vector(right.x * scalar, right.y * scalar, right.z * scalar);
        }
        public static Vector operator *(Vector left, float scalar)
        {
            return new Vector(left.x * scalar, left.y * scalar, left.z * scalar);
        }
        public static Vector operator *(Vector left, Vector right)
        {
            return new Vector(left.x * right.x, left.y * right.y, left.z * right.z);
        }
        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.x + right.x, left.y + right.y, left.z + right.z);
        }
        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.x - right.x, left.y - right.y, left.z - right.z);
        }
        public static Vector operator -(Vector left)
        {
            return new Vector(-left.x, -left.y, -left.z);
        }
        public static bool operator ==(Vector left, Vector right)
        {
            return (left.x == right.x && left.y == right.y && left.z == right.z);
        }
        public static bool operator !=(Vector left, Vector right)
        {
            return (left.x != right.x || left.y != right.y || left.z != right.z);
        }

        public static Vector operator /(Vector left, float scalar)
        {
            Vector vector = new Vector(); // domyślnie wektor jednostkowy (1,1,1)
            if (scalar != 0)
            {
                float inverse = 1.0f / scalar;
                vector.x = left.x * inverse;
                vector.y = left.y * inverse;
                vector.z = left.z * inverse;
                return vector;
            }
            else throw new Exception("Can't divide by 0");
            
        }
        #endregion Operators

        public Vector reflect(Vector normal)
        {
            return this - (2 * this.dot(normal) * normal); // padający - 2*(padający dot normalny)*normalny
        }
        //??

        public static Vector magProduct(Vector v, float f)
        {
            return new Vector(v.X * f, v.Y * f, v.Z * f);
        }

        public Vector toPoint()
        {
            Vector p = new Vector(this.X, this.Y, this.Z);
            return p;
        }

        public Vector lerp(Vector v, float t)
        {
            Vector vector = new Vector();
            vector.x = this.x + t * (v.x - this.x);
            vector.y = this.y + t * (v.y - this.y);
            vector.z = this.z + t * (v.z - this.z);
            return vector;
        }
        //??
    }
}
