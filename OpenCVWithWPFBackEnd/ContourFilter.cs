using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;

namespace OpenCVWithWPFBackEnd
{
    public class ContourFilter
    {
        public double MinArea;

        private Func<Contour<System.Drawing.Point>, bool> m_isAppropriateFunction;

        public ContourFilter(double minArea = 4 * 4)
        {
            m_isAppropriateFunction =
                new Func<Contour<System.Drawing.Point>, bool>(c => c.Area >= MinArea);
        }

        public List<Contour<System.Drawing.Point>> Filter(Contour<System.Drawing.Point> ocvContours)
        {
            List<Contour<System.Drawing.Point>> appropriateContours = new List<Contour<System.Drawing.Point>>();

            ocvContours.ForEach(c =>
                    {
                        if (m_isAppropriateFunction.Invoke(c))
                            appropriateContours.Add(c);
                    });

            return appropriateContours;
        }
    }
}
