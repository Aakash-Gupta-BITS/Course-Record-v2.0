using ConsoleAppEngine.Globals;
using ConsoleAppEngine.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
            {
                WebsiteBox.Items.Add(s);
            }

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

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            string URL1, URL2;
            URL1 = URL2 = @"https://www.google.com";

            switch (WebsiteBox.SelectedItem as string)
            {
                case "SWD":
                    URL1 = @"swd/";
                    URL2 = @"http://swd.bits-pilani.ac.in";
                    break;
                case "ERP":
                    URL1 = @"erp/";
                    URL2 = @"http://erp.bits-pilani.ac.in";
                    break;
                case "Nalanda":
                    URL1 = @"nalanda/";
                    URL2 = @"http://nalanda.bits-pilani.ac.in";
                    break;
                case "ID":
                    URL1 = @"http://id";
                    break;
                case "AUGS/AUGR":
                    URL2 = @"http://rc.bits-pilani.ac.in/";
                    break;
                case "Library":
                    URL1 = @"library/";
                    URL2 = @"http://www.bits-pilani.ac.in:12354/";
                    break;

            }

            if (await Windows.System.Launcher.LaunchUriAsync(
                    new Uri(
                        sender.Equals(Link1) ? URL1 : URL2),
                        new Windows.System.LauncherOptions { TreatAsUntrusted = true }))
            {
                LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + WebsiteBox.SelectedItem.ToString() + " opened successfully");
            }
            else
            {
                LoggingServices.Instance.WriteLine<MainPage>("The WebPage " + WebsiteBox.SelectedItem.ToString() + " was not opened");
            }
        }
    }
}