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
    public class EBooks : ECourseElemBase<EBookItem>
    {
        TextBox NameBox;
        TextBox AuthorBox;
        TextBox EditionBox;
        TextBox PressBox;
        ComboBox BookTypeBox;
        CheckBox BestBookBox;

        public void AddBook(EBookItem eBookItem)
        {
            lists.AddLast(eBookItem);
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

            NameBox = null;
            AuthorBox = null;
            EditionBox = null;
            PressBox = null;
            BookTypeBox = null;
            BestBookBox = null;
        }

        protected override void AddNewItem()
        {
            AddBook(new EBookItem(
                (TextBookType)Enum.Parse(typeof(TextBookType), BookTypeBox.SelectedItem as string),
                AuthorBox.Text,
                NameBox.Text,
                int.Parse(EditionBox.Text),
                PressBox.Text,
                BestBookBox.IsChecked == true));
        }

        protected override void CheckInputs()
        {
            // Edition Check
            if (!int.TryParse(EditionBox.Text, out int ed) || ed <= 0)
            {
                EditionBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));   // Red
                throw new Exception();
            }

            // Book Type Check
            if (BookTypeBox.SelectedItem == null)
            {
                BookTypeBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));   // Red
                throw new Exception();
            }


            EditionBox.BorderBrush = AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
        }

        protected override void ClearAddGrid()
        {
            ItemToChange = null;
            AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

            NameBox.Text =
            AuthorBox.Text =
            EditionBox.Text =
            PressBox.Text = "";
            BookTypeBox.SelectedItem = null;
            BestBookBox.IsChecked = false;

        } 

        protected override Grid Header()
        {
            Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock NameBlock = new TextBlock()
            {
                Text = "Name",
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold
            };
            TextBlock AuthorBlock = new TextBlock()
            {
                Text = "Author",
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold
            };
            TextBlock BookTypeBlock = new TextBlock()
            {
                Text = "Book Type",
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold
            };
            TextBlock BestBlock = new TextBlock()
            {
                Text = "Best Book",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.Bold
            };

            Grid.SetColumn(NameBlock, 0);
            Grid.SetColumn(AuthorBlock, 1);
            Grid.SetColumn(BookTypeBlock, 2);
            Grid.SetColumn(BestBlock, 3);

            grid.Children.Add(NameBlock);
            grid.Children.Add(AuthorBlock);
            grid.Children.Add(BookTypeBlock);
            grid.Children.Add(BestBlock);

            return grid;
        }

        protected override void InitializeAddGrid()
        {
            TextBlock tb1 = new TextBlock()
            {
                Margin = new Thickness(10, 10, 5, 10),
                HorizontalAlignment = HorizontalAlignment.Right,
                Text = "Name : "
            };
            Grid.SetRow(tb1, 0);
            Grid.SetColumn(tb1, 0);

            TextBlock tb2 = new TextBlock()
            {
                Margin = new Thickness(10, 10, 5, 10),
                HorizontalAlignment = HorizontalAlignment.Right,
                Text = "Author : "
            };
            Grid.SetRow(tb2, 1);
            Grid.SetColumn(tb2, 0);

            TextBlock tb3 = new TextBlock()
            {
                Margin = new Thickness(10, 10, 5, 10),
                HorizontalAlignment = HorizontalAlignment.Right,
                Text = "Edition : "
            };
            Grid.SetRow(tb3, 2);
            Grid.SetColumn(tb3, 0);

            TextBlock tb4 = new TextBlock()
            {
                Margin = new Thickness(10, 10, 5, 10),
                HorizontalAlignment = HorizontalAlignment.Right,
                Text = "Press : "
            };
            Grid.SetRow(tb4, 3);
            Grid.SetColumn(tb4, 0);

            TextBlock tb5 = new TextBlock()
            {
                Margin = new Thickness(10, 10, 5, 10),
                HorizontalAlignment = HorizontalAlignment.Right,
                Text = "Type : "
            };
            Grid.SetRow(tb5, 4);
            Grid.SetColumn(tb5, 0);

            NameBox = new TextBox()
            {
                Margin = new Thickness(5, 10, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            Grid.SetRow(NameBox, 0);
            Grid.SetColumn(NameBox, 1);

            AuthorBox = new TextBox()
            {
                Margin = new Thickness(5, 10, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            Grid.SetRow(AuthorBox, 1);
            Grid.SetColumn(AuthorBox, 1);

            EditionBox = new TextBox()
            {
                Margin = new Thickness(5, 10, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            Grid.SetRow(EditionBox, 2);
            Grid.SetColumn(EditionBox, 1);

            PressBox = new TextBox()
            {
                Margin = new Thickness(5, 10, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            Grid.SetRow(PressBox, 3);
            Grid.SetColumn(PressBox, 1);

            BookTypeBox = new ComboBox()
            {
                Margin = new Thickness(5, 10, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            Grid.SetRow(BookTypeBox, 4);
            Grid.SetColumn(BookTypeBox, 1);

            BestBookBox = new CheckBox()
            {
                Margin = new Thickness(5, 10, 10, 5),
                Content = "Best Book"
            };
            Grid.SetRow(BestBookBox, 5);
            Grid.SetColumn(BestBookBox, 1);

            AddButton = new Button()
            {
                Margin = new Thickness(10, 5, 10, 10),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Content = "Add"
            };
            Grid.SetRow(AddButton, 6);
            Grid.SetColumn(AddButton, 0);
            Grid.SetColumnSpan(AddButton, 2);

            BookTypeBox.Items.Add(TextBookType.TextBook.ToString());
            BookTypeBox.Items.Add(TextBookType.Reference.ToString());
            BookTypeBox.Items.Add(TextBookType.Extra.ToString());

            AddGrid.Children.Add(tb1);
            AddGrid.Children.Add(tb2);
            AddGrid.Children.Add(tb3);
            AddGrid.Children.Add(tb4);
            AddGrid.Children.Add(tb5);
            AddGrid.Children.Add(NameBox);
            AddGrid.Children.Add(AuthorBox);
            AddGrid.Children.Add(EditionBox);
            AddGrid.Children.Add(PressBox);
            AddGrid.Children.Add(BookTypeBox);
            AddGrid.Children.Add(BestBookBox);
            AddGrid.Children.Add(AddButton);
        }

        protected override void ItemToChangeUpdate()
        {
            ItemToChange.Update(
                (TextBookType)Enum.Parse(typeof(TextBookType), BookTypeBox.SelectedItem as string),
                AuthorBox.Text,
                NameBox.Text,
                int.Parse(EditionBox.Text),
                PressBox.Text,
                BestBookBox.IsChecked == true);
        }

        protected override IOrderedEnumerable<EBookItem> OrderList() => lists.OrderBy(a => a.BookType).ThenBy(a => !a.IsBest).ThenBy(a => a.Name);

        protected override void SetAddGrid_ItemToChange()
        {
            NameBox.Text = ItemToChange.Name;
            AuthorBox.Text = ItemToChange.Author;
            EditionBox.Text = ItemToChange.Edition.ToString();
            PressBox.Text = ItemToChange.Press;
            BookTypeBox.SelectedIndex = (int)ItemToChange.BookType;
            BestBookBox.IsChecked = ItemToChange.IsBest;

        }

        protected override void SetContentDialog()
        {
            contentDialog.Title = ItemToChange.Name;
            string content = string.Format(
                "Author\t- {0}\nName\t- {1}\nPress\t- {2}\nEdition\t- {3}",
                ItemToChange.Author,
                ItemToChange.Name,
                ItemToChange.Press,
                ItemToChange.Edition);

            switch (ItemToChange.Edition)
            {
                case 1:
                    content += "st";
                    break;
                case 2:
                    content += "nd";
                    break;
                case 3:
                    content += "rd";
                    break;
                default:
                    content += "th";
                    break;
            }
            content += " edition";

            contentDialog.Content = content;
        }
    }
}