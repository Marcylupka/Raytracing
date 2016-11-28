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

        public override bool Intersect(Ray ray, ref int am, ref Vector ppp, ref float distance, ref Vector normal)
        {
            ppp = new Vector(0, 0, 0);

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

                am = 1;
                return true;
                /*
                if (this.hasDistance == false)
                {
                    am = 1;
                    return true;
                }
                else
                {
                    //Vector d = this.point - ppp;
                    Vector zer = new Vector(0, 0, 0);
                    Vector jakis = zer - this.point;
                    //Console.WriteLine(jakis);
                    Vector rand = this.normal.cross(jakis);
                    rand.normalize();
                    //Console.WriteLine(rand);
                    Vector right = this.normal.cross(rand);
                    //right.normalize();
                    //Console.WriteLine(right);
                    Vector pr = this.point + Distance * right;
                    Vector down = this.normal.cross(right);
                    down.normalize();
                    //Console.WriteLine(down);
                    Vector dl = this.point + Distance * down;
                    Vector dp = dl + Distance * right;
                    //Console.WriteLine(this.point + ", " + pr + ", " + dp + ", " + dl);
                    //Console.WriteLine(Distance);
                    Vector fa = this.point - ppp;
                    Vector fb = pr - ppp;
                    Vector fc = dl - ppp;
                    Vector fd = dp - ppp;
                    Vector crossVP = new Vector();
                    crossVP = fa.cross(fb);
                    if (crossVP.dot(this.normal) < -0.0001f)
                    {
                        ppp = new Vector(0, 0, 0);
                        am = 0;
                        normal = this.normal;
                        return false;
                    }
                    else
                    {
                        crossVP = fb.cross(fc);
                        if (crossVP.dot(this.normal) < -0.0001f)
                        {
                            ppp = new Vector(0, 0, 0);
                            am = 0;
                            normal = this.normal;
                            return false;
                        }
                        else
                        {
                            crossVP = fc.cross(fd);
                            if (crossVP.dot(this.normal) < -0.0001f)
                            {
                                ppp = new Vector(0, 0, 0);
                                am = 0;
                                normal = this.normal;
                                return false;
                            }
                            else
                            {
                                crossVP = fd.cross(fa);
                                if (crossVP.dot(this.normal) < -0.0001f)
                                {
                                    ppp = new Vector(0, 0, 0);
                                    am = 0;
                                    normal = this.normal;
                                    return false;
                                }
                                else
                                {
                                    am = 1;
                                    return true;
                                }
                            }
                        }
                    }

                                /*float dd = (float)Math.Sqrt((d.X*d.X)+ (d.Y * d.Y)+ (d.Z * d.Z));
                                if (dd > this.Distance)
                                {
                                    ppp = new Vector(0, 0, 0);
                                    am = 0;
                                    normal = this.normal;
                                    return false;
                                } else
                                {
                                    am = 1;
                                    return true;
                                }*/
                //}
            }
            am = 0;
            normal = this.normal;
            return false;
        }
    }
}
