using System;
using System.Linq;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
using ConsoleAppEngine.Course.Abstracts;

namespace ConsoleAppEngine.Course
{
    public class EHandouts : ECourseElemBase<EHandoutItem>
    {
        TextBox LectureBox;
        TextBox TopicBox;
        TextBox DescriptionBox;
        CheckBox DoneByMeBox;

        public void AddHandout(EHandoutItem handoutItem)
        {
            lists.AddLast(handoutItem);
            UpdateList();
        }

        public override void DestructViews()
        {
            ViewGrid.Children.Clear();
            AddGrid.Children.Clear();
            ViewList.Items.Clear();

            ViewGrid = null;
            AddGrid = null;
            ViewList = null;
            AddButton = null;
            ViewCommand = null;
            AddCommand = null;

            LectureBox = null;
            TopicBox = null;
            DescriptionBox = null;
            DoneByMeBox = null;
        }

        protected override void AddNewItem()
        {
            AddHandout(new EHandoutItem(
                int.Parse(LectureBox.Text),
                TopicBox.Text,
                DoneByMeBox.IsChecked == true,
                DescriptionBox.Text));
        }

        protected override void CheckInputs()
        {
            // Valid Lecture Check
            if (!int.TryParse(LectureBox.Text, out int lecture) || lecture <= 0)
            {
                LectureBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));    // Red
                throw new Exception();
            }
            // Lecture Repeatition check
            foreach (var x in (from a in lists where a != ItemToChange && a.IsDeleted == false select a.LectureNo))
                if (x == lecture)
                {
                    LectureBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));    // Red
                    throw new Exception();
                }

            LectureBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
        }

        protected override void ClearAddGrid()
        {
            ItemToChange = null;
            LectureBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

            LectureBox.Text = 
            TopicBox.Text = 
            DescriptionBox.Text = "";
            DoneByMeBox.IsChecked = false;
        }

        protected override Grid Header()
        {
            Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });

            TextBlock LectureNo = new TextBlock()
            {
                Text = "Lecture No",
                HorizontalAlignment = HorizontalAlignment.Left,
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
                HorizontalAlignment = HorizontalAlignment.Center,
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

        protected override void InitializeAddGrid()
        {
            TextBlock tb1 = new TextBlock()
            {
                Margin = new Thickness(10, 10, 10, 10),
                HorizontalAlignment = HorizontalAlignment.Right,
                Text = "Lecture No : "
            };
            Grid.SetRow(tb1, 0);
            Grid.SetColumn(tb1, 0);

            TextBlock tb2 = new TextBlock()
            {
                Margin = new Thickness(10, 10, 10, 10),
                HorizontalAlignment = HorizontalAlignment.Right,
                Text = "Topic : "
            };
            Grid.SetRow(tb2, 1);
            Grid.SetColumn(tb2, 0);

            TextBlock tb3 = new TextBlock()
            {
                Margin = new Thickness(10, 10, 10, 10),
                HorizontalAlignment = HorizontalAlignment.Right,
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

            AddGrid.Children.Add(tb1);
            AddGrid.Children.Add(tb2);
            AddGrid.Children.Add(tb3);
            AddGrid.Children.Add(LectureBox);
            AddGrid.Children.Add(TopicBox);
            AddGrid.Children.Add(DescriptionBox);
            AddGrid.Children.Add(DoneByMeBox);
            AddGrid.Children.Add(AddButton);
        }

        protected override void ItemToChangeUpdate()
        {
            ItemToChange.Update(int.Parse(LectureBox.Text), TopicBox.Text, DoneByMeBox.IsChecked == true, DescriptionBox.Text);
        }

        protected override IOrderedEnumerable<EHandoutItem> OrderList() => lists.OrderBy(a => a.LectureNo);

        protected override void SetAddGrid_ItemToChange()
        {
            LectureBox.Text = ItemToChange.LectureNo.ToString();
            DescriptionBox.Text = ItemToChange.Description;
            TopicBox.Text = ItemToChange.Topic;
            DoneByMeBox.IsChecked = ItemToChange.DoneByMe;
        }

        protected override void SetContentDialog()
        {
            contentDialog.Title = ItemToChange.Topic;
            contentDialog.Content = ItemToChange.Description;
        }

    }
}