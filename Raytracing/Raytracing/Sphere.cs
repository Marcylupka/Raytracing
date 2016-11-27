using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class Sphere : Object1
    {
        //Type myType = Type.GetType("Sphere");
        /*public String Object1.MyType()
        {
            return this.type;
        }*/

        /* private String name = "sph";
         public String Name
         {
             get { return name; }
             set { name = value; }
         }*/

        //public new Material mat;
        /* public Material Mat
         {
             get { return mat; }
             set { mat = value; }
         }*/

        private Vector center;
        public Vector Center
        {
            get { return center; }
            set { center = value; }
        }

        private float radius;
        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }


        public Sphere()
        {
            this.center = new Vector(1, 1, 1);
            this.radius = 1;
            this.color = new LightIntensity(1, 1, 1);
            this.mat = new Material();
            this.name = "sph";
        }

        public Sphere (Vector c, float r, LightIntensity col)
        {
            this.center = c;
            this.radius = r;
            this.color = col;
            this.mat = new Material();
            this.name = "sph";
        }

        public Sphere(Vector c, float r, Material material)
        {
            this.center = c;
            this.radius = r;
            //this.color = col;
            this.mat = material;
            this.name = "sph";
        }

        public override bool Intersect(Ray ray, ref int amount, ref Vector tempMin, ref float dist, ref Vector normal)
        {
            Vector notMin = new Vector();
            float t;
            Vector distance = ray.Origin - center;
            float tMin = 0;
            tempMin = new Vector(0, 0, 0);
            notMin = new Vector(0, 0, 0);
            amount = 0;
            /*if(distance > ray.Distance)
            {
                return false;
            }*/
            Vector dir = ray.Origin + ray.Direction;
            float a = (ray.Direction.lengthSquared);
            float b = (distance * 2).dot(ray.Direction);
            float c = distance.lengthSquared - radius * radius;
            
            //(ray-center)dot(the same)-r^2 - równanie sfery
            // ray = origin + x*direct
            //(origin+x*direct - center)dot(the same)-r^2 = 0
            //(direct dot direct)x^2 + [2(origin-center)dot direct]x + (origin - center)dot(the same) - r^2 = 0
            //a*x^2 + b*x + c = 0
            float delta = b * b - 4 * a * c; //delta równania kwadratowego

            if (delta < 0) { return false; } //delta<0 - nie ma przecięć

            float deltaSq = (float)Math.Sqrt(delta); //pierwiastek z delty
            float denom = 2 * a; //denomiator - mianownik

            t = (-b - deltaSq) / denom; //rozwiązanie 1. równania kwadratowego (przecięcie 1)
                if (t > Ray.Mini)
                {
                    tMin = t;
                    amount = amount + 1;
                    //Console.Write("t1: ");
                    //Console.WriteLine(tMin);
                    tempMin = ray.Origin + tMin * ray.Direction;
                    //Console.Write("Punkt przeciecia 1: ");
                    //Console.WriteLine(tempMin);
                    float temp2 = (-b + deltaSq) / denom;
                    if (temp2 > Ray.Mini)
                    {
                        if (temp2 != t)
                        {
                            //Console.Write("t2: ");
                            //Console.WriteLine(temp2);
                            amount = amount + 1;
                            notMin = ray.Origin + temp2 * ray.Direction;
                            //Console.Write("Punkt przeciecia 2: ");
                            //Console.WriteLine(notMin);
                        }
                        if (temp2 < tMin)
                        {
                            notMin = tempMin;
                            tMin = temp2;
                        }
                    }
                }
                else
                {
                    t = (-b + deltaSq) / denom;
                    if (t > Ray.Mini)
                    {
                        tMin = t;
                        amount = amount + 1;
                        //Console.Write("t1: ");
                        //Console.WriteLine(tMin);
                        tempMin = ray.Origin + tMin * ray.Direction;
                        //Console.Write("Punkt przeciecia 1: ");
                        //Console.WriteLine(tempMin);
                    }
                    else return false;
                }
            dist = (tempMin - ray.Origin).length;
            normal = tempMin - center;
            normal.normalize();
         return true;

        }
    }
}
