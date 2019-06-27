using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ConsoleAppEngine.Course;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Teachers : Page
    {
        ETeachers TeacherEntry;

        public Teachers()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TeacherEntry = (e.Parameter as CourseEntry).TeacherEntry;
            TeacherEntry.SetTimeEntry((e.Parameter as CourseEntry).TimeEntry);
            TeacherEntry.InitializeViews(
                ViewGrid,
                AddGrid,
                ViewCommand,
                AddCommand,
                NameInput,
                ChamberInput,
                Phone1Input,
                Phone2Input,
                Email1Input,
                Email2Input,
                WebsiteInput,
                OtherInput,
                AddButton);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            TeacherEntry.DestructViews();
        }
    }
}
