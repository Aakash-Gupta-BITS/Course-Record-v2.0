using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Course;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Books : Page
    {
        EBooks BookEntry;
        public Books()
        {
            this.InitializeComponent();
            foreach (var x in Enum.GetNames(typeof(TextBookType)))
                BookTypeInput.Items.Add(x);
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            BookEntry.DestructViews();
        }
    }
}
