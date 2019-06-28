using ConsoleAppEngine.Course;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Events : Page
    {
        private EEvents EventEntry;

        public Events()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            EventEntry = e.Parameter as EEvents;
            EventEntry.InitializeViews(
                ViewGrid,
                AddGrid,
                ViewCommand,
                AddCommand,
                TitleInput,
                DateInput,
                TimeInput,
                LocationInput,
                DescriptionInput,
                AddButton);

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            EventEntry.DestructViews();
        }
    }
}
