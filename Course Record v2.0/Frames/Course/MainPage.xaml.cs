using System.Collections.Generic;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.Contacts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ConsoleAppEngine.Log;

namespace Course_Record_v2._0.Frames.Course
{
    public sealed partial class MainPage : Page
    {
        private AllCourses allCourses;
        private AllContacts allContacts;
        private readonly NavigationViewItem GoBack = new NavigationViewItem() { Content = "Go Back" };
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = sender.SelectedItem as NavigationViewItem;
            NavView.Header = SelectedItem.Content;

            LoggingServices.Instance.WriteLine<MainPage>("\"" + SelectedItem.Content as string + "\" is selected at Course Main Page.");

            if (SelectedItem == AddCoursesNavigation)
            {
                LinkedList<object> temp = new LinkedList<object>();
                temp.AddLast(allCourses);
                temp.AddLast(allContacts);
                temp.AddLast(NavView);
                ContentFrame.Navigate(typeof(Add_Course), temp);
                SecNav.Visibility = Visibility.Collapsed;
            }
            else if (SelectedItem == GoBack)
            {
                this.Frame.GoBack();
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
            foreach (var x in allCourses.CoursesList)
            {
                if (NavView.SelectedItem == x.navigationViewItem)
                {
                    SelectedCourse = x;
                    break;
                }
            }

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
                    LinkedList<object> lis = new LinkedList<object>();
                    lis.AddLast(SelectedCourse);
                    lis.AddLast(allContacts.TeacherEntry);
                    ContentFrame.Navigate(typeof(Teachers), lis);
                    break;
                case "CT log":
                    lis = new LinkedList<object>();
                    lis.AddLast(SelectedCourse.CTLog);
                    lis.AddLast(allContacts.StudentEntry);
                    ContentFrame.Navigate(typeof(CT_log), lis);
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
                case "Files":
                    ContentFrame.Navigate(typeof(Files));
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoggingServices.Instance.WriteLine<MainPage>("Course Main Page loading...");
            var a = (e.Parameter as LinkedList<object>);
            allCourses = a.First.Value as AllCourses;
            allContacts = a.First.Next.Value as AllContacts;

            foreach (var x in allCourses.CoursesList)
            {
                NavView.MenuItems.Add(x.navigationViewItem);
            }

            NavView.MenuItems.Add(new NavigationViewItemSeparator());
            NavView.MenuItems.Add(new NavigationViewItemHeader() { Content = "Navigation" });
            NavView.MenuItems.Add(GoBack);
            NavView.MenuItems.Add(new NavigationViewItemSeparator());

            if (allCourses.CoursesList.Count == 0)
            {
                NavView.SelectedItem = AddCoursesNavigation;
            }
            else
            {
                NavView.SelectedItem = allCourses.CoursesList.First.Value.navigationViewItem;
            }
            LoggingServices.Instance.WriteLine<MainPage>("Course Main Page loaded.");
        }
        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavView.MenuItems.Clear();
            LoggingServices.Instance.WriteLine<MainPage>("Course Main Page unloaded.");
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ContentFrame.ForwardStack.Clear();
            ContentFrame.BackStack.Clear();
        }
    }
}