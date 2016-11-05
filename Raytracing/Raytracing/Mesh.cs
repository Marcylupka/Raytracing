using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /*public Mesh parseOBJ(String filename)
        {
            List<Vector> Vertices = new List<Vector>();
            List<Vector> Normals = new List<Vector>();
            List<Triangle> Groups = new List<Triangle>();

            Mesh resultMesh = new Mesh();

            using (FileStream fs = File.Open(filename, FileMode.Open))
            {

            }



                
            //public IList< Vertex > Vertices { get; set; }
            //public IList<Texture> Textures { get; set; }
            //public IList<Normal> Normals { get; set; }
            //public IList<Group> Groups { get; set; }
            //public IList<Material> Materials { get; set; }
            
            }
    */
    }
}
