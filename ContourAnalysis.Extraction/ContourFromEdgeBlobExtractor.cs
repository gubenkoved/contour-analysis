using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContourAnalysis.Core;
using AForge.Imaging;
using AForge;

using Point = ContourAnalysis.Core.Point;

namespace ContourAnalysis.Extraction
{
    public class ContourFromEdgeBlobExtractor
    {
        public ContourFromEdgeBlobExtractor()
        {
        }

        public Contour Extract(UnmanagedImage edgeBlob)
        {
            bool[,] passed = new bool[edgeBlob.Height, edgeBlob.Width];

            List<Point> points = new List<Point>();
            IntPoint first = FindFirst(edgeBlob);
            IntPoint cur = first;

            passed[cur.Y, cur.X] = true;
            points.Add(new Point(cur.X, cur.Y));

            while (true)
            {
                IntPoint? next = FindNext(edgeBlob, passed, cur);
                if (next != null)
                {
                    IntPoint nextVal = next.Value;
                    points.Add(new Point(nextVal.X, nextVal.Y));
                    cur = nextVal;
                }
                else break;
            }

            points.Add(new Point(first.X, first.Y)); // closure

            return new Contour(points.ToArray());
        }

        private unsafe IntPoint? FindNext(UnmanagedImage blob, bool[,] passed, IntPoint current)
        {
            // check order:
            //
            // 1 2 3
            // 8 x 4
            // 7 6 5
            //
            // x - current position

            int w = blob.Width;
            int h = blob.Height;

            IntRange xRange = new IntRange(0, w - 1);
            IntRange yRange = new IntRange(0, h - 1);

            List<IntPoint> order = new List<IntPoint>();

            order.Add(new IntPoint(current.X - 1, current.Y - 1)); // 1
            order.Add(new IntPoint(current.X, current.Y - 1)); // 2
            order.Add(new IntPoint(current.X + 1, current.Y - 1)); // 3
            order.Add(new IntPoint(current.X + 1, current.Y)); // 4
            order.Add(new IntPoint(current.X + 1, current.Y + 1)); // 5
            order.Add(new IntPoint(current.X, current.Y + 1)); // 6
            order.Add(new IntPoint(current.X - 1, current.Y + 1)); // 7
            order.Add(new IntPoint(current.X - 1, current.Y)); // 8

            foreach (IntPoint p in order)
            {
                if (p.InRect(xRange, yRange) && GetPixel(blob, p) != 0 && !passed[p.Y, p.X])
                {
                    passed[p.Y, p.X] = true;
                    return p;
                }
            }

            return null;
        }
        private unsafe IntPoint FindFirst(UnmanagedImage blob)
        {
            for (int j = 0; j < blob.Width; j++)
            {
                IntPoint point = new IntPoint(j, 0);

                if (GetPixel(blob, point) != 0)
                    return point;
            }

            throw new Exception("Invalid blob");
        }
        private unsafe byte GetPixel(UnmanagedImage image, IntPoint point)
        {
            byte* ptr = (byte*)image.ImageData.ToPointer();

            ptr += image.Stride * point.Y + point.X;

            return *ptr;
        }
    }
}
