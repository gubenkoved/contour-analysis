using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Emgu.CV;
using Emgu.CV.WPF;
using System.Windows.Threading;
using Emgu.CV.Structure;
using Microsoft.Win32;
using ContourAnalysis.Core;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;

using Size = System.Drawing.Size;
using OCVContour = Emgu.CV.Contour<System.Drawing.Point>;
using Rectangle = System.Drawing.Rectangle;

namespace OpenCVWithWPFBackEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Size m_frameSize = new Size(640, 480);

        private Task m_mainWorkerTask;

        private Capture m_capture;
        private Image<Bgr, byte> m_imageSource;
        private bool m_captureFromImage;

        private ContourInformationCollection m_familiar;
        private ContoursMatcher m_matcher;
        private ShowMode m_showMode;

        private double m_fps;
        private DateTime m_lastFrameProcessing;

        private ImagePreprocessor m_preprocessor;
        private ContourFilter m_filter;

        private List<ContourAndMatchInfo> m_lastFindedContours;
        private object m_sync = new object();

        private Font m_font = new Font("Arial", 12, GraphicsUnit.Pixel);

        #region Dependency properties
        private int m_targetLen;
        public int TargetLength
        {
            get { return (int)GetValue(TargetLengthProperty); }
            set { SetValue(TargetLengthProperty, value); }
        }
        public static readonly DependencyProperty TargetLengthProperty = DependencyProperty.Register("TargetLength", typeof(int), typeof(MainWindow), new UIPropertyMetadata(30));
        private DependencyPropertyDescriptor m_targetLenDescriptor = DependencyPropertyDescriptor.FromProperty(TargetLengthProperty, typeof(MainWindow));

        public int WindowSize
        {
            get { return (int)GetValue(WindowSizeProperty); }
            set { SetValue(WindowSizeProperty, value); }
        }
        public static readonly DependencyProperty WindowSizeProperty = DependencyProperty.Register("WindowSize", typeof(int), typeof(MainWindow), new UIPropertyMetadata(31));
        private DependencyPropertyDescriptor m_winSizeDescriptor = DependencyPropertyDescriptor.FromProperty(WindowSizeProperty, typeof(MainWindow));

        public double ThresholdSubstractionConstant
        {
            get { return (double)GetValue(ThresholdSubstractionConstantProperty); }
            set { SetValue(ThresholdSubstractionConstantProperty, value); }
        }
        public static readonly DependencyProperty ThresholdSubstractionConstantProperty = DependencyProperty.Register("ThresholdSubstractionConstant", typeof(double), typeof(MainWindow), new UIPropertyMetadata(20.0));
        private DependencyPropertyDescriptor m_thresholdSubConstantDescriptor = DependencyPropertyDescriptor.FromProperty(ThresholdSubstractionConstantProperty, typeof(MainWindow));

        public double MinArea
        {
            get { return (double)GetValue(MinAreaProperty); }
            set { SetValue(MinAreaProperty, value); }
        }
        public static readonly DependencyProperty MinAreaProperty = DependencyProperty.Register("MinArea", typeof(double), typeof(MainWindow), new UIPropertyMetadata(10.0));
        private DependencyPropertyDescriptor m_minAreaDescriptor = DependencyPropertyDescriptor.FromProperty(MinAreaProperty, typeof(MainWindow));

        public double MinACF_NSP
        {
            get { return (double)GetValue(MinACF_NSPProperty); }
            set { SetValue(MinACF_NSPProperty, value); }
        }
        public static readonly DependencyProperty MinACF_NSPProperty = DependencyProperty.Register("MinACF_NSP", typeof(double), typeof(MainWindow), new UIPropertyMetadata(0.8));
        private DependencyPropertyDescriptor m_minACF_NSPDescriptor = DependencyPropertyDescriptor.FromProperty(MinACF_NSPProperty, typeof(MainWindow));

        public double MinNSP
        {
            get { return (double)GetValue(MinNSPProperty); }
            set { SetValue(MinNSPProperty, value); }
        }
        public static readonly DependencyProperty MinNSPProperty = DependencyProperty.Register("MinNSP", typeof(double), typeof(MainWindow), new UIPropertyMetadata(0.85));
        private DependencyPropertyDescriptor m_minNSPDescriptor = DependencyPropertyDescriptor.FromProperty(MinNSPProperty, typeof(MainWindow));

        private bool m_showContours;
        public bool ShowContours
        {
            get { return (bool)GetValue(ShowContoursProperty); }
            set { SetValue(ShowContoursProperty, value); }
        }
        public static readonly DependencyProperty ShowContoursProperty = DependencyProperty.Register("ShowContours", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(false));
        private DependencyPropertyDescriptor m_showContoursDescriptor = DependencyPropertyDescriptor.FromProperty(ShowContoursProperty, typeof(MainWindow));

        private bool m_showAngle;
        public bool ShowAngle
        {
            get { return (bool)GetValue(ShowAngleProperty); }
            set { SetValue(ShowAngleProperty, value); }
        }
        public static readonly DependencyProperty ShowAngleProperty = DependencyProperty.Register("ShowAngle", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(false));
        private DependencyPropertyDescriptor m_showAngleDescriptor = DependencyPropertyDescriptor.FromProperty(ShowAngleProperty, typeof(MainWindow));

        private bool m_showBounding;
        public bool ShowBounding
        {
            get { return (bool)GetValue(ShowBoundingProperty); }
            set { SetValue(ShowBoundingProperty, value); }
        }
        public static readonly DependencyProperty ShowBoundingProperty = DependencyProperty.Register("ShowBounding", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(true));
        private DependencyPropertyDescriptor m_showBoundingDescriptor = DependencyPropertyDescriptor.FromProperty(ShowBoundingProperty, typeof(MainWindow));

        private bool m_showAccuracy;
        public bool ShowAccuracy
        {
            get { return (bool)GetValue(ShowAccuracyProperty); }
            set { SetValue(ShowAccuracyProperty, value); }
        }
        public static readonly DependencyProperty ShowAccuracyProperty = DependencyProperty.Register("ShowAccuracy", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(false));
        private DependencyPropertyDescriptor m_showAccuracyDescriptor = DependencyPropertyDescriptor.FromProperty(ShowAccuracyProperty, typeof(MainWindow));

        public bool UseBlur
        {
            get { return (bool)GetValue(UseBlurProperty); }
            set { SetValue(UseBlurProperty, value); }
        }
        public static readonly DependencyProperty UseBlurProperty = DependencyProperty.Register("UseBlur", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(false));
        private DependencyPropertyDescriptor m_useBlurDescriptor = DependencyPropertyDescriptor.FromProperty(UseBlurProperty, typeof(MainWindow));

        private bool m_retriveOnlyExternalContours;
        public bool RetriveOnlyExternalContours
        {
            get { return (bool)GetValue(RetriveOnlyExternalContoursProperty); }
            set { SetValue(RetriveOnlyExternalContoursProperty, value); }
        }
        public static readonly DependencyProperty RetriveOnlyExternalContoursProperty = DependencyProperty.Register("RetriveOnlyExternalContours", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(true));
        private DependencyPropertyDescriptor m_retriveOnlyExternalContoursDescriptor = DependencyPropertyDescriptor.FromProperty(RetriveOnlyExternalContoursProperty, typeof(MainWindow));
        
        private void OnDependencyPropertyChanged(object sender, EventArgs e)
        {            
            m_preprocessor.ATWindowSize = WindowSize;
            m_preprocessor.ATSubstractionConstant = ThresholdSubstractionConstant;
            m_preprocessor.UseBlur = UseBlur;

            m_filter.MinArea = MinArea;

            m_matcher.MinNSP = (float)MinNSP;
            m_matcher.MinACF_NSP = (float)MinACF_NSP;

            m_showContours = ShowContours;
            m_targetLen = TargetLength;
            m_showAngle = ShowAngle;
            m_showBounding = ShowBounding;
            m_showAccuracy = ShowAccuracy;
            m_retriveOnlyExternalContours = RetriveOnlyExternalContours;
        }        
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            // Set the logical focus to the window to correct initialize Commands in context menu
            Focus();

            m_matcher = new ContoursMatcher();
            m_preprocessor = new ImagePreprocessor();
            m_filter = new ContourFilter();
            m_mainWorkerTask = Task.Factory.StartNew(() => DoWorkerJob());

            IEnumerable<DependencyPropertyDescriptor> activeDescriptors =
                new DependencyPropertyDescriptor[]
                {
                    m_winSizeDescriptor,
                    m_thresholdSubConstantDescriptor,
                    m_showContoursDescriptor,
                    m_minAreaDescriptor,
                    m_minNSPDescriptor,
                    m_minACF_NSPDescriptor,
                    m_targetLenDescriptor,
                    m_showAngleDescriptor,
                    m_showBoundingDescriptor,
                    m_showAccuracyDescriptor,
                    m_useBlurDescriptor,
                    m_retriveOnlyExternalContoursDescriptor
                };

            activeDescriptors.ToList()
                .ForEach(d => d.AddValueChanged(this, OnDependencyPropertyChanged));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OnDependencyPropertyChanged(this, new EventArgs());
        }
        private void Invoke(Action action)
        {
            Dispatcher.Invoke((Delegate)action);
        }
        //private void InvokeSafe(Action action)
        //{
        //    Action safeAction = () =>
        //        {
        //            try
        //            {
        //                action.Invoke();
        //            }
        //            catch (Exception exc)
        //            {
        //                MessageBox.Show(exc.Message);
        //            }
        //        };

        //    Dispatcher.Invoke((Delegate)safeAction);
        //}
        private void InvokeAsync(Action action)
        {
            Dispatcher.BeginInvoke((Delegate)action);
        }
        private void DoWorkerJob()
        {
            while (true)
            {
                if (m_capture == null && m_captureFromImage == false)
                {
                    Thread.Sleep(10);
                    continue;
                }

                Image<Bgr, byte> frame = null;

                if (m_capture != null)
                {
                    frame = m_capture.QueryFrame();
                }
                else //if (m_captureFromImage)
                {
                    frame = m_imageSource.Clone();
                }

                ProcessFrame(frame);

                double momentalFPS = 1 / (DateTime.Now - m_lastFrameProcessing).TotalSeconds;
                m_fps = 0.2 * momentalFPS + 0.8 * m_fps; // making FPS smoother
                m_lastFrameProcessing = DateTime.Now;
            }
        }
        private void ProcessFrame(Image<Bgr, byte> frame)
        {
            Image<Gray, byte> preprocessed = m_preprocessor.Preprocess(frame);
            Image<Bgr, byte> imageToDraw;

            if (m_showMode == ShowMode.Source)
                imageToDraw = frame;
            else // if (m_showMode == ShowMode.Preprocessed)
                imageToDraw = preprocessed.Convert<Bgr, byte>();

            Emgu.CV.CvEnum.RETR_TYPE retrType = m_retriveOnlyExternalContours ? 
                Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL : 
                Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST;

            OCVContour ocvContours = preprocessed.FindContours(
               Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE,
               retrType);

            // NOTE: To improove recognition system I can use whole groups of contours as individual glyph
            // In this case is necessary to check relative parameters, such as including or relative positioning

            //OCVContour ocvContours = preprocessed.FindContours(
            //   Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE,
            //   Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST);

            List<OCVContour> ocvFilteredContours = m_filter.Filter(ocvContours);

            // thread-safe access to m_lastFindedContours
            lock (m_sync)
            {
                m_lastFindedContours = new List<ContourAndMatchInfo>();

                OpenCVContourConverter.ConvertMany(
                    ocvFilteredContours,
                    m_targetLen,
                    (ocvContour, converted) =>
                    {
                        lock (m_lastFindedContours)
                        {
                            m_lastFindedContours.Add(new ContourAndMatchInfo() { OCVContour = ocvContour, Contour = converted });
                        }
                    });

                if (m_showContours)
                {
                    DrawContours(imageToDraw, ocvFilteredContours);
                }

                m_matcher.GetMatches(
                    m_lastFindedContours,
                    fullContourInfo => fullContourInfo.Contour,
                    (cmInfo, match) => cmInfo.Match = match);

                foreach (ContourAndMatchInfo fcInfo in m_lastFindedContours)
                {
                    if (fcInfo.Match == null)
                        continue;

                    Rectangle boundRect = fcInfo.OCVContour.BoundingRectangle;

                    Graphics g = Graphics.FromImage(imageToDraw.Bitmap);

                    if (m_showBounding)
                        g.DrawRectangle(Pens.Green, boundRect);

                    string text = fcInfo.Match.FamiliarContour.Description;

                    if (m_showAngle)
                        text += string.Format(", {0:F0}°", fcInfo.Match.MaxNSP.ArgumentDegrees());

                    if (m_showAccuracy)
                        text += string.Format(", {0:P1}", fcInfo.Match.MaxNSP.AbsoluteValue());

                    g.DrawString(text, m_font, System.Drawing.Brushes.Red, new PointF(boundRect.Left, boundRect.Top - 13));
                }
            }

            // Update UI
            Invoke(() =>
                {
                    StatTextBlock.Text = string.Format("{0}/{1} (recognized/finded)",
                        m_lastFindedContours.Count(cInfo => cInfo.Match != null),
                        ocvFilteredContours.Count);
                    FPSTextBlock.Text = string.Format("{0:F1} FPS", m_fps);

                    BitmapSource source = imageToDraw.ToBitmapSource();

                    if (m_capture != null || m_captureFromImage != false)
                        ImagePresenter.Source = source;
                });
        }
        private void DrawContours(Image<Bgr, byte> bgrImage, List<Contour<System.Drawing.Point>> ocvContours)
        {
            ocvContours.ForEach(c => bgrImage.Draw(c, new Bgr(System.Drawing.Color.Red), 1));
        }
        private void StopCapture()
        {
            if (m_capture != null)
            {
                m_capture.Dispose();;
                m_capture = null;
            }

            if (m_captureFromImage == true)
            {
                m_captureFromImage = false;
                m_imageSource = null;
            }

            ImagePresenter.Source = null;
        }
        private void StartNewCapture(Capture capture)
        {
            StopCapture();

            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, m_frameSize.Width);
            capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, m_frameSize.Height);

            m_capture = capture;
        }
        private void OpenDevice_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CaptureSourceSelectorWindow sourceDeviceSelector = new CaptureSourceSelectorWindow();

            if (sourceDeviceSelector.ShowDialog() == true)
            {
                var selected = sourceDeviceSelector.SelectedDevice;

                StartNewCapture(new Capture(selected.DeviceID));
            }
        }
        private void StopCapture_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StopCapture();
        }
        private void OpenPicture_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    StopCapture();

                    Image<Bgr, byte> imageFromFile = new Image<Bgr, byte>(ofd.FileName);
                    Image<Bgr, byte> resized = imageFromFile.Resize(m_frameSize.Width, m_frameSize.Height, Emgu.CV.CvEnum.INTER.CV_INTER_AREA);

                    m_imageSource = resized;
                    m_captureFromImage = true;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(string.Format("Error while opening picture: {0}", exc.Message));
                }
            }
        }
        private void LoadFamiliar_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Contours set (*.set)|*.set";

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    Stream stream = File.OpenRead(ofd.FileName);

                    m_familiar = ContourInformationCollection.Load(stream);
                    
                    m_matcher.FamiliarContours = m_familiar;
                    m_matcher.BringFamiliarContours(m_targetLen);

                    LoadedContourCollectionInfo.Text = string.Format("{0} contour(s) has been successfully loaded", m_familiar.Count);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(string.Format("Error while loading contour collection: {0}", exc.Message));
                }
            }
            
        }
        private void SetShowMode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            m_showMode = (ShowMode)e.Parameter;
        }
        private void GrabContours_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lock (m_sync)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Contours set (*.set)|*.set";

                if (sfd.ShowDialog() == true)
                {
                    var contourInfos = m_lastFindedContours.Select(fullInfo => new ContourInformation(Contour.AlignInUnitRectangle(fullInfo.Contour)));
                    var collection = new ContourInformationCollection(contourInfos);

                    try
                    {
                        using (Stream stream = sfd.OpenFile())
                        {
                            collection.Save(stream);
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(string.Format("Error while saving contour collection: {0}", exc.Message));
                    }
                }
            }
        }
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StopCapture();

            Close();
        }

        private class ContourAndMatchInfo
        {
            public OCVContour OCVContour;
            public Contour Contour;
            public ContoursMatcher.Match Match;
        }
        
    }
}
