using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class OCamera
    {
        /*public void reg_aa_render_scene(int width, int height, List<Sphere> sphereList, List<Plain> plainList, String name, int gridSize)
        {
            float gridStep = 1.0f / gridSize;
            Picture img = new Picture(width, height);

            float[,] zBuffer = new float[width,height];
            for (int i=0; i<width; i++)
            {
                for (int j=0; j<height; j++)
                {
                    zBuffer[i,j] = (float)Double.PositiveInfinity;
                }
            }

            int am = 0;
            float dist = 0;
            Vector pp1 = new Vector(0, 0, 0);
            Vector pp2 = new Vector(0, 0, 0);

            LightIntensity backColor = new LightIntensity(0, 0, 0);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    img.setPixel(i, j, backColor);
                }
            }
            float szerokoscPixela = 2.0f / width;
            float wysokoscPixela = 2.0f / height;

            float aspectRatio = (float)width / (float)height*1.0f;

            if (aspectRatio >= 1)
            {
                szerokoscPixela = 2.0f * aspectRatio / width;
                wysokoscPixela = 2.0f / height;
            } else
            {
                szerokoscPixela = 2.0f / width;
                wysokoscPixela = 2.0f / aspectRatio / height;
            }

            float srodekX = 0;
            float srodekY = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    srodekX = -1.0f + (i + 0.5f) * szerokoscPixela;
                    srodekY = 1.0f - (j + 0.5f) * wysokoscPixela;
                    Ray ray = new Ray(new Vector(srodekX, srodekY, 0), new Vector(0, 0, 1));
                    if (sphereList != null)
                    {
                        foreach (Sphere sphere in sphereList)
                        {
                            if (sphere.Intersect(ray, ref am, ref pp1, ref pp2) == true)
                            {
                                LightIntensity pxAA = new LightIntensity();
                                float dis = pp1.Z;
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
                                                srodekX = -1.0f + (i + k*gridStep) * szerokoscPixela;
                                                srodekY = 1.0f - (j + l*gridStep) * wysokoscPixela;
                                                Ray rayAA = new Ray(new Vector(srodekX, srodekY, 0), new Vector(0, 0, 1));
                                                if (sphereAA.Intersect(rayAA, ref am, ref pp1, ref pp2) == true)
                                                {
                                                    float disAA = pp1.Z;
                                                    if (disAA < zBufferAA[k, l])
                                                    {
                                                        amountAA[k, l] = pxe;
                                                        zBufferAA[k, l] = disAA;
                                                    }
                                                }
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

            LightIntensity backColor = new LightIntensity(0, 0, 0);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    img.setPixel(i, j, backColor);
                }
            }
            float szerokoscPixela = 2.0f / width;
            float wysokoscPixela = 2.0f / height;

            float aspectRatio = (float)width / (float)height * 1.0f;

            if (aspectRatio >= 1)
            {
                szerokoscPixela = 2.0f * aspectRatio / width;
                wysokoscPixela = 2.0f / height;
            }
            else
            {
                szerokoscPixela = 2.0f / width;
                wysokoscPixela = 2.0f / aspectRatio / height;
            }

            float srodekX = 0;
            float srodekY = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    srodekX = -1.0f + (i + 0.5f) * szerokoscPixela;
                    srodekY = 1.0f - (j + 0.5f) * wysokoscPixela;
                    Ray ray = new Ray(new Vector(srodekX, srodekY, 0), new Vector(0, 0, 1));
                    if (sphereList != null)
                    {
                        foreach (Sphere sphere in sphereList)
                        {
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
        }*/


    }
}
