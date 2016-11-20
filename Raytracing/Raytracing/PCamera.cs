﻿using System;
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

        public LightIntensity sampling(float xc, float yc, float pw, float ph, ref List<Object1> objectList, int lvl, ref float dist, List<PointLight> plList)
        {

            //float dist=0;
            Vector[] verts = new Vector[4];
            LightIntensity[] colors  = new LightIntensity[4];
            verts[0] = new Vector((xc - (0.5f * pw)), (yc + (0.5f * ph)), 0);
            verts[1] = new Vector((xc + (0.5f * pw)), (yc + (0.5f * ph)), 0);
            verts[2] = new Vector((xc + (0.5f * pw)), (yc - (0.5f * ph)), 0);
            verts[3] = new Vector((xc - (0.5f * pw)), (yc - (0.5f * ph)), 0);
            //Console.WriteLine(verts[0]);
            LightIntensity col = new LightIntensity(0, 0, 0);
            //LightIntensity constAmb = new LightIntensity(0.01f, 0f, 0.02f);
            LightIntensity constAmb = new LightIntensity(0.2f, 0.2f, 0.2f);
            Vector newInDir = new Vector();

            for (int i=0; i<4; i++)
            {
                colors[i] = new LightIntensity(0, 0, 0);
            } 

            int am = 0;
            Vector pp1 = new Vector(0, 0, 0);
            
            for (int i = 0; i < 4; i++)
            {
                float minDist = 1000f;
                Object1 objectHit = null;
                Vector nrm = null;

                Vector rayDirection = u * verts[i].X + v * verts[i].Y + w * (-1);
                Ray ray = new Ray(this.position, rayDirection);

                foreach (Object1 obj in objectList)
                {
                    if (obj.Intersect(ray, ref am, ref pp1, ref dist) == true)
                    {
                        if (dist < minDist)
                        {
                            objectHit = obj;
                            minDist = dist;

                        }
                    }
                }
                if (objectHit != null)
                {
                    //Console.WriteLine("PRZECIALEM!!!");
                    Vector reflected = new Vector();
                    LightIntensity result = new LightIntensity(0, 0, 0);
                    //result += constAmb * objectHit.mat.Color;
                    Vector norm = new Vector();
                    if (objectHit.name != "sph")
                    {
                        //     Console.WriteLine("jestem trojkatem");
                        Triangle trHit = (Triangle)objectHit;
                        /*Vector Vector1 = new Vector();
                        Vector1.X = trHit.VertexA.X - trHit.VertexB.X;
                        Vector1.Y = trHit.VertexA.Y - trHit.VertexB.Y;
                        Vector1.Z = trHit.VertexA.Z - trHit.VertexB.Z;
                        Vector Vector2 = new Vector();
                        Vector2.X = trHit.VertexB.X - trHit.VertexC.X;
                        Vector2.Y = trHit.VertexB.Y - trHit.VertexC.Y;
                        Vector2.Z = trHit.VertexB.Z - trHit.VertexC.Z;*/

                        trHit.Normal = (trHit.VertexB - trHit.VertexA).cross(trHit.VertexC - trHit.VertexA);

                        //trHit.Normal.X = (Vector1.Y * Vector2.Z - Vector1.Z * Vector2.Y);
                        //trHit.Normal.Y = (Vector1.Z * Vector2.X - Vector1.X * Vector2.Z);
                        //trHit.Normal.Z = (Vector1.X * Vector2.Y - Vector1.Y * Vector2.X);

                        trHit.Normal.normalize();

                        foreach (PointLight pl in plList)
                        {
                            //result *= pl.Color;
                            Vector inDirection = (pl.Location - pp1);
                            inDirection.normalize();
                            //Console.WriteLine("1: " + inDirection.ToString());
                            newInDir = new Vector(inDirection);//*/ inDirection;
                                                                      //Console.WriteLine("2: " + newInDir.ToString());
                            //newInDir.normalize();
                            float diffuseFactor = newInDir.dot(trHit.Normal);
                            if (diffuseFactor < 0) { return new LightIntensity(0, 0, 0); }

                            result += (pl.Color * objectHit.mat.Color * diffuseFactor * objectHit.mat.KDiffuse);
                            reflected = newInDir.reflect(newInDir, trHit.Normal);
                        }
                    }
                    else if (objectHit.name == "sph")
                    {
                        //Console.WriteLine("Jestem sfera");
                        Sphere trHit = (Sphere)objectHit;
                        //Console.WriteLine("Srodek " + trHit.Center);
                        //Console.WriteLine("Promien " + trHit.Radius);
                        trHit.Normal = pp1 - trHit.Center;
                        trHit.Normal.normalize();
                        //Console.WriteLine("Normalna " + norm);
                        foreach (PointLight pl in plList)
                        {
                            Vector inDirection = (pl.Location - pp1);
                            inDirection.normalize();
                            //Console.WriteLine("1: " + inDirection.ToString());
                            newInDir = new Vector(inDirection);//*/ inDirection;
                                                                      //Console.WriteLine("2: " + newInDir.ToString());
                            //newInDir.normalize();
                            float diffuseFactor = newInDir.dot(trHit.Normal);
                            if (diffuseFactor < 0) { return new LightIntensity(0, 0, 0); }

                            result += (pl.Color * objectHit.mat.Color * diffuseFactor * objectHit.mat.KDiffuse);
                            reflected = newInDir.reflect(newInDir, trHit.Normal);
                            norm = trHit.Normal;
                        }
                    }

                    /*
                    LightIntensity ambient = new LightIntensity(0f, 0f, 0.1f);
                    LightIntensity specular = new LightIntensity(1f, 1f, 1f);

                    Vector L = pp1 - this.position;
                    L.normalize();

                    float LdotN = L.dot(norm);
                    if (LdotN < 0) LdotN = 0;
                    ray.Direction.normalize();
                    Vector H = L - ray.Direction;
                    H.normalize();

                    float nh = (float)Math.Pow(norm.dot(H), 16);

                    colors[i] += ambient + objectHit.mat.Color * LdotN + specular * nh;
                }
                else colors[i] = new LightIntensity(0, 0, 0);
                */


                    //Console.WriteLine("Result: " + result);
                    //result *= (1f/(float)plList.Count);
                    float phongFactor = 0;
                    ray.Direction.normalize();
                    //Vector tmpVect = new Vector(this.position);
                    //tmpVect.sub(pp1);
                    //tmpVect.normalize();
                    reflected = newInDir.reflect(newInDir, norm);
                    float cosAngle = reflected.dot(-ray.Direction);//;(tmpVect)
                    
                    //if (cosAngle < 0) phongFactor = 0;
                    //else phongFactor = (float)Math.Pow(cosAngle, objectHit.mat.SpecularExponent);
                    phongFactor = (float)Math.Pow(cosAngle, objectHit.mat.SpecularExponent);
                    if (phongFactor != 0)
                    {
                        result += (objectHit.mat.Color * objectHit.mat.KSpecular * phongFactor);
                        colors[i] += result;
                    }
                    //else colors[i] += constAmb * objectHit.mat.Color;
                    else colors[i] = new LightIntensity(0, 0, 0);
                }
                else colors[i] = new LightIntensity(0, 0, 0);

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
                    colors[g] = sampling(verts[g].X, verts[g].Y, pw / 2, ph / 2, ref objectList, lvl + 1, ref dist, plList);
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
                    foreach (Triangle t in meszu.TriangleList)
                    {
                        objectList.Add(t);
                    }
                }
            }


            for (int i = 0; i < width; i++)
            {
                if (i % 50 == 0) Console.WriteLine("Wykonano " + 100.0f*i/width +  "%");
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

                        LightIntensity tmp = sampling(ijx, ijy, (1f / (float)width), (1f / (float)height), ref objectList, 0, ref dd, plList);
                        

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
