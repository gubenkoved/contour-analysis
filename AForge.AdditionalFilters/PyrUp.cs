using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging;
using System.Drawing;

namespace AForge.Imaging.Filters
{
    public class PyrUp : BaseTransformationFilter
    {
        private Dictionary<PixelFormat, PixelFormat> _formatTranslations = new Dictionary<PixelFormat, PixelFormat>();
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get { return _formatTranslations; }
        }

        public PyrUp()
        {
            _formatTranslations.Add(PixelFormat.Format8bppIndexed, PixelFormat.Format8bppIndexed);
        }

        protected unsafe override void ProcessFilter(UnmanagedImage sourceData, UnmanagedImage destinationData)
        {
            int w = sourceData.Width;
            int h = sourceData.Height;

            byte* src = (byte*)sourceData.ImageData.ToPointer();
            byte* dst = (byte*)destinationData.ImageData.ToPointer();

            int dstStride = destinationData.Stride;

            for (int i = 0; i < h; i++, dst += dstStride)
            {
                for (int j = 0; j < w; j++, src++, dst += 2)
                {
                    byte srcVal_01 = (byte)(4 * 0.1 * *src);
                    byte srcVal_005 = (byte)(4 * 0.05 * *src);

                    *dst += srcVal_01;
                    *(dst + 1) += srcVal_01;
                    *(dst + dstStride) += srcVal_01;
                    *(dst + dstStride + 1) += srcVal_01;

                    if (j > 0)
                    {
                        *(dst - 1) += srcVal_005;
                        *(dst + dstStride - 1) += srcVal_005;

                        if (i > 0)
                            *(dst - dstStride - 1) += srcVal_005;

                        if (i < h - 1)
                            *(dst + dstStride + dstStride - 1) += srcVal_005;
                    }

                    if (i > 0)
                    {
                        *(dst - dstStride) += srcVal_005;
                        *(dst - dstStride + 1) += srcVal_005;

                        if (j < w - 1)
                            *(dst - dstStride + 2) += srcVal_005;
                    }

                    if (j < w - 1)
                    {
                        *(dst + 2) += srcVal_005;
                        *(dst + dstStride + 2) += srcVal_005;

                        if (i < h - 1)
                            *(dst + dstStride + dstStride + 2) += srcVal_005;
                    }

                    if (i < h - 1)
                    {
                        *(dst + dstStride + dstStride) += srcVal_005;
                        *(dst + dstStride + dstStride + 1) += srcVal_005;
                    }
                }
            }
        }

        protected override Size CalculateNewImageSize(UnmanagedImage sourceData)
        {
            return new Size(sourceData.Width << 1, sourceData.Height << 1);
        }
    }
}
