﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Raytracing
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            int am = 0;
            Vector pp1 = new Vector(0, 0, 0);
            Vector pp2 = new Vector(0, 0, 0);

            //INSTRUKCJA 1
            /*
            Vector S0 = new Vector(0, 0, 0);
            Vector R0 = new Vector(0, 0, -20);
            Vector R1D = S0-R0;
            Vector R2D = new Vector(0, 10, 0);
            Sphere S = new Sphere(S0, 10);
            Ray R1 = new Ray(R0, R1D); // 2 punkty przecięcia
            Ray R2 = new Ray(R0, R2D); // brak przecięcia

            Console.WriteLine();
            Console.WriteLine("promien R1");
            S.Intersect(R1, ref am, ref pp1, ref pp2);
            Console.Write("Ilosc punktow przeciecia: ");
            Console.WriteLine(am);
            if (am>1)
            {
                Console.Write("Pierwszy punkt przeciecia: ");
                Console.WriteLine(pp1);
            }

            Console.WriteLine();
            Console.WriteLine("promien R2");
            S.Intersect(R2, ref am, ref pp1, ref pp2);
            Console.Write("Ilosc punktow przeciecia: ");
            Console.WriteLine(am);
            if (am>1)
            {
                Console.Write("Pierwszy punkt przeciecia: ");
                Console.WriteLine(pp1);
            }

            Vector R03 = new Vector(0, 0, 5);
            Vector R3D = new Vector(0, 0, 2);
            Ray R3 = new Raytracing.Ray(R03, R3D);
            Console.WriteLine();
            Console.WriteLine("promien R3");
            S.Intersect(R3, ref am, ref pp1, ref pp2);
            Console.Write("Ilosc punktow przeciecia: ");
            Console.WriteLine(am);
            if (am>1)
            {
                Console.Write("Pierwszy punkt przeciecia: ");
                Console.WriteLine(pp1);
            }

            Console.WriteLine();

            Vector normalna = new Vector(0, 1, 1);
            Plain P1 = new Plain(S0, normalna);
            Vector ppp = new Vector(0, 0, 0);
            float odleglosc = 0;
            Console.WriteLine("plaszczyzna P1");
            if (P1.Intersect(R2, ref odleglosc, ref ppp))
            {
                Console.Write("Punkt przeciecia P1 z R2: ");
                Console.WriteLine(ppp);
            }

            Console.WriteLine();

            Vector S1 = new Vector(0, 0, -20);
            Vector S2 = new Vector(10, 0, 10);
            Vector R4DD = new Vector(0, 0, -1);
            Vector R4D = R4DD - S0;
            Ray R4 = new Ray(S0, R4D);
            //Sphere Sn1 = new Sphere(S1, 10);
            Sphere Sn1 = new Sphere(S2, 10);
            Console.WriteLine();
            Console.WriteLine("promien R4");
            Sn1.Intersect(R4, ref am, ref pp1, ref pp2);
            Console.Write("Ilosc punktow przeciecia: ");
            Console.WriteLine(am);
            if (am > 1)
            {
                Console.Write("Pierwszy punkt przeciecia: ");
                Console.WriteLine(pp1);
            }

            Console.WriteLine();
            */

            //INSTRUKCJA 2

            //ORTHOCAMERA
            /*LightIntensity kolorek1 = new LightIntensity(1, 0.5f, 0);
            Vector VV1 = new Vector(0, 0, 10);
            Sphere SS1 = new Sphere(VV1, 0.2f, kolorek1);
            LightIntensity kolorek2 = new LightIntensity(0, 1, 0.5f);
            Vector VV2 = new Vector(-0.5f, 0, 100);
            Sphere SS2 = new Sphere(VV2, 0.5f, kolorek2);
            LightIntensity kolorek3 = new LightIntensity(0, 0, 1);
            Vector VV3 = new Vector(0.2f, 0.3f, 0);
            Sphere SS3 = new Sphere(VV3, 0.3f, kolorek3);
            LightIntensity kolorek4 = new LightIntensity(1, 1, 1);
            Vector VV4 = new Vector(0, 0, 200);
            Sphere SS4 = new Sphere(VV4, 0.9f, kolorek4);
            List<Sphere> sphereList = new List<Sphere>();
            sphereList.Add(SS1);
            sphereList.Add(SS2);
            sphereList.Add(SS3);
            sphereList.Add(SS4);
            OCamera orthoCamera1 = new OCamera();
            orthoCamera1.render_scene(600, 400, sphereList, null, "ocampro1.jpg");
            OCamera orthoCamera2 = new OCamera();
            orthoCamera2.render_scene(400, 600, sphereList, null, "ocampro2.jpg");
            */

            //PERSPECTIVE CAMERA
            LightIntensity kolorek1 = new LightIntensity(1, 0.5f, 0);
            Vector VV1 = new Vector(0, 0, 0);
            Sphere SS1 = new Sphere(VV1, 0.5f, kolorek1);
            LightIntensity kolorek2 = new LightIntensity(0, 1, 0.5f);
            Vector VV2 = new Vector(1, 0, 5);
            Sphere SS2 = new Sphere(VV2, 0.5f, kolorek2);
            LightIntensity kolorek3 = new LightIntensity(0, 0, 1);
            Vector VV3 = new Vector(2, 1, 10);
            Sphere SS3 = new Sphere(VV3, 0.5f, kolorek3);
            LightIntensity kolorek4 = new LightIntensity(1, 1, 1);
            Vector VV4 = new Vector(-2, -1, 15);
            Sphere SS4 = new Sphere(VV4, 0.5f, kolorek4);
            List<Sphere> sphereList = new List<Sphere>();
            sphereList.Add(SS1);
            sphereList.Add(SS2);
            sphereList.Add(SS3);
            sphereList.Add(SS4);
            Vector pos = new Vector(0, 0, -5);
            Vector targ = new Vector(0, 0, 0);
            float fov = 60f;
            PCamera perspCamera = new PCamera(pos, targ, fov);
            perspCamera.render_scene(1000, 1000, sphereList, null, "pcamera0.jpg");
            perspCamera.render_scene(600, 400, sphereList, null, "pcamera1.jpg");
            perspCamera.render_scene(400, 600, sphereList, null, "pcamera2.jpg");
            OCamera orthoCamera = new OCamera();
            orthoCamera.render_scene(1000, 1000, sphereList, null, "ocamera.jpg");
            orthoCamera.render_scene(600, 400, sphereList, null, "ocamera1.jpg");
            orthoCamera.render_scene(400, 600, sphereList, null, "ocamera2.jpg");

        }
    }
}
