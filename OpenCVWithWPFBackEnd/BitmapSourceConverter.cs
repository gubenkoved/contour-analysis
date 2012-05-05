//----------------------------------------------------------------------------
//  Copyright (C) 2004-2011 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Emgu.CV;
using System.Runtime.ExceptionServices;

namespace Emgu.CV.WPF
{
    public static class BitmapSourceConvert
    {
        /// <summary>
        /// Delete a GDI object
        /// </summary>
        /// <param name="o">The poniter to the GDI object to be deleted</param>
        /// <returns></returns>
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        /// <summary>
        /// Convert an IImage to a WPF BitmapSource. The result can be used in the Set Property of Image.Source
        /// </summary>
        /// <param name="image">The Emgu CV Image</param>
        /// <returns>The equivalent BitmapSource</returns>
        [HandleProcessCorruptedStateExceptions] // added to handle CSE (Corrupted State Exception)
        public static BitmapSource ToBitmapSource(this IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                try
                {
                    IntPtr ptr = source.GetHbitmap(); // obtain the Hbitmap

                    BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        ptr,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                    DeleteObject(ptr); // release the HBitmap
                    return bs;
                }
                catch (AccessViolationException exc)
                {
                    // 
                    // this "Corrupted State Exception" can be thrown while video capture device switching
                    //MessageBox.Show(exc.Message);

                    return null;
                }
            }
        }
    }
}