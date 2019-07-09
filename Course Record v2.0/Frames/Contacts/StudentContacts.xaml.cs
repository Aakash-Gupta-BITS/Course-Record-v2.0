using ConsoleAppEngine.Course;
using System.Collections.Generic;
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
            this.Unloaded += (object sender, Windows.UI.Xaml.RoutedEventArgs e) =>
                {
                    StudentEntry.DestructViews();
                };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var temp = e.Parameter as LinkedList<object>;
            StudentEntry = temp.First.Value as EStudents;
            StudentEntry.SetAllCourses(temp.First.Next.Value as AllCourses);
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