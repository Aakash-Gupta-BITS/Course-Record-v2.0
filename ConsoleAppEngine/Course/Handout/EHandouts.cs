using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace ConsoleAppEngine
{
    public class EHandouts
    {
        readonly LinkedList<EHandoutItem> lists = new LinkedList<EHandoutItem>();
        EHandoutItem ItemToChange { get; set; }

        Grid ViewGrid;
        Grid AddGrid;
        ListView ViewList;
        TextBox LectureBox;
        TextBox TopicBox;
        TextBox DescriptionBox;
        CheckBox DoneByMeBox;
        Button AddButton;
        readonly ContentDialog contentDialog = new ContentDialog()
        {
            PrimaryButtonText = "Modify",
            SecondaryButtonText = "Delete",
            CloseButtonText = "Ok"
        };
        AppBarButton ViewCommand;
        AppBarButton AddCommand;

        private void AddHandout()
        {
            AddHandout(new EHandoutItem(int.Parse(LectureBox.Text), TopicBox.Text, DoneByMeBox.IsChecked == true, DescriptionBox.Text));
        }

        public void AddHandout(EHandoutItem handoutItem)
        {
            lists.AddLast(handoutItem);
            UpdateList();
        }

        public void InitializeViews(Grid viewGrid, Grid addGrid, AppBarButton viewCommand, AppBarButton addCommand)
        {
            Grid Header()
            {
                Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });

                TextBlock LectureNo = new TextBlock()
                {
                    Text = "Lecture No",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    TextWrapping = TextWrapping.Wrap,
                    FontWeight = FontWeights.Bold
                };
                TextBlock Topic = new TextBlock()
                {
                    Text = "Topic",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };
                TextBlock DonebyMe = new TextBlock()
                {
                    Text = "Done by Me",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };

                Grid.SetColumn(LectureNo, 0);
                Grid.SetColumn(Topic, 1);
                Grid.SetColumn(DonebyMe, 2);

                grid.Children.Add(LectureNo);
                grid.Children.Add(Topic);
                grid.Children.Add(DonebyMe);

                return grid;
            }

            void FillViewGrid()
            {
                Grid header = Header();
                Grid.SetRow(header, 0);
                ViewList = new ListView();
                Grid.SetRow(ViewList, 1);

                ViewGrid.Children.Add(header);
                ViewGrid.Children.Add(ViewList);

                UpdateList();
            }

            void FillAddGrid()
            {
                TextBlock tb1 = new TextBlock()
                {
                    Margin = new Thickness(10, 10, 10, 10),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Text = "Lecture No : "
                };
                Grid.SetRow(tb1, 0);
                Grid.SetColumn(tb1, 0);

                TextBlock tb2 = new TextBlock()
                {
                    Margin = new Thickness(10, 10, 10, 10),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Text = "Topic : "
                };
                Grid.SetRow(tb2, 1);
                Grid.SetColumn(tb2, 0);

                TextBlock tb3 = new TextBlock()
                {
                    Margin = new Thickness(10, 10, 10, 10),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Text = "Description : "
                };
                Grid.SetRow(tb3, 2);
                Grid.SetColumn(tb3, 0);

                LectureBox = new TextBox()
                {
                    Margin = new Thickness(10, 10, 10, 10),
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
                Grid.SetRow(LectureBox, 0);
                Grid.SetColumn(LectureBox, 1);

                TopicBox = new TextBox()
                {
                    Margin = new Thickness(10, 10, 10, 10),
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
                Grid.SetRow(TopicBox, 1);
                Grid.SetColumn(TopicBox, 1);

                DescriptionBox = new TextBox()
                {
                    Margin = new Thickness(10, 10, 10, 10),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    TextWrapping = TextWrapping.Wrap
                };
                Grid.SetRow(DescriptionBox, 2);
                Grid.SetColumn(DescriptionBox, 1);

                DoneByMeBox = new CheckBox()
                {
                    Margin = new Thickness(10, 10, 10, 10),
                    Content = "Done By Me"
                };
                Grid.SetRow(DoneByMeBox, 3);
                Grid.SetColumn(DoneByMeBox, 1);

                AddButton = new Button()
                {
                    Margin = new Thickness(10, 10, 10, 10),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Content = "Add"
                };
                Grid.SetRow(AddButton, 4);
                Grid.SetColumn(AddButton, 0);
                Grid.SetColumnSpan(AddButton, 2);

                addGrid.Children.Add(tb1);
                addGrid.Children.Add(tb2);
                addGrid.Children.Add(tb3);
                addGrid.Children.Add(LectureBox);
                addGrid.Children.Add(TopicBox);
                addGrid.Children.Add(DescriptionBox);
                addGrid.Children.Add(DoneByMeBox);
                addGrid.Children.Add(AddButton);
            }

            ViewGrid = viewGrid;
            AddGrid = addGrid;
            ViewCommand = viewCommand;
            AddCommand = addCommand;

            FillViewGrid();
            FillAddGrid();
            SetEvents();
        }

        public void DeleteViews()
        {
            ViewGrid.Children.Clear();
            AddGrid.Children.Clear();
            ViewList.Items.Clear();

            ViewGrid = null;
            AddGrid = null;
            ViewList = null;
            LectureBox = null;
            TopicBox = null;
            DescriptionBox = null;
            DoneByMeBox = null;
            AddButton = null;
            ViewCommand = null;
            AddCommand = null;
        }

        private void SetEvents()
        {
            ViewCommand.Click += (object sender, RoutedEventArgs e) =>
            {
                AddGrid.Visibility = Visibility.Collapsed;
                ViewGrid.Visibility = Visibility.Visible;
                if (AddButton.Content.ToString() == "Modify")
                    ClearAddGrid();
            };
            AddCommand.Click += (object sender, RoutedEventArgs e) =>
            {
                ViewGrid.Visibility = Visibility.Collapsed;
                AddGrid.Visibility = Visibility.Visible;
                AddButton.Content = "Add";
            };
            ViewList.SelectionChanged += async (object sender, SelectionChangedEventArgs e) =>
            {
                if (ViewList.SelectedItem == null) return;

                foreach (var a in lists)
                    if (a.GetView == ViewList.SelectedItem)
                    {
                        ItemToChange = a;
                        break;
                    }

                ViewList.SelectedItem = null;

                if (ItemToChange.DoneViewBox.IsPointerOver)
                    return;

                contentDialog.Title = ItemToChange.Topic;
                contentDialog.Content = ItemToChange.Description;

                switch (await contentDialog.ShowAsync())
                {
                    // DELETE
                    case ContentDialogResult.Secondary:
                        ViewList.Items.Remove(ItemToChange.GetView);
                        lists.Remove(ItemToChange);
                        ItemToChange.IsDeleted = true;
                        break;

                    // MODIFY
                    case ContentDialogResult.Primary:

                        ViewGrid.Visibility = Visibility.Collapsed;
                        AddGrid.Visibility = Visibility.Visible;

                        AddButton.Content = "Modify";
                        LectureBox.Text = ItemToChange.LectureNo.ToString();
                        DescriptionBox.Text = ItemToChange.Description;
                        TopicBox.Text = ItemToChange.Topic;
                        DoneByMeBox.IsChecked = ItemToChange.DoneByMe;

                        break;
                }
            };
            AddButton.Click += (object sender, RoutedEventArgs e) =>
            {
                try
                {
                    CheckInputs();
                }
                catch
                {
                    return;
                }
                if (AddButton.Content.ToString() == "Add")
                    AddHandout();
                else if (AddButton.Content.ToString() == "Modify")
                    ItemToChange.Update(int.Parse(LectureBox.Text), TopicBox.Text, DoneByMeBox.IsChecked == true, DescriptionBox.Text);

                ViewGrid.Visibility = Visibility.Visible;
                AddGrid.Visibility = Visibility.Collapsed;
                ClearAddGrid();
                ItemToChange = null;
                return;
            };
        }

        private void UpdateList()
        {
            if (ViewList == null)
                return;
            foreach (var a in (from x in lists where x.IsDeleted == true select x).ToArray())
                lists.Remove(a);

            var list = (from element in lists orderby element.LectureNo select element).ToArray();
            lists.Clear();
            foreach (var x in list)
                lists.AddLast(x);

            ViewList.Items.Clear();

            foreach (var a in (from a in lists select a.GetView))
                ViewList.Items.Add(a);
        }

        private void CheckInputs()
        {
            if (!int.TryParse(LectureBox.Text, out int lecture))
            {
                LectureBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));    // Red
                throw new Exception();
            }
            foreach (var x in (from a in lists where a != ItemToChange && a.IsDeleted == false select a.LectureNo))
                if (x == lecture)
                {
                    LectureBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));    // Red
                    throw new Exception();
                }

            LectureBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
        }

        private void ClearAddGrid()
        {
            ItemToChange = null;
            LectureBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            LectureBox.Text = "";
            TopicBox.Text = "";
            DescriptionBox.Text = "";
            DoneByMeBox.IsChecked = false;
            AddButton.Content = "Add";
        }
    }
}