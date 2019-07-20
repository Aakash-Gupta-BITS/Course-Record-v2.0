using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0.Frames.Course
{
    public sealed partial class Teachers : Page
    {
        private CourseEntry SelectedCourse;

        private ETeachers CourseTeachers => SelectedCourse.TeacherEntry;

        private ETeachers AllTeachers => AllContacts.Instance.TeacherEntry;

        public Teachers()
        {
            this.InitializeComponent();
            this.Unloaded += (object sender, RoutedEventArgs e) =>
            {
                foreach (var x in CourseTeachers.lists)
                {
                    x.DestroyTeacherViews();
                }

                ViewList.Items.Clear();
            };
        }

        private async void AddCommand_Click(object sender, RoutedEventArgs e)
        {
            // Define Combobox for Display in ContentDialog
            ComboBox comboBox = new ComboBox()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Header = "Select Teachers Here"
            };

            // Fill only those names that are not not yet added in course
            foreach (var x in (from a in AllTeachers.lists where !ViewList.Items.Contains(a.GetView) select a.Name))
            {
                comboBox.Items.Add(x);
            }

            // Instance of Content Dialog that will be displayed
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
            {
                comboBox.SelectedItem = comboBox.Items.First();
            }

            switch (await contentDialog.ShowAsync())
            {
                // Add
                case ContentDialogResult.Primary:

                    // Find the selected teacher
                    foreach (var x in AllTeachers.lists)
                    {
                        if (x.Name == comboBox.SelectedItem.ToString())
                        {
                            CourseTeachers.lists.AddLast(x);
                            break;
                        }
                    }

                    // Sort Teachers
                    List<ETeacherEntry> v = CourseTeachers.lists.OrderBy(a => a.Name).ToList();
                    CourseTeachers.lists.Clear();
                    foreach (var x in v)
                    {
                        CourseTeachers.lists.AddLast(x);
                    }

                    // Fill ViewList with new sorted order
                    ViewList.Items.Clear();
                    foreach (var a in CourseTeachers.lists)
                    {
                        if (a.GetView == null)
                        {
                            a.InitializeTeacher();
                        }

                        ViewList.Items.Add(a.GetView);
                    }

                    break;
            }
        }

        private async void ViewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewList.SelectedItem == null)
            {
                return;
            }

            // Get selected teacher first
            ETeacherEntry SelectedTeacher = null;
            foreach (var x in CourseTeachers.lists)
            {
                if (x.GetView == (ViewList.SelectedItem))
                {
                    SelectedTeacher = x;
                    break;
                }
            }

            // Unselect the selected item, it will again call this function but null check return it
            ViewList.SelectedItem = null;

            // Content Dialog that will be displayed
            ContentDialog contentDialog = new ContentDialog()
            {
                PrimaryButtonText = "Remove from this course",
                CloseButtonText = "Ok",
                Title = SelectedTeacher.Name,
                Content =
                new TextBlock()
                {
                    Text = string.Format(
                        "{0}\n\n" +
                        "Phone   \t:\t{1}, {2}\n" +
                        "Email   \t:\t{3}, {4}\n" +
                        "Website \t:\t{5}\n" +
                        "Other Info :\t{6}",
                        SelectedTeacher.Address,
                        SelectedTeacher.Phone[0],
                        SelectedTeacher.Phone[1],
                        SelectedTeacher.Email[0],
                        SelectedTeacher.Email[1],
                        SelectedTeacher.Website,
                        SelectedTeacher.OtherInfo),
                    IsTextSelectionEnabled = true
                }
            };

            switch (await contentDialog.ShowAsync())
            {
                // Delete
                case ContentDialogResult.Primary:
                    ViewList.Items.Remove(SelectedTeacher.GetView);
                    CourseTeachers.lists.Remove(SelectedTeacher);
                    SelectedTeacher.DestroyTeacherViews();
                    SelectedCourse.RemoveTeacherFromCourse(SelectedTeacher);
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SelectedCourse = e.Parameter as CourseEntry;

            foreach (var x in CourseTeachers.lists)
            {
                if (x.GetView == null)
                {
                    x.InitializeTeacher();
                }

                ViewList.Items.Add(x.GetView);
            }
        }
    }
}