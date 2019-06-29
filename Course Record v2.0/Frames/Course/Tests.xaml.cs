using ConsoleAppEngine.Course;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Tests : Page
    {
        private ETests TestEntry;
        public Tests()
        {
            this.InitializeComponent();
            this.Unloaded += (object sender, Windows.UI.Xaml.RoutedEventArgs e) =>
            {
                TestEntry.DestructViews();
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TestEntry = e.Parameter as ETests;

            TestEntry.InitializeViews(
                ViewGrid,
                AddGrid,
                ViewCommand,
                AddCommand,
                DateInput,
                TypeInput,
                IndexInput,
                MarksInput,
                MaxMarkInput,
                DescriptionInput,
                ButtonInput);
        }
    }
}
