using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Raytracing
{
    class PCamera
    {
        private Vector position;
        public Vector Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector target;
        public Vector Target
        {
            get { return target; }
            set { target = value; }
        }
        private Vector up;
        public Vector Up
        {
            get { return up; }
            set { up = value; }
        }
        private float nearPlane;
        public float NearPlane
        {
            get { return nearPlane; }
            set { nearPlane = value; }
        }
        private float farPlane;
        public float FarPlane
        {
            get { return farPlane; }
            set { farPlane = value; }
        }
        private float fov;
        public float Fov
        {
            get { return fov; }
            set { fov = value; }
        }
        public PCamera()
        {
            this.position = new Vector(0, 0, 0);
            this.target = new Vector(0, 0, 1);
            this.nearPlane = 1;
            this.farPlane = 1000;
            this.fov = 60;
            this.up = new Vector(0, 1, 0);
        }
        public PCamera(Vector position, Vector target)
        {
            this.position = position;
            this.target = target;
            this.nearPlane = 1;
            this.farPlane = 1000;
            this.fov = 60;
            this.up = new Vector(0, 1, 0);
        }
        public PCamera(Vector position, Vector target, float fov)
        {
            this.position = position;
            this.target = target;
            this.fov = fov;
            this.nearPlane = 1;
            this.farPlane = 1000;
            this.up = new Vector(0, 1, 0);
        }
        public void render_scene(int width, int height, List<Sphere> sphereList, List<Plain> plainList, String name)
        {

            Picture img = new Picture(width, height);

            float[,] zBuffer = new float[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    zBuffer[i, j] = (float)Double.PositiveInfinity;
                }
            }

            int am = 0;
            float dist = 0;
            Vector pp1 = new Vector(0, 0, 0);
            Vector pp2 = new Vector(0, 0, 0);

            float radFov = this.fov * (float)Math.PI / 180f;
            float tanFov2 = (float)Math.Tan(radFov / 2f);

            LightIntensity backColor = new LightIntensity(0, 0, 0);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    img.setPixel(i, j, backColor);
                }
            }

            Vector w = this.position - this.target;
            w.normalize();
            float distance = w.length;
            Vector u = (this.up).cross(w);
            u.normalize();
            Vector v = w.cross(u);
            u.negate();

            float aspectRatio = (float)width / (float)height * 1.0f;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (sphereList != null)
                    {
                        foreach (Sphere sphere in sphereList)
                        {
                            float ijx = ((2f * (i + 0.5f) / (float)width) - 1f) * tanFov2;
                            float ijy = (1f - (2f * (j + 0.5f) / (float)height)) * tanFov2;
                            if (aspectRatio >= 1)
                            {
                                ijx = ((2f * (i + 0.5f) * aspectRatio / (float)width) - 1f) * tanFov2;
                                ijy = (1f - (2f * (j + 0.5f) / (float)height)) * tanFov2;
                            } else
                            {
                                ijx = ((2f * (i + 0.5f) / (float)width) - 1f) * tanFov2;
                                ijy = (1f - (2f * (j + 0.5f) / aspectRatio / (float)height)) * tanFov2;
                            }
                            Vector rayDirection = u * ijx + v * ijy + w * (-distance);
                            Ray ray = new Ray(this.position, rayDirection);
                            if (sphere.Intersect(ray, ref am, ref pp1, ref pp2) == true)
                            {
                                if (pp1.Z < zBuffer[i, j])
                                {
                                    img.setPixel(i, j, sphere.Color);
                                    zBuffer[i, j] = pp1.Z;
                                }
                            }
                        }
                    }

                }
            }
            img.Obraz.Save(name);
        }
    }
}
