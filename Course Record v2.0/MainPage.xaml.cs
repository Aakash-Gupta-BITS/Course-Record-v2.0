using ConsoleAppEngine.Log;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0
{
    public sealed partial class MainPage
    {
        private readonly LinkedList<(NavigationViewItem item, Type type, string header, Frame frame)> list = new LinkedList<(NavigationViewItem, Type, string, Frame)>();
        private bool Load1 = true;

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            list.AddLast((HomeMenu, typeof(Frames.HomePage), "Home", ContentFrame));
            list.AddLast((CourseMenu, typeof(Frames.Course.MainPage), "Courses", this.Frame));
            list.AddLast((ContactMenu, typeof(Frames.Contacts.MainPage), "Contacts", this.Frame));
            list.AddLast((TimeMenu, typeof(Frames.OverallTimeTableView), "Time Table", ContentFrame));
            list.AddLast((FeedBack, typeof(Frames.FeedBack), "FeedBack", ContentFrame));
            list.AddLast((LogOut, typeof(Registration), "Unregister App", ContentFrame));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoggingServices.Instance.WriteLine<MainPage>("Initial Main Page loaded.");
            NavView.SelectedItem = HomeMenu;
            this.Frame.BackStack.Clear();
            this.Frame.ForwardStack.Clear();
            ContentFrame.ForwardStack.Clear();
            ContentFrame.BackStack.Clear();

            ContentFrame.Navigate(typeof(Frames.HomePage));
            NavView.Header = "Home";
            LoggingServices.Instance.WriteLine<MainPage>(string.Format(@"Home is selected at initial Main Page."));

            if (Load1)
            {
                Load1 = !Load1;
                foreach (var (item, type, header, frame) in list)
                {
                    item.Tapped += (sender, abcd) =>
                    {
                        if (frame != null)
                        {
                            frame.Navigate(type);
                        }
                        else
                        {
                            this.Frame.Navigate(type);
                        }

                        NavView.Header = header;
                        LoggingServices.Instance.WriteLine<MainPage>(string.Format(@"""{0}"" is selected at initial Main Page.", header));
                        NavView.SelectedItem = sender;

                    };
                }
            }
        }

        private void ShowAbout(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
    }
}