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
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0
{
    partial class ExtendedSplash : Page
    {
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private SplashScreen splash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal Frame rootFrame;

        // Define methods and constructor
        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            InitializeComponent();

            splash = splashscreen;
            if (splash != null)
            {
                splash.Dismissed += new TypedEventHandler<SplashScreen, object>(DismissedEventHandler);

                splashImageRect = splash.ImageLocation;
            }

            rootFrame = new Frame();
        }

        void DismissedEventHandler(SplashScreen sender, object e)
        {
            dismissed = true;

            DismissExtendedSplash();
        }

        async void DismissExtendedSplash()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
                rootFrame.Navigate(typeof(MainPage));
            });
        }
    }

    public sealed partial class ExtendedSplash : Page
    {
        public ExtendedSplash()
        {
            this.InitializeComponent();
        }
    }
}
