using ConsoleAppEngine.Course;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Course_Record_v2._0.Frames.Contacts
{
    public sealed partial class StudentContacts : Page
    {
        private EStudents StudentEntry;

        public StudentContacts()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StudentEntry = (e.Parameter as EStudents);
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            StudentEntry.DestructViews();
        }
    }
}
