using ConsoleAppEngine.Globals;
using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Log;
using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using System.Linq;

namespace Course_Record_v2._0
{
    public sealed partial class MainPage
    {
        public delegate void GetDelegate();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            ContactMenu.Icon = new FontIcon() { FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Glyph = string.Format("{0}", (char)0xE779) };
            TimeMenu.Icon = new FontIcon() { FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Glyph = string.Format("{0}", (char)0xE787) };
            CourseMenu.Icon = new FontIcon() { FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Glyph = string.Format("{0}", (char)0xE7BE) };
            Directory.Text = ApplicationData.Current.LocalFolder.Path;

            HDDSync.GetAllFromHDD();

            #region ContactsAdd
          /*    AllContacts.Instance.TeacherEntry.AddTeacher(new ETeacherEntry(
                  "Dr. Manoj Kannan",
                  new string[] { @"+91-1596-515-855", "" },
                  new string[] { @"manojkannan@pilani.bits-pilani.ac.in", "" },
                  @"#3270, New Science Block
  Faculty Division III
  BITS Pilani, Pilani Campus
  Vidya Vihar, Pilani 333031 (Rajasthan)",
                  @"https://www.bits-pilani.ac.in/pilani/manojkannan/Contact",
                  "Katayi Bdia Master"));


            AllContacts.Instance.StudentEntry.AddStudent(new EStudentEntry(
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

            #region WebsitesAdd
            LinkedList<string> list = new LinkedList<string>();
            list.AddLast("AUGS/AUGR");
            list.AddLast("SWD");
            list.AddLast("Nalanda");
            list.AddLast("ID");
            list.AddLast("ERP");
            list.AddLast("Library");
            foreach (var s in list.OrderBy(a => a))
                WebsiteBox.Items.Add(s);
            WebsiteBox.SelectedIndex = 0;
            #endregion
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (sender.SelectedItem == null)
            {
                return;
            }

            NavigationViewItem SelectedItem = (sender.SelectedItem as NavigationViewItem);

            LoggingServices.Instance.WriteLine<MainPage>("\"" + SelectedItem.Content as string + "\" is selected at initial Main Page.");

            if (SelectedItem == CourseMenu)
            {
                this.Frame.Navigate(typeof(Frames.Course.MainPage));
            }
            else if (SelectedItem == ContactMenu)
            {
                this.Frame.Navigate(typeof(Frames.Contacts.MainPage));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HDDSync.AddAllToHDD();
            LoggingServices.Instance.WriteLine<MainPage>("Initial Main Page loaded.");
            NavView.SelectedItem = null;
            this.Frame.BackStack.Clear();
            this.Frame.ForwardStack.Clear();
        }

        private async void HyperlinkButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var uriBing = new Uri(@"http://google.co.in/");
            var options = new Windows.System.LauncherOptions();
            switch (WebsiteBox.SelectedItem.ToString())
            {
                case "SWD":
                    uriBing = new Uri(@"http://swd.bits-pilani.ac.in");
                    break;
                case "ERP":
                    uriBing = new Uri(@"http://erp.bits-pilani.ac.in");
                    break;
                case "Nalanda":
                    uriBing = new Uri(@"http://nalanda.bits-pilani.ac.in");
                    break;
                case "ID":
                    uriBing = new Uri(@"http://id");
                    break;
                case "AUGS/AUGR":
                    uriBing = new Uri(@"http://rc.bits-pilani.ac.in/");
                    break;
                case "Library":
                    uriBing = new Uri(@"http://www.bits-pilani.ac.in:12354/");
                    break;

            }
            // Launch the URI with a warning prompt
            options.TreatAsUntrusted = false;
            var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);

            if (success)
            {
                LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + WebsiteBox.SelectedItem.ToString() + " opened successfully");
                NavView.SelectedItem = null;
            }
            else
            {
                LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + WebsiteBox.SelectedItem.ToString() + " was not opened");
            }                                               
            
        }
    }
}