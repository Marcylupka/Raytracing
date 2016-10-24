using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing
{
    public class Ray
    {
        private Vector origin;
        public Vector Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        private Vector direction;
        public Vector Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        /*private Vector destination;
        public Vector Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        private float distance;
        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }*/

        public const float Mini = 0.000001f;
        public const float Max = float.MaxValue;

        public Ray() //wszystko jedynkowe
        {
            /*
            this.origin = new Raytracing.Vector (1, 1, 1);
            this.direction = new Raytracing.Vector(1, 1, 1);
            this.destination = new Raytracing.Vector(1, 1, 1);
            this.distance = 1;
            */
        }
        /*
        public Ray (Vector v, Vector d1, Vector d2, float d3)
        {
            this.origin = v;
            this.direction = d1;
            this.destination = d2;
            this.distance = d3;
        }
        */
        public Ray (Vector v, Vector d)
        {
            this.origin = v;
            this.direction = d;
        }
    }
}
