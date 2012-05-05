using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge;
using AForge.Imaging;
using System.Drawing;

namespace ContourAnalysis.Extraction
{
    public static class Extensions
    {
        public static bool InRect(this IntPoint p, IntRange xRange, IntRange yRange)
        {
            return xRange.IsInside(p.X) && yRange.IsInside(p.Y);
        }

        public unsafe static void CopyWithOffset(this UnmanagedImage source, UnmanagedImage destination, IntPoint offset)
        {
            if (source.Width + offset.X > destination.Width
                || source.Height + offset.Y > destination.Height
                || offset.X < 0 || offset.Y < 0)
                throw new Exception("Invalid parameters");

            byte* ptr = (byte*)destination.ImageData.ToPointer();

            ptr += offset.Y * destination.Stride;
            ptr += offset.X * Bitmap.GetPixelFormatSize(destination.PixelFormat) / 8;

            using (UnmanagedImage destinationAreaWrapper = new UnmanagedImage(new IntPtr(ptr), source.Width, source.Height, destination.Stride, destination.PixelFormat))
            {
                source.Copy(destinationAreaWrapper);
            }
        }
    }
}
