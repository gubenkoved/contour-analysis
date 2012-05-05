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
using System.Windows.Shapes;
using DirectShowLib;

namespace OpenCVWithWPFBackEnd
{
    /// <summary>
    /// Interaction logic for CaptureSourceSelectorWindow.xaml
    /// </summary>
    public partial class CaptureSourceSelectorWindow : Window
    {
        public static readonly RoutedUICommand ChooseSource = new RoutedUICommand("Choose source", "ChooseSource", typeof(CaptureSourceSelectorWindow));

        public class CaptureDeviceInfo
        {
            public int DeviceID { get; set; }
            public string Name { get; set; }
        }
        public List<CaptureDeviceInfo> AvailableCaptureSources { get; private set; }

        public CaptureDeviceInfo SelectedDevice { get; private set; }

        public CaptureSourceSelectorWindow()
        {
            FillAvailableCaptureSourcesList();

            InitializeComponent();
        }

        private void FillAvailableCaptureSourcesList()
        {
            AvailableCaptureSources = new List<CaptureDeviceInfo>();

            DsDevice[] videoCaptureDevice = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            for (int i = 0; i < videoCaptureDevice.Length; i++)
            {
                DsDevice device = videoCaptureDevice[i];
                CaptureDeviceInfo info = new CaptureDeviceInfo() { DeviceID = i, Name = device.Name };

                AvailableCaptureSources.Add(info);
            }
        }

        private void ChooseSource_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SelectedDevice = (CaptureDeviceInfo)e.Parameter;

            DialogResult = true;

            Close();
        }
    }
}
