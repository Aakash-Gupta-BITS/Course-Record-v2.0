using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class AllCoursesView : Page
    {
        private AllCourses Courses;
        private AllContacts Contacts;
        private NavigationView NavView;
        public AllCoursesView()
        {
            this.InitializeComponent();

            // dynamically fill CourseType
            foreach (var x in Enum.GetValues(typeof(CourseType)))
            {
                TyeInput.Items.Add(x.ToString());
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // No Course Type selected
            if (TyeInput.SelectedItem == null ||
                IdInput.Text == "" ||
                TitleInput.Text == null ||
                !byte.TryParse(LectureInput.Text, out byte lecture) ||
                !byte.TryParse(PracticalInput.Text, out byte practical) ||
                ICSelect.SelectedItem == null)
            {
                // make addbutton color red
                (sender as Button).BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                return;
            }


            ETeacherEntry eTeacher = null;
            foreach (var y in Contacts.TeacherEntry.lists)
            {
                if (y.Name == ICSelect.SelectedItem.ToString())
                {
                    eTeacher = y;
                    break;
                }
            }

            CourseType ctype = (CourseType)Enum.Parse(typeof(CourseType), TyeInput.SelectedItem as string);
            CourseEntry entry = new CourseEntry(
                (ctype, IdInput.Text),
                TitleInput.Text,
                lecture,
                practical,
                eTeacher);

            try
            {
                Courses.AddCourse(entry);
            }
            catch
            {
                return;
            }

            LoggingServices.Instance.WriteLine<AllCoursesView>(string.Format("{0} course is added", entry.Title));
            (sender as Button).BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));

            var list = NavView.MenuItems.ToArray();
            NavView.MenuItems.Clear();
            for (int i = 0; i < list.Length - 4; ++i)
            {
                NavView.MenuItems.Add(list[i]);
            }

            NavView.MenuItems.Add(entry.navigationViewItem);
            for (int i = list.Length - 4; i < list.Length; ++i)
            {
                NavView.MenuItems.Add(list[i]);
            } (sender as Button).BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));

            NavView.IsPaneOpen = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var x = e.Parameter as LinkedList<object>;

            Courses = x.First.Value as AllCourses;
            Contacts = x.First.Next.Value as AllContacts;
            NavView = x.First.Next.Next.Value as NavigationView;

            foreach (var y in Contacts.TeacherEntry.lists)
            {
                ICSelect.Items.Add(y.Name);
            }
        }
    }
}