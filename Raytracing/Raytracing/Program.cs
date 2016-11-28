﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Kuc_Ray
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
            LightIntensity kolorek1 = new LightIntensity(1.0f, 0, 0);
            Vector VV1 = new Vector(-1.5f, 0, 0);
            Sphere SS1 = new Sphere(VV1, 0.85f, kolorek1);
            LightIntensity kolorek2 = new LightIntensity(0, 1.0f, 0);
            Vector VV2 = new Vector(0, 0, 0);
            Sphere SS2 = new Sphere(VV2, 0.85f, kolorek2);
            LightIntensity kolorek3 = new LightIntensity(0, 0, 1.0f);
            Vector VV3 = new Vector(1.5f, 0, 0);
            Sphere SS3 = new Sphere(VV3, 0.85f, kolorek3);
            LightIntensity kolorek4 = new LightIntensity(0, 1, 1);
            Vector VV4 = new Vector(-2, -1, 15);
            Sphere SS4 = new Sphere(VV4, 0.5f, kolorek4);
            List<Object1> object1List = new List<Object1>();
            object1List.Add(SS1);
            object1List.Add(SS2);
            object1List.Add(SS3);
            //sphereList.Add(SS4);
            Vector pos = new Vector(0, 0, -5);
            Vector targ = new Vector(0, 0, 0);
            float fov = 60f;
            OCamera orthoCamera = new OCamera();

            List<Object1> objectList = new List<Object1>();
            //objectList.Clear();
            //objectList.Add(tr1);
            //Vector pos2 = new Vector(0, 60, 60);
            Vector pos2 = new Vector(0, 700, 200);
            Vector targ2 = new Vector(10f, 10f, 10f);
            float fov2 = 90f;
            PCamera perspCamera2 = new PCamera(pos2, targ2, fov2);
            //perspCamera2.reg_object_render_scene(1000, 1000, objectList, "objectnew.jpg", 4);
            List<Mesh> meszuList = new List<Mesh>();
            meszuList = null;
            Mesh meszu = new Mesh();
            //meszuList.Clear();
            //meszuList = meszu.parseOBJ("D:\\Dokumenty\\studia\\studia\\IIst\\2.sem\\fotorealistyczna\\zad1_v1 - Kopia\\Raytracing\\teapot_bez_plane.obj");

            //meszuList[0].Mat = new Material(new LightIntensity(1, 0, 0)); 

            //meszuList = meszu.parseOBJ("D:\\Dokumenty\\studia\\studia\\IIst\\2.sem\\fotorealistyczna\\zad1_v1 - Kopia\\Raytracing\\teapot_z.obj");

            //meszuList = meszu.parseOBJ("D:\\Dokumenty\\studia\\studia\\IIst\\2.sem\\fotorealistyczna\\zad1_v1 - Kopia\\Raytracing\\Sphere0.obj");
            //meszuList.Add(meszu);
            //PointLight light1 = new PointLight(new Vector(50, 10, 0), new LightIntensity(1f, 1f, 1f));
            //PointLight light1 = new PointLight(new Vector(20, 0, -10), new LightIntensity(1f, 1f, 1f));
            PointLight light1 = new PointLight(new Vector(580, 200, 520), new LightIntensity(1f, 1f, 1f));
            PointLight light2 = new PointLight(new Vector(90, 200, 75), new LightIntensity(1f, 1f, 1f));
            //PointLight light1 = new PointLight(new Vector(70, 200, 110), new LightIntensity(1f, 1f, 1f));
            //PointLight light2 = new PointLight(new Vector(110, 200, 70), new LightIntensity(1f, 1f, 1f));
            Material mate = new Material();
            Material mate2 = new Material();
            mate.KDiffuse = new LightIntensity(1f, 1f, 1f);
            mate.KSpecular = new LightIntensity(0.5f, 0.5f, 0.5f);
            mate.Color = new LightIntensity(1f, 0f, 0f);
            Object1 sfera = new Sphere(new Vector(-10,-10,-10), 30f, mate);
            //objectList.Add(sfera);
            mate2.KDiffuse = new LightIntensity(1f, 1f, 1f);
            mate2.KSpecular = new LightIntensity(0.5f, 0.5f, 0.5f);
            mate2.Color = new LightIntensity(0f, 0f, 1f);
            Object1 sfera2 = new Sphere(new Vector(10, 10, 10), 50f, mate2);
            //objectList.Add(sfera2);
            List<PointLight> lights = new List<PointLight>();
            lights.Add(light1);
            lights.Add(light2);

            //perspCamera2.aa_object_render_scene(200, 200, objectList, meszuList, "_szfere_dwie.jpg", 25, lights);

            String path = "D:\\Dokumenty\\studia\\studia\\IIst\\2.sem\\fotorealistyczna\\zad1_v1 - Kopia\\Raytracing\\Raytracing\\world1.jpg";
            Texture earth = new Texture(path);
            Object1 ziemia = new Sphere(new Vector(280, 10, 10), 180f, mate2);
            mate2.HasTexture = true;
            mate2.Text = earth;
            objectList.Add(ziemia);

            String path2 = "D:\\Dokumenty\\studia\\studia\\IIst\\2.sem\\fotorealistyczna\\zad1_v1 - Kopia\\Raytracing\\Raytracing\\metal.jpg";
            Texture metal = new Texture(path2);
            Material mate4 = new Material();
            mate4.KDiffuse = new LightIntensity(1f, 1f, 1f);
            mate4.KSpecular = new LightIntensity(0.5f, 0.5f, 0.5f);
            mate4.Color = new LightIntensity(0f, 0f, 1f);

            Object1 kula = new Sphere(new Vector(-280, 10, 10), 180f, mate4);
            mate4.HasTexture = true;
            mate4.Text = metal;
            kula.name = "sph";
            objectList.Add(kula);

            Vector normpl = new Vector(-0.01397133f,0.9640219f,0.2654553f);

            String path1 = "D:\\Dokumenty\\studia\\studia\\IIst\\2.sem\\fotorealistyczna\\zad1_v1 - Kopia\\Raytracing\\Raytracing\\kafelki.jpg";
            Texture green = new Texture(path1);
            Material mate3 = new Material();
            mate3.KDiffuse = new LightIntensity(1f, 1f, 1f);
            mate3.KSpecular = new LightIntensity(0.5f, 0.5f, 0.5f);
            mate3.Color = new LightIntensity(0f, 0f, 1f);
            mate3.HasTexture = true;
            mate3.Text = green;
            Vector p = new Vector(10f, 10f, 10f);
            Vector pn = pos2 - p;
            pn.normalize();
            Console.WriteLine(pn);
            Object1 tlo = new Plain(p, pn, true, 5f);
            tlo.normal = pn;
            tlo.mat = mate3;
            objectList.Add(tlo);

            

            
            perspCamera2.aa_object_render_scene(500, 500, objectList, meszuList, "_bla.jpg", 25, lights);
        }
    }
}
