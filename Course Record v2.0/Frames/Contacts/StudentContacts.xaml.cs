using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Course_Record_v2._0.Frames.Contacts
{
    public sealed partial class StudentContacts : Page
    {
        private EStudents StudentEntry => AllContacts.Instance.StudentEntry;

        public StudentContacts()
        {
            this.InitializeComponent();
            this.Unloaded += (object sender, Windows.UI.Xaml.RoutedEventArgs e) =>
                {
                    StudentEntry.DestructViews();
                };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StudentEntry.InitializeViews(
                ViewGrid,
                AddGrid,
                ViewCommand,
                AddCommand,
                NameInput,
                Phone1Input,
                Phone2Input,
                IdInput,
                EmailInput,
                HostelInput,
                RoomInput,
                OtherInput,
                AddButton);
        }
    }
}