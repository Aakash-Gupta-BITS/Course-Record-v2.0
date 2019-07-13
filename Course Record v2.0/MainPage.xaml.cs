using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.Log;
using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;

namespace Course_Record_v2._0
{
    public sealed partial class MainPage
    {
        private readonly AllCourses Courses = new AllCourses();
        private readonly AllContacts Contacts = new AllContacts();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            Directory.Text = ApplicationData.Current.LocalFolder.Path;
            Courses.GetFromHdd();
            Contacts.GetFromHdd();

            #region ContactsAdd
          /*  Contacts.TeacherEntry.AddTeacher(new ETeacherEntry(
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
                "Developer of this App"));*/
            #endregion 
        }

        private async void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (sender.SelectedItem == null)
            {
                return;
            }

            NavigationViewItem SelectedItem = (sender.SelectedItem as NavigationViewItem);

            LoggingServices.Instance.WriteLine<MainPage>("\"" + SelectedItem.Content as string + "\" is selected at initial Main Page.");

            if (SelectedItem == CourseMenu)
            {
                LinkedList<object> lis = new LinkedList<object>();
                lis.AddLast(Courses);
                lis.AddLast(Contacts);
                this.Frame.Navigate(typeof(Frames.Course.MainPage), lis);
            }
            else if (SelectedItem == ContactMenu)
            {
                LinkedList<object> lis = new LinkedList<object>();
                lis.AddLast(Contacts);
                lis.AddLast(Courses);
                this.Frame.Navigate(typeof(Frames.Contacts.MainPage), lis);
            }
            else if (SelectedItem == SWD)
            {
                var uriBing = new Uri(@"http://swd.bits-pilani.ac.in");
                var options = new Windows.System.LauncherOptions();
                // Launch the URI with a warning prompt
                options.TreatAsUntrusted = false;
                var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);

                if (success)
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage opened successfully");
                    NavView.SelectedItem = null;
                }
                else
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage was not opened");
                }

            }
            else if (SelectedItem == ERP)
            {
                var uriBing = new Uri(@"http://erp.bits-pilani.ac.in");
                var options = new Windows.System.LauncherOptions();
                // Launch the URI with a warning prompt
                options.TreatAsUntrusted = false;
                var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);

                if (success)
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + SelectedItem.Content.ToString() + " opened successfully");
                    NavView.SelectedItem = null;
                }
                else
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + SelectedItem.Content.ToString() + " was not opened");
                }

            }
            else if (SelectedItem == Nalanda)
            {
                var uriBing = new Uri(@"http://nalanda.bits-pilani.ac.in");
                var options = new Windows.System.LauncherOptions();
                // Launch the URI with a warning prompt
                options.TreatAsUntrusted = false;
                var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);

                if (success)
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + SelectedItem.Content.ToString() + " opened successfully");
                    NavView.SelectedItem = null;
                }
                else
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + SelectedItem.Content.ToString() + " was not opened");
                }

            }
            else if (SelectedItem == ID)
            {
                var uriBing = new Uri(@"http://id");
                var options = new Windows.System.LauncherOptions();
                // Launch the URI with a warning prompt
                options.TreatAsUntrusted = false;
                var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);

                if (success)
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + SelectedItem.Content.ToString() + " opened successfully");
                    NavView.SelectedItem = null;
                }
                else
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + SelectedItem.Content.ToString() + " was not opened");
                }

            }
            else if (SelectedItem == RC)
            {
                var uriBing = new Uri(@"http://rc.bits-pilani.ac.in/");
                var options = new Windows.System.LauncherOptions();
                // Launch the URI with a warning prompt
                options.TreatAsUntrusted = false;
                var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);

                if (success)
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + SelectedItem.Content.ToString() + " opened successfully");
                    NavView.SelectedItem = null;
                }
                else
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + SelectedItem.Content.ToString() + " was not opened");
                }

            }
            else if (SelectedItem == LIB)
            {
                var uriBing = new Uri(@"http://www.bits-pilani.ac.in:12354/");
                var options = new Windows.System.LauncherOptions();
                // Launch the URI with a warning prompt
                options.TreatAsUntrusted = false;
                var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);

                if (success)
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + SelectedItem.Content.ToString() + " opened successfully");
                    NavView.SelectedItem = null;
                }
                else
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + SelectedItem.Content.ToString() + " was not opened");
                }

            }


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoggingServices.Instance.WriteLine<MainPage>("Initial Main Page loaded.");
            NavView.SelectedItem = null;
            this.Frame.BackStack.Clear();
            this.Frame.ForwardStack.Clear();
            Courses.AddToHdd_NewThread();
            Contacts.AddToHdd_NewThread();
        }
        
    }
}