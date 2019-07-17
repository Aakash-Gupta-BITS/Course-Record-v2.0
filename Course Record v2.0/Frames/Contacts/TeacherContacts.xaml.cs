using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0.Frames.Contacts
{
    public sealed partial class TeacherContacts : Page
    {
        private ETeachers TeacherEntry => AllContacts.Instance.TeacherEntry;

        public TeacherContacts()
        {
            this.InitializeComponent();
            this.Unloaded += (object sender, Windows.UI.Xaml.RoutedEventArgs e) =>
            {
                TeacherEntry.DestructViews();
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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

    }
}
