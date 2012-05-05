using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Imaging.Filters;
using System.Drawing.Imaging;
using AForge.Imaging;
using System.Drawing;

namespace AForge.Imaging.Filters
{
    public class PyrDown : BaseTransformationFilter
    {
        private Dictionary<PixelFormat, PixelFormat> _formatTranslations = new Dictionary<PixelFormat, PixelFormat>();
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get { return _formatTranslations; }
        }

        public PyrDown()
        {
            _formatTranslations.Add(PixelFormat.Format8bppIndexed, PixelFormat.Format8bppIndexed);
        }

        protected unsafe override void ProcessFilter(UnmanagedImage sourceData, UnmanagedImage destinationData)
        {
            int w = destinationData.Width;
            int h = destinationData.Height;

            byte* src = (byte*) sourceData.ImageData.ToPointer( );
            byte* dst = (byte*) destinationData.ImageData.ToPointer( );

            int srcStride = sourceData.Stride;
            int dstStride = destinationData.Stride;

            //int srcOffset = srcStride - sourceData.Width;
            //int dstOffset = dstStride - destinationData.Width;

            for (int i = 0; i < h; i++, src += srcStride)
            {
                for (int j = 0; j < w; j++, dst++, src += 2)
                {
                    byte b1 = *src;
                    byte b2 = *(src + 1);
                    byte b3 = *(src + srcStride);
                    byte b4 = *(src + srcStride + 1);

                    byte avg = (byte)((b1 + b2 + b3 + b4) >> 2); // div 4

                    *dst = avg;
                }

                //dst += dstOffset;
                //src += srcOffset;
            }
        }

        protected override Size CalculateNewImageSize(UnmanagedImage sourceData)
        {
            return new Size(sourceData.Width >> 1, sourceData.Height >> 1);
        }
    }
}
