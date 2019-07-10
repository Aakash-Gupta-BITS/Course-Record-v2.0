using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Course;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0.Frames.Course
{
    public sealed partial class Books : Page
    {
        private EBooks BookEntry;
        public Books()
        {
            this.InitializeComponent();
            foreach (var x in Enum.GetNames(typeof(TextBookType)))
            {
                BookTypeInput.Items.Add(x);
            }

            this.Unloaded += (object sender, Windows.UI.Xaml.RoutedEventArgs e) =>
            {
                BookEntry.DestructViews();
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BookEntry = e.Parameter as EBooks;
            BookEntry.InitializeViews(
                ViewGrid,
                AddGrid,
                ViewCommand,
                AddCommand,
                NameInput,
                AuthorInput,
                EditionInput,
                PressInput,
                BookTypeInput,
                BestBookInput,
                AddButton);
        }
    }
}
