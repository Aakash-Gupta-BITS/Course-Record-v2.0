using ConsoleAppEngine.Log;
using System;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Course_Record_v2._0.Frames
{
    public sealed partial class FeedBack : Page
    {
        LinkedList<string> FeedBackLinks = new LinkedList<string>();

        public FeedBack()
        {
            this.InitializeComponent();
            combo.Items.Clear();
            combo.Items.Add("New Feedback");
            webView1.Navigate(new Uri(@"ms-appx-web:///Assets/FeedbackComplete.html"));
            webView1.NavigationStarting += (sender, args) =>
            {
                Progress.IsActive = true;
            };
            webView1.NavigationCompleted += (sender, args) =>
            {
                Progress.IsActive = false;
                Update();
            };
            combo.SelectionChanged += (sender, e) =>
            {
                if (combo.SelectedIndex == -1)
                    return;
                if (combo.SelectedIndex == 0)
                    webView1.Navigate(new Uri(@"https://docs.google.com/forms/d/e/1FAIpQLSexBkTA-zBSAeQPd3M24wXCIJPXc31YAJ61U2uusFSegF-VzA/viewform"));
                else
                    webView1.Navigate(new Uri(FeedBackLinks.ToArray()[combo.SelectedIndex - 1]));
            };
        }

        public async void Update()
        {
            Uri uriForm = new Uri("https://docs.google.com/forms/d/e/1FAIpQLSexBkTA-zBSAeQPd3M24wXCIJPXc31YAJ61U2uusFSegF-VzA/formResponse");

            if (webView1.Source.ToString() == uriForm.ToString())
            {
                string html = await webView1.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });
                int TextIndex = html.LastIndexOf("Edit your response");
                int linkstartindex = html.Substring(0, TextIndex).LastIndexOf(@"https://");
                string link = html.Substring(linkstartindex, TextIndex - linkstartindex - 2);
                if (!link.Contains(@"usp=form_confirm&amp;"))
                    return;
                link = link.Replace(@"usp=form_confirm&amp;", "");
                LoggingServices.Instance.WriteLine<FeedBack>(link);
                FeedBackLinks.AddLast(link);
                combo.Items.Clear();
                combo.Items.Add("New Feedback");
                for (int i = 0; i < FeedBackLinks.Count; ++i)
                    combo.Items.Add("Feedback " + (i + 1));
                webView1.Navigate(new Uri(@"ms-appx-web:///Assets/FeedbackComplete.html"));
            }
        }
    }
}