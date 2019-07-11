using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0.Frames.Course
{
    public sealed partial class AllCoursesView : Page
    {
        private AllCourses Courses;
        private AllContacts Contacts;
        private NavigationView NavView;

        public AllCoursesView()
        {
            this.InitializeComponent();

            foreach (var x in Enum.GetValues(typeof(CourseType)))
            {
                TypeInput.Items.Add(x.ToString());
            }

            this.Unloaded += (sender, e) =>
            {
                Courses.DestructViews();
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var x = e.Parameter as LinkedList<object>;

            Courses = x.First.Value as AllCourses;
            Contacts = x.First.Next.Value as AllContacts;
            NavView = x.First.Next.Next.Value as NavigationView;

            Courses.Contacts = Contacts;
            Courses.NavView = NavView;

            foreach (var y in Contacts.TeacherEntry.lists)
            {
                ICSelect.Items.Add(y.Name);
            }

            Courses.InitializeViews(
                ViewGrid,
                AddGrid,
                ViewCommand,
                AddCommand,
                TypeInput,
                IdInput,
                TitleInput,
                LectureInput,
                PracticalInput,
                ICSelect,
                AddButton);
        }
    }
}