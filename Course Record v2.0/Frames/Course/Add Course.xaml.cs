using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class Add_Course : Page
    {
        private readonly ConsoleAppEngine.Course.AllCourses Courses = new ConsoleAppEngine.Course.AllCourses ();
        public Add_Course()
        {
            this.InitializeComponent();
            
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byte i=0;
            byte length = (byte)System.Enum.GetNames(typeof(ConsoleAppEngine.AllEnums.CourseType)).Length;
            ConsoleAppEngine.AllEnums.CourseType type = new ConsoleAppEngine.AllEnums.CourseType();
            while(i<length)
            {
                string m = System.Enum.GetName(typeof(ConsoleAppEngine.AllEnums.CourseType), i);
               
                if(TypeInput.SelectedItem.ToString()==m)
                {
                    type = (ConsoleAppEngine.AllEnums.CourseType)i;
                    ConsoleAppEngine.Course.CourseEntry courseadd = new ConsoleAppEngine.Course.CourseEntry((type, IdInput.Text.ToString()), TitleInput.Text.ToString(), byte.Parse(LectureInput.Text.ToString()), byte.Parse(PracticalInput.Text.ToString()), null);
                    Courses.CoursesList.AddLast(courseadd);
                }
                i++;
                
            }
            
           
        }

    }
}
