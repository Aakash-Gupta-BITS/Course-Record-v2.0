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
        ETeachers teachers;
        AllContacts allContacts;

        public Teachers()
        {
            this.InitializeComponent();
            this.Unloaded += Teachers_Unloaded;
        }

        private void Teachers_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewList.Items.Clear();
        }

        private async void AddCommand_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ComboBox comboBox = new ComboBox()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Header = "Select Teachers Here"
            };
            foreach (var x in (from a in allContacts.TeacherEntry.lists where !ViewList.Items.Contains(a.GetView) select a.Name))
                comboBox.Items.Add(x);

            ContentDialog contentDialog = new ContentDialog()
            {
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                Title = "Add New Item",
                Content = comboBox
            };

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

                    foreach (var x in allContacts.TeacherEntry.lists)
                        if (x.Name == comboBox.SelectedItem.ToString())
                        {
                            teachers.lists.AddLast(x);
                            break;
                        }

                    List<ETeacherEntry> v = teachers.lists.OrderBy(a => a.Name).ToList();
                    teachers.lists.Clear();
                    foreach (var x in v)
                    {
                        teachers.lists.AddLast(x);
                    }

                    ViewList.Items.Clear();

                    foreach (var a in from a in teachers.lists select a.GetView)
                    {
                        ViewList.Items.Add(a);
                    }
                    break;
            }
        }

        private async void ViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewList.SelectedItem == null)
                return;

            ETeacherEntry ItemToChange = null;
            foreach (var x in teachers.lists)
                if (x.GetView == (ViewList.SelectedItem))
                {
                    ItemToChange = x;
                    break;
                }
            ViewList.SelectedItem = null;

            ContentDialog contentDialog = new ContentDialog()
            {
                PrimaryButtonText = "Remove from this course",
                CloseButtonText = "Ok",
                Title = ItemToChange.Name,
                Content = 
                new TextBlock()
                {
                    Text = string.Format(
                        "{0}\n\n" +
                        "Phone   \t:\t{1}, {2}\n" +
                        "Email   \t:\t{3}, {4}\n" +
                        "Website \t:\t{5}\n" +
                        "Other Info :\t{6}",
                        ItemToChange.Address,
                        ItemToChange.Phone[0],
                        ItemToChange.Phone[1],
                        ItemToChange.Email[0],
                        ItemToChange.Email[1],
                        ItemToChange.Website,
                        ItemToChange.OtherInfo),
                    IsTextSelectionEnabled = true
                }
            };

            switch (await contentDialog.ShowAsync())
            {
                // Delete
                case ContentDialogResult.Primary:
                    ViewList.Items.Remove(ItemToChange.GetView);
                    teachers.lists.Remove(ItemToChange);
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var a = e.Parameter as LinkedList<object>;
            teachers = a.First.Value as ETeachers;
            allContacts = a.First.Next.Value as AllContacts;

            foreach (var x in (from x in teachers.lists select x.GetView))
                ViewList.Items.Add(x);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewList.Items.Clear();
        }
    }
}