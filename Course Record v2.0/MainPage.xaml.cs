using System;
using System.Collections.Generic;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Course_Record_v2._0
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly LinkedList<NavigationViewItemBase> MainPageNavigationItems = new LinkedList<NavigationViewItemBase>();
        private readonly LinkedList<NavigationViewItemBase> CourseNavigationItems = new LinkedList<NavigationViewItemBase>();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            // Main Page
            MainPageNavigationItems.AddLast(new NavigationViewItemSeparator());
            MainPageNavigationItems.AddLast(new NavigationViewItemHeader() { Content = "Contacts" });
            MainPageNavigationItems.AddLast(new NavigationViewItem() { Content = "Teachers" });
            MainPageNavigationItems.AddLast(new NavigationViewItemSeparator());
            MainPageNavigationItems.AddLast(new NavigationViewItemHeader() { Content = "Semester Data" });
            MainPageNavigationItems.AddLast(new NavigationViewItem() { Content = "Course" });
            MainPageNavigationItems.AddLast(new NavigationViewItem() { Content = "Time Table" });
            MainPageNavigationItems.AddLast(new NavigationViewItemSeparator());
            MainPageNavigationItems.AddLast(new NavigationViewItemHeader() { Content = "Websites" });
            MainPageNavigationItems.AddLast(new NavigationViewItem() { Content = "ID" });
            MainPageNavigationItems.AddLast(new NavigationViewItem() { Content = "Nalanda" });
            MainPageNavigationItems.AddLast(new NavigationViewItem() { Content = "SWD" });
            MainPageNavigationItems.AddLast(new NavigationViewItemSeparator());

            NavView.MenuItems.Clear();
            foreach (NavigationViewItemBase b in MainPageNavigationItems)
                NavView.MenuItems.Add(b);


        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem)
            {
                // Navigation1_2();
            }
            if (ContentFrame.CanGoBack)
                sender.IsBackEnabled = true;
            else
                sender.IsBackEnabled = false;
        }

        private void SecondNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (ContentFrame.BackStackDepth == 1)
                sender.IsBackEnabled = false;
            else
                sender.IsBackEnabled = true;
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            ContentFrame.GoBack(new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
            if (sender == SecondNavigationView)
                sender.IsBackEnabled = ContentFrame.BackStackDepth == 1 ? false : true;
            else
                sender.IsBackEnabled = ContentFrame.CanGoBack ? true : false;
        }


        private void Navigation1_2(Type ToPage, LinkedList<NavigationViewItemBase> list)
        {
            NavView.IsPaneOpen = false;

            ContentFrame.BackStack.Clear();
            ContentFrame.ForwardStack.Clear();

            ContentFrame.Navigate(ToPage, null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });

            SecondNavigationView.Visibility = Visibility.Visible;
            SecondNavigationView.IsPaneOpen = true;

            NavView.Visibility = Visibility.Collapsed;
        }

        [Obsolete("Main Page not set")]
        private void Navigation2_1()
        {

            SecondNavigationView.IsPaneOpen = false;

            ContentFrame.BackStack.Clear();
            ContentFrame.ForwardStack.Clear();

            // ContentFrame.Navigate( new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft }); // Goto Main Page

            NavView.Visibility = Visibility.Visible;
            NavView.IsPaneOpen = true;

            SecondNavigationView.Visibility = Visibility.Collapsed;
            SecondNavigationView.MenuItems.Clear();
        }
    }
}