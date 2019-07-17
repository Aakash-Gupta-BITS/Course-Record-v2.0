using ConsoleAppEngine.Log;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0.Frames.Contacts
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            GoBack.Icon = new FontIcon() { FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Glyph = string.Format("{0}", (char)0xE72B) };
            StudentsNavigation.Icon = new FontIcon() { FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Glyph = string.Format("{0}", (char)0xE902) };
            TeachersNavigation.Icon = new FontIcon() { FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Glyph = string.Format("{0}", (char)0xE902) };
            OthersNavigation.Icon = new FontIcon() { FontFamily = new Windows.UI.Xaml.Media.FontFamily("Segoe MDL2 Assets"), Glyph = string.Format("{0}", (char)0xE7EE) };
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = sender.SelectedItem as NavigationViewItem;

            LoggingServices.Instance.WriteLine<MainPage>("\"" + SelectedItem.Content as string + "\" is selected at Teacher Main Page.");
            if (SelectedItem == null)
            {
                return;
            }

            NavView.Header = SelectedItem.Content;

            if (SelectedItem == TeachersNavigation)
            {
                ContentFrame.Navigate(typeof(TeacherContacts));
            }
            else if (SelectedItem == StudentsNavigation)
            {
                ContentFrame.Navigate(typeof(StudentContacts));
            }
            else if (SelectedItem == GoBack)
            {
                Frame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoggingServices.Instance.WriteLine<MainPage>("Constacts Main Page loading...");
            NavView.SelectedItem = StudentsNavigation;
            LoggingServices.Instance.WriteLine<MainPage>("Contacts Main Page loaded.");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            LoggingServices.Instance.WriteLine<MainPage>("Contacts Main Page unloaded.");
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ContentFrame.ForwardStack.Clear();
            ContentFrame.BackStack.Clear();
        }
    }
}