using ConsoleAppEngine.Course;
using ConsoleAppEngine.Contacts;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Course_Record_v2._0.Frames.Course
{
    public sealed partial class Teachers : Page
    {
        CourseEntry entry;
        ETeachers teachers => entry.TeacherEntry;
        AllContacts allContacts;

        public Teachers()
        {
            this.InitializeComponent();
            this.Unloaded += (object sender, RoutedEventArgs e) =>
            {
                ViewList.Items.Clear();
                entry.SyncTimeTablewithTeachers();
            };
        }

        private async void AddCommand_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Define Combobox for Display in ContentDialog
            ComboBox comboBox = new ComboBox()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Header = "Select Teachers Here"
            };

            // Fill only those names that are not not yet added in course
            foreach (var x in (from a in allContacts.TeacherEntry.lists where !ViewList.Items.Contains(a.GetView) select a.Name))
                comboBox.Items.Add(x);

            // Instance of Content Dialog thaat will be displayed
            ContentDialog contentDialog = new ContentDialog()
            {
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                Title = "Add New Item",
                Content = comboBox
            };

            // If no teacher if left
            if (comboBox.Items.Count == 0)
            {
                comboBox.IsEnabled = false;
                contentDialog.IsPrimaryButtonEnabled = false;
            }
            else
                comboBox.SelectedItem = comboBox.Items.First();


            switch (await contentDialog.ShowAsync())
            {
                // Add
                case ContentDialogResult.Primary:

                    // Find the selected teacher
                    foreach (var x in allContacts.TeacherEntry.lists)
                        if (x.Name == comboBox.SelectedItem.ToString())
                        {
                            teachers.lists.AddLast(x);
                            break;
                        }


                    // Sort Teachers
                    List<ETeacherEntry> v = teachers.lists.OrderBy(a => a.Name).ToList();
                    teachers.lists.Clear();
                    foreach (var x in v)
                        teachers.lists.AddLast(x);

                    // Fill ViewList with new sorted order
                    ViewList.Items.Clear();
                    foreach (var a in from a in teachers.lists select a.GetView)
                        ViewList.Items.Add(a);

                    break;
            }
        }

        private async void ViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewList.SelectedItem == null)
                return;

            // Get selected teacher first
            ETeacherEntry ItemSelected = null;
            foreach (var x in teachers.lists)
                if (x.GetView == (ViewList.SelectedItem))
                {
                    ItemSelected = x;
                    break;
                }

            // Unselect the selected item, it will again call this function but null check return it
            ViewList.SelectedItem = null;

            ContentDialog contentDialog = new ContentDialog()
            {
                PrimaryButtonText = "Remove from this course",
                CloseButtonText = "Ok",
                Title = ItemSelected.Name,
                Content =
                new TextBlock()
                {
                    Text = string.Format(
                        "{0}\n\n" +
                        "Phone   \t:\t{1}, {2}\n" +
                        "Email   \t:\t{3}, {4}\n" +
                        "Website \t:\t{5}\n" +
                        "Other Info :\t{6}",
                        ItemSelected.Address,
                        ItemSelected.Phone[0],
                        ItemSelected.Phone[1],
                        ItemSelected.Email[0],
                        ItemSelected.Email[1],
                        ItemSelected.Website,
                        ItemSelected.OtherInfo),
                    IsTextSelectionEnabled = true
                }
            };

            switch (await contentDialog.ShowAsync())
            {
                // Delete
                case ContentDialogResult.Primary:

                    // Important : Item not to be removed from AllContacts
                    ViewList.Items.Remove(ItemSelected.GetView);
                    teachers.lists.Remove(ItemSelected);
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var a = e.Parameter as LinkedList<object>;
            entry = a.First.Value as CourseEntry;
            allContacts = a.First.Next.Value as AllContacts;

            foreach (var x in (from x in teachers.lists select x.GetView))
                ViewList.Items.Add(x);
        }
    }
}