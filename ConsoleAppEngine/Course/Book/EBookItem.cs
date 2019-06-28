using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    public class EBookItem : EElementItemBase
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
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 2), (typeof(string), 2), (typeof(string), 1), (typeof(bool), 1));

            NameViewBlock = controls[0] as TextBlock;
            AuthorViewBlock = controls[1] as TextBlock;
            BookTypeViewBlock = controls[2] as TextBlock;
            IsBestViewBox = controls[3] as CheckBox;

            Update(bookType, author, name, edition, press, isBest);

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