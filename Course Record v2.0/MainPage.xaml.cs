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
        private readonly LinkedList<NavigationViewItemBase> NavigationItems = new LinkedList<NavigationViewItemBase>();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            
            NavigationItems.AddLast(new NavigationViewItemSeparator());
            NavigationItems.AddLast(new NavigationViewItemHeader() { Content = "Contacts" });
            NavigationItems.AddLast(new NavigationViewItem() { Content = "Teachers" });
            NavigationItems.AddLast(new NavigationViewItemSeparator());
            NavigationItems.AddLast(new NavigationViewItemHeader() { Content = "Semester Data" });
            NavigationItems.AddLast(new NavigationViewItem() { Content = "Course" });
            NavigationItems.AddLast(new NavigationViewItem() { Content = "Time Table" });
            NavigationItems.AddLast(new NavigationViewItemSeparator());
            NavigationItems.AddLast(new NavigationViewItemHeader() { Content = "Internet" });
            NavigationItems.AddLast(new NavigationViewItem() { Content = "Internet" });
            NavigationItems.AddLast(new NavigationViewItemSeparator());

            NavView.MenuItems.Clear();
            foreach (NavigationViewItemBase b in NavigationItems)
                NavView.MenuItems.Add(b);

        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            object SelectedItem = (sender.SelectedItem as NavigationViewItem).Content;

            switch (SelectedItem)
            {
                case "Course":
                    this.Frame.Navigate(typeof(Frames.Course.MainPage));
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) => NavView.SelectedItem = NavigationItems.First.Next.Next.Value;
    }
}