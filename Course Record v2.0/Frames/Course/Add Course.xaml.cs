using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.AllEnums;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Text;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using ConsoleAppEngine.Log;

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

            // dynamically fill CourseType
            foreach (var x in Enum.GetValues(typeof(CourseType)))
                TyeInput.Items.Add(x.ToString());
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
                if (y.Name == ICSelect.SelectedItem.ToString())
                {
                    eTeacher = y;
                    break;
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
            LoggingServices.Instance.WriteLine<Add_Course>(string.Format("{0} course is added", entry.Title));

            //  Restore addbutton color -- only if all inputs are valid
                (sender as Button).BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));

            LinkedList<object> temp = new LinkedList<object>();
            temp.AddLast(Courses);
            temp.AddLast(Contacts);

            this.Frame.Navigate(typeof(MainPage), temp);
            // All inputs are valid here


            /*
            OLD CODE
            byte i = 0;
            
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
                    
                }
                i++; 
                }*/

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