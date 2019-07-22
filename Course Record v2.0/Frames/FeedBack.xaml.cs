using ConsoleAppEngine.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
               
            };
           
        }
        public void Update()
        {
            bool flag = false;
            Uri uriForm = new Uri("https://docs.google.com/forms/d/e/1FAIpQLSexBkTA-zBSAeQPd3M24wXCIJPXc31YAJ61U2uusFSegF-VzA/formResponse");
            if (webView1.Source == uriForm)
            {
                flag = true;
                LoggingServices.Instance.WriteLine<FeedBack>("\"" + flag as string + "\" .");
            }
        }
    }
}
