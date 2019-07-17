using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ConsoleAppEngine.TimeTable;

namespace Course_Record_v2._0.Frames
{
    public sealed partial class OverallTimeTableView : Page
    {
        public OverallTimeTableView()
        {
            this.InitializeComponent();
            TimeTableOverallView view = new TimeTableOverallView();
            view.InitializeList();
            view.Sort();
            view.InitializeViews();
            foreach (var x in view.ViewItems)
                ViewList.Items.Add(x);
        }
    }
}
