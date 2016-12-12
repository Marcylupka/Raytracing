using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class Plain : Object1
    {
        /*public String Object1.MyType()
        {
            return this.type;
        }*/

        private Vector point;
        public Vector Point
        {
            get { return point; }
            set { point = value; }
        }

        private float distance;
        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public bool hasDistance;


        private Vector normal;
        public Vector Normal
        {
            get { return normal; }
            set { normal = value; }
        }

        public Plain(Vector p, Vector norm)
        {
            this.point = p;
            this.normal = norm;
            this.mat = new Material();
            this.name = "pl";
            this.hasDistance = false;
            this.Distance = 0;
        }

        public Plain(Vector p, Vector norm, bool has, float di)
        {
            this.point = p;
            this.normal = norm;
            this.mat = new Material();
            this.name = "pl";
            this.hasDistance = has;
            this.Distance = di;
        }

        public Plain (Vector p, Vector norm, LightIntensity col)
        {
            this.point = p;
            this.normal = norm;
            this.color = col;
            this.mat = new Material();
            this.name = "pl";
            this.hasDistance = false;
            this.Distance = 0;

        }

        public Plain()
        {
            this.mat = new Material();
            this.name = "pl";
            this.hasDistance = false;
            this.Distance = 0;
        }

        public override HitInfo Intersect(Ray ray)
        {
           Vector ppp = new Vector(0, 0, 0);

            //promień = origin + dir*x
            //r. płaszczyzny (promien - point) * normal = 0
            //(origin+dir*x-point)*normal = 0
            //x = (point - origin)*normal/(dir*normal)
            float t = (this.point - ray.Origin).dot(this.normal) / ray.Direction.dot(this.normal);
            if (t > Ray.Mini)
            {
                distance = t;

                ppp.X = ray.Origin.X + distance * ray.Direction.X;
                ppp.Y = ray.Origin.Y + distance * ray.Direction.Y;
                ppp.Z = ray.Origin.Z + distance * ray.Direction.Z;

             //   am = 1;
         return new HitInfo(true, t, normal, ppp);
              //  return true;
            }
          //  am = 0;
            normal = this.normal;
            return new HitInfo ();
        }
    }
}
