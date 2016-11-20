using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class Triangle : Object1
    {
        public String name = "tr";
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

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
            this.mat = new Material();
            this.name = "tr";
        }

        public Triangle (Vector vA, Vector vB, Vector vC)
        {
            this.vertexA = vA;
            this.vertexB = vB;
            this.vertexC = vC;
            this.normal = ((this.vertexB - this.vertexA).cross(this.vertexC - this.vertexA));
            this.normal.normalize();
            this.color = new LightIntensity(1, 1, 1);
            this.mat = new Material();
            this.name = "tr";
        }

        public Triangle(Vector vA, Vector vB, Vector vC, LightIntensity col)
        {
            this.vertexA = vA;
            this.vertexB = vB;
            this.vertexC = vC;
            this.normal = ((this.vertexB - this.vertexA).cross(this.vertexC - this.vertexA));
            this.normal.normalize();
            this.color = col;
            this.mat = new Material();
            this.name = "tr";
        }

        public Triangle(Vector vA, Vector vB, Vector vC, Material mater)
        {
            this.vertexA = vA;
            this.vertexB = vB;
            this.vertexC = vC;
            this.normal = ((this.vertexB - this.vertexA).cross(this.vertexC - this.vertexA));
            this.normal.normalize();
            this.color = new LightIntensity(0, 0, 0);
            this.mat = mater;
            this.name = "tr";
        }

        public Triangle(Vector vA, Vector vB, Vector vC, Material mater, String nam)
        {
            this.vertexA = vA;
            this.vertexB = vB;
            this.vertexC = vC;
            this.normal = ((this.vertexB - this.vertexA).cross(this.vertexC - this.vertexA));
            this.normal.normalize();
            //this.color = new LightIntensity(0, 0, 0);
            this.mat = mater;
            this.name = nam;
        }

        public override string ToString()
        {
            return "(" + vertexA.X.ToString() + "," + vertexA.Y.ToString() + "," + vertexA.Z.ToString() + "), " + "(" + vertexB.X.ToString() + "," + vertexB.Y.ToString() + "," + vertexB.Z.ToString() + "), " + "(" + vertexC.X.ToString() + "," + vertexC.Y.ToString() + "," + vertexC.Z.ToString() + "), ";
        }
        /*
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

        }*/
        //barycentryczne
        public override bool Intersect(Ray ray, ref int am, ref Vector crossP, ref float dist)
        {
            Vector e1 = this.vertexB - this.vertexA;
            Vector e2 = this.vertexC - this.vertexA;

            Vector p = ray.Direction.cross(e2);
            float det = e1.dot(p); // wyznacznik macierzy

            if (det > -0.00005 && det < 0.00005)
                return false;

            float iDet = 1f / det;

            Vector t = ray.Origin - this.vertexA;
            float m_u = (t.dot(p) * iDet);
            if (m_u < 0 || m_u > 1) return false;

            Vector q = t.cross(e1);

            float m_v = (ray.Direction.dot(q) * iDet);
            if (m_v < 0 || m_u + m_v > 1) return false;

            float m_w = (e2.dot(q) * iDet);

            if (m_w > 0.00005)
            {
                crossP = ray.Origin + ray.Direction * m_w;
                am = 1;
                dist = m_w;
                return true;
            }

            return false;

        }


    }
}
