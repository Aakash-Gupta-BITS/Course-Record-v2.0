using ConsoleAppEngine.Globals;
using ConsoleAppEngine.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0.Frames
{
    public sealed partial class FeedBack : Page
    {
        public static LinkedList<string> FeedBackLinks = null;

        private string FeedbackAddedLink => @"https://docs.google.com/forms/d/e/1FAIpQLSexBkTA-zBSAeQPd3M24wXCIJPXc31YAJ61U2uusFSegF-VzA/formResponse";

        private readonly Uri HomePageUri = new Uri(@"ms-appx-web:///Assets/index.html");
        private readonly Uri NewFeedBackUri = new Uri(@"https://docs.google.com/forms/d/e/1FAIpQLSexBkTA-zBSAeQPd3M24wXCIJPXc31YAJ61U2uusFSegF-VzA/viewform");

        public FeedBack()
        {
            this.InitializeComponent();

            webView1.NavigationStarting += (sender, args) =>
            {
                Progress.IsActive = true;
            };

            webView1.NavigationCompleted += async (sender, args) =>
            {
                Progress.IsActive = false;
                if (args.Uri.ToString() == FeedbackAddedLink)
                {
                    await AddFeedbackToList();
                    UpdateComboBox();
                    combo.SelectedIndex = 0;
                }
                else if (args.Uri.ToString().Contains(FeedbackAddedLink))
                {
                    combo.SelectedIndex = 0;
                }
            };

            combo.SelectionChanged += (sender, e) =>
            {
                if (combo.SelectedIndex == -1)
                {
                    return;
                }

                webView1.Navigate(GetUriFromComboIndex(combo.SelectedIndex));
            };
        }

        private void UpdateComboBox()
        {
            combo.Items.Clear();
            combo.Items.Add("Homepage");
            combo.Items.Add("New Feedback");
            for (int i = 0; i < FeedBackLinks.Count; ++i)
            {
                combo.Items.Add("Feedback #" + (i + 1));
            }
        }

        private Uri GetUriFromComboIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return HomePageUri;
                case 1:
                    return NewFeedBackUri;
            }
            return new Uri(FeedBackLinks.ToArray()[index - 2]);
        }

        private async Task AddFeedbackToList()
        {
            string html = await webView1.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });

            int TextIndex = html.LastIndexOf("Edit your response");
            int linkstartindex = html.Substring(0, TextIndex).LastIndexOf(@"https://");

            string link = html.Substring(linkstartindex, TextIndex - linkstartindex - 2);
            link = link.Replace(@"usp=form_confirm&amp;", "");

            FeedBackLinks.AddLast(link);
            
            LoggingServices.Instance.WriteLine<FeedBack>(string.Format("{0} link is added to Feedback #{1}", link, FeedBackLinks.Count));

            HDDSync.SaveFeedBackToHDD(FeedBackLinks);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FeedBackLinks = HDDSync.GetFeedBackFromHdd();

            UpdateComboBox();
            combo.SelectedIndex = 0;
        }
    }
}