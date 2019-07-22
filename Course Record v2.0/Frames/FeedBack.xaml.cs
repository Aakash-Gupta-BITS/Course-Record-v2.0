using ConsoleAppEngine.Log;
using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public struct uriEdit
    {
        string Name;
        Uri uriE;
    }
    public sealed partial class FeedBack : Page
    {
        public FeedBack()
        {
            this.InitializeComponent();
            webView1.NavigationStarting += (sender, args) =>
            {
                Progress.IsActive = true;
            };
            webView1.NavigationCompleted += (sender, args) =>
            {
                Progress.IsActive = false;
                Update();
            };
        }
        public async void Update()
        {
            Uri uriForm = new Uri("https://docs.google.com/forms/d/e/1FAIpQLSexBkTA-zBSAeQPd3M24wXCIJPXc31YAJ61U2uusFSegF-VzA/formResponse");
              
            if(webView1.Source.ToString() == uriForm.ToString())
            {
                char ch;
                string html = await webView1.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });
                int index1 = html.IndexOf("freebirdFormviewerViewResponseLinksContainer");
                int index2 = html.IndexOf("https", index1);
                string uriE="";
                int i = index2;
                for(i=index2; html[i+1] != 34; ++i)
                {         
                    ch = html[i];
                    uriE = uriE + html[i];
                    if (ch == 63)
                    { i = i + 21; }
                }
                combo.Items.Add(uriE);
            }
        }
    }
}
