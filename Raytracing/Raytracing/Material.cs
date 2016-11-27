using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class Material
    {
        private LightIntensity color;
        public LightIntensity Color
        {
            get { return color; }
            set { color = value; }
        }

        private LightIntensity kAmbient;
        public LightIntensity KAmbient
        {
            get { return kAmbient; }
            set { kAmbient = value; }
        }

        private LightIntensity kDiffuse;
        public LightIntensity KDiffuse
        {
            get { return kDiffuse; }
            set { kDiffuse = value; }
        }

        private LightIntensity kSpecular;
        public LightIntensity KSpecular
        {
            get { return kSpecular; }
            set { kSpecular = value; }
        }

        private float specularExponent;
        public float SpecularExponent
        {
            get { return specularExponent; }
            set { specularExponent = value; }
        }

        private float alpha;
        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        private Texture text;
        public Texture Text
        {
            get { return text; }
            set { text = value; }
        }

        private bool hasTexture;
        public bool HasTexture
        {
            get { return hasTexture; }
            set { hasTexture = value; }
        }

        public Material()
        {
            this.color = new LightIntensity(0, 0, 0);
            /*for (int i = 0; i < 3; i++)
            {
                this.kAmbient[i] = 0.3f;
                this.kDiffuse[i] = 0.5f;
                this.kSpecular[i] = 0.8f;
            }*/
            this.kAmbient = new LightIntensity(0.3f, 0.3f, 0.3f);
            this.kDiffuse = new LightIntensity(0.5f, 0.5f, 0.5f);
            this.kSpecular = new LightIntensity(0.8f, 0.8f, 0.8f);
            this.alpha = 100;
            this.hasTexture = false;
            this.text = new Texture();
            this.specularExponent = 30;
        }

        public Material(LightIntensity col)
        {
            this.color = col;
            /*for (int i = 0; i < 3; i++)
            {
                this.kAmbient[i] = 0.3f;
                this.kDiffuse[i] = 0.5f;
                this.kSpecular[i] = 0.8f;
            }*/
            this.kAmbient = new LightIntensity(0.3f, 0.3f, 0.3f);
            this.kDiffuse = new LightIntensity(0.5f, 0.5f, 0.5f);
            this.kSpecular = new LightIntensity(0.8f, 0.8f, 0.8f);
            this.alpha = 100;
            this.hasTexture = false;
            this.text = new Texture();
            this.specularExponent = 30;
        }
    }
}
