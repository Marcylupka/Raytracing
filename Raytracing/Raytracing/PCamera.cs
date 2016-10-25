using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kuc_Ray
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
        /*
         * public void render_scene(int width, int height, List<Sphere> sphereList, List<Plain> plainList, String name)
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
                            LightIntensity coloral = sphere.Color;
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
                            LightIntensity pxa = backColor;
                            LightIntensity pxb = backColor;
                            LightIntensity pxc = backColor;
                            LightIntensity pxd = backColor;
                            if (sphere.Intersect(ray, ref am, ref pp1, ref pp2) == true)
                            {
                                LightIntensity pxe = sphere.Color;

                                float dis = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                if (dis < zBuffer[i, j])
                                {
                                    img.setPixel(i, j, pxe);
                                    zBuffer[i, j] = dis;
                                }
                            }
                        }
                    }

                }
            }
            img.Obraz.Save(name);
        }
        */


        public void reg_aa_render_scene(int width, int height, List<Sphere> sphereList, List<Plain> plainList, String name, int gridSize)
        {
            float gridStep = 1.0f / gridSize;
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
                            LightIntensity coloral = sphere.Color;
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
                            //LightIntensity pxa = backColor;
                            //LightIntensity pxb = backColor;
                            //LightIntensity pxc = backColor;
                            //LightIntensity pxd = backColor;
                            if (sphere.Intersect(ray, ref am, ref pp1, ref pp2) == true)
                            {
                                LightIntensity pxAA = new LightIntensity();
                                float dis = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                if (dis < zBuffer[i, j])
                                {
                                    LightIntensity[,] amountAA = new LightIntensity[gridSize, gridSize];
                                    float[,] zBufferAA = new float[gridSize, gridSize];
                                    for (int i2 = 0; i2 < gridSize; i2++)
                                    {
                                        for (int j2 = 0; j2 < gridSize; j2++)
                                        {
                                            amountAA[i2, j2] = new LightIntensity();
                                            zBufferAA[i2, j2] = (float)Double.PositiveInfinity;
                                        }
                                    }
                                    foreach (Sphere sphereAA in sphereList)
                                    {
                                        LightIntensity pxe = sphereAA.Color;
                                        for (int k = 0; k < gridSize; k++)
                                        {
                                            for (int l = 0; l < gridSize; l++)
                                            {
                                                if (aspectRatio >= 1)
                                                {
                                                    ijx = ((2f * (i + k * gridStep) * aspectRatio / (float)width) - 1f) * tanFov2;
                                                    ijy = (1f - (2f * (j + l * gridStep) / (float)height)) * tanFov2;
                                                }
                                                else
                                                {
                                                    ijx = ((2f * (i + k * gridStep) / (float)width) - 1f) * tanFov2;
                                                    ijy = (1f - (2f * (j + l * gridStep) / aspectRatio / (float)height)) * tanFov2;
                                                }
                                                Vector rayAADirection = u * ijx + v * ijy + w * (-distance);
                                                Ray rayAA = new Ray(this.position, rayAADirection);
                                                if (sphereAA.Intersect(rayAA, ref am, ref pp1, ref pp2) == true)
                                                {
                                                    float disAA = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                                    if (disAA < zBufferAA[k, l])
                                                    {
                                                        amountAA[k, l] = pxe;
                                                        zBufferAA[k, l] = disAA;
                                                    }
                                                    //foundIntersection = true;
                                                }
                                               // if (!foundIntersection) amountAA[k, l] = backColor;
                                            }
                                        }
                                    }
                                    for (int k = 0; k < gridSize; k++)
                                    {
                                        for (int l = 0; l < gridSize; l++)
                                        {
                                            pxAA.add(amountAA[k, l]);
                                        }
                                    }
                                    //Console.WriteLine(pxAA.R + "," + pxAA.G + "," + pxAA.B);
                                    //img.setPixel(i, j, pxAA);
                                    pxAA.div(gridSize * gridSize);
                                    //Console.WriteLine(pxAA.R + "," + pxAA.G + "," + pxAA.B);
                                    img.setPixel(i, j, pxAA);
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
