using ConsoleAppEngine.Course;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ConsoleAppEngine.Contacts;

namespace Course_Record_v2._0.Frames.Contacts
{
    public sealed partial class MainPage : Page
    {
        AllContacts allContacts;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = sender.SelectedItem as NavigationViewItem;

            NavView.Header = SelectedItem.Content;

            if (SelectedItem == TeachersNavigation)
            {
                ContentFrame.Navigate(typeof(TeacherContacts), allContacts.TeacherEntry);
            }
            else if (SelectedItem == GoBack)
            {
                if (!allContacts.TeacherEntry.IsDestructed)
                    allContacts.TeacherEntry.DestructViews();
                this.Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            allContacts = e.Parameter as AllContacts;
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ContentFrame.ForwardStack.Clear();
            ContentFrame.BackStack.Clear();
        }
    }
}
