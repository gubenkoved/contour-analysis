using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ContourEditor
{
    public static class Commands
    {
        public static readonly RoutedUICommand AddContour = new RoutedUICommand("Add contour", "AddContour", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.B, ModifierKeys.Control) });
        public static readonly RoutedUICommand AddFrom = new RoutedUICommand("Add from...", "AddFrom", typeof(Commands), new InputGestureCollection() { new KeyGesture(Key.P, ModifierKeys.Control) });
        public static readonly RoutedUICommand ForceUpdate = new RoutedUICommand("Force update", "ForceUpdate", typeof(Commands));
    }
}
