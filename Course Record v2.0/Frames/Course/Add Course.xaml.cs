using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Add_Course : Page
    {
        private readonly ConsoleAppEngine.Course.AllCourses Courses = new ConsoleAppEngine.Course.AllCourses ();
        public Add_Course()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byte i;
            ConsoleAppEngine.AllEnums.CourseType type = new ConsoleAppEngine.AllEnums.CourseType();
            for(type=0,i=0;i<27;i++)
            {               
                if(nameof(type) == TypeInput.ToString())
                {
                    ConsoleAppEngine.Course.CourseEntry courseadd = new ConsoleAppEngine.Course.CourseEntry((type, IdInput.ToString()), TitleInput.ToString(), byte.Parse(LectureInput.ToString()), byte.Parse(PracticalInput.ToString()), null);
                    Courses.CoursesList.AddLast(courseadd);
                }
                type++;
            }
            
           
        }

    }
}
