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
using ContourAnalysis.Core;
using System.ComponentModel;

namespace ContoursControlLibrary
{
    /// <summary>
    /// Interaction logic for ContourPresenterControl.xaml
    /// </summary>
    public partial class ContourPresenterControl : UserControl
    {
        public static Style GetContourStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(ContourStyleProperty);
        }
        public static void SetContourStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(ContourStyleProperty, value);
        }
        public static readonly DependencyProperty ContourStyleProperty = DependencyProperty.RegisterAttached("ContourStyle", typeof(Style), typeof(ContourPresenterControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        public Style ContourStyle
        {
            get
            {
                return GetContourStyle(this);
            }
            set
            {
                SetContourStyle(this, value);
            }
        }
        private DependencyPropertyDescriptor m_contourStyleProperyDescriptor = DependencyPropertyDescriptor.FromProperty(ContourStyleProperty, typeof(ContourPresenterControl));

        public static Style GetMarkerStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(MarkerStyleProperty);
        }
        public static void SetMarkerStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(MarkerStyleProperty, value);
        }
        public static readonly DependencyProperty MarkerStyleProperty = DependencyProperty.RegisterAttached("MarkerStyle", typeof(Style), typeof(ContourPresenterControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        public Style MarkerStyle
        {
            get
            {
                return GetMarkerStyle(this);
            }
            set
            {
                SetMarkerStyle(this, value);
            }
        }
        private DependencyPropertyDescriptor m_contourMarkerStyleProperyDescriptor = DependencyPropertyDescriptor.FromProperty(MarkerStyleProperty, typeof(ContourPresenterControl));

        public static Size GetMarkerSize(DependencyObject obj)
        {
            return (Size)obj.GetValue(MarkerSizeProperty);
        }
        public static void SetMarkerSize(DependencyObject obj, Size value)
        {
            obj.SetValue(MarkerSizeProperty, value);
        }
        public static readonly DependencyProperty MarkerSizeProperty = DependencyProperty.RegisterAttached("MarkerSize", typeof(Size), typeof(ContourPresenterControl), new FrameworkPropertyMetadata(new Size(6, 6), FrameworkPropertyMetadataOptions.Inherits));
        public Size MarkerSize
        {
            get
            {
                return GetMarkerSize(this);
            }
            set
            {
                SetMarkerSize(this, value);
            }
        }
        private DependencyPropertyDescriptor m_contourMarkerSizeProperyDescriptor = DependencyPropertyDescriptor.FromProperty(MarkerSizeProperty, typeof(ContourPresenterControl));

        public static Contour GetContour(DependencyObject obj)
        {
            return (Contour)obj.GetValue(ContourProperty);
        }
        public static void SetContour(DependencyObject obj, Contour value)
        {
            obj.SetValue(ContourProperty, value);
        }
        public static readonly DependencyProperty ContourProperty = DependencyProperty.RegisterAttached("Contour", typeof(Contour), typeof(ContourPresenterControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        public Contour Contour
        {
            get
            {
                return GetContour(this);
            }
            set
            {
                SetContour(this, value);
            }
        }
        private DependencyPropertyDescriptor m_contourProperyDescriptor = DependencyPropertyDescriptor.FromProperty(ContourProperty, typeof(ContourPresenterControl));

        private DependencyPropertyDescriptor m_actualHeightDescriptor = DependencyPropertyDescriptor.FromProperty(ActualHeightProperty, typeof(ContourPresenterControl));
        private DependencyPropertyDescriptor m_actualWidthDescriptor = DependencyPropertyDescriptor.FromProperty(ActualWidthProperty, typeof(ContourPresenterControl));

        public ContourPresenterControl()
        {
            InitializeComponent();

            m_contourStyleProperyDescriptor.AddValueChanged(this, OnPropertyChanged);
            m_contourMarkerStyleProperyDescriptor.AddValueChanged(this, OnPropertyChanged);
            m_contourMarkerSizeProperyDescriptor.AddValueChanged(this, OnPropertyChanged);
            m_contourProperyDescriptor.AddValueChanged(this, OnPropertyChanged);

            m_actualHeightDescriptor.AddValueChanged(this, OnPropertyChanged);
            m_actualWidthDescriptor.AddValueChanged(this, OnPropertyChanged);
        }

        public void Draw()
        {            
            ContourCanvas.Children.Clear();
            if (Contour == null) return;

            Size size = new Size(ActualWidth, ActualHeight);
            Polyline polyline = new Polyline();            

            for (int i = 0; i < Contour.Points.Length; i++)
            {
                var contourPoint = Contour.Points[i];
                var winPoint = contourPoint.ToWinPoint(size);

                polyline.Points.Add(winPoint);

                if (MarkerStyle != null)
                {
                    var marker = new Ellipse();                    
                    
                    marker.Width = MarkerSize.Width;
                    marker.Height = MarkerSize.Height;

                    Canvas.SetLeft(marker, winPoint.X - marker.Width / 2);
                    Canvas.SetTop(marker, winPoint.Y - marker.Width / 2);

                    marker.ToolTip = string.Format("{0}", i);

                    marker.Style = MarkerStyle;
                    
                    ContourCanvas.Children.Add(marker);
                }
            }

            polyline.Style = ContourStyle ?? (Style)Resources["DefaultContourStyle"];
            polyline.IsHitTestVisible = false;
            Panel.SetZIndex(polyline, -1);

            ContourCanvas.Children.Add(polyline);
        }
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
        }

        private void OnPropertyChanged(object sender, EventArgs e)
        {
            var presenter = (ContourPresenterControl)sender;

            presenter.Draw();
        }
    }
}
