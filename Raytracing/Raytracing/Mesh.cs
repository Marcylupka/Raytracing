using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Kuc_Ray
{
    public class Mesh : Object1
    {
        private List<Triangle> triangleList;
        public List<Triangle> TriangleList
        {
            get { return triangleList; }
            set { triangleList = value; }
        }

        public List<Mesh> parseOBJ(String filename)
        {
            int il = 0;
            String[] so;
            if (File.Exists(filename))
            {
                using (StreamReader sr = File.OpenText(filename))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        so = s.Split(' ');
                        if (so[0] == "o")
                        {
                            il++;
                        }
                        else if (so[0] == "#")
                        {
                            //Console.WriteLine(so[0]);
                            if (so.Length > 1)
                            {
                                //Console.WriteLine(so[1]);
                                if (so[1] == "object")
                                {
                                    il++;
                                }
                            }
                        }
                    }
                }
            }
            //Console.WriteLine("il: " + il);
            Mesh[] tabMesh = new Mesh[il];
            List<Vector>[] tabTempFaces = new List<Vector>[il];
            List<Vector>[] tabFaces = new List<Vector>[il];
            for (int i = 0; i < il; i++)
            {
                tabMesh[i] = new Mesh();
                tabMesh[i].triangleList = new List<Triangle>();
                tabTempFaces[i] = new List<Vector>();
                tabFaces[i] = new List<Vector>();
            }
            List<Vector> Vertices = new List<Vector>();
            List<Vector> Normals = new List<Vector>();
            /*triangleList = new List<Triangle>();
            List<Vector> Vertices = new List<Vector>();
            List<Vector> Normals = new List<Vector>();
            List<Vector> TempFaces = new List<Vector>();
            List<Vector> Faces = new List<Vector>();
            //Vector vect = new Vector();
            */
            String[] st;
            String[] sf;

            int nr = 0;

            List<Mesh> resultMesh = new List<Mesh>();
            if (File.Exists(filename))
            {
                using (StreamReader sr = File.OpenText(filename))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        //s = sr.ReadLine();
                        st = s.Split(' ');
                        if (st[0] == "o")
                        {
                            nr++;
                        }
                        if (st[0] == "#")
                        {
                            //Console.WriteLine(st[0]);
                            if (st.Length > 1)
                            {
                                //Console.WriteLine(st[1]);
                                if (st[1] == "object")
                                {
                                    nr++;
                                }
                            }
                        }
                        //Console.WriteLine(nr);
                        if (st[0] == "v")
                        {
                            //Console.WriteLine("czytam wierzcholki");
                            //Console.WriteLine(nr);
                            if (st[1] != "")
                            {
                                //Console.WriteLine(st[0] + " " + st[1] + " " + st[2] + " " + st[3]);
                                Vector vect = new Vector();
                                vect.X = float.Parse(st[1], CultureInfo.InvariantCulture.NumberFormat);
                                vect.Y = float.Parse(st[2], CultureInfo.InvariantCulture.NumberFormat);
                                vect.Z = float.Parse(st[3], CultureInfo.InvariantCulture.NumberFormat);
                                Vertices.Add(vect);
                            }
                            else
                            {
                                //Console.WriteLine(st[0] + " " + st[1] + " " + st[2] + " " + st[3] + " " + st[4]);
                                Vector vect = new Vector();
                                vect.X = float.Parse(st[2], CultureInfo.InvariantCulture.NumberFormat);
                                vect.Y = float.Parse(st[3], CultureInfo.InvariantCulture.NumberFormat);
                                vect.Z = float.Parse(st[4], CultureInfo.InvariantCulture.NumberFormat);
                                Vertices.Add(vect);
                            }
                        }
                        if (st[0] == "vn")
                        {
                            //Console.WriteLine("czytam normalne");
                            Vector vect = new Vector();
                            vect.X = float.Parse(st[1], CultureInfo.InvariantCulture.NumberFormat);
                            vect.Y = float.Parse(st[2], CultureInfo.InvariantCulture.NumberFormat);
                            vect.Z = float.Parse(st[3], CultureInfo.InvariantCulture.NumberFormat);
                            Normals.Add(vect);
                        }
                        if (st[0] == "f")
                        {
                            //Console.WriteLine("czytam sciany");
                            for (int i = 1; i < 4; i++)
                            {
                                sf = st[i].Split('/');
                                if (sf[1] != "")
                                {
                                    //3 dane - wierzchołek/tekstura/normalna
                                    Vector vect = new Vector();
                                    vect.X = float.Parse(sf[0], CultureInfo.InvariantCulture.NumberFormat);
                                    vect.Y = float.Parse(sf[1], CultureInfo.InvariantCulture.NumberFormat);
                                    vect.Z = float.Parse(sf[2], CultureInfo.InvariantCulture.NumberFormat);
                                    tabTempFaces[nr - 1].Add(vect);
                                    //Console.WriteLine("3 dane:" + vect);
                                }
                                else if (sf[1] == "")
                                {
                                    //2 dane - wierzchołek//normalna
                                    Vector vect = new Vector();
                                    vect.X = float.Parse(sf[0], CultureInfo.InvariantCulture.NumberFormat);
                                    vect.Y = 0f;
                                    vect.Z = float.Parse(sf[2], CultureInfo.InvariantCulture.NumberFormat);
                                    tabTempFaces[nr - 1].Add(vect);
                                    //Console.WriteLine("2 dane:" + vect);
                                }
                            }
                        }
                    }
                }
                Random rnd = new Random();
                float v1, v2, v3;
                LightIntensity col = new LightIntensity();
                Material mater = new Material();
                for (int iln = 0; iln < il; iln++)
                {
                    for (int i = 0; i < tabTempFaces[iln].Count; i += 3)
                    {
                        v1 = (float)rnd.NextDouble();
                        v2 = (float)rnd.NextDouble();
                        v3 = (float)rnd.NextDouble();
                        col = new LightIntensity(v1, v2, v3);
                        mater.Alpha = 100;
                        mater.KDiffuse = new LightIntensity(1f, 1f, 1f);
                        mater.KSpecular = new LightIntensity(1f, 1f, 1f);
                        mater.SpecularExponent = 30f;
                        if (iln % 4 == 0)
                        {
                            //LightCoral
                            mater.Color = new LightIntensity(0.94f, 0.5f, 0.5f);
                        }
                        else if (iln % 4 == 1)
                        {
                            //LightGreen
                            mater.Color = new LightIntensity(0.56f, 0.93f, 0.56f);
                        }
                        else if (iln % 4 == 2)
                        {
                            //Gray
                            mater.Color = new LightIntensity(0.5f, 0.5f, 0.5f);
                        }
                        else
                        {
                            //LightBlue
                            mater.Color = new LightIntensity(0.68f, 0.85f, 0.9f);
                        }
                        mater.Color = new LightIntensity(0f, 0f, 1f);
                        tabMesh[iln].triangleList.Add(
                            new Triangle
                                (
                                Vertices[(int)tabTempFaces[iln][i].X - 1],
                                Vertices[(int)tabTempFaces[iln][i + 1].X - 1],
                                Vertices[(int)tabTempFaces[iln][i + 2].X - 1],
                                mater,
                                "tr"
                                )
                        );
                        //Console.WriteLine(tabMesh[iln].triangleList.ElementAt(iln).name);
                    }
                }

            }

            for (int iln = 0; iln < il; iln++)
            {
                tabMesh[iln].mat = new Material(new LightIntensity(1, 0, 0)); ;
                tabMesh[iln].mat.Alpha = 100;
                tabMesh[iln].mat.KDiffuse = new LightIntensity(1f, 1f, 1f);
                tabMesh[iln].mat.KSpecular = new LightIntensity(1f, 1f, 1f);
                tabMesh[iln].mat.SpecularExponent = 30f;
                resultMesh.Add(tabMesh[iln]);
            }

            //ma = new Material(new LightIntensity(1, 0, 0));
            //mat.Alpha = 100;
            //mat.KDiffuse = new LightIntensity(1f, 1f, 1f);
            //mat.KSpecular = new LightIntensity(1f, 1f, 1f);
            //mat.SpecularExponent = 30f;
            return resultMesh;
        }



        public override HitInfo Intersect(Ray ray)
        {

            float dist = 1000f;
            Triangle objectHit = new Triangle();
            Vector c_normal = new Vector();
            float minDist = 1000;
            float lastDist = 0;
            //int am = 1;
            Vector pp1 = new Vector(0, 0, 0);
            //      bool isIntersection = false;
            HitInfo hit = new HitInfo();


            foreach (Triangle t in this.triangleList)
            {

                HitInfo hi = t.Intersect(ray);
                if (hi.isIntersect == true)
                {
                    lastDist = hit.distance;
                    if (lastDist > 0 && lastDist < minDist)
                    {
                        minDist = lastDist;
                        normal = t.Normal;
                        //new Vector(12, 1, 5);// 
                       // c_normal;
                        normal.normalize();
                     //   amount = am;
                        objectHit = t;
                        hit = hi;
                       Vector tempMin = ray.Origin + lastDist * ray.Direction;

                    }
                }
            }


                dist = minDist;
            if (minDist < 1000)
            {
                //  normal = objectHit.normal;

                //return ;
                return new HitInfo(true, dist, normal, pp1);


            }
            return new HitInfo();
        }
    }
}
