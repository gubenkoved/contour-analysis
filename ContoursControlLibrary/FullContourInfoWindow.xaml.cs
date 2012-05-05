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
using ContourAnalysis.Core;
using Visiblox.Charts;
using System.Reflection;

namespace ContoursControlLibrary
{
    /// <summary>
    /// Interaction logic for FullContourInfoWindow.xaml
    /// </summary>
    public partial class FullContourInfoWindow : Window
    {
        private ContourInformation m_contourInformation;

        public FullContourInfoWindow(ContourInformation contourInfo)
        {
            InitializeComponent();

            this.DataContext = m_contourInformation = contourInfo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            DescriptionTextBox.Focus();
            DescriptionTextBox.SelectAll();

            UpdateACFChart();
        }

        private void UpdateACFChart()
        {
            Complex[] acf = m_contourInformation.Contour.ACF;

            var acfValueDataSeries = new DataSeries<int, float>("ACF (absolute value)");
            var acfArgumentDataSeries = new DataSeries<int, float>("ACF (argument)");

            for (int i = 0; i < acf.Length; i++)
            {
                acfValueDataSeries.Add(new DataPoint<int, float>(i, acf[i].AbsoluteValue()));

                float argument = acf[i].ArgumentDegrees();

                if (argument < 0)
                    argument += 360;

                acfArgumentDataSeries.Add(new DataPoint<int, float>(i, argument));
            }

            ACFValueSeries.DataSeries = acfValueDataSeries;
            
            ACFArgumentSeries.YAxis = ACFArgumentAxis;
            ACFArgumentSeries.DataSeries = acfArgumentDataSeries;            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Enter)
            {
                Close();
            }
        }
    }

    public class MomentGetterValueConverter : DependencyObject, IValueConverter
    {
        public enum MomentType
        {
            Moment,
            CentralMoment,
            NormalizedCentralMoment
        }
        
        public MomentType Type
        {
            get { return (MomentType)GetValue(MomentTypeProperty); }
            set { SetValue(MomentTypeProperty, value); }
        }
        
        public static readonly DependencyProperty MomentTypeProperty = DependencyProperty.Register("MomentType", typeof(MomentType), typeof(MomentGetterValueConverter), new UIPropertyMetadata(MomentType.Moment));
        public static readonly MethodInfo MomentMethod = typeof(Contour).GetMethod("Moment");
        public static readonly MethodInfo CentralMomentMethod = typeof(Contour).GetMethod("CentralMoment");
        public static readonly MethodInfo NormalizedCentralMomentMethod = typeof(Contour).GetMethod("NormalizedCentralMoment");

        public object Convert(object obj, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            uint[] methodParams = (parameter as string).Split(',').Select(s => uint.Parse(s)).ToArray();

            uint p = methodParams[0];
            uint q = methodParams[1];

            MethodInfo method = null;
            switch (Type)
            {
                case MomentType.Moment:
                    method = MomentMethod;
                    break;
                case MomentType.CentralMoment:
                    method = CentralMomentMethod;
                    break;
                case MomentType.NormalizedCentralMoment:
                    method = NormalizedCentralMomentMethod;
                    break;
                default:
                    break;
            }
            
            return method.Invoke(obj, new object[] { p, q });
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
