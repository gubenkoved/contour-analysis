using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ContoursControlLibrary
{
    public static class Commands
    {
        public static readonly RoutedUICommand DeleteSelectedContours = new RoutedUICommand("Delete selected contours", "DeleteSelectedContours", typeof(Commands));
        public static readonly RoutedUICommand ShowFullContourInfo = new RoutedUICommand("Full contour info", "FullContourInfo", typeof(Commands));
    }
}
