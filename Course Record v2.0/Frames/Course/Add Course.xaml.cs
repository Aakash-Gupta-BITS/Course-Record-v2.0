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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Add_Course : Page
    {
        public Add_Course()
        {
            this.InitializeComponent();
        }

        private void LectureUnits_SelectionChanged(object sender, SelectionChangedEventArgs e) => Grid_Lecture.Visibility = Input_3LectureUnits.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
        private void PracticalUnits_SelectionChanged(object sender, SelectionChangedEventArgs e) => Grid_Prac.Visibility = Input_4PracticalUnits.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
        private void HaveTuts_Checked(object sender, RoutedEventArgs e) => Grid_Tut.Visibility = Visibility.Visible;
        private void HaveTuts_Unchecked(object sender, RoutedEventArgs e) => Grid_Tut.Visibility = Visibility.Collapsed;

        private void Search_IC_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {

        }
    }
}
