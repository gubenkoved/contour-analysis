using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Video.DirectShow;
using System.Threading;
using AForge.Video;
using AForge.AdditionalFilters;
using ContourAnalysis.Core;
using System.IO;
using ContourAnalysis.Extraction;
using AForge;

namespace ContourAnalysis
{
    public partial class MainForm : Form
    {
        private class FindedContourInfo
        {
            public Contour Contour;
            public IntPoint CenterOfGravity;
            public Rectangle Rect;
        }

        private VideoCaptureDevice m_device;
        private Size m_frameSize = new Size(640, 480);

        private FiltersSequence m_preprocessingFilters;

        private double m_fps;
        private int m_frameCount;
        private DateTime m_lastFPSUpdate;

        private UnmanagedImage m_currentUnpreprocessed;
        private UnmanagedImage m_currentPreprocessed;

        private List<FindedContourInfo> m_findedContours;

        private Thread m_workerThread;
        private AutoResetEvent m_frameRecievedEvent = new AutoResetEvent(false);

        private enum ShowImageMode
        {
            Source,
            Preprocessed
        }
        private ShowImageMode m_showMode;

        private ContourFromBlobExtractor m_extractor = new ContourFromBlobExtractor(ContourFromBlobExtractor.PreprocessMethod.Scale);
        private Font m_font = new System.Drawing.Font("Arial", 12, GraphicsUnit.Pixel);
        private BlobCounter m_blobExtractor = new BlobCounter();
        private Brush m_textBrush = Brushes.Red;
        
        private bool m_showAngle = false;
        private bool m_showBoundingRect = false;
        private int m_targetContoursLenght = 30;

        private ContoursMatcher m_matcher;

        public MainForm()
        {
            InitializeComponent();

            m_findedContours = new List<FindedContourInfo>();            
            m_matcher = new ContoursMatcher();

            m_workerThread = new Thread(DoWorkerJob);
            m_workerThread.Start();
        }

        private void StopVideoCaptureDevice()
        {
            if (m_device == null)
                return;

            m_device.SignalToStop();
            m_device.WaitForStop();

            m_device.NewFrame -= NewFrameReceived;

            m_device = null;
        }
        private void OpenLogithechButton_Click(object sender, EventArgs e)
        {
            StopVideoCaptureDevice();

            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            foreach (FilterInfo device in videoDevices)
            {
                if (device.Name.Contains("Logitech"))
                {
                    m_device = new VideoCaptureDevice(device.MonikerString);

                    m_device.DesiredFrameSize = m_frameSize;

                    m_device.NewFrame += NewFrameReceived;
                    m_device.Start();

                    return;
                }
            }
        }
        private void OpenDeviceButton_Click(object sender, EventArgs e)
        {
            StopVideoCaptureDevice();

            using (VideoCaptureDeviceForm openDeviceForm = new VideoCaptureDeviceForm())
            {
                if (openDeviceForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openDeviceForm.VideoDevice != null)
                    {
                        m_device = openDeviceForm.VideoDevice;

                        m_device.NewFrame += NewFrameReceived;
                        m_device.Start();
                    }
                }
            }
        }
        private void OpenPictureButton_Click(object sender, EventArgs e)
        {
            StopVideoCaptureDevice();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap managedImage = (Bitmap)Bitmap.FromFile(ofd.FileName);

                ResizeBicubic resizer = new ResizeBicubic(m_frameSize.Width, m_frameSize.Height);
                Bitmap resized = resizer.Apply(managedImage);

                NewFrameReceived(this, new NewFrameEventArgs(resized));
            }
        }
        private void NewFrameReceived(object sender, NewFrameEventArgs args)
        {
            m_currentUnpreprocessed = UnmanagedImage.FromManagedImage(args.Frame);

            // allows worker thread to do the job
            m_frameRecievedEvent.Set();

            if ((DateTime.Now - m_lastFPSUpdate).TotalMilliseconds > 1000)
            {
                m_fps = m_frameCount / (DateTime.Now - m_lastFPSUpdate).TotalSeconds;

                m_lastFPSUpdate = DateTime.Now;
                m_frameCount = 0;
            }
        }
        private void DoWorkerJob()
        {
            // infinite cycle
            while (true)
            {
                m_frameRecievedEvent.WaitOne();

                ProcessFrame();
            }
        }



        private void ProcessFrame()
        {
            UnmanagedImage unpreprocessedCopy = m_currentUnpreprocessed.Clone();
            m_currentPreprocessed = m_preprocessingFilters.Apply(unpreprocessedCopy);

            Bitmap image = new Bitmap(m_currentPreprocessed.Width, m_currentPreprocessed.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);            
            Graphics graphics = Graphics.FromImage(image);

            if (m_showMode == ShowImageMode.Source)
                graphics.DrawImageUnscaled(m_currentUnpreprocessed.ToManagedImage(), 0, 0);
            else
                graphics.DrawImageUnscaled(m_currentPreprocessed.ToManagedImage(), 0, 0);

            m_blobExtractor.ProcessImage(m_currentPreprocessed);
            var blobs = m_blobExtractor.GetObjects(m_currentPreprocessed, false);

            lock (m_findedContours)
            {
                m_findedContours.Clear();
                foreach (var blob in blobs)
                {
                    Contour contour = m_extractor.Extract(blob.Image);
                    m_findedContours.Add(new FindedContourInfo()
                    {
                        Contour = contour.BringLength(m_targetContoursLenght),
                        CenterOfGravity = blob.CenterOfGravity,
                        Rect = blob.Rectangle
                    });
                }
                
                foreach (var finded in m_findedContours)                    
                {
                    ContoursMatcher.Match bestMatch = m_matcher.GetMatch(finded.Contour);

                    if (bestMatch != null)
                    {
                        var text = string.Format("{0}", bestMatch.FamiliarContour.Description, bestMatch.MaxNSP.AbsoluteValue());
                        var textPosition = new PointF(finded.Rect.Left + finded.Rect.Width / 2, finded.Rect.Bottom);

                        graphics.DrawString(text, m_font, m_textBrush, textPosition);

                        if (m_showAngle)
                            graphics.DrawString(string.Format("{0:F0}°", bestMatch.MaxNSP.ArgumentDegrees()), m_font, m_textBrush, PointF.Add(textPosition, new Size(0, 13)));

                        if (m_showBoundingRect)
                            graphics.DrawRectangle(Pens.Green, finded.Rect);
                    }
                }
            }

            ImageBox.Image = image;
            BlobsCountLabel.Text = string.Format("blobs count: {0}", blobs.Length);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_workerThread.Abort();

            if (m_device != null)
            {
                m_device.SignalToStop();
                m_device.WaitForStop();
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Contour collection|*.set";

            lock (m_findedContours)
            {
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (Stream stream = sfd.OpenFile())
                    {
                        var findedContourInfos = m_findedContours.Select(finded => new ContourInformation(Contour.AlignInUnitRectangle(finded.Contour)));
                        var findedCollection = new ContourInformationCollection(findedContourInfos);
                        
                        findedCollection.Save(stream);
                    }
                }
            }
        }
        private void LoadFamiliarButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Contour collection|*.set";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream stream = File.Open(ofd.FileName, FileMode.Open))
                {
                    var loadedContours = ContourInformationCollection.Load(stream);

                    m_matcher.FamiliarContours = loadedContours;
                    m_matcher.BringFamiliarContours(m_targetContoursLenght);
                }
            }
        }       

        private void PreprocessParams_Changed(object sender, EventArgs e)
        {
            int winSize = (int)WindowSizeSelector.Value;
            float brightnessDiffLimit = (float)PixelBrightnessLimitSelector.Value;

            bool filterSmallBlobs = FilterSmallBlobsCheckBox.Checked;                        
            bool filterBigBlobs = FilterBigBlobsCheckBox.Checked;

            bool filterSmallCoupled = FilterSmallCoupledCheckBox.Checked;
            bool filterBigCoupled = FilterBigCoupledCheckBox.Checked;

            int minWidth = (int)MinWidthSelector.Value;
            int maxWidth = (int)MaxWidthSelector.Value;

            int minHeight = (int)MinHeightSelector.Value;
            int maxHeight = (int)MaxHeightSelector.Value;

            var filter = new FiltersSequence(
                    Grayscale.CommonAlgorithms.RMY,
                    new BradleyLocalThresholding() { WindowSize = winSize, PixelBrightnessDifferenceLimit = brightnessDiffLimit },
                    new Invert()                
            );

            if (filterSmallBlobs)
                filter.Add(new BlobsFiltering() { MinWidth = minWidth, MinHeight = minHeight, CoupledSizeFiltering = filterSmallCoupled });

            if (filterBigBlobs)
                filter.Add(new BlobsFiltering() { MaxWidth = maxWidth, MaxHeight = maxHeight, CoupledSizeFiltering = filterBigCoupled });

            m_preprocessingFilters = filter;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowStageSelector.Items.Add(ShowImageMode.Source);
            ShowStageSelector.Items.Add(ShowImageMode.Preprocessed);
            ShowStageSelector.SelectedIndex = 0;
            //ShowStageSelector.SelectedItem = ShowImageMode.Source;


            TextColorSelector.Items.Add(Color.Red);
            TextColorSelector.Items.Add(Color.White);
            TextColorSelector.Items.Add(Color.Black);
            TextColorSelector.Items.Add(Color.Green);
            TextColorSelector.Items.Add(Color.Blue);
            TextColorSelector.Items.Add(Color.Yellow);
            //TextColorSelector.SelectedItem = Color.Red;
            TextColorSelector.SelectedIndex = 0;
            

            PreprocessParams_Changed(this, new EventArgs());

            CommonParams_Changed(this, new EventArgs());
        }

        private void CommonParams_Changed(object sender, EventArgs e)
        {
            m_targetContoursLenght = (int)ContoursLenghtSelector.Value;
            m_matcher.MinNSP = (float)MinMatchNSPSelector.Value;            

            if (ShowStageSelector.SelectedItem != null)
                m_showMode = (ShowImageMode)ShowStageSelector.SelectedItem;

            if (TextColorSelector.SelectedItem != null)
                m_textBrush = new SolidBrush((Color)TextColorSelector.SelectedItem);

            m_showAngle = ShowAngleCheckBox.Checked;
            m_showBoundingRect = ShowBoundingRectCheckBox.Checked;
        }      
    }
}
