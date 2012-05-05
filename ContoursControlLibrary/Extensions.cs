using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ContoursControlLibrary
{
    /// <summary>
    /// Provides points convertation with coordinate transormation
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Provides coordinates transformation
        /// <remarks>For contour each coordinate belongs to [0; 1] range.
        /// (0,0) - left up corner, and (1,1) - right down corner</remarks>
        /// </summary>
        static class CoordinateConverter
        {
            public static ContourAnalysis.Core.Point ToContourPoint(Size size, System.Windows.Point point)
            {
                return new ContourAnalysis.Core.Point(
                    (float)(point.X / size.Width),
                    (float)(point.Y / size.Height));
            }

            public static System.Windows.Point FromContourPoint(Size size, ContourAnalysis.Core.Point point)
            {
                return new Point(
                    point.X * size.Width,
                    point.Y * size.Height);
            }
        }

        public static ContourAnalysis.Core.Point ToContourPoint(this System.Windows.Point point, Size controlSize)
        {
            return CoordinateConverter.ToContourPoint(controlSize, point);
        }

        public static System.Windows.Point ToWinPoint(this ContourAnalysis.Core.Point point, Size controlSize)
        {
            return CoordinateConverter.FromContourPoint(controlSize, point);
        }
    }
}
