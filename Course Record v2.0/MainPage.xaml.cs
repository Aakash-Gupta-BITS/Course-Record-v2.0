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

            ContentFrame.Navigate(typeof(Frames.HomePage));
            NavView.SelectedItem = HomeMenu;
            //      HDDSync.GetAllFromHDD();

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
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (sender.SelectedItem == null)
            {
                return;
            }

            NavigationViewItem SelectedItem = sender.SelectedItem as NavigationViewItem;

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
           // HDDSync.AddAllToHDD();
            LoggingServices.Instance.WriteLine<MainPage>("Initial Main Page loaded.");
            NavView.SelectedItem = HomeMenu;
            this.Frame.BackStack.Clear();
            this.Frame.ForwardStack.Clear();
        }
    }
}