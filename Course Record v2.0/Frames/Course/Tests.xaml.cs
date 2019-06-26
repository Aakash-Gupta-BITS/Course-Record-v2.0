using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ConsoleAppEngine.Course;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Tests : Page
    {
        ETests TestEntry;
        public Tests()
        {
            this.InitializeComponent();
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            TestEntry.DestructViews();
        }
    }
}
