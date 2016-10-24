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
            float[,] zBuffera = new float[width, height];
            float[,] zBufferb = new float[width, height];
            float[,] zBufferc = new float[width, height];
            float[,] zBufferd = new float[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    zBuffer[i, j] = (float)Double.PositiveInfinity;
                    zBuffera[i, j] = (float)Double.PositiveInfinity;
                    zBufferb[i, j] = (float)Double.PositiveInfinity;
                    zBufferc[i, j] = (float)Double.PositiveInfinity;
                    zBufferd[i, j] = (float)Double.PositiveInfinity;
                }
            }

            int am = 0;
            float dist = 0;
            Vector pp1 = new Vector(0, 0, 0);
            Vector pp2 = new Vector(0, 0, 0);

            float radFov = this.fov * (float)Math.PI / 180f;
            float tanFov2 = (float)Math.Tan(radFov / 2f);

            LightIntensity backColor = new LightIntensity(0, 0, 0);
            LightIntensity coloral = backColor;
            int it = 1;
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
                            }
                            else
                            {
                                ijx = ((2f * (i + 0.5f) / (float)width) - 1f) * tanFov2;
                                ijy = (1f - (2f * (j + 0.5f) / aspectRatio / (float)height)) * tanFov2;
                            }
                            Vector rayDirection = u * ijx + v * ijy + w * (-distance);
                            Ray ray = new Ray(this.position, rayDirection);
                            if (sphere.Intersect(ray, ref am, ref pp1, ref pp2) == true)
                            {
                                LightIntensity pxa = backColor;
                                LightIntensity pxb = backColor;
                                LightIntensity pxc = backColor;
                                LightIntensity pxd = backColor;
                                LightIntensity pxe = backColor;
                                pxe = sphere.Color;

                                float dis = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                if (dis < zBuffer[i, j])
                                {
                                    float lux, luy, ldx, ldy, rux, ruy, rdx, rdy;
                                    if (aspectRatio >= 1)
                                    {
                                        lux = ((2f * i * aspectRatio / (float)width) - 1f) * tanFov2;
                                        luy = (1f - (2f * j / (float)height)) * tanFov2;
                                        ldx = ((2f * i * aspectRatio / (float)width) - 1f) * tanFov2;
                                        ldy = (1f - (2f * (j + 1f) / (float)height)) * tanFov2;
                                        rux = ((2f * (i + 1f) * aspectRatio / (float)width) - 1f) * tanFov2;
                                        ruy = (1f - (2f * j / (float)height)) * tanFov2;
                                        rdx = ((2f * (i + 1f) * aspectRatio / (float)width) - 1f) * tanFov2;
                                        rdy = (1f - (2f * (j + 1f) / (float)height)) * tanFov2;
                                    }
                                    else
                                    {
                                        lux = ((2f * i / (float)width) - 1f) * tanFov2;
                                        luy = (1f - (2f * j / aspectRatio / (float)height)) * tanFov2;
                                        ldx = ((2f * i / (float)width) - 1f) * tanFov2;
                                        ldy = (1f - (2f * (j + 1f) / aspectRatio / (float)height)) * tanFov2;
                                        rux = ((2f * (i + 1f) / (float)width) - 1f) * tanFov2;
                                        ruy = (1f - (2f * j / aspectRatio / (float)height)) * tanFov2;
                                        rdx = ((2f * (i + 1f) / (float)width) - 1f) * tanFov2;
                                        rdy = (1f - (2f * (j + 1f) / aspectRatio / (float)height)) * tanFov2;
                                    }

                                    
                                    foreach (Sphere spherebis in sphereList)
                                    {
                                        Vector rayDirectiona = u * ldx + v * ldy + w * (-distance);
                                        Ray raya = new Ray(this.position, rayDirectiona);
                                        if (spherebis.Intersect(raya, ref am, ref pp1, ref pp2) == true)
                                        {
                                            float disa = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                            if (disa < zBuffera[i, j])
                                            {
                                                pxa = spherebis.Color;
                                                zBuffera[i, j] = disa;
                                            }
                                        }
                                        Vector rayDirectionb = u * rdx + v * rdy + w * (-distance);
                                        Ray rayb = new Ray(this.position, rayDirectionb);
                                        if (spherebis.Intersect(rayb, ref am, ref pp1, ref pp2) == true)
                                        {
                                            float disb = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                            if (disb < zBufferb[i, j])
                                            {
                                                pxb = spherebis.Color;
                                                zBufferb[i, j] = disb;
                                            }
                                        }
                                        Vector rayDirectionc = u * rux + v * ruy + w * (-distance);
                                        Ray rayc = new Ray(this.position, rayDirectionc);
                                        if (spherebis.Intersect(rayc, ref am, ref pp1, ref pp2) == true)
                                        {
                                            float disc = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                            if (disc < zBufferc[i, j])
                                            {
                                                pxc = spherebis.Color;
                                                zBufferc[i, j] = disc;
                                            }
                                        }
                                        Vector rayDirectiond = u * lux + v * luy + w * (-distance);
                                        Ray rayd = new Ray(this.position, rayDirectiond);
                                        if (spherebis.Intersect(rayd, ref am, ref pp1, ref pp2) == true)
                                        {
                                            float disd = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                            if (disd < zBufferd[i, j])
                                            {
                                                pxd = spherebis.Color;
                                                zBufferd[i, j] = disd;
                                            }
                                        }
                                    }
                                    Sampler s1 = new Sampler(1, 11, 0.1f);
                                    //if (s1.adaptiveSampling(pxa, pxb, pxc, pxd, pxe, ref it, ref coloral))
                                    //{
                                        s1.adaptiveSampling(pxa, pxb, pxc, pxd, pxe, ref it, ref coloral);
                                    //}

                                        img.setPixel(i, j, coloral);
                                        zBuffer[i, j] = dis;
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
