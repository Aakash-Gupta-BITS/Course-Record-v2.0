using ConsoleAppEngine.Course;
using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.AllEnums;
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
    public sealed partial class CT_log : Page
    {
        EStudents ctLog;
        EStudents allStudents;

        public CT_log()
        {
            this.InitializeComponent();
            this.Unloaded += (object sender, RoutedEventArgs e) =>
            {
                ViewList.Items.Clear();
            };
        }

        private async void AddCommand_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Define Combobox for Display in ContentDialog
            ComboBox comboBox = new ComboBox()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Header = "Select CT log Here"
            };

            // Fill only those names that are not not yet added in course
            foreach (var x in (from a in allStudents.lists where !ViewList.Items.Contains(a.GetView) select a.Name))
                comboBox.Items.Add(x);

            // Instance of Content Dialog thaat will be displayed
            ContentDialog contentDialog = new ContentDialog()
            {
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                Title = "Add New Item",
                Content = comboBox
            };

            // If no student if left
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

                    // Find the selected student
                    foreach (var x in allStudents.lists)
                        if (x.Name == comboBox.SelectedItem.ToString())
                        {
                            ctLog.lists.AddLast(x);
                            break;
                        }


                    // Sort ctLog
                    List<EStudentEntry> v = ctLog.lists.OrderBy(a => a.Name).ToList();
                    ctLog.lists.Clear();
                    foreach (var x in v)
                        ctLog.lists.AddLast(x);

                    // Fill ViewList with new sorted order
                    ViewList.Items.Clear();
                    foreach (var a in from a in ctLog.lists select a.GetView)
                        ViewList.Items.Add(a);

                    break;
            }
        }

        private async void ViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewList.SelectedItem == null)
                return;

            // Get selected student first
            EStudentEntry ItemSelected = null;
            foreach (var x in ctLog.lists)
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
                        "{6}\n\n" +
                        "Contact\t\t:\t{0}\n" +
                        "Id\t\t:\t{1}\n" +
                        "BITS Email Id\t:\t{2}\n" +
                        "Personal Email Id\t:\t{3}\n" +
                        "Hostel\t\t:\t{4}\n" +
                        "Room No\t\t:\t{5}\n",
                        string.Join(", ", ItemSelected.Phone),
                        ItemSelected.Year + " " + ((BranchId)(int)ItemSelected.Branch[0]).ToString() + ((BranchId)(int)ItemSelected.Branch[1]).ToString() + " " + ItemSelected.Digits.ToString().PadLeft(4, '0'),
                        string.Format(@"f{0}{1}@pilani.bits-pilani.ac.in", ItemSelected.Year, ItemSelected.Digits.ToString().PadLeft(4, '0')),
                        ItemSelected.PersonalMail,
                        ItemSelected.Hostel,
                        ItemSelected.Room,
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
                    ctLog.lists.Remove(ItemSelected);
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var a = e.Parameter as LinkedList<object>;
            ctLog = a.First.Value as EStudents;
            allStudents = a.First.Next.Value as EStudents;

            foreach (var x in (from x in ctLog.lists where allStudents.lists.Contains(x) select x.GetView))
                ViewList.Items.Add(x);
        }
    }
}
