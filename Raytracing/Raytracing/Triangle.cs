using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class Triangle : Object1
    {
        private Vector normal;
        public Vector Normal
        {
            get { return normal; }
            set { normal = value; }
        }

        private Vector vertexA;
        public Vector VertexA
        {
            get { return vertexA; }
            set { vertexA = value; }
        }

        private Vector vertexB;
        public Vector VertexB
        {
            get { return vertexB; }
            set { vertexB = value; }
        }

        private Vector vertexC;
        public Vector VertexC
        {
            get { return vertexC; }
            set { vertexC = value; }
        }

        public Triangle()
        {
            this.vertexA = new Vector(1, 1, 1);
            this.vertexB = new Vector(1, 1, 1);
            this.vertexC = new Vector(1, 1, 1);
            this.normal = ((this.vertexB - this.vertexA).cross(this.vertexC - this.vertexA));
            this.normal.normalize();
            this.color = new LightIntensity(1, 1, 1);
        }

        public Triangle (Vector vA, Vector vB, Vector vC)
        {
            this.vertexA = vA;
            this.vertexB = vB;
            this.vertexC = vC;
            this.normal = ((this.vertexB - this.vertexA).cross(this.vertexC - this.vertexA));
            this.normal.normalize();
            this.color = new LightIntensity(1, 1, 1);
        }

        public override bool Intersect(Ray ray,ref int am, ref Vector crossP, ref float dist)
        {
            Plain plainTr = new Plain(this.vertexA, this.normal);
            float dis = 0.0f;
            am = 0;
            Vector pp = new Vector();
            if (!plainTr.Intersect(ray, ref am, ref pp, ref dis))
            {
                return false;
            } else
            {
                Vector fa = new Vector();
                Vector fb = new Vector();
                Vector fc = new Vector();
                fa = this.vertexA - pp;
                fb = this.vertexB - pp;
                fc = this.vertexC - pp;
                Vector crossVP = new Vector();
                crossVP= fa.cross(fb);
                if (crossVP.dot(this.normal)< -0.0001f)
                {
                    am = 0;
                    return false;
                } else
                {
                    crossVP = fb.cross(fc);
                    if (crossVP.dot(this.normal)< -0.0001f)
                    {
                        return false;
                    } else
                    {
                        crossVP = fc.cross(fa);
                        if (crossVP.dot(this.normal)< -0.0001f)
                        {
                            return false;
                        } else
                        {
                            crossP = pp;
                            am = 1;
                            return true;
                        }
                    }
                }
            }

        }


        }
}
