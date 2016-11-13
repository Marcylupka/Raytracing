using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class ColorInfo
    {
        //Trafiony obiekt lub null jeśli promień w nic nie trafił</summary>
        public Object1 HitObject { get; set; }
        //Normalna do punktu trafienia</summary>
        public Vector Normal { get; set; }
        //Punkt trafienia (w koordynatach świata)</summary>
        public Vector HitPoint { get; set; }
        //Promień który trafił obiekt</summary>
        public Ray Ray { get; set; }
        //Światła w scenie
        public List<PointLight> LightList { get; set; }

        public LightIntensity colorize()
        {
            LightIntensity color = new LightIntensity(0, 0, 0);
            foreach (PointLight pl in this.LightList)
            {
                Vector inDirection = (pl.Location - this.HitPoint);
                inDirection.normalize();
                float diffuseFactor = inDirection.dot(this.Normal);
                if (diffuseFactor < 0) { return new LightIntensity(0, 0, 0); }
                else { color += (pl.Color * HitObject.mat.Color * diffuseFactor); }
            }
            color *= (1f/(float)LightList.Count);
           return color;
        }

        /*public ColorRgb Radiance(PointLight light, HitInfo hit)
        {
            Vector3 inDirection = (light.Position - hit.HitPoint).Normalized;
            double diffuseFactor = inDirection.Dot(hit.Normal);
            if (diffuseFactor < 0) { return ColorRgb.Black; }
            return light.Color * materialColor * diffuseFactor;
        }        */
    }
}
