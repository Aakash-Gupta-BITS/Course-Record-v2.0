using ConsoleAppEngine.Course;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Contacts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TeacherContacts : Page
    {
        private ETeachers TeacherEntry;

        public TeacherContacts()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TeacherEntry = (e.Parameter as ETeachers);
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
