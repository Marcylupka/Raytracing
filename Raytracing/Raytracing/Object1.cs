using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public abstract class Object1
    {
        public abstract bool Intersect(Ray ray, ref int amount, ref Vector tempMin, ref float dist);
        public LightIntensity color;
        public LightIntensity Color
        {
            get { return color; }
            set { color = value; }
        }
        //bool Intersect();
        //bool Intersect(Ray ray, ref int amount, ref Vector tempMin, ref Vector notMin);
        //bool Intersect(Ray ray, ref float distance, ref Vector ppp);
        //public String MyType();
    }
}
