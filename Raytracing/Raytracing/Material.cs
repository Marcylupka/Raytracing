using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class Material
    {
        private float[] kAmbient = new float[3];
        public float[] KAmbient
        {
            get { return kAmbient; }
            set { kAmbient = value; }
        }

        private float[] kDiffuse = new float[3];
        public float[] KDiffuse
        {
            get { return kDiffuse; }
            set { kDiffuse = value; }
        }

        private float[] kSpecular = new float[3];
        public float[] KSpecular
        {
            get { return kSpecular; }
            set { kSpecular = value; }
        }

        private float alpha;
        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        /*private Texture text;
        public Texture Text
        {
            get { return text; }
            set { text = value; }
        }*/

        private bool hasTexture;
        public bool HasTexture
        {
            get { return hasTexture; }
            set { hasTexture = value; }
        }

        public Material()
        {
            for (int i=0; i<3; i++)
            {
                this.kAmbient[i] = 0.3f;
                this.kDiffuse[i] = 0.5f;
                this.kSpecular[i] = 0.8f;
            }
            this.alpha = 100;
            this.hasTexture = false;
        }
    }
}
