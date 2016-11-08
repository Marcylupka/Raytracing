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

        /*
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
                            //LightIntensity coloral = sphere.Color;
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
        }*/

        public void reg_object_render_scene(int width, int height, List<Object1> objectList, List<Mesh> meszuList, String name, int gridSize)
        {
            Console.WriteLine();
            Console.WriteLine();
            //Console.WriteLine("renderowanie");
            Console.WriteLine();
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
            float dd = 0;
            //float dist = 0;
            Vector pp1 = new Vector(0, 0, 0);
            Vector pp2 = new Vector(0, 0, 0);

            float radFov = this.fov * (float)Math.PI / 180f;
            float tanFov2 = (float)Math.Tan(radFov / 2f);

            LightIntensity backColor = new LightIntensity(0, 0, 0);
            //int it = 1;
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
            foreach (Mesh meszu in meszuList)
            {
                foreach (Triangle t in meszu.TriangleList)
                {
                    //Console.WriteLine("przeksztalcam trojkaty w obiekty");
                    objectList.Add(t);
                }
            }
            //Console.WriteLine(objectList.Count);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (objectList != null)
                    {
                        //foreach (Object1 object1 in objectList)
                            for (int ilo = 0; ilo<(objectList.Count); ilo++)
                        {
                            Object1 object1 = objectList[ilo];
                            //Console.WriteLine(ilo);
                            //LightIntensity coloral = sphere.Color;
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
                            //Console.WriteLine("licze przeciecie ray: " + i + ", " + j);
                            //LightIntensity pxa = backColor;
                            //LightIntensity pxb = backColor;
                            //LightIntensity pxc = backColor;
                            //LightIntensity pxd = backColor;
                            if (object1.Intersect(ray, ref am, ref pp1, ref dd) == true)
                            {
                                //Console.WriteLine("licze antialiasing");
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
                                    foreach (Object1 objectAA in objectList)
                                    {

                                            LightIntensity pxe = objectAA.Color;
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
                                                    if (objectAA.Intersect(rayAA, ref am, ref pp1, ref dd) == true)
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

        public Boolean contrastFunc(LightIntensity a, LightIntensity b, LightIntensity c, LightIntensity d, float contrast)
        {
            if (((Math.Abs(a.R - b.R)) < contrast) && ((Math.Abs(a.G - b.G)) < contrast) && ((Math.Abs(a.B - b.B)) < contrast) && ((Math.Abs(a.R - c.R)) < contrast) && ((Math.Abs(a.G - c.G)) < contrast) && ((Math.Abs(a.B - c.B)) < contrast) &&
                ((Math.Abs(a.R - d.R)) < contrast) && ((Math.Abs(d.G - b.G)) < contrast) && ((Math.Abs(b.B - c.B)) < contrast) && ((Math.Abs(b.R - c.R)) < contrast) && ((Math.Abs(b.G - c.G)) < contrast) && ((Math.Abs(b.B - c.B)) < contrast) &&
                ((Math.Abs(b.R - d.R)) < contrast) && ((Math.Abs(b.G - d.G)) < contrast) && ((Math.Abs(b.B - d.B)) < contrast) && ((Math.Abs(c.R - d.R)) < contrast) && ((Math.Abs(c.G - d.G)) < contrast) && ((Math.Abs(c.B - d.B)) < contrast))
            {
                return true;
            }
            else return false;
        }

        /*public void adaptive_aa_render_scene(int width, int height, List<Object> objectList, String name, int gridSize)
        {
            float gridStep = 1.0f / gridSize;
            Picture img = new Picture(width, height);
            Sphere probna = new Sphere();
            Sphere probna1 = new Sphere();
            Plain probny1 = new Plain();
            float dis = 0;
            Vector ppp = new Vector();

            float[,] zBuffer = new float[width, height];
            float[,] zBuffera = new float[width, height];
            float[,] zBufferb = new float[width, height];
            float[,] zBufferc = new float[width, height];
            float[,] zBufferd = new float[width, height];
            float[,] zBuffere = new float[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    zBuffer[i, j] = (float)Double.PositiveInfinity;
                    zBuffera[i, j] = (float)Double.PositiveInfinity;
                    zBufferb[i, j] = (float)Double.PositiveInfinity;
                    zBufferc[i, j] = (float)Double.PositiveInfinity;
                    zBufferd[i, j] = (float)Double.PositiveInfinity;
                    zBuffere[i, j] = (float)Double.PositiveInfinity;
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
                    if (objectList != null)
                    {
                        foreach (Object object1 in objectList)
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
                            if(object1.GetType().Name == "spehere") { 
                                probna = (Sphere)object1;
                                if (probna.Intersect(ray, ref am, ref pp1, ref pp2) == true)
                                {
                                    float pixelLeft = (float)(-1 + i * gridStep);
                                    float pixelRight = (float)(-1 + (i + 1) * gridStep);
                                    float pixelCenterX = (float)(pixelLeft + (pixelRight - pixelLeft) / 2.0);
                                    float pixelTop = (float)(1 - j * gridStep);
                                    float pixelBottom = (float)(1 - (j + 1) * gridStep);
                                    float pixelCenterY = (float)(pixelBottom + (pixelTop - pixelBottom) / 2.0);

                                    Vector A = new Vector(pixelLeft * u.X + pixelBottom * v.X + 0,
                                        pixelLeft * u.Y + pixelBottom * v.Y + 0,
                                        pixelLeft * u.Z + pixelBottom * v.Z + 0);

                                    Vector B = new Vector(pixelRight * u.X + pixelBottom * v.X + 0,
                                                            pixelRight * u.Y + pixelBottom * v.Y + 0,
                                                            pixelRight * u.Z + pixelBottom * v.Z + 0);

                                    Vector C = new Vector(pixelRight * u.X + pixelTop * v.X + 0,
                                                            pixelRight * u.Y + pixelTop * v.Y + 0,
                                                            pixelRight * u.Z + pixelTop * v.Z + 0);

                                    Vector D = new Vector(pixelLeft * u.X + pixelTop * v.X + 0,
                                                            pixelLeft * u.Y + pixelTop * v.Y + 0,
                                                            pixelLeft * u.Z + pixelTop * v.Z + 0);

                                    Vector E = new Vector(pixelCenterX * u.X + pixelCenterY * v.X + 0,
                                                            pixelCenterX * u.Y + pixelCenterY * v.Y + 0,
                                                            pixelCenterX * u.Z + pixelCenterY * v.Z + 0);

                                    Ray rayA = new Ray(A, rayDirection);
                                    Ray rayB = new Ray(B, rayDirection);
                                    Ray rayC = new Ray(C, rayDirection);
                                    Ray rayD = new Ray(D, rayDirection);
                                    Ray rayE = new Ray(E, rayDirection);

                                    Vector pointA = new Vector();
                                    Vector pointB = new Vector();
                                    Vector pointC = new Vector();
                                    Vector pointD = new Vector();
                                    Vector pointE = new Vector();

                                    LightIntensity colorA = new LightIntensity();
                                    LightIntensity colorB = new LightIntensity();
                                    LightIntensity colorC = new LightIntensity();
                                    LightIntensity colorD = new LightIntensity();
                                    LightIntensity colorE = new LightIntensity();

                                    foreach (Object object2 in objectList)
                                    {
                                        if (object2.GetType().Name == "Spehere")
                                        {
                                            probna1 = (Sphere)object2;
                                            if (probna1.Intersect(rayA, ref am, ref pp1, ref pp2))
                                            {
                                                float disa = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                                if (disa < zBuffera[i, j])
                                                {
                                                    pointA = pp1;
                                                    colorA = probna1.Color;
                                                    zBuffera[i, j] = disa;
                                                }
                                            }
                                            if (probna1.Intersect(rayB, ref am, ref pp1, ref pp2))
                                            {
                                                float disb = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                                if (disb < zBufferb[i, j])
                                                {
                                                    pointB = pp1;
                                                    colorB = probna1.Color;
                                                    zBufferb[i, j] = disb;
                                                }
                                            }
                                            if (probna1.Intersect(rayC, ref am, ref pp1, ref pp2))
                                            {
                                                float disc = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                                if (disc < zBufferc[i, j])
                                                {
                                                    pointC = pp1;
                                                    colorC = probna1.Color;
                                                    zBufferc[i, j] = disc;
                                                }
                                            }
                                            if (probna1.Intersect(rayD, ref am, ref pp1, ref pp2))
                                            {
                                                float disd = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                                if (disd < zBufferd[i, j])
                                                {
                                                    pointD = pp1;
                                                    colorD = probna1.Color;
                                                    zBufferd[i, j] = disd;
                                                }
                                            }
                                            if (probna1.Intersect(rayE, ref am, ref pp1, ref pp2))
                                            {
                                                float dise = (float)Math.Sqrt(((pp1.Z - this.position.Z) * (pp1.Z - this.position.Z)) + ((pp1.Y - this.position.Y) * (pp1.Y - this.position.Y)) + ((pp1.X - this.position.X) * (pp1.X - this.position.X)));
                                                if (dise < zBuffere[i, j])
                                                {
                                                    pointE = pp1;
                                                    colorE = probna1.Color;
                                                    zBuffere[i, j] = dise;
                                                }
                                            }
                                        }
                                        else if (object2.GetType().Name == "Plain")
                                        {
                                            probny1 = (Plain)object1;
                                            probny1.Intersect(rayA, ref dis, ref ppp);
                                            pointA = ppp;
                                            colorA = probny1.Color;
                                            probny1.Intersect(rayB, ref dis, ref ppp);
                                            pointB = ppp;
                                            colorB = probny1.Color;
                                            probny1.Intersect(rayC, ref dis, ref ppp);
                                            pointC = ppp;
                                            colorC = probny1.Color;
                                            probny1.Intersect(rayD, ref dis, ref ppp);
                                            pointD = ppp;
                                            colorD = probny1.Color;
                                            probny1.Intersect(rayE, ref dis, ref ppp);
                                            pointE = ppp;
                                            colorE = probny1.Color;
                                        }
                                    }

                                    if (!((pointA == null) && (pointB == null) && (pointC == null) && (pointD == null) && (pointE == null)))
                                    {
                                        if (pointA == null) { colorA = probna.Color; }
                                        if (pointB == null) { colorB = probna.Color; }
                                        if (pointC == null) { colorC = probna.Color; }
                                        if (pointD == null) { colorD = probna.Color; }
                                        if (pointE == null) { colorE = probna.Color; }

                                        if (this.contrastFunc(colorA, colorB, colorC, colorD, 0.05f))
                                        {
                                            LightIntensity result = new LightIntensity();
                                            result.R=((float)((colorA.R + colorB.R + colorC.R + colorD.R + colorE.R * 4.0) / 8.0));
                                            result.G=((float)((colorA.G + colorB.G + colorC.G + colorD.G + colorE.G * 4.0) / 8.0));
                                            result.B=((float)((colorA.B + colorB.B + colorC.B + colorD.B + colorE.B * 4.0) / 8.0));
                                            img.setPixel(i, j, result);
                                        }
                                        else
                                        {
                                            LightIntensity result = this.adaptationSampling(pixelLeft, pixelRight, pixelTop, pixelBottom, pixelCenterX, pixelCenterY, colorC, colorE, i, j, object1, img);
                                            result.R=((float)(result.R + ((colorA.R + colorB.R + colorC.R + colorD.R + colorE.R * 3.0) / 8.0)));
                                            result.G=((float)(result.G + ((colorA.G + colorB.G + colorC.G + colorD.G + colorE.G * 3.0) / 8.0)));
                                            result.B=((float)(result.B + ((colorA.B + colorB.B + colorC.B + colorD.B + colorE.B * 3.0) / 8.0)));
                                            img.setPixel(i, j, result);
                                        }
                                    }



                                        /*LightIntensity pxAA = new LightIntensity();
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
                                            foreach (Sphere sphereAA in objectList)
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
                                        }*/
                                    /*}
                            }
                        }

                    }
                }
            }
            img.Obraz.Save(name);
        }

        public LightIntensity adaptationSampling(float pixelLeft, float pixelRight, float pixelTop, float pixelBottom, float pixelCenterX, float pixelCenterY,
                                         LightIntensity colorC, LightIntensity colorE, short i, short j, Object1 object1, Image image)
        {
            return;
            /*short maxIteration = 4;
            this.samplingIteration++;

            //ustalamy nowe wartosci dla malego kwadracika
            pixelLeft = pixelCenterX;
            pixelBottom = pixelCenterY;
            pixelCenterX = (float)(pixelLeft + (pixelRight - pixelLeft) / 2.0);
            pixelCenterY = (float)(pixelBottom + (pixelTop - pixelBottom) / 2.0);
            LightIntensity colorA = colorE;
            LightIntensity colorB = new LightIntensity();
            LightIntensity colorD = new LightIntensity();

            Vector G = new Vector(pixelRight * this.u.getX() + pixelBottom * this.v.getX() + 0,
                                    pixelRight * this.u.getY() + pixelBottom * this.v.getY() + 0,
                                    pixelRight * this.u.getZ() + pixelBottom * this.v.getZ() + 0);

            Vector F = new Vector(pixelLeft * this.u.getX() + pixelTop * this.v.getX() + 0,
                                    pixelLeft * this.u.getY() + pixelTop * this.v.getY() + 0,
                                    pixelLeft * this.u.getZ() + pixelTop * this.v.getZ() + 0);

            Vector H = new Vector(pixelCenterX * this.u.getX() + pixelCenterY * this.v.getX() + 0,
                                    pixelCenterX * this.u.getY() + pixelCenterY * this.v.getY() + 0,
                                    pixelCenterX * this.u.getZ() + pixelCenterY * this.v.getZ() + 0);

            Ray rayG = new Ray(G, this.direction);
            Ray rayF = new Ray(F, this.direction);
            Ray rayH = new Ray(H, this.direction);

            Vector pointG = new Vector();
            Vector pointF = new Vector();
            Vector pointH = new Vector();
            if (object.getClass().getName() == "Sphere")
            {
                pointG = ((Sphere)object).intersectSphere(rayG);
                pointF = ((Sphere)object).intersectSphere(rayF);
                pointH = ((Sphere)object).intersectSphere(rayH);
            }
            else if (object.getClass().getName() == "Plane")
            {
                pointG = ((Plane)object).intersectPlane(rayG);
                pointF = ((Plane)object).intersectPlane(rayF);
                pointH = ((Plane)object).intersectPlane(rayH);
            }


            if (pointG != null) { colorB = lightIntensity; }
            else { colorB = image.getPixel(i, j); }
            if (pointF != null) { colorD = lightIntensity; }
            else { colorD = image.getPixel(i, j); }
            if (pointH != null) { colorE = lightIntensity; }
            else { colorE = image.getPixel(i, j); }

            if (colorA.isEqualContrast(colorB, colorC, colorD))
            {
                LightIntensity result = new LightIntensity();
                result.setR((float)((colorA.getR() + colorB.getR() + colorC.getR() + colorD.getR() + colorE.getR() * 4.0) / 8.0 / (Math.pow(4.0, this.samplingIteration))));
                result.setG((float)((colorA.getG() + colorB.getG() + colorC.getG() + colorD.getG() + colorE.getG() * 4.0) / 8.0 / (Math.pow(4.0, this.samplingIteration))));
                result.setB((float)((colorA.getB() + colorB.getB() + colorC.getB() + colorD.getB() + colorE.getB() * 4.0) / 8.0 / (Math.pow(4.0, this.samplingIteration))));
                return result;
            }
            else
            {
                if (this.samplingIteration < maxIteration)
                {
                    LightIntensity result = this.adaptationSampling(pixelLeft, pixelRight, pixelTop, pixelBottom, pixelCenterX, pixelCenterY, colorC, colorE, lightIntensity, i, j, object, image);
                    result.setR((float)(result.getR() + ((colorA.getR() + colorB.getR() + colorC.getR() + colorD.getR() + colorE.getR() * 3.0) / 8.0 / (Math.pow(4.0, this.samplingIteration)))));
                    result.setG((float)(result.getG() + ((colorA.getG() + colorB.getG() + colorC.getG() + colorD.getG() + colorE.getG() * 3.0) / 8.0 / (Math.pow(4.0, this.samplingIteration)))));
                    result.setB((float)(result.getB() + ((colorA.getB() + colorB.getB() + colorC.getB() + colorD.getB() + colorE.getB() * 3.0) / 8.0 / (Math.pow(4.0, this.samplingIteration)))));
                    return result;
                }
                else
                {
                    LightIntensity result = new LightIntensity();
                    result.setR((float)((colorA.getR() + colorB.getR() + colorC.getR() + colorD.getR() + colorE.getR() * 4.0) / 8.0 / (Math.pow(4.0, this.samplingIteration))));
                    result.setG((float)((colorA.getG() + colorB.getG() + colorC.getG() + colorD.getG() + colorE.getG() * 4.0) / 8.0 / (Math.pow(4.0, this.samplingIteration))));
                    result.setB((float)((colorA.getB() + colorB.getB() + colorC.getB() + colorD.getB() + colorE.getB() * 4.0) / 8.0 / (Math.pow(4.0, this.samplingIteration))));
                    return result;
                }
            }
            */
        //}

    }
}
