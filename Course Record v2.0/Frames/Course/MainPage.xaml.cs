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
using ConsoleAppEngine.Course;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly LinkedList<NavigationViewItemBase> CourseNavigationNames = new LinkedList<NavigationViewItemBase>();
        CourseEntry Math3Course = new CourseEntry("MATHEMATICS 3", "MATH F113", 3, 0, true);

        public MainPage()
        {
            this.InitializeComponent();

            CourseNavigationNames.AddLast(new NavigationViewItem() { Content = "Overview" });
            CourseNavigationNames.AddLast(new NavigationViewItem() { Content = "Books" });
            CourseNavigationNames.AddLast(new NavigationViewItem() { Content = "Handout" });
            CourseNavigationNames.AddLast(new NavigationViewItem() { Content = "Teachers" });
            CourseNavigationNames.AddLast(new NavigationViewItem() { Content = "CT log" });
            CourseNavigationNames.AddLast(new NavigationViewItem() { Content = "Time Table" });
            CourseNavigationNames.AddLast(new NavigationViewItem() { Content = "Events" });
            CourseNavigationNames.AddLast(new NavigationViewItem() { Content = "Tests" });
            CourseNavigationNames.AddLast(new NavigationViewItem() { Content = "Files" });

            LinkedList<NavigationViewItem> x = new LinkedList<NavigationViewItem>();

            foreach (NavigationViewItem t in CourseNavigationNames)
                SecNav.MenuItems.Add(t);


            foreach (NavigationViewItemBase temp in NavView.MenuItems)
            {
                if (temp is NavigationViewItem t)
                {
                    x.AddLast(t);
                }
            }

            NavView.SelectedItem = x.Last.Value;


            Math3Course.HandoutEntry.AddHandout(new EHandoutItem(1, "AgCD", true, "Hekko"));
            Math3Course.HandoutEntry.AddHandout(new EHandoutItem(2, "ABCD", true, "Hekko"));
            Math3Course.HandoutEntry.AddHandout(new EHandoutItem(3, "AfD", true, "Hekko"));

        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            object SelectedItem = (sender.SelectedItem as NavigationViewItem).Content;
            NavView.Header = SelectedItem;

            switch (SelectedItem)
            {
                case "Add Courses":
                    ContentFrame.Navigate(typeof(Add_Course));
                    SecNav.Visibility = Visibility.Collapsed;
                    break;
                default:
                    SecNav.SelectedItem = CourseNavigationNames.First.Value;
                    SecNav.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void SecNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            object SelectedItem = (sender.SelectedItem as NavigationViewItem).Content;
            switch (SelectedItem)
            {
                case "Overview":
                    ContentFrame.Navigate(typeof(Overview));
                    break;
                case "Books":
                    ContentFrame.Navigate(typeof(Books));
                    break;
                case "Handout":
                    ContentFrame.Navigate(typeof(Handout), Math3Course.HandoutEntry);
                    break;
                case "Teachers":
                    ContentFrame.Navigate(typeof(Teachers));
                    break;
                case "CT log":
                    ContentFrame.Navigate(typeof(CT_log));
                    break;
                case "Time Table":
                    ContentFrame.Navigate(typeof(TimeTable));
                    break;
                case "Events":
                    ContentFrame.Navigate(typeof(Events));
                    break;
                case "Tests":
                    ContentFrame.Navigate(typeof(Tests));
                    break;
                case "Files":
                    ContentFrame.Navigate(typeof(Files));
                    break;
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            this.Frame.GoBack();
            this.Frame.ForwardStack.Clear();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e) => ContentFrame.BackStack.Clear();
    }
}