using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml.Media;
using System.Text;

namespace ConsoleAppEngine.Course
{
    public partial class EBooks
    {
        private TextBox NameBox;
        private TextBox AuthorBox;
        private TextBox EditionBox;
        private TextBox PressBox;
        private ComboBox BookTypeBox;
        private CheckBox BestBookBox;

        public void AddBook(EBookItem eBookItem)
        {
            lists.AddLast(eBookItem);
            UpdateList();
        }
    }

    public partial class EBooks : EElementBase<EBookItem>
    {
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

        protected async override void AddNewItem()
        {
            EBookItem x = new EBookItem(
                (TextBookType)Enum.Parse(typeof(TextBookType), BookTypeBox.SelectedItem as string),
                AuthorBox.Text,
                NameBox.Text,
                int.Parse(EditionBox.Text),
                PressBox.Text,
                BestBookBox.IsChecked == true);
            AddBook(x);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string output = "";
            using (MemoryStream m = new MemoryStream())
            {
                List<EBookItem> list = lists.ToList(); 
                binaryFormatter.Serialize(m, list);
                output = Encoding.ASCII.GetString(m.ToArray());
            }

            await FileIO.WriteTextAsync(await ApplicationData.Current.LocalFolder.CreateFileAsync(@"Database\Books.txt", CreationCollisionOption.OpenIfExists), output);
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

        protected override Grid Header()
        {
            return GenerateHeader(("Name", 2), ("Author", 2), ("Book Type", 1), ("Best Book", 1));
        }

        protected override void InitializeAddGrid(params FrameworkElement[] AddViewGridControls)
        {
            NameBox = AddViewGridControls[0] as TextBox;
            AuthorBox = AddViewGridControls[1] as TextBox;
            EditionBox = AddViewGridControls[2] as TextBox;
            PressBox = AddViewGridControls[3] as TextBox;
            BookTypeBox = AddViewGridControls[4] as ComboBox;
            BestBookBox = AddViewGridControls[5] as CheckBox;
            AddButton = AddViewGridControls[6] as Button;
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

        protected override IOrderedEnumerable<EBookItem> OrderList()
        {
            return lists.OrderBy(a => a.BookType).ThenBy(a => !a.IsBest).ThenBy(a => a.Name);
        }

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