using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing
{
    public class OCamera
    {
        public void render_scene(int width, int height, List<Sphere> sphereList, List<Plain> plainList, String name)
        {
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
                                if (pp1.Z < zBuffer[i, j])
                                {
                                    img.setPixel(i, j, sphere.Color);
                                    zBuffer[i, j] = pp1.Z;
                                }
                            }
                        }
                    }
                    /*
                    if (plainList != null)
                    {
                        foreach (Plain plain in plainList)
                        {
                            if (plain.Intersect(ray, ref dist, ref pp1) == true)
                            {
                                img.setPixel(i, j, plain.Color);
                            }
                            else img.setPixel(i, j, backColor);
                        }
                    }
                    */
                }
            }
            img.Obraz.Save(name);
        }
    }
}
