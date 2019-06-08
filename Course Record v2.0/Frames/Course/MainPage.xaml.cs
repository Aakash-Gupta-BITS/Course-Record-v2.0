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
    public sealed partial class MainPage : Page
    {
        private Globals Global = null;
        private readonly LinkedList<NavigationViewItemBase> SecondNavCourseItems = new LinkedList<NavigationViewItemBase>();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;


           SecondNavCourseItems.AddLast(new NavigationViewItem() { Content = "Overview" });
           SecondNavCourseItems.AddLast(new NavigationViewItem() { Content = "Books" });
           SecondNavCourseItems.AddLast(new NavigationViewItem() { Content = "Handout" });
           SecondNavCourseItems.AddLast(new NavigationViewItem() { Content = "Teachers" });
           SecondNavCourseItems.AddLast(new NavigationViewItem() { Content = "CT log" });
           SecondNavCourseItems.AddLast(new NavigationViewItem() { Content = "Time Table" });
           SecondNavCourseItems.AddLast(new NavigationViewItem() { Content = "Events" });
           SecondNavCourseItems.AddLast(new NavigationViewItem() { Content = "Tests" });
           SecondNavCourseItems.AddLast(new NavigationViewItem() { Content = "Files" });

            foreach (NavigationViewItemBase temp in NavView.MenuItems)
            {
                if (temp is NavigationViewItem t)
                {
                    NavView.SelectedItem = t;
                    break;
                }
            }
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            object SelectedItem = (sender.SelectedItem as NavigationViewItem).Content;
            NavView.Header = SelectedItem;

            SecNav.Visibility = Visibility.Visible;
            switch (SelectedItem)
            {
                case "Add Courses":
                    ContentFrame.Navigate(typeof(Add_Course), Global);
                    SecNav.Visibility = Visibility.Collapsed;
                    break;
                default:
                    PopulateSecondNavView(SecondNavCourseItems);
                    break;
            }
        }

        private void PopulateSecondNavView(LinkedList<NavigationViewItemBase> list)
        {
            SecNav.MenuItems.Clear();
            foreach (NavigationViewItemBase temp in list)
                SecNav.MenuItems.Add(temp);
            SecNav.SelectedItem = list.First.Value;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Globals g)
                Global = g;
            base.OnNavigatedTo(e);
        }

        private void SecNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            // object SelectedItem = (sender.SelectedItem as NavigationViewItem).Content;
            switch ((NavView.SelectedItem as NavigationViewItem).Content)
            {
                case "Add Courses":
                    break;
                default:
                    ContentFrame.Navigate(typeof(EmptyPage));
                    break;
            }
        }
    }
}