using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing
{
    public class Plain
    {
        private Vector point;
        public Vector Point
        {
            get { return point; }
            set { point = value; }
        }

        private Vector normal;
        public Vector Normal
        {
            get { return normal; }
            set { normal = value; }
        }

        private LightIntensity color;
        public LightIntensity Color
        {
            get { return color; }
            set { color = value; }
        }

        public Plain (Vector p, Vector norm, LightIntensity col)
        {
            this.point = p;
            this.normal = norm;
            this.color = col;
        }

        public bool Intersect(Ray ray, ref float distance, ref Vector ppp)
        {
            ppp = new Vector(0, 0, 0);

            //promień = origin + dir*x
            //r. płaszczyzny (promien - point) * normal = 0
            //(origin+dir*x-point)*normal = 0
            //x = (point - origin)*normal/(dir*normal)
            float t = (point - ray.Origin).dot(normal) / ray.Direction.dot(normal);
            if ( t > Ray.Mini)
            {
                distance = t;
                
                ppp.X = ray.Origin.X + distance * ray.Direction.X;
                ppp.Y = ray.Origin.Y + distance * ray.Direction.Y;
                ppp.Z = ray.Origin.Z + distance * ray.Direction.Z;
                return true;
            }
            return false;
        }
    }
}
