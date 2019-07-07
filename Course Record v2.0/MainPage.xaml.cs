using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.Contacts;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;
using ConsoleAppEngine.Log;
using System.Runtime.Serialization.Formatters.Binary;
using Windows.UI.Xaml;
using Windows.Storage;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml.Media;
using System.Text;
using System.IO;
using System;

namespace Course_Record_v2._0
{
    public sealed partial class MainPage
    {
        private readonly AllCourses Courses = new AllCourses();
        private readonly AllContacts Contacts = new AllContacts();
        public static LoggingServices log = new LoggingServices();
        public ILoggingServices loggingServices = log as ILoggingServices;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            string DirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Courses");
            string FileLocation = Path.Combine(DirectoryLocation, "Math3.txt");

            CourseEntry Math3Course = null;

            #region Deserialize

            BinaryFormatter formatter = new BinaryFormatter();
            if (!File.Exists(FileLocation))
                return;
            using (var s = new FileStream(FileLocation, FileMode.OpenOrCreate))
            {
                Math3Course = formatter.Deserialize(s) as CourseEntry;
            }

            #endregion

            #region Serialize

            Math3Course = new CourseEntry((CourseType.MATH, "F213"), "Mathematics 3", 3, 0, null);
            Math3Course.BookEntry.AddBook(new EBookItem(TextBookType.Reference, "LALA", "JI", 5, "ABCD", true));

            if (!Directory.Exists(DirectoryLocation))
                Directory.CreateDirectory(DirectoryLocation);
            using (Stream m = new FileStream(FileLocation, FileMode.Create))
            {
                new BinaryFormatter().Serialize(m, Math3Course);
            }

            #endregion

            if (Courses.CoursesList.Count == 0)
            {
                Courses.CoursesList.AddLast(Math3Course);
            }


            Contacts.TeacherEntry.AddTeacher(new ETeacherEntry(
                "Dr. Manoj Kannan",
                new string[] { @"+91-1596-515-855", "" },
                new string[] { @"manojkannan@pilani.bits-pilani.ac.in", "" },
                @"#3270, New Science Block
Faculty Division III
BITS Pilani, Pilani Campus
Vidya Vihar, Pilani 333031 (Rajasthan)",
                @"https://www.bits-pilani.ac.in/pilani/manojkannan/Contact",
                "Katayi Bdia Master"));


            Contacts.StudentEntry.AddStudent(new EStudentEntry(
                "Aakash Gupta",
                (2018,
                new ExpandedBranch[] { ExpandedBranch.Mathematics, ExpandedBranch.ComputerScience },
                887),
                new string[] { "7496811413", "" },
                @"uchanahome1@gmail.com",
                "RAM",
                4136,
                "Developer of this App"));

            loggingServices.WriteLine<MainPage>("Entered Main Menu");
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (sender.SelectedItem == null)
            {
                return;
            }
            
            object SelectedItem = (sender.SelectedItem as NavigationViewItem);
            
            if (SelectedItem == CourseMenu)
            {
                LinkedList<object> lis = new LinkedList<object>();
                lis.AddLast(Courses);
                lis.AddLast(Contacts);
                loggingServices.WriteLine<string>("Entered Course Menu" );
                this.Frame.Navigate(typeof(Frames.Course.MainPage), lis);
            }
            else if (SelectedItem == ContactMenu)
            {
                LinkedList<object> lis = new LinkedList<object>();
                lis.AddLast(Contacts);
                lis.AddLast(Courses);
                loggingServices.WriteLine<string>("Entered Contact Menu");
                this.Frame.Navigate(typeof(Frames.Contacts.MainPage), lis);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavView.SelectedItem = null;
            this.Frame.BackStack.Clear();
            this.Frame.ForwardStack.Clear();
        }

    }
}