using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.Contacts;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;
using ConsoleAppEngine.Log;

namespace Course_Record_v2._0
{
    public sealed partial class MainPage
    {
        private readonly AllCourses Courses = new AllCourses();
        private readonly AllContacts Contacts = new AllContacts();
        static LoggingServices log = new LoggingServices();
        ILoggingServices loggingServices = log as ILoggingServices;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            loggingServices.WriteLine<string>("The App executed successfully.");
            CourseEntry Math3Course = new CourseEntry((CourseType.MATH, "F213"), "Mathematics 3", 3, 0, null);

            Contacts.TeacherEntry.AddTeacher(new ETeacherEntry(
                "Dr. Manoj Kannan",
                new string[] { @"+91-1596-515-855", "" },
                new string[] { @"manojkannan@pilani.bits-pilani.ac.in", "" },
                @"#3270, New Science Block
Faculty Division III
BITS Pilani, Pilani Campus
Vidya Vihar, Pilani 333031 (Rajasthan)",
                @"https://www.bits-pilani.ac.in/pilani/manojkannan/Contact",
                "Katayi Bdia Master"));
            if (Courses.CoursesList.Count == 0)
                Courses.CoursesList.AddLast(Math3Course);

            Contacts.StudentEntry.AddStudent(new EStudentEntry(
                "Aakash Gupta",
                (2018,
                new ExpandedBranch[] { ExpandedBranch.Mathematics, ExpandedBranch.ComputerScience },
                887),
                new string[] { "7496811413", "" },
                @"uchanahome1@gmail.com",
                "RAM",
                4136,
                "Developer of this App"));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (sender.SelectedItem == null)
            {
                return;
            }

            object SelectedItem = (sender.SelectedItem as NavigationViewItem);

            if (SelectedItem == CourseMenu)
            {
                LinkedList<object> lis = new LinkedList<object>();
                lis.AddLast(Courses);
                lis.AddLast(Contacts);
                this.Frame.Navigate(typeof(Frames.Course.MainPage), lis);
            }
            else if (SelectedItem == ContactMenu)
            {
                LinkedList<object> lis = new LinkedList<object>();
                lis.AddLast(Contacts);
                lis.AddLast(Courses);
                this.Frame.Navigate(typeof(Frames.Contacts.MainPage), lis);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavView.SelectedItem = null;
            this.Frame.BackStack.Clear();
            this.Frame.ForwardStack.Clear();
        }
    }
}