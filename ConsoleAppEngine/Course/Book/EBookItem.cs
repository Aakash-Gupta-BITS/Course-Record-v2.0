using System.Runtime.Serialization;
using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public class EBookItem : EElementItemBase, ISerializable
    {
        public TextBookType BookType { get; private set; }
        public string Author { get; private set; }
        public string Name { get; private set; }
        public int Edition { get; private set; }
        public string Press { get; private set; }
        public bool IsBest { get; private set; }

        [NonSerialized]
        internal readonly TextBlock NameViewBlock;
        [NonSerialized]
        internal readonly TextBlock AuthorViewBlock;
        [NonSerialized]
        internal readonly TextBlock BookTypeViewBlock;
        [NonSerialized]
        internal readonly CheckBox IsBestViewBox;

        protected EBookItem(SerializationInfo info, StreamingContext context)
        {
            GetView = new ListViewItem() { HorizontalContentAlignment = HorizontalAlignment.Stretch };
            BookType = (TextBookType)Enum.Parse(typeof(TextBookType), info.GetString("BookType"));
            Author = info.GetString("Author");
            Name = info.GetString("Name");
            Edition = info.GetInt32("Edition");
            Press = info.GetString("Press");
            IsBest = info.GetBoolean("IsBest");

            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 2), (typeof(string), 2), (typeof(string), 1), (typeof(bool), 1));

            NameViewBlock = controls[0] as TextBlock;
            AuthorViewBlock = controls[1] as TextBlock;
            BookTypeViewBlock = controls[2] as TextBlock;
            IsBestViewBox = controls[3] as CheckBox;

            NameViewBlock.Text = Name;
            AuthorViewBlock.Text = Author;
            BookTypeViewBlock.Text = BookType.ToString();
            IsBestViewBox.IsChecked = IsBest;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("BookType", BookType.ToString());
            info.AddValue("Author", Author);
            info.AddValue("Name", Name);
            info.AddValue("Edition", Edition);
            info.AddValue("Press", Press);
            info.AddValue("IsBest", IsBest);
        }

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