using ConsoleAppEngine.Course;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeTable : Page
    {
        private ECourseTimeTable TimeEntry;

        public TimeTable()
        {
            this.InitializeComponent();

            this.Unloaded += (object sender, Windows.UI.Xaml.RoutedEventArgs e) =>
            {
                TimeEntry.DestructViews();
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TimeEntry = (e.Parameter as CourseEntry).TimeEntry;
            TimeEntry.InitializeViews(
                ViewGrid,
                AddGrid,
                ViewCommand,
                AddCommand,
                EntryInput,
                SectionInput,
                Teacher1Input,
                Teacher2Input,
                Teacher3Input,
                RoomInput,
                DayInput,
                HourInput,
                AddButton);

            TimeEntry.SetTeachersEntry((e.Parameter as CourseEntry).TeacherEntry);
        }
    }
}
