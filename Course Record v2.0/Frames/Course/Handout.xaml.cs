using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ConsoleAppEngine.Course;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Handout : Page
    {
        EHandouts HandoutEntry;

        public Handout()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HandoutEntry = e.Parameter as EHandouts;
            HandoutEntry.InitializeViews(
                ViewGrid,
                AddGrid,
                ViewCommand,
                AddCommand,
                LectureInput,
                TopicInput,
                DescriptionInput,
                DoneInput,
                AddButton);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HandoutEntry.DestructViews();
        }
    }
}