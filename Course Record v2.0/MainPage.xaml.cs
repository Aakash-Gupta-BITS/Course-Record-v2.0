using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Course;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0
{
    public sealed partial class MainPage
    {
        AllCourses Courses = new AllCourses();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            CourseEntry Math3Course = new CourseEntry((BranchType.MATH, "F213"), "Mathematics 3", 3, 0, null);
            Math3Course.TeacherEntry.AddTeacher(new ETeacherEntry(
                "Dr. Manoj Kannan",
                new string[] { @"+91-1596-515-855", "" },
                new string[] { @"manojkannan@pilani.bits-pilani.ac.in", "" },
                @"#3270, New Science Block
Faculty Division III
BITS Pilani, Pilani Campus
Vidya Vihar, Pilani 333031 (Rajasthan)",
                @"https://www.bits-pilani.ac.in/pilani/manojkannan/Contact",
                "Katayi Bdia Master"));

            Courses.CoursesList.AddLast(Math3Course);
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (sender.SelectedItem == null) return;
            object SelectedItem = (sender.SelectedItem as NavigationViewItem);

            if (SelectedItem == CourseMenu)
                this.Frame.Navigate(typeof(Frames.Course.MainPage), Courses);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.Frame.BackStack.Clear();
            this.Frame.ForwardStack.Clear();
        }
    }
}