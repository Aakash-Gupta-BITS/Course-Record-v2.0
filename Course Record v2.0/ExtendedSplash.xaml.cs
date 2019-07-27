using System;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Course_Record_v2._0
{
    public sealed partial class ExtendedSplash : Page
    {
        public ExtendedSplash()
        {
            this.InitializeComponent();
            Thread t = new Thread(new ThreadStart(LoadingTasks));
            t.Start();
        }

        public async void LoadingTasks()
        {
            Thread.Sleep(1000);
            ConsoleAppEngine.Globals.HDDSync.GetAllFromHDD();
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame rootFrame = new Frame();
                Window.Current.Content = rootFrame;
                rootFrame.Navigate(typeof(MainPage));
            });
        }
    }
}
