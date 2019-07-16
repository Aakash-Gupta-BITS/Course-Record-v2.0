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
        private AllCourses Courses => AllCourses.Instance;
        private AllContacts Contacts => AllContacts.Instance;

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
            Courses.NavView = e.Parameter as NavigationView;

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