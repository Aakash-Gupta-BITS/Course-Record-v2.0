using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.Log;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using System;

namespace Course_Record_v2._0
{
    public sealed partial class MainPage
    {
        public delegate void GetDelegate();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            //      HDDSync.GetAllFromHDD();

            #region ContactsAdd
            AllContacts.Instance.TeacherEntry.lists.AddLast(new ETeacherEntry(
                "Dr. Manoj Kannan",
                new string[] { @"+91-1596-515-855", "" },
                new string[] { @"manojkannan@pilani.bits-pilani.ac.in", "" },
                @"#3270, New Science Block
    Faculty Division III
    BITS Pilani, Pilani Campus
    Vidya Vihar, Pilani 333031 (Rajasthan)",
                @"https://www.bits-pilani.ac.in/pilani/manojkannan/Contact",
                "Katayi Bdia Master"));


            AllContacts.Instance.StudentEntry.lists.AddLast(new EStudentEntry(
                  "Aakash Gupta",
                  (2018,
                  new ExpandedBranch[] { ExpandedBranch.Mathematics, ExpandedBranch.ComputerScience },
                  887),
                  new string[] { "7496811413", "" },
                  @"uchanahome1@gmail.com",
                  "RAM",
                  4136,
                  "Developer of this App"));
            #endregion
        }

        private async void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (sender.SelectedItem == null)
            {
                return;
            }

            NavigationViewItem SelectedItem = sender.SelectedItem as NavigationViewItem;

            LoggingServices.Instance.WriteLine<MainPage>("\"" + SelectedItem.Content as string + "\" is selected at initial Main Page.");

            if (SelectedItem == HomeMenu)
            {
                ContentFrame.Navigate(typeof(Frames.HomePage));
            }
            else if (SelectedItem == TimeMenu)
            {
                ContentFrame.Navigate(typeof(Frames.OverallTimeTableView));
            }
            else if (SelectedItem == CourseMenu)
            {
                this.Frame.Navigate(typeof(Frames.Course.MainPage));
            }
            else if (SelectedItem == ContactMenu)
            {
                this.Frame.Navigate(typeof(Frames.Contacts.MainPage));
            }
            else if(SelectedItem == FeedBack)
            {
                if (await Windows.System.Launcher.LaunchUriAsync(
                    new Uri(
                       "https://forms.gle/eQQsubt368QXdSev8")))
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The FeedBack form opened successfully");
                }
                else
                {
                    LoggingServices.Instance.WriteLine<MainPage>("The FeedBack form was not opened");
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // HDDSync.AddAllToHDD();
            LoggingServices.Instance.WriteLine<MainPage>("Initial Main Page loaded.");
            NavView.SelectedItem = HomeMenu;
            this.Frame.BackStack.Clear();
            this.Frame.ForwardStack.Clear();
            ContentFrame.ForwardStack.Clear();
            ContentFrame.BackStack.Clear();
        }

        private void ShowAbout(object sender, TappedRoutedEventArgs e) => FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }
}