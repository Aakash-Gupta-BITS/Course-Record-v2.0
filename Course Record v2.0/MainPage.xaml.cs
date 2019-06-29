using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.Contacts;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;

namespace Course_Record_v2._0
{
    public sealed partial class MainPage
    {
        private readonly AllCourses Courses = new AllCourses();
        private readonly AllContacts Contacts = new AllContacts();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            CourseEntry Math3Course = new CourseEntry((BranchType.MATH, "F213"), "Mathematics 3", 3, 0, null);

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
                this.Frame.Navigate(typeof(Frames.Contacts.MainPage), Contacts);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Frame.BackStack.Clear();
            this.Frame.ForwardStack.Clear();
            foreach (var courses in Courses.CoursesList)
            {
                if (!courses.HandoutEntry.IsDestructed)
                    courses.HandoutEntry.DestructViews();
                if (!courses.BookEntry.IsDestructed)
                    courses.BookEntry.DestructViews();
                if (!courses.TeacherEntry.IsDestructed)
                    courses.TeacherEntry.DestructViews();
                if (!courses.TimeEntry.IsDestructed)
                    courses.TimeEntry.DestructViews();
                if (!courses.EventEntry.IsDestructed)
                    courses.EventEntry.DestructViews();
                if (!courses.TestEntry.IsDestructed)
                    courses.TestEntry.DestructViews();
            }
            if (!Contacts.TeacherEntry.IsDestructed)
                Contacts.TeacherEntry.DestructViews();
        }
    }
}