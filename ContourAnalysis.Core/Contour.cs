using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ContourAnalysis.Core
{
    [Serializable]
    public class Contour : ComplexSequence
    {
        private Point[] m_pointsRep; // 2D points representation
        public Point[] Points
        {
            get
            {
                if (m_pointsRep == null)
                    CalculatePointsRepresentation(ComplexSeq);

                return m_pointsRep;
            }
        }

        public Contour(Point[] points)
            : base(CalculateChainCodeRepresentation(points))
        {
            m_pointsRep = points;
        }
        /// <summary>
        /// Returns contour with target lenght
        /// </summary>
        public Contour BringLength(int targetLength)
        {
            if (m_pointsRep.Length <= 1)
                return this;

            float multiplier = (float)(m_pointsRep.Length - 1) / (targetLength - 1);

            Point[] newPoints = new Point[targetLength];
            newPoints[0] = m_pointsRep[0]; // add first point

            int currentPointIndx = 1;

            while (currentPointIndx != targetLength)
            {
                if (currentPointIndx != targetLength - 1)
                {
                    float value = currentPointIndx * multiplier;
                    int integralPart = (int)Math.Truncate(value);
                    float fractionalPart = value - integralPart;

                    Point p1 = m_pointsRep[integralPart];
                    Point p2 = m_pointsRep[integralPart + 1];

                    newPoints[currentPointIndx] = Interpolate(p1, p2, fractionalPart);
                }
                else
                {
                    newPoints[currentPointIndx] = m_pointsRep[m_pointsRep.Length - 1]; // add last point

                }

                ++currentPointIndx;
            }

            return new Contour(newPoints.ToArray());
        }
        /// <summary>
        /// Returns rearranged contour where points located with equal distance in original contour
        /// </summary>
        public Contour EventlyRearrange()
        {
            float totalDistance = 0f;
            for (int i = 0; i < m_complexSequence.Length; i++)
                totalDistance += m_complexSequence[i].AbsoluteValue();

            if (totalDistance == 0.0f)
                return this;

            float distanceStep = totalDistance / (m_pointsRep.Length - 1);

            var newPoints = new List<Point>();
            newPoints.Add(m_pointsRep[0]); // add first point

            int cur = 0;
            float curFinishFactor = 0;
            float distance = distanceStep;

            while (newPoints.Count < m_pointsRep.Length - 1)
            {
                var curSegment = m_pointsRep[cur + 1] - m_pointsRep[cur];
                float curSegmentLen = curSegment.Norm();
                float restPartLen = curSegmentLen * (1 - curFinishFactor);

                // point in current segment
                if (restPartLen > distance)
                {
                    curFinishFactor += distance / curSegmentLen;
                    newPoints.Add(Interpolate(m_pointsRep[cur], m_pointsRep[cur + 1], curFinishFactor));
                    distance = distanceStep;
                }
                else // point not in current segment, go next
                {
                    distance -= restPartLen;
                    curFinishFactor = 0;
                    ++cur;
                }                
            }

            newPoints.Add(m_pointsRep[m_pointsRep.Length - 1]);

            return new Contour(newPoints.ToArray());
        }        
        /// <summary>
        /// Returns center of gravity = (avg(x), avg(y))
        /// </summary>
        public Point CenterOfGravity()
        {
            int len = m_pointsRep.Length;
            float sumX = 0.0f;
            float sumY = 0.0f;

            for (int i = 0; i < len; i++)
            {
                sumX += m_pointsRep[i].X;
                sumY += m_pointsRep[i].Y;
            }

            return new Point(sumX / len, sumY / len);
        }            
        private static Point[] CalculatePointsRepresentation(Complex[] complexSequence)
        {
            int segmentsAmount = complexSequence.Length;

            Point current = new Point(0,0);

            Point[] pointsRep = new Point[segmentsAmount + 1];
            pointsRep[0] = current; // first point is (0, 0)
            
            for (int i = 0; i < segmentsAmount; i++)
			{
                current += complexSequence[i].ToPoint();

                pointsRep[i + 1] = current;
			}

            return pointsRep;
        }
        private static Complex[] CalculateChainCodeRepresentation(Point[] points)
        {
            Complex[] m_chainCodeRep = new Complex[points.Length - 1];

            if (points.Length <= 1)
                return m_chainCodeRep;

            for (int i = 1; i < points.Length; i++)
            {
                Complex diff = (points[i] - points[i - 1]).ToComplex();

                m_chainCodeRep[i - 1] = diff;
            }

            return m_chainCodeRep;
        }
        private Point Interpolate(Point p1, Point p2, float moveFactor)
        {
            // linear interpolation
            return new Point(
                p1.X * (1 - moveFactor) + p2.X * moveFactor, 
                p1.Y * (1 - moveFactor) + p2.Y * moveFactor);
        }

        #region Moments
        /// <summary>
        /// Calculates (p,q) contour moment = SUM x^p * y^q
        /// </summary>
        public double Moment(uint p, uint q)
        {
            double result = 0.0f;
            int len = m_pointsRep.Length;

            for (int i = 0; i < len; i++)
            {
                double x = m_pointsRep[i].X;
                double y = m_pointsRep[i].Y;

                result += IntPow(x, p) * IntPow(y, q);
            }

            return result;
        }
        /// <summary>
        /// Calculates (p,q) central contour moment = SUM (x - avg(x))^p * (y - avg(y))^q
        /// </summary>
        public double CentralMoment(uint p, uint q)
        {
            Point center = CenterOfGravity();

            double result = 0.0f;
            int len = m_pointsRep.Length;

            for (int i = 0; i < len; i++)
            {
                double x = m_pointsRep[i].X;
                double y = m_pointsRep[i].Y;

                result += IntPow(x - center.X, p) * IntPow(y - center.Y, q);
            }

            return result;
        }
        public double NormalizedCentralMoment(uint p, uint q)
        {
            float a = (p + q) / 2f + 1;

            return CentralMoment(p, q) / Math.Pow(CentralMoment(0, 0), a);
        }
        /// <summary>
        /// Optimized version for integer power
        /// </summary>
        private double IntPow(double value, uint pow)
        {
            double result = 1.0f;

            while (pow != 0)
            {
                if (pow % 2 == 1)
                {
                    result *= value;
                }

                value *= value;
                pow /= 2;
            }

            return result;
        }
        #endregion

        public static Contour AlignInUnitRectangle(Contour c)
        {
            const float indention = 0.05f;
            const float side = 1.0f;
            const float sideWithoutIndentions = side - 2 * indention;

            float minX = c.Points.Select(p => p.X).Min();
            float minY = c.Points.Select(p => p.Y).Min();

            float maxX = c.Points.Select(p => p.X).Max();
            float maxY = c.Points.Select(p => p.Y).Max();

            float realW = maxX - minX;
            float realH = maxY - minY;

            float maxSide = (float)Math.Max(realW,realH);

            float mult = sideWithoutIndentions / maxSide;

            var points = new List<ContourAnalysis.Core.Point>();

            foreach (var p in c.Points)
            {
                float newX = indention + (p.X - minX) * mult + (sideWithoutIndentions - realW * mult) / 2.0f;
                float newY = indention + (p.Y - minY) * mult + (sideWithoutIndentions - realH * mult) / 2.0f;

                Core.Point np = new Core.Point(newX, newY);

                points.Add(np);
            }

            return new Contour(points.ToArray());
        }
    }
}
