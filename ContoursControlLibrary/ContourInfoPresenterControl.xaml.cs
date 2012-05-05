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
using System.Collections.ObjectModel;

namespace ContoursControlLibrary
{
    /// <summary>
    /// Interaction logic for ContourInfoPresenterControl.xaml
    /// </summary>
    public partial class ContourInfoPresenterControl : UserControl
    {
        public ContourInformation ContourInfo
        {
            get { return (ContourInformation)GetValue(ContourInfoProperty); }
            set { SetValue(ContourInfoProperty, value); }
        }
        public static readonly DependencyProperty ContourInfoProperty = DependencyProperty.Register("ContourInfo", typeof(ContourInformation), typeof(ContourInfoPresenterControl));

        public object[] ContextMenuExtension
        {
            get { return (object[])GetValue(ContextMenuExtensionProperty); }
            set { SetValue(ContextMenuExtensionProperty, value); }
        }
        public static readonly DependencyProperty ContextMenuExtensionProperty = DependencyProperty.Register("ContextMenuExtension", typeof(object[]), typeof(ContourInfoPresenterControl), new UIPropertyMetadata(new object[0]));

        public bool ShowReverse
        {
            get { return (bool)GetValue(ShowReverseProperty); }
            set { SetValue(ShowReverseProperty, value); }
        }
        public static readonly DependencyProperty ShowReverseProperty = DependencyProperty.Register("ShowReverse", typeof(bool), typeof(ContourInfoPresenterControl), new UIPropertyMetadata(true));

        public ContourInfoPresenterControl()
        {
            InitializeComponent();
        }

        private void ShowFullContourInfo_Executed(object sender, ExecutedRoutedEventArgs e)
        {            
            var fullInfoWin = new FullContourInfoWindow(ContourInfo);
            fullInfoWin.ShowDialog();
        }

        private void ShowFullContourInfo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ContourInfo != null;
        }
        
    }
}
