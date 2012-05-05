using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContourAnalysis.Core;
using Emgu.CV;
using OCVContour = Emgu.CV.Contour<System.Drawing.Point>;
using System.Threading.Tasks;

namespace OpenCVWithWPFBackEnd
{
    public static class OpenCVContourConverter
    {
        public static Contour Convert(OCVContour ocvContour, int targetLen)
        {
            int ocvLen = ocvContour.Total;
            Point[] points = new Point[ocvLen + 1];

            for (int i = 0; i < ocvLen; i++)
            {
                points[i] = new Point(ocvContour[i].X, ocvContour[i].Y);
            }

            points[ocvLen] = points[0]; // clojure contour

            return new Contour(points).BringLength(targetLen);
        }

        public static List<Contour> ConvertMany(OCVContour ocvContours, int targetLen)
        {
            List<OCVContour> allContours = new List<OCVContour>();
            ocvContours.ForEach(c => allContours.Add(c));

            return ConvertMany(allContours, targetLen);
        }

        public static List<Contour> ConvertMany(List<OCVContour> allContours, int targetLen)
        {
            List<Contour> result = new List<Contour>();

            Parallel.ForEach(allContours,
                contour =>
                {
                    var converted = Convert(contour, targetLen);

                    lock (result)
                    {
                        result.Add(converted);
                    }
                });

            return result;
        }

        public static void ConvertMany(List<OCVContour> allContours, int targetLen, Action<OCVContour, Contour> customContourConvertedHandler)
        {
            Parallel.ForEach(allContours,
                ocvContour =>
                {
                    var converted = Convert(ocvContour, targetLen);

                    customContourConvertedHandler.Invoke(ocvContour, converted);
                });
        }
    }

}
