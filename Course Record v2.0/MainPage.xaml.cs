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
        public Globals Global = new Globals();
        private readonly LinkedList<NavigationViewItemBase> MainPageNavigationItems = new LinkedList<NavigationViewItemBase>();

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
            object SelectedItem = (sender.SelectedItem as NavigationViewItem).Content;
            switch (SelectedItem)
            {
                case "Course":
                    this.Frame.Navigate(typeof(Frames.Course.MainPage), Global);
                    break;
            }
        }
    }
}