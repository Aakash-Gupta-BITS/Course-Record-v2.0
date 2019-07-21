using ConsoleAppEngine.Log;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;




namespace Course_Record_v2._0.Frames
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            Directory.Text = ApplicationData.Current.LocalFolder.Path;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] scopes = new string[] { Oauth2Service.Scope.UserinfoEmail };
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = @"99450014535-93f37a216juu2jtenue0ikintb74pal1.apps.googleusercontent.com",
                    ClientSecret = @"DG8dQkcGyb-71YJW-u_ABGRB"
                },
                scopes,
                "user",
                CancellationToken.None).Result;
            LoggingServices.Instance.WriteLine<MainPage>("Done");
            if (credential.Token.IsExpired(credential.Flow.Clock))
            {
                LoggingServices.Instance.WriteLine<MainPage>("Access Token is expired. Refreshing it.");
                if (credential.RefreshTokenAsync(CancellationToken.None).Result)
                    LoggingServices.Instance.WriteLine<MainPage>("Access Token is now refreshed.");
                else
                    LoggingServices.Instance.WriteLine<MainPage>("Access Token is expired but we couldn't refresh it.");
            }
            else
                LoggingServices.Instance.WriteLine<MainPage>("Access token is Ok. Continuing");

            Oauth2Service oauth2Service = new Oauth2Service(
                new BaseClientService.Initializer { HttpClientInitializer = credential });
            Userinfoplus userinfo = oauth2Service.Userinfo.Get().ExecuteAsync().Result;
            (sender as Button).Content = ("Email : " + userinfo.Email);
            credential.RevokeTokenAsync(CancellationToken.None).Wait();
        }
    }
}
