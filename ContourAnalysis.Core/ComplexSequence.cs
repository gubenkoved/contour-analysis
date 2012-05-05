using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContourAnalysis.Core
{
    [Serializable]
    public class ComplexSequence
    {
        protected Complex[] m_complexSequence;
        /// <summary>
        /// Array of complex values
        /// </summary>
        public Complex[] ComplexSeq { get { return m_complexSequence; } }

        private Complex[] m_acf;
        /// <summary>
        /// Auto-correlation function of sequence
        /// </summary>
        public Complex[] ACF
        {
            get
            {
                if (m_acf == null)
                    m_acf = AutoCorrelationFunction();

                return m_acf;
            }
        }

        public ComplexSequence(IEnumerable<Complex> values)
        {
            m_complexSequence = values.ToArray();
        }
        public ComplexSequence(Complex[] values)
        {
            m_complexSequence = values;
        }
 
        /// <summary>
        /// Returns absolute lenght
        /// </summary>
        public float Norm()
        {
            var sumOfSquares = 0.0f;
            for (int i = 0; i < m_complexSequence.Length; i++)
            {
                sumOfSquares += m_complexSequence[i].SquaredAbsoluteValue();
            }

            return (float)Math.Sqrt(sumOfSquares);
        }
        /// <summary>
        /// Returns scalar product, where specified shifted on specified value
        /// </summary>
        /// <param name="shift">Cycle shift value</param>
        public Complex ScalarProduct(ComplexSequence seq, int shift = 0)
        {
            if (m_complexSequence.Length != seq.m_complexSequence.Length)
                throw new Exception("Lenght must be equals");

            if (shift < 0 && shift >= m_complexSequence.Length)
                throw new Exception("Shift must be positive and less than sequence len");

            int segmentCount = m_complexSequence.Length;

            Complex result = new Complex();

            for (int i = 0, i2 = shift; i < segmentCount; i++, i2++)
            {
                if (i2 >= segmentCount) i2 -= segmentCount;

                result += m_complexSequence[i].HermitScalarProduct(seq.m_complexSequence[i2]);
            }

            return result;
        }
        /// <summary>
        /// Returns scalar product divided by the product of contours lenght
        /// </summary>        
        public Complex NormalizedScalarProduct(ComplexSequence contour, int shift = 0)
        {
            // NOTE: Performace can be improved by rewriting this code where will be used 
            // only one for-cycle passage instead three (2xNorm + 1xScalarProduct)

            float normProduct = Norm() * contour.Norm();

            Complex scalarProduct = ScalarProduct(contour, shift);

            return scalarProduct / normProduct;
        }
        /// <summary>
        /// Returns reversed complex sequence
        /// </summary>
        public ComplexSequence Reverse()
        {
            return new ComplexSequence(m_complexSequence.Reverse());
        }
        /// <summary>
        /// Return maximal (by absolute value) value of normalized correlation fucntion
        /// </summary>
        public Complex MaximalNormalizedScalarProduct(ComplexSequence second)
        {
            Complex maxNsp = new Complex(0, 0);

            for (int i = 0; i < m_complexSequence.Length; i++)
            {
                Complex nsp = this.NormalizedScalarProduct(second, i);

                if (nsp.AbsoluteValue() > maxNsp.AbsoluteValue())
                    maxNsp = nsp;
            }

            return maxNsp;
        }
        /// <summary>
        /// Returns auto-correlation function - correlation with itself with every shift value
        /// </summary>
        private Complex[] AutoCorrelationFunction()
        {
            // int len = (m_complexSequence.Length + 1) / 2;
            // NOTE: Performace can be improved by reduce ACF calculation on 1/2 contour len
            // ACF is center symmetric function, because            

            int len = m_complexSequence.Length;
            Complex[] acf = new Complex[len];

            for (int i = 0; i < len; i++)
            {
                acf[i] = NormalizedScalarProduct(this, i);
            }

            return acf;
        }
    }

    public static class ComplexSequnceExtenstion
    {
        public static ComplexSequence AsComplexSequence(this Complex[] array)
        {
            return new ComplexSequence(array);
        }
    }
}
