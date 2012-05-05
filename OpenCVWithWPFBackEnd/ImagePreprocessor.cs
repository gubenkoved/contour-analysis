using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace OpenCVWithWPFBackEnd
{
    public class ImagePreprocessor
    {
        public int ATWindowSize;
        public double ATSubstractionConstant;
        public bool UseBlur;

        public ImagePreprocessor(int winSize = 33, double substractionConstant = 15.0)
        {
            ATWindowSize = winSize;
            ATSubstractionConstant = substractionConstant;
        }

        public Image<Gray, byte> Preprocess(Image<Bgr, byte> source)
        {
            Image<Gray, byte> grayscaled = source.Convert<Gray, Byte>();

            if (UseBlur)
            {
                grayscaled = grayscaled.PyrDown().PyrUp();
            }

            Image<Gray, byte> thresholded = grayscaled.CopyBlank();

            CvInvoke.cvAdaptiveThreshold(
                grayscaled,
                thresholded,
                255,
                Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C,
                Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY,
                ATWindowSize,
                ATSubstractionConstant);

            thresholded._Not();

            return thresholded;           
        }        
    }
}
