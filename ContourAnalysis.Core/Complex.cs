using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContourAnalysis.Core
{
    [Serializable]
    public struct Complex
    {
        private float m_re;
        private float m_im;

        public float Re { get { return m_re; } set { m_re = value; } }
        public float Im { get { return m_im; } set { m_im = value; } }

        public Complex(float re, float im)
        {
            m_re = re;
            m_im = im;
        }

        public Point ToPoint()
        {
            return new Point(m_re, m_im);
        }

        /// <summary>
        /// Calculates Hermit scalar product
        /// </summary>
        public Complex HermitScalarProduct(Complex c2)
        {
            // (a + ib, c + id) = (a + ib) * (c - id) = ac + bd + i(bc - ad)

            return this * c2.Conjugate();
        }
        public Complex Conjugate()
        {
            return new Complex(m_re, -m_im);
        }
        public float AbsoluteValue()
        {
            return (float)Math.Sqrt(SquaredAbsoluteValue());
        }
        public float ArgumentRadians()
        {
            return (float)Math.Atan2(Im, Re);
        }
        public float ArgumentDegrees()
        {
            return (float)(180.0 * Math.Atan2(Im, Re) / Math.PI);
        }
        public float SquaredAbsoluteValue()
        {
            return m_re * m_re + m_im * m_im;
        }

        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.m_re + c2.m_re, c1.m_im + c2.m_im);
        }
        public static Complex operator *(Complex c1, Complex c2)
        {
            // (a + ib) * (c + id) = ac - bd + i(bc + ad)

            return new Complex(c1.m_re * c2.m_re - c1.m_im * c2.m_im, c1.m_re * c2.m_im + c1.m_im * c2.m_re);
        }
        public static Complex operator *(Complex c, float f)
        {
            return new Complex(c.m_re * f, c.m_im * f);
        }
        public static Complex operator *(float f, Complex c)
        {
            return new Complex(c.m_re * f, c.m_im * f);
        }
        public static Complex operator /(Complex c1, Complex c2)
        {
            // (a + ib) / (c + id) = ((a + bi) * (c - di)) / (c^2 + d^2)

            return c1 * c2.Conjugate() / c2.SquaredAbsoluteValue();
        }
        public static Complex operator /(Complex c1, float factor)
        {
            return new Complex(c1.m_re / factor, c1.m_im / factor);
        }

        public override string ToString()
        {
            return string.Format("{0:0.00}{1:+0.00;-0.00;+0.00}i ({2:F3}, {3:F3}°)", Re, Im, AbsoluteValue(), ArgumentDegrees());
        }
    }
}
