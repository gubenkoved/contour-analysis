using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace OpenCVWithWPFBackEnd
{
    public static class Commands
    {
        public static readonly RoutedUICommand OpenDevice = new RoutedUICommand("Open device", "OpenDevice", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.O, ModifierKeys.Control) });
        public static readonly RoutedUICommand OpenPicture = new RoutedUICommand("Open picture", "OpenPicture", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.P, ModifierKeys.Control) });
        public static readonly RoutedUICommand StopCapture = new RoutedUICommand("Stop capture", "StopCapture", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.Q, ModifierKeys.Control) });
        public static readonly RoutedUICommand GrabContours = new RoutedUICommand("Grab contours", "GrabContours", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.G, ModifierKeys.Control) });
        public static readonly RoutedUICommand LoadFamiliar = new RoutedUICommand("Load familiar", "LoadFamiliar", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.L, ModifierKeys.Control) });
        public static readonly RoutedUICommand SetShowMode = new RoutedUICommand("Set show mode", "SetShowMode", typeof(Commands));
    }
}
