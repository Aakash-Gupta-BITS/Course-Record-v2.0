using ConsoleAppEngine.Course;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0.Frames.Course
{
    public sealed partial class TimeTable : Page
    {
        private ETimeTable TimeEntry;

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
            Teacher1Input.Items.Add("");
            Teacher2Input.Items.Add("");
            Teacher3Input.Items.Add("");

            foreach (var y in (e.Parameter as CourseEntry).TeacherEntry.lists)
            {
                Teacher1Input.Items.Add(y.Name);
                Teacher2Input.Items.Add(y.Name);
                Teacher3Input.Items.Add(y.Name);
            }
        }
    }
}
