using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContourAnalysis.Core
{
    [Serializable]
    public struct Point
    {
        private float m_x;
        private float m_y;

        public float X
        {
            get
            {
                return m_x;
            }

            set
            {
                m_x = value;
            }
        }
        public float Y
        {
            get
            {
                return m_y;
            }

            set
            {
                m_y = value;
            }
        }

        public Point(float x, float y)
        {
            m_x = x;
            m_y = y;
        }
        
        public Complex ToComplex()
        {
            return new Complex(m_x, m_y);
        }
        public float SquaredNorm()
        {
            return m_x * m_x + m_y * m_y;
        }
        public float Norm()
        {
            return (float)Math.Sqrt(SquaredNorm());
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.m_x - p2.m_x, p1.m_y - p2.m_y);
        }
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.m_x + p2.m_x, p1.m_y + p2.m_y);
        }
        public static Point operator *(Point p1, float f)
        {
            return new Point(p1.m_x * f, p1.m_y * f);
        }

        public override string ToString()
        {
            return string.Format("({0:F4}, {1:F4})", m_x, m_y);
        }
    }
}
