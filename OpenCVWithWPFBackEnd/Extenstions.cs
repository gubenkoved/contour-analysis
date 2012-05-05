using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;

namespace OpenCVWithWPFBackEnd
{
    public static class Extenstions
    {
        public static void ForEach<T>(this Contour<T> contours, Action<Contour<T>> action) where T : struct
        {
            var cur = contours;

            while (cur != null)
            {
                action.Invoke(cur);

                cur = cur.HNext;
            }
        }

        public static int Amount<T>(this Contour<T> contours) where T : struct
        {
            int count = 0;

            contours.ForEach(c => ++count);

            return count;
        }
    }
}
