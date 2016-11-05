using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Kuc_Ray
{
    public class Mesh
    {
        private List<Triangle> triangleList;
        public List<Triangle> TriangleList
        {
            get { return triangleList; }
            set { triangleList = value; }
        }
        
        public Mesh parseOBJ(String filename)
        {
            triangleList = new List<Triangle>();
            List<Vector> Vertices = new List<Vector>();
            List<Vector> Normals = new List<Vector>();
            List<Vector> TempFaces = new List<Vector>();
            List<Vector> Faces = new List<Vector>();
            //Vector vect = new Vector();
            String[] st;
            String[] sf;
            

            Mesh resultMesh = new Mesh();
            if (File.Exists(filename))
            {
                using (StreamReader sr = File.OpenText(filename))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        st = s.Split(' ');
                        if (st[0] == "v")
                        {
                            Vector vect = new Vector();
                            vect.X = float.Parse(st[1], CultureInfo.InvariantCulture.NumberFormat);
                            vect.Y = float.Parse(st[2], CultureInfo.InvariantCulture.NumberFormat);
                            vect.Z = float.Parse(st[3], CultureInfo.InvariantCulture.NumberFormat);
                            Vertices.Add(vect);
                        }
                        if (st[0] == "vn")
                        {
                            Vector vect = new Vector();
                            vect.X = float.Parse(st[1], CultureInfo.InvariantCulture.NumberFormat);
                            vect.Y = float.Parse(st[2], CultureInfo.InvariantCulture.NumberFormat);
                            vect.Z = float.Parse(st[3], CultureInfo.InvariantCulture.NumberFormat);
                            Normals.Add(vect);
                        }
                        if (st[0] == "f")
                        {
                            for (int i = 1; i < 4; i++) {
                                sf = st[i].Split('/');
                                Vector vect = new Vector();
                                vect.X = float.Parse(sf[0], CultureInfo.InvariantCulture.NumberFormat);
                                vect.Y = float.Parse(sf[1], CultureInfo.InvariantCulture.NumberFormat);
                                vect.Z = float.Parse(sf[2], CultureInfo.InvariantCulture.NumberFormat);
                                TempFaces.Add(vect);
                            }

                        }
                    }
                    for (int i = 0; i < TempFaces.Count; i += 3)
                    {

                        triangleList.Add(
                            new Triangle
                            (
                                Vertices[(int)TempFaces[i].X-1],
                                Vertices[(int)TempFaces[i+1].X - 1],
                                Vertices[(int)TempFaces[i+2].X - 1]
                                )

                            );

                    }

                }

                Console.WriteLine("Vertices:");
                foreach (Vector ver in Vertices)
                {
                    Console.WriteLine(ver);
                }
                Console.WriteLine("Normals:");
                foreach (Vector ver in Normals)
                {
                    Console.WriteLine(ver);
                }
                Console.WriteLine("Facets:");
                foreach (Vector ver in TempFaces)
                {
                    Console.WriteLine(ver);
                }
                Console.WriteLine("Meszu:");
                foreach (Triangle ver in triangleList)
                {
                    Console.WriteLine(ver);
                }
            }
            return this;
            }
    
    }
}
