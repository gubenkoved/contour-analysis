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

namespace ContoursControlLibrary
{
    /// <summary>
    /// Interaction logic for ContourInputControl.xaml
    /// </summary>
    public partial class ContourInputControl : UserControl
    {
        private Contour m_rawContour;
        private Contour m_simplifiedContour;

        //private double m_discretizationStep;
        private bool m_isContourInputMode;

        public int TargetLength
        {
            get { return (int)GetValue(TargetLengthProperty); }
            set { SetValue(TargetLengthProperty, value); }
        }
        public static readonly DependencyProperty TargetLengthProperty = DependencyProperty.Register("TargetLength", typeof(int), typeof(ContourInputControl), new UIPropertyMetadata(30, OnPropertyChanged));

        public bool IsClosedContour
        {
            get { return (bool)GetValue(IsClosedContourProperty); }
            set { SetValue(IsClosedContourProperty, value); }
        }
        public static readonly DependencyProperty IsClosedContourProperty = DependencyProperty.Register("IsClosedContour", typeof(bool), typeof(ContourInputControl), new UIPropertyMetadata(false));

        public Style RawContourStyle
        {
            get { return (Style)GetValue(RawContourStyleProperty); }
            set { SetValue(RawContourStyleProperty, value); }
        }
        public static readonly DependencyProperty RawContourStyleProperty = DependencyProperty.Register("RawContourStyle", typeof(Style), typeof(ContourInputControl));

        public Style SimplifiedContourStyle
        {
            get { return (Style)GetValue(SimplifiedContourStyleProperty); }
            set { SetValue(SimplifiedContourStyleProperty, value); }
        }
        public static readonly DependencyProperty SimplifiedContourStyleProperty = DependencyProperty.Register("SimplifiedContourStyle", typeof(Style), typeof(ContourInputControl));

        public Style SimplifiedContourMarkerStyle
        {
            get { return (Style)GetValue(SimplifiedContourMarkerStyleProperty); }
            set { SetValue(SimplifiedContourMarkerStyleProperty, value); }
        }
        public static readonly DependencyProperty SimplifiedContourMarkerStyleProperty = DependencyProperty.Register("SimplifiedContourMarkerStyle", typeof(Style), typeof(ContourInputControl));

        public Contour Contour
        {
            get
            {
                return m_simplifiedContour;
            }
        }
        
        public ContourInputControl()
        {
            InitializeComponent();
        }

        private void ContourPresenter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var presenter = (ContourPresenterControl)sender;
            var size = presenter.RenderSize;

            if (!m_isContourInputMode && e.LeftButton == MouseButtonState.Pressed)
            {
                var point = e.GetPosition(presenter).ToContourPoint(size);

                if (IsClosedContour)
                    m_rawContour = new Contour(new ContourAnalysis.Core.Point[] { point, point });
                else
                    m_rawContour = new Contour(new ContourAnalysis.Core.Point[] { point });
            
                m_isContourInputMode = true;

                ContourPresenter.CaptureMouse();

                ContoursChanged();
            }
        }
        private void ContourPresenter_MouseMove(object sender, MouseEventArgs e)
        {
            var presenter = (ContourPresenterControl)sender;
            var size = presenter.RenderSize;

            if (m_isContourInputMode)
            {
                var newPoint = e.GetPosition(presenter).ToContourPoint(size);
                var newPoints = new List<ContourAnalysis.Core.Point>();

                for (int i = 0; i < m_rawContour.Points.Length; i++)
                {
                    if (i != m_rawContour.Points.Length - 1)
                        newPoints.Add(m_rawContour.Points[i]);
                    else if (!IsClosedContour)
                        newPoints.Add(m_rawContour.Points[i]);
                }

                newPoints.Add(newPoint);

                if (IsClosedContour)
                    newPoints.Add(newPoints[0]);

                m_rawContour = new Contour(newPoints.ToArray());

                ContoursChanged();
            }
        }
        private void ContourPresenter_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (m_isContourInputMode)
            {
                m_isContourInputMode = false;

                ContourPresenter.ReleaseMouseCapture();
            }
        }

        private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((ContourInputControl)sender).ContoursChanged();
        }
        private void ContoursChanged()
        {
            if (m_rawContour == null)
                return;

            m_simplifiedContour = m_rawContour.EventlyRearrange().BringLength(TargetLength);

            if (ContourPresenter.Contour != m_rawContour)
                ContourPresenter.Contour = m_rawContour;
            else
                ContourPresenter.Draw();

            SimplifiedContourPresenter.Contour = m_simplifiedContour;

            OnContourChanged();
        }

        private void OnContourChanged()
        {
            if (ContourChanged != null)
            {
                ContourChanged.Invoke(this, null);
            }
        }
        public event EventHandler ContourChanged;
    }
}
