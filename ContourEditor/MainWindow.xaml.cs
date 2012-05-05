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
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;

namespace ContourEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ContourInformation> Contours
        {
            get { return (ObservableCollection<ContourInformation>)GetValue(ContoursProperty); }
            set { SetValue(ContoursProperty, value); }
        }
        public static readonly DependencyProperty ContoursProperty = DependencyProperty.Register("Contours", typeof(ObservableCollection<ContourInformation>), typeof(MainWindow));

        public bool CheckBothDirections
        {
            get { return (bool)GetValue(CheckBothDirectionsProperty); }
            set { SetValue(CheckBothDirectionsProperty, value); }
        }
        public static readonly DependencyProperty CheckBothDirectionsProperty = DependencyProperty.Register("CheckBothDirections", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(true));
        private DependencyPropertyDescriptor m_checkBothDescriptor = DependencyPropertyDescriptor.FromProperty(CheckBothDirectionsProperty, typeof(MainWindow));
        private void OnCheckBothDirectionsPropertyChanged(object sender, EventArgs e)
        {
            m_matcher.CheckBothDirections = CheckBothDirections;
        }

        public bool AutoUpdateInfoInCollection
        {
            get { return (bool)GetValue(AutoUpdateInfoInCollectionProperty); }
            set { SetValue(AutoUpdateInfoInCollectionProperty, value); }
        }
        public static readonly DependencyProperty AutoUpdateInfoInCollectionProperty = DependencyProperty.Register("AutoUpdateInfoInCollection", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(true));        

        private string m_openedPath;

        private OpenFileDialog m_ofd = new OpenFileDialog() { Filter = "Contour collection|*.set" };
        private SaveFileDialog m_sfd = new SaveFileDialog() { Filter = "Contour collection|*.set" };

        enum UpdateState
        {
            ForceUpdate,
            Needed,
            NotNeeded
        }
        private UpdateState m_updateState;
        private Task m_updateTask;

        private ContoursMatcher m_matcher;

        public MainWindow()
        {
            InitializeComponent();

            Contours = new ObservableCollection<ContourInformation>();
            Contours.CollectionChanged += (s, a) => m_updateState = UpdateState.Needed; // update request when collection changed

            m_matcher = new ContoursMatcher() { MinNSP = 0.0f, MinACF_NSP = 0.0f };
            m_matcher.FamiliarContours = Contours;

            ContourInput.ContourChanged += new EventHandler(ContourInput_ContourChanged);
        }
        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            m_checkBothDescriptor.AddValueChanged(this, OnCheckBothDirectionsPropertyChanged);
            
            OnCheckBothDirectionsPropertyChanged(this, new EventArgs());

            m_updateTask = Task.Factory.StartNew(DoUpdaterWork);
        }
        private void InvokeAsync(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            Invoke(action, true, priority);
        }
        private void Invoke(Action action, bool async = false, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (!async)
                Dispatcher.Invoke((Delegate)action, priority);
            else
                Dispatcher.BeginInvoke((Delegate)action, priority);
        }

        private void ContourInput_ContourChanged(object sender, EventArgs e)
        {
            m_updateState = UpdateState.Needed;            
        }
        private void DoUpdaterWork()
        {
            while (true)
            {
                if (m_updateState == UpdateState.NotNeeded)
                    continue;
               
                // make copies
                Contour inputContour = null;
                bool checkBoth = false;
                bool autoUpdateInfo = false;

                Invoke(() =>
                {
                    inputContour = ContourInput.Contour;
                    checkBoth = CheckBothDirections;
                    autoUpdateInfo = AutoUpdateInfoInCollection;
                });

                if (inputContour == null)
                    continue;

                ContoursMatcher.Match bestMatch = m_matcher.GetMatch(inputContour, true);

                if (bestMatch == null)
                    continue;

                Invoke(() => BestMatchContourInfoPresenter.ContourInfo = bestMatch.FamiliarContour);

                // Expensive operation for big collections. Do with background priority to increase responsibility
                if (m_updateState == UpdateState.ForceUpdate || autoUpdateInfo)
                    InvokeAsync(() =>
                        {
                            foreach (var cInfo in Contours)
                            {
                                cInfo.SendDynamicInfosChangedNotification();
                            }
                        }, DispatcherPriority.Background);

                m_updateState = UpdateState.NotNeeded;
            }
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (m_ofd.ShowDialog() == true)
            {
                using (Stream stream = File.Open(m_ofd.FileName, FileMode.Open))
                {
                    try
                    {
                        Contours.Clear();

                        ContourInformationCollection.Load(stream).ForEach(c => Contours.Add(c));                        

                        m_openedPath = m_ofd.FileName;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            }
        }

        private void AddFromCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (m_ofd.ShowDialog() == true)
            {
                using (Stream stream = File.Open(m_ofd.FileName, FileMode.Open))
                {
                    try
                    {
                        ContourInformationCollection.Load(stream).ForEach(c => Contours.Add(c));
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            }
        }   

        private void SaveTo(string path)
        {
            using (Stream stream = File.Create(path))
            {
                try
                {
                    new ContourInformationCollection(Contours).Save(stream);

                    m_openedPath = path;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

            }
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            if (m_sfd.ShowDialog() == true)
            {
                SaveTo(m_sfd.FileName);
            }
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (m_openedPath != null)
            {
                SaveTo(m_openedPath);                
            }
            else
            {
                ApplicationCommands.SaveAs.Execute(null, null);
            }
        }

        private void AddContourCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var contour = Contour.AlignInUnitRectangle(ContourInput.Contour);

            if (contour != null)
                Contours.Add(new ContourInformation(contour));
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Contours.Clear();

            m_openedPath = null;
        }

        private void ForceUpdateCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            m_updateState = UpdateState.ForceUpdate;
        }

        
    }
}
