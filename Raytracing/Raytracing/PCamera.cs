using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kuc_Ray
{
    public class PCamera
    {
        private Vector u, v, w;

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

            this.w = this.position - this.target;
            this.w.normalize();
            float distance = w.length;
            this.u = (this.up).cross(w);
            this.u.normalize();
            this.v = w.cross(u);
            this.u.negate();

        }

        public LightIntensity getColor(Ray ray, int lvl, ref List<Object1> objectList, List<PointLight> plList)
        {
            HitInfo hit = new HitInfo();

            float minDist = 1000f;
            Object1 objectHit = null;
            LightIntensity reflected = new LightIntensity(0, 0, 0);
            LightIntensity refract = new LightIntensity(0, 0, 0);

            //  Vector c_normal = new Vector();

            LightIntensity result = new LightIntensity(0, 0, 0);
            foreach (Object1 obj in objectList)
            {
                HitInfo hi = obj.Intersect(ray);

                //    if (obj.Intersect(ray, ref am, ref pp1, ref dist, ref c_normal) == true)
                if (hi.isIntersect == true)
                {
                    if (hi.distance < minDist)
                    {
                        objectHit = obj;
                        hit = hi;
                        minDist = hi.distance;
                    }
                }
            }
            if (objectHit != null)
            {
                LightIntensity ia = new LightIntensity(objectHit.mat.Color * 0.001f);
                //LightIntensity ia = new LightIntensity(0.01f, 0.01f, 0.01f);
                LightIntensity Diff = new LightIntensity(0, 0, 0);
                LightIntensity Spec = new LightIntensity(0, 0, 0);
                LightIntensity textCol = new LightIntensity(0, 0, 0);

                float isReflective = 0f;
                foreach (PointLight pl in plList)
                {
                    Vector L = pl.Location - hit.point;
                    L.normalize();
                    float LdotN = L.dot(hit.normal);
                    if (LdotN < 0) LdotN = 0f;
                    Vector H = L - ray.Direction;
                    H.normalize();
                    float nh = (float)Math.Pow((hit.normal.dot(H)), 2 * 20);

                    if (objectHit.mat.HasTexture == true)
                    {
                        if (objectHit.name == "sph")
                        {
                            Sphere newObjectHit = (Sphere)objectHit;
                            textCol = objectHit.mat.Text.pixelColorSph(hit.point, newObjectHit.Radius, newObjectHit.Center);
                            Diff += pl.Color * LdotN * textCol;
                            Spec += pl.Color * nh * objectHit.mat.KSpecular;
                            //result += ia + Diff + Spec;
                            //result += ia + pl.Color * LdotN * textCol + pl.Color * nh * objectHit.mat.KSpecular;
                        }
                        else if (objectHit.name == "pl")
                        {
                            //Console.WriteLine("im plane");
                            Plain newObjectHit = (Plain)objectHit;
                            textCol = objectHit.mat.Text.pixelColorPl(hit.point, newObjectHit.normal);
                            Diff += pl.Color * LdotN * textCol;
                            Spec += pl.Color * nh * objectHit.mat.KSpecular * 0.01f;
                            //result += ia + Diff + Spec;
                            //result += ia + pl.Color * LdotN * textCol + pl.Color * nh * objectHit.mat.KSpecular * 0.01f;
                        }
                    }
                    else
                    {
                        if (objectHit.name == "sph")
                        {
                            Sphere newObjectHit = (Sphere)objectHit;
                            //textCol = objectHit.mat.Text.pixelColorSph(hit.point, newObjectHit.Radius, newObjectHit.Center);
                            Diff += pl.Color * LdotN * objectHit.mat.Color;
                            Spec += pl.Color * nh * objectHit.mat.KSpecular;
                           // result += ia + Diff + Spec;
                            //result += ia + pl.Color * LdotN * objectHit.mat.Color + pl.Color * nh * objectHit.mat.KSpecular;
                        }
                        else if (objectHit.name == "pl")
                        {
                            //Console.WriteLine("im plane");
                            Plain newObjectHit = (Plain)objectHit;
                            //textCol = objectHit.mat.Text.pixelColorPl(hit.point, newObjectHit.normal);
                            Diff += pl.Color * LdotN * objectHit.mat.Color;
                            Spec += pl.Color * nh * objectHit.mat.KSpecular * 0.01f;
                           // result += ia + Diff + Spec;
                            //result += ia + pl.Color * LdotN * objectHit.mat.Color + pl.Color * nh * objectHit.mat.KSpecular * 0.01f;
                        }
                    }
                }
                if (lvl > 0)
                // refl
                {
                    if (objectHit.mat.Mirror == true)
                    {
                        //isReflective = 1f;
                        float NdotI = hit.normal.dot(ray.Direction); //cos(promień a normalna)
                        Vector R = ray.Direction - hit.normal * 2 * NdotI; //kierunek odbitego (ze wzoru)
                        R.normalize();
                        Vector offset = R * 0.0005f; //żeby promień nie wychodził z powierzchni, tlyko przed
                        Ray rayR = new Ray(hit.point + offset, R);
                        reflected = getColor(rayR, lvl - 1, ref objectList, plList);
                        Diff *= 0.001f; //tekstura fe
                       // reflected += Diff;
                    }
                    if (objectHit.mat.Refract == true)
                    {
                        float rIndex = 1.55f; //indeks refrakcji (niby materiału)
                        float n = 1.0003f / rIndex; //stosunek indeksów powietrza i materiału

                        float NdotI = hit.normal.dot(ray.Direction); //cos(promien a normalna)
                        Vector N = hit.normal;
                        if (NdotI > 0 ) //jak mniejszy od 90st to odwracamy
                        {
                            N = -N;
                            NdotI = hit.normal.dot(ray.Direction);
                        }

                        float sin2t = (n * n) * (1f - NdotI * NdotI);

                        if (sin2t < 1 )
                        {
                            //float tmp = (n + (float)Math.Sqrt(1f - sin2t));
                            Vector T = ray.Direction * n - N * (n + (float)Math.Sqrt(1f - sin2t)); //kierunek prommienia transmitowanego
                            T.normalize();
                            Vector offset = T * 0.0005f;


                            Ray rayT = new Ray(hit.point + offset, T); //promien
                            //LightIntensity absorb = Diff * 0.15f * hit.distance;
                            //LightIntensity transp = new LightIntensity((float)Math.Exp(absorb.R), (float)Math.Exp(absorb.G), (float)Math.Exp(absorb.B));

                            Diff *= 0.001f;
                            refract += /*transp **/ getColor(rayT, lvl - 1, ref objectList, plList);
                        }
                    }
                }
                result += reflected + refract + Diff/* * objectHit.Mat.KDiffuse*/ + Spec;
                return result;
            }
            else

                return new LightIntensity(0, 0, 0);
        }

        public LightIntensity sampling(float xc, float yc, float pw, float ph, ref List<Object1> objectList, int lvl, ref float dist, List<PointLight> plList, int hitcount, int maxHitCount)
        {

            //int hitcount = 0;   
            Vector[] verts = new Vector[4];
            LightIntensity[] colors = new LightIntensity[4];
            verts[0] = new Vector((xc - (0.5f * pw)), (yc + (0.5f * ph)), 0);
            verts[1] = new Vector((xc + (0.5f * pw)), (yc + (0.5f * ph)), 0);
            verts[2] = new Vector((xc + (0.5f * pw)), (yc - (0.5f * ph)), 0);
            verts[3] = new Vector((xc - (0.5f * pw)), (yc - (0.5f * ph)), 0);
            LightIntensity col = new LightIntensity(0, 0, 0);
            //LightIntensity constAmb = new LightIntensity(0.01f, 0f, 0.02f);
            LightIntensity constAmb = new LightIntensity(0.2f, 0.2f, 0.2f);
            Vector newInDir = new Vector();

            for (int i = 0; i < 4; i++)
            {
                colors[i] = new LightIntensity(0, 0, 0);
            }

            int am = 0;
            // Vector pp1 = new Vector(0, 0, 0);

            for (int i = 0; i < 4; i++)
            {
                Vector rayDirection = u * verts[i].X + v * verts[i].Y + w * (-1);
                Ray ray = new Ray(this.position, rayDirection);
                colors[i] = getColor(ray, 5, ref objectList, plList);


            }

            bool similar = ((contrastFunc(colors[0], colors[1], 0.00005f)) && (contrastFunc(colors[0], colors[2], 0.00005f)) && (contrastFunc(colors[0], colors[3], 0.00005f)) && (contrastFunc(colors[1], colors[2], 0.00005f)) && (contrastFunc(colors[1], colors[3], 0.00005f)) && (contrastFunc(colors[2], colors[3], 0.00005f)));
            if (lvl >= 2 || similar)
            {
                LightIntensity co = new LightIntensity(0, 0, 0);
                for (int k = 0; k < 4; k++)
                {
                    co.R += colors[k].R;
                    co.G += colors[k].G;
                    co.B += colors[k].B;
                }
                co.R /= 4;
                co.G /= 4;
                co.B /= 4;
                return co;
            }
            else
            {
                verts[0] = new Vector((xc - (0.25f * pw)), (yc + (0.25f * ph)), 0);
                verts[1] = new Vector((xc + (0.25f * pw)), (yc + (0.25f * ph)), 0);
                verts[2] = new Vector((xc + (0.25f * pw)), (yc - (0.25f * ph)), 0);
                verts[3] = new Vector((xc - (0.25f * pw)), (yc - (0.25f * ph)), 0);
                for (int g = 0; g < 4; g++)
                {
                    colors[g] = sampling(verts[g].X, verts[g].Y, pw / 2, ph / 2, ref objectList, lvl + 1, ref dist, plList, 0, 4);
                }
                LightIntensity co = new LightIntensity(0, 0, 0);
                for (int k = 0; k < 4; k++)
                {
                    co.R += colors[k].R;
                    co.G += colors[k].G;
                    co.B += colors[k].B;
                }
                co.R /= 4;
                co.G /= 4;
                co.B /= 4;
                return co;
            }
            return new LightIntensity(0, 0, 0);
        }

        public void aa_object_render_scene(int width, int height, List<Object1> objectList, List<Mesh> meszuList, String name, int gridSize, List<PointLight> plList)
        {
            Console.WriteLine();
            Console.WriteLine();
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
            Vector pp1 = new Vector(0, 0, 0);
            Vector pp2 = new Vector(0, 0, 0);


            float radFov = this.fov * (float)Math.PI / 180f;
            float tanFov2 = (float)Math.Tan(radFov / 2f);

            LightIntensity backColor = new LightIntensity(1f, 1f, 1f);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    img.setPixel(i, j, backColor);
                }
            }

            float pw = 1f / (float)width,
                ph = 1f / (float)height;


            float aspectRatio = (float)width / (float)height * 1.0f;
            if (meszuList != null)
            {
                foreach (Mesh meszu in meszuList)
                {

                    objectList.Add(meszu);
                    //                    foreach (Triangle t in meszu.TriangleList)
                    {
                        //                     objectList.Add(t);
                    }
                }
            }


            for (int i = 0; i < width; i++)
            {
                if (i % 50 == 0) Console.WriteLine("Wykonano " + 100.0f * i / width + "%");
                for (int j = 0; j < height; j++)
                {

                    if (objectList != null)
                    {
                        float ijx = ((2f * (i + 0.5f) * pw) - 1f) * tanFov2;
                        float ijy = (1f - (2f * (j + 0.5f) * ph)) * tanFov2;
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

                        LightIntensity tmp = sampling(ijx, ijy, (1f / (float)width), (1f / (float)height), ref objectList, 0, ref dd, plList, 0, 4);


                        img.setPixel(i, j, tmp);


                    }
                }
            }

            img.Obraz.Save(name);
        }

        public Boolean contrastFunc(LightIntensity a, LightIntensity b, float contrast)
        {
            if (((Math.Abs(a.R - b.R)) < contrast) && ((Math.Abs(a.G - b.G)) < contrast) && ((Math.Abs(a.B - b.B)) < contrast))
            {
                return true;
            }
            else return false;
        }
    }
}
