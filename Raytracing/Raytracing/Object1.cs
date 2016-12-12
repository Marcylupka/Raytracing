using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public abstract class Object1
    {
        //public abstract bool Intersect(Ray ray, ref int amount, ref Vector tempMin, ref float dist, ref Vector normal);
        public abstract HitInfo Intersect(Ray ray);

        public LightIntensity color;
        public LightIntensity Color
        {
            get { return color; }
            set { color = value; }
        }

        public Material mat;
        public Material Mat
        {
            get { return mat; }
            set { mat = value; }
        }

        public Vector normal;
        public Vector Normal
        {
            get { return normal; }
            set { normal = value; }
        }

        public String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        //bool Intersect();
        //bool Intersect(Ray ray, ref int amount, ref Vector tempMin, ref Vector notMin);
        //bool Intersect(Ray ray, ref float distance, ref Vector ppp);
        //public String MyType();
    }
}
