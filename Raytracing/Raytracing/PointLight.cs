using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class PointLight
    {
        private Vector location;
        public Vector Location
        {
            get { return location; }
            set { location = value; }
        }

        private LightIntensity color;
        public LightIntensity Color
        {
            get { return color; }
            set { color = value; }
        }

        private float constAtten;
        public float ConstAtten
        {
            get { return constAtten; }
            set { constAtten = value; }
        }

        private float linearAtten;
        public float LinearAtten
        {
            get { return linearAtten; }
            set { linearAtten = value; }
        }

        private float quadAtten;
        public float QuadAtten
        {
            get { return quadAtten; }
            set { quadAtten = value; }
        }

       // public int isInShadow () 
        /*
int read(FILE* fp);
void write(FILE* fp=stdout);
Point3D getDiffuse(Point3D cameraPosition,IntersectionInfo
iInfo);
Point3D getSpecular(Point3D cameraPosition,IntersectionInfo
iInfo);
int isInShadow(IntersectionInfo iInfo,Shape* shape);
};
*/
        public PointLight()
        {

        }

        public PointLight(Vector Loc, LightIntensity col)
        {
            this.Location = Loc;
            this.Color = col;
        }
    }
}
