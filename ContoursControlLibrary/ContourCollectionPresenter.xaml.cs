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
using System.Collections.Specialized;

namespace ContoursControlLibrary
{
    /// <summary>
    /// Interaction logic for ContourCollectionPresenter.xaml
    /// </summary>
    public partial class ContourCollectionPresenter : UserControl
    {
        public ObservableCollection<ContourInformation> Contours
        {
            get { return (ObservableCollection<ContourInformation>)GetValue(ContoursProperty); }
            set { SetValue(ContoursProperty, value); }
        }
        public static readonly DependencyProperty ContoursProperty = DependencyProperty.Register("Contours", typeof(ObservableCollection<ContourInformation>), typeof(ContourCollectionPresenter));

        public int ItemHeight
        {
            get { return (int)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register("ItemHeight", typeof(int), typeof(ContourCollectionPresenter), new UIPropertyMetadata(50));

        public ContourCollectionPresenter()
        {
            InitializeComponent();
        }

        private void DeleteSelectedContours_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selected = ContoursBox.SelectedItems.Cast<ContourInformation>().ToList();

            foreach (ContourInformation cInfo in selected)
            {
                Contours.Remove(cInfo);
            }
        }
    }
}
