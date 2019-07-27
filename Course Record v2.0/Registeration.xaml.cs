using ConsoleAppEngine.Globals;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0
{
    public sealed partial class Registration : Page
    {
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public Registration()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Id.Text = Hash.Instance.DisplayHash;
            if (!(bool)localSettings.Values["IsRegistered"])
            {
                button.Content = "Register";

                button.Click += (sender, a) =>
                {
                    if (Hash.Instance.VerifyHashFromConsole(Key.Text))
                    {
                        ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue
                        {
                            ["Id"] = Hash.Instance.DisplayHash,
                            ["Key"] = Key.Text,
                            ["Unregister"] = Hash.ComputeSha256Hash(Key.Text)
                        };

                        localSettings.Values["RegistrationComposites"] = composite;
                        localSettings.Values["IsRegistered"] = true;
                        (Window.Current.Content as Frame).Navigate(typeof(ExtendedSplash));
                    }
                    else
                    {
                        Key.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                    }
                };
            }
            else
            {
                Key.IsReadOnly = true;
                Key.Text = Hash.ComputeSha256Hash(Id.Text);
                UnregisterId.Visibility = Visibility.Visible;
                button.Content = "UnRegister";
                button.Click += (sender, a) =>
                {
                    if (Hash.ComputeSha256Hash(Key.Text) == UnregisterId.Text)
                    {
                        localSettings.Values["RegistrationComposites"] = null;
                        localSettings.Values["IsRegistered"] = false;
                        (Window.Current.Content as Frame).Navigate(typeof(Registration));
                    }
                    else
                    {
                        UnregisterId.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                    }
                };
            }
        }
    }
}
