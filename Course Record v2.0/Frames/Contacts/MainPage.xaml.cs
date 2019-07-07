using ConsoleAppEngine.Course;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ConsoleAppEngine.Contacts;
using System.Collections.Generic;
using ConsoleAppEngine.Log;

namespace Course_Record_v2._0.Frames.Contacts
{
    public sealed partial class MainPage : Page
    {
        AllContacts allContacts;
        AllCourses allCourses;

        public MainPage()
        {
            InitializeComponent();
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = sender.SelectedItem as NavigationViewItem;

            LoggingServices.Instance.WriteLine<MainPage>("\"" + SelectedItem.Content as string + "\" is selected at Teacher Main Page.");
            if (SelectedItem == null)
                return;

            NavView.Header = SelectedItem.Content;

            if (SelectedItem == TeachersNavigation)
            {
                LinkedList<object> lis = new LinkedList<object>();
                lis.AddLast(allContacts.TeacherEntry);
                lis.AddLast(allCourses);
                ContentFrame.Navigate(typeof(TeacherContacts), lis);
            }
            else if(SelectedItem == StudentsNavigation)
            {
                LinkedList<object> lis = new LinkedList<object>();
                lis.AddLast(allContacts.StudentEntry);
                lis.AddLast(allCourses);
                ContentFrame.Navigate(typeof(StudentContacts), lis);
            }
            else if (SelectedItem == GoBack)
            {
                Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoggingServices.Instance.WriteLine<MainPage>("Constacts Main Page loading...");
            var temp = e.Parameter as LinkedList<object>;
            allContacts = temp.First.Value as AllContacts;
            allCourses = temp.First.Next.Value as AllCourses;
            NavView.SelectedItem = StudentsNavigation;
            LoggingServices.Instance.WriteLine<MainPage>("Contacts Main Page loaded.");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            LoggingServices.Instance.WriteLine<MainPage>("Contacts Main Page unloaded.");
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ContentFrame.ForwardStack.Clear();
            ContentFrame.BackStack.Clear();
        }
    }
}
