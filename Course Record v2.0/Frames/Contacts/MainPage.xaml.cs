using ConsoleAppEngine.Course;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ConsoleAppEngine.Contacts;
using System.Collections.Generic;

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
            var temp = e.Parameter as LinkedList<object>;
            allContacts = temp.First.Value as AllContacts;
            allCourses = temp.First.Next.Value as AllCourses;
            NavView.SelectedItem = StudentsNavigation;
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ContentFrame.ForwardStack.Clear();
            ContentFrame.BackStack.Clear();
        }
    }
}
