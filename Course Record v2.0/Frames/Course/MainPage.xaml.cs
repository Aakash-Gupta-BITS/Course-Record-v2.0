using ConsoleAppEngine.Course;
using ConsoleAppEngine.Globals;
using ConsoleAppEngine.Log;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0.Frames.Course
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = sender.SelectedItem as NavigationViewItem;
            NavView.Header = SelectedItem.Content;

            LoggingServices.Instance.WriteLine<MainPage>("\"" + SelectedItem.Content as string + "\" is selected at Course Main Page.");

            HDDSync.SelectedCourse = null;

            if (SelectedItem == AddCoursesNavigation)
            {
                ContentFrame.Navigate(typeof(AllCoursesView));
                SecNav.Visibility = Visibility.Collapsed;
            }
            else
            {
                SecNav.SelectedItem = null;
                SecNav.SelectedItem = OverViewItem;
                SecNav.Visibility = Visibility.Visible;
            }
        }

        private void SecNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = (sender.SelectedItem as NavigationViewItem);
            if (SelectedItem == null)
            {
                return;
            }

            CourseEntry SelectedCourse = null;
            foreach (var x in AllCourses.Instance.CoursesList)
            {
                if ((NavView.SelectedItem as NavigationViewItem).Content.ToString() == x.Title)
                {
                    SelectedCourse = x;
                    break;
                }
            }

            HDDSync.SelectedCourse = SelectedCourse;

            LoggingServices.Instance.WriteLine<MainPage>(SelectedItem.Content as string + " Tab of " + NavView.Header as string + " is selected.");

            switch (SelectedItem.Content)
            {
                case "Overview":
                    ContentFrame.Navigate(typeof(Overview));
                    break;
                case "Books":
                    ContentFrame.Navigate(typeof(Books), SelectedCourse.BookEntry);
                    break;
                case "Handout":
                    ContentFrame.Navigate(typeof(Handout), SelectedCourse.HandoutEntry);
                    break;
                case "Teachers":
                    ContentFrame.Navigate(typeof(Teachers), SelectedCourse);
                    break;
                case "CT log":
                    ContentFrame.Navigate(typeof(CT_log), SelectedCourse.CTLog);
                    break;
                case "Time Table":
                    ContentFrame.Navigate(typeof(TimeTable), SelectedCourse);
                    break;
                case "Events":
                    ContentFrame.Navigate(typeof(Events), SelectedCourse.EventEntry);
                    break;
                case "Tests":
                    ContentFrame.Navigate(typeof(Tests), SelectedCourse.TestEntry);
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoggingServices.Instance.WriteLine<MainPage>("Course Main Page loading...");

            AllCourses.Instance.NavView = NavView as NavigationView;

            foreach (var x in AllCourses.Instance.CoursesList)
            {
                x.InitializeNavViewItem();
                NavView.MenuItems.Add(x.CourseNavigationItem);
            }

            NavView.MenuItems.Add(new NavigationViewItemSeparator());

            if (AllCourses.Instance.CoursesList.Count == 0)
            {
                NavView.SelectedItem = AddCoursesNavigation;
            }
            else
            {
                NavView.SelectedItem = AllCourses.Instance.CoursesList.First.Value.CourseNavigationItem;
            }
            LoggingServices.Instance.WriteLine<MainPage>("Course Main Page loaded.");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HDDSync.SelectedCourse = null;

            NavView.MenuItems.Clear();
            LoggingServices.Instance.WriteLine<MainPage>("Course Main Page unloaded.");
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ContentFrame.ForwardStack.Clear();
            ContentFrame.BackStack.Clear();
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            this.Frame.GoBack();
        }
    }
}