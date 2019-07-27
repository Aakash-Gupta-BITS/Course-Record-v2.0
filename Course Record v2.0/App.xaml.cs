using ConsoleAppEngine.Globals;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0
{
    public sealed partial class App : Application
    {
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState != ApplicationExecutionState.Running)
                {
                    Window.Current.Content = rootFrame;
                }
            }

            ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)localSettings.Values["RegistrationComposites"];
            localSettings.Values["IsRegistered"] = false;
            if (composite != null)
            {
                string idhash = composite["Id"] as string;
                string KeyHash = composite["Key"] as string;
                string UnRegister = composite["Unregister"] as string;

                if (KeyHash == Hash.ComputeSha256Hash(idhash) && UnRegister == Hash.ComputeSha256Hash(KeyHash))
                {
                    localSettings.Values["IsRegistered"] = true;
                }
            }

            if ((bool)localSettings.Values["IsRegistered"])
            {
                rootFrame.Navigate(typeof(ExtendedSplash));
            }
            else
            {
                rootFrame.Navigate(typeof(Registration));
            }

            Window.Current.Activate();
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            deferral.Complete();
        }
    }
}
