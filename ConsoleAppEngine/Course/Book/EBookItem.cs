using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ConsoleAppEngine.Course.Abstracts;
using ConsoleAppEngine.AllEnums;

namespace ConsoleAppEngine.Course
{
    public class EBookItem : ECourseElemItemBase
    {
        public TextBookType BookType { get; private set; }
        public string Author { get; private set; }
        public string Name { get; private set; }
        public int Edition { get; private set; }
        public string Press { get; private set; }
        public bool IsBest { get; private set; }

        internal readonly TextBlock NameViewBlock;
        internal readonly TextBlock AuthorViewBlock;
        internal readonly TextBlock BookTypeViewBlock;
        internal readonly CheckBox IsBestViewBox;

        public EBookItem(TextBookType bookType, string author, string name, int edition, string press, bool isBest)
        {
            BookType = bookType;
            Author = author;
            Name = name;
            Edition = edition;
            Press = press;
            IsBest = isBest;

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            NameViewBlock = new TextBlock()
            {
                Text = Name,
                HorizontalAlignment = HorizontalAlignment.Left,
                TextWrapping = TextWrapping.Wrap
            };
            AuthorViewBlock = new TextBlock()
            {
                Text = Author,
                HorizontalAlignment = HorizontalAlignment.Left,
                TextWrapping = TextWrapping.Wrap
            };
            BookTypeViewBlock = new TextBlock()
            {
                Text = BookType.ToString(),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            IsBestViewBox = new CheckBox()
            {
                IsChecked = IsBest,
                Content = "",
                HorizontalAlignment = HorizontalAlignment.Center,
                MinWidth = 32
            };

            Grid.SetColumn(NameViewBlock, 0);
            Grid.SetColumn(AuthorViewBlock, 1);
            Grid.SetColumn(BookTypeViewBlock, 2);
            Grid.SetColumn(IsBestViewBox, 3);

            grid.Children.Add(NameViewBlock);
            grid.Children.Add(AuthorViewBlock);
            grid.Children.Add(BookTypeViewBlock);
            grid.Children.Add(IsBestViewBox);

            GetView.Content = grid;
            IsBestViewBox.Click += (object sender, RoutedEventArgs e) => IsBest = IsBestViewBox.IsChecked == true ? true : false;
        }

        internal void Update(TextBookType bookType, string author, string name, int edition, string press, bool isBest)
        {
            BookType = bookType;
            Author = author;
            Name = name;
            Edition = edition;
            Press = press;
            IsBest = isBest;

            NameViewBlock.Text = Name;
            AuthorViewBlock.Text = Author;
            BookTypeViewBlock.Text = BookType.ToString();
            IsBestViewBox.IsChecked = IsBest;
        }

        internal override object PointerOverObject => IsBestViewBox;
    }
}