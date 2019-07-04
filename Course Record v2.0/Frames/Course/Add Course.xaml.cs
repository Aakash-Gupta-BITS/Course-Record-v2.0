using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.AllEnums;
using System.Collections.Generic;
using System.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class Add_Course : Page
    {
        private AllCourses Courses;
        private AllContacts Contacts;
        public Add_Course()
        {
            this.InitializeComponent();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byte i = 0;
            int flag = 0;
            byte length = (byte)System.Enum.GetNames(typeof(CourseType)).Length;
            CourseType type;
            while (i < length)
            {
                string m = System.Enum.GetName(typeof(CourseType), i);

                if (TypeInput.SelectedItem.ToString() == m)
                {
                    type = (CourseType)i;

                    ETeacherEntry eTeacher = null;
                    foreach (var y in Contacts.TeacherEntry.lists) 
                        if (y.Name == ICSelect.SelectedItem.ToString())
                        {
                            eTeacher = y;
                            break;
                        }

                    CourseEntry courseadd = new CourseEntry((type, IdInput.Text.ToString()), TitleInput.Text.ToString(), byte.Parse(LectureInput.Text.ToString()), byte.Parse(PracticalInput.Text.ToString()), eTeacher);
                    Courses.CoursesList.AddLast(courseadd);
                    flag = 1;
                }
                i++;
                _0.MainPage.log.WriteLine<string>("Value of flag" + flag.ToString());
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var x = e.Parameter as LinkedList<object>;

            Courses = x.First.Value as AllCourses;
            Contacts = x.First.Next.Value as AllContacts;

            foreach (var y in Contacts.TeacherEntry.lists)
                ICSelect.Items.Add(y.Name);
        }

    }

}