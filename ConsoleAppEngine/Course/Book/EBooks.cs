using System;
using System.Linq;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;
using ConsoleAppEngine.Course.Abstracts;
using ConsoleAppEngine.AllEnums;
using System.Collections.Generic;

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

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
            Controls.AddLast(EditionBox);
            Controls.AddLast(BookTypeBox);

            if (!int.TryParse(EditionBox.Text, out int ed) || ed <= 0)
            {
                ErrorWaale.AddLast(EditionBox);
            }

            // Book Type Check
            if (BookTypeBox.SelectedItem == null)
            {
                ErrorWaale.AddLast(BookTypeBox);
            }
        }

        protected override void ClearAddGrid()
        {
            ItemToChange = null;
            AddButton.BorderBrush =
            EditionBox.BorderBrush =
            BookTypeBox.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

            NameBox.Text =
            AuthorBox.Text =
            EditionBox.Text =
            PressBox.Text = "";
            BookTypeBox.SelectedItem = null;
            BestBookBox.IsChecked = false;

        } 

        protected override Grid Header() => GenerateHeader(("Name", 2), ("Author", 2), ("Book Type", 1), ("Best Book", 1));

        protected override void InitializeAddGrid(params FrameworkElement[] AddViewGridControls)
        {
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