using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContourAnalysis.Core;
using AForge.Imaging;
using AForge;

using Point = ContourAnalysis.Core.Point;
using AForge.Imaging.Filters;
using AForge.AdditionalFilters;

namespace ContourAnalysis.Extraction
{
    /// <summary>
    /// 0. Extend image (add indention 1 px on each side)
    /// 1. Dilatation
    /// 2. Delete internal pixels
    /// 3. Trace edge
    /// </summary>
    public class ContourFromBlobExtractor
    {
        private Dilatation3x3 m_dilatation = new Dilatation3x3();
        private DeleteInternalPixels m_deleteInternal = new DeleteInternalPixels();
        private ContourFromEdgeBlobExtractor m_edgeTracer = new ContourFromEdgeBlobExtractor();
        private ResizeNearestNeighbor m_resizer = new ResizeNearestNeighbor(0, 0);

        public PreprocessMethod Method;

        public enum PreprocessMethod
        {
            /// <summary>
            /// Low accuracy, but high stability
            /// </summary>
            ExtendAndDilatation,
            /// <summary>
            /// High accuracy, but expensive
            /// </summary>
            Scale,
            /// <summary>
            /// High accuracy, but low stability
            /// </summary>
            None
        }

        public ContourFromBlobExtractor(PreprocessMethod method)
        {
            Method = method;
        }

        public Contour Extract(UnmanagedImage blob)
        {            
            switch (Method)
            {
                case PreprocessMethod.ExtendAndDilatation:
                    // extend with 1px indention
                    UnmanagedImage preprocessed = UnmanagedImage.Create(blob.Width + 2, blob.Height + 2, blob.PixelFormat);
                    blob.CopyWithOffset(preprocessed, new IntPoint(1, 1));

                    // dilatation
                    m_dilatation.ApplyInPlace(preprocessed);

                    // delete internal pixels
                    m_deleteInternal.ApplyInPlace(preprocessed);

                    // trace edge
                    return m_edgeTracer.Extract(preprocessed);
                case PreprocessMethod.Scale:
                    int scaleMultiplier = 3;
                    m_resizer.NewHeight = blob.Height * scaleMultiplier;
                    m_resizer.NewWidth = blob.Width * scaleMultiplier;

                    // scale blob
                    UnmanagedImage resized = m_resizer.Apply(blob);

                    // delete internal pixels
                    m_deleteInternal.ApplyInPlace(resized);

                    // trace edge
                    return m_edgeTracer.Extract(resized);
                case PreprocessMethod.None:
                    // delete internal pixels
                    m_deleteInternal.ApplyInPlace(blob);

                    // trace edge
                    return m_edgeTracer.Extract(blob);
                default:
                    throw new Exception();
            }            
        }        
    }
}
