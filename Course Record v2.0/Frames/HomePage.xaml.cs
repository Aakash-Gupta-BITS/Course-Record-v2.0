using ConsoleAppEngine.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Course_Record_v2._0.Frames
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            // Directory.Text = ApplicationData.Current.LocalFolder.Path;

            LinkedList<string> list = new LinkedList<string>();
            list.AddLast("Academic");
            list.AddLast("SWD");
            list.AddLast("Nalanda");
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
                    URL1 = @"http://swd/";
                    URL2 = @"http://swd.bits-pilani.ac.in";
                    break;
                case "ERP":
                    URL1 = @"http://erp/";
                    URL2 = @"http://erp.bits-pilani.ac.in";
                    break;
                case "Nalanda":
                    URL1 = @"http://nalanda/";
                    URL2 = @"http://nalanda.bits-pilani.ac.in";
                    break;
                case "Academic":
                    URL1 = @"http://academic.bits-pilani.ac.in";
                    URL2 = @"http://academic.bits-pilani.ac.in";
                    break;
                case "Library":
                    URL1 = @"http://library/";
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
