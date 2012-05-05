using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Imaging.Filters;
using System.Drawing.Imaging;
using AForge.Imaging;

namespace AForge.AdditionalFilters
{
    public class DeleteInternalPixels : BaseInPlaceFilter
    {
        private Dictionary<PixelFormat, PixelFormat> _formatTranslations = new Dictionary<PixelFormat, PixelFormat>();
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get { return _formatTranslations; }
        }

        public DeleteInternalPixels()
        {
            _formatTranslations.Add(PixelFormat.Format8bppIndexed, PixelFormat.Format8bppIndexed);
        }

        protected unsafe override void ProcessFilter(UnmanagedImage image)
        {
            UnmanagedImage copy = image.Clone();

            int w = copy.Width;
            int h = copy.Height;

            byte* copyPtr = (byte*)copy.ImageData.ToPointer();
            byte* imagePtr = (byte*)image.ImageData.ToPointer();

            int stride = copy.Stride;
            int offset = stride - w;

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++, copyPtr++, imagePtr++)
                {
                    if (i == 0 || j == 0 || i == h - 1 || j == w - 1)
                        continue;

                    byte up = *(copyPtr - stride);
                    byte left = *(copyPtr - 1);
                    byte right = *(copyPtr + 1);
                    byte down = *(copyPtr + stride);

                    if (up != 0 && left != 0 && right != 0 && down != 0)
                    {
                        *imagePtr = 0;
                    }
                        
                }

                imagePtr += offset;
                copyPtr += offset;
            }
        }
    }
}
