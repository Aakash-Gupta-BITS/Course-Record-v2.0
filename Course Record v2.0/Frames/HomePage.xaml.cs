using ConsoleAppEngine.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


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
    }
}
