using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuc_Ray
{
    public class HitInfo
    {
        private bool m_isIntersect;
        public bool isIntersect
        {
            get { return m_isIntersect; }
            set { m_isIntersect = value; }
        }

        private Vector m_normal;
        public Vector normal
        {
            get { return m_normal; }
        }

        private Vector m_point;
        public Vector point
        {
            get { return m_point; }
        }

        private float m_distance;
        public float distance
        {
            get { return m_distance; }
        }
        //     private Material

        public HitInfo ()
        {
            m_isIntersect = false;
            m_distance = -1;
            m_normal = new Vector(0, 0, 0);
            m_point = new Vector(0, 0, 0);

        }

        public HitInfo(bool isIntersect, float distance, Vector normal, Vector point)
        {
            m_isIntersect = isIntersect;
            m_distance = distance;
            m_normal = normal;
            m_point = point;
        }

    }
}
