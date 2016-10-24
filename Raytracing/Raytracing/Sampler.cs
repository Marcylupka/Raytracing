using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Raytracing
{
    public class Sampler
    {
        private int minimum;
        public int Minimum
        {
            get { return minimum; }
            set { minimum = value; }
        }

        private int maximum;
        public int Maximum
        {
            get { return maximum; }
            set { maximum = value; }
        }

        private float contrast;
        public float Contrast
        {
            get { return contrast; }
            set { contrast = value; }
        }

        public LightIntensity adaptiveSampling (Bitmap pix)
        {

            return;
        }
    }
}
