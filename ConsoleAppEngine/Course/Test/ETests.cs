using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public partial class ETests : EElementBase<ETestItem>, ISerializable
    {
        #region DisplayBoxes

        private DatePicker DateBox;
        private ComboBox TestTypeBox;
        private TextBox TestIndexBox;
        private TextBox MarksObtainedBox;
        private TextBox TotalMarksBox;
        private TextBox DescriptionBox;

        #endregion

        public void AddTest(ETestItem testItem)
        {
            lists.AddLast(testItem);
            UpdateList();
        }

        #region Serialization

        public ETests() : base()
        {

        }

        protected ETests(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion
    }

    public partial class ETests : EElementBase<ETestItem>
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

            DateBox = null;
            TestTypeBox = null;
            TestIndexBox = null;
            MarksObtainedBox = null;
            TotalMarksBox = null;
            DescriptionBox = null;
        }

        protected override void AddNewItem()
        {
            AddTest(new ETestItem(
                DateBox.SelectedDate.Value.DateTime,
                (TestType)Enum.Parse(typeof(TestType), (TestTypeBox.SelectedItem as string).Replace(' ', '_')),
                int.Parse(TestIndexBox.Text),
                float.Parse(MarksObtainedBox.Text),
                float.Parse(TotalMarksBox.Text),
                DescriptionBox.Text));
        }

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
            bool TypeExists(int _index)
            {
                foreach (var x in (from a in lists where a != ItemToChange select a))
                {
                    if (x.TypeOfTest.ToString() == TestTypeBox.SelectedItem.ToString() && x.TestIndex == _index)
                    {
                        return true;
                    }
                }

                return false;
            }

            if (!double.TryParse(MarksObtainedBox.Text, out double obtained))
            {
                ErrorWaale.AddLast(MarksObtainedBox);
            }

            if (!double.TryParse(TotalMarksBox.Text, out double totalmark))
            {
                ErrorWaale.AddLast(TotalMarksBox);
            }

            if (!int.TryParse(TestIndexBox.Text, out int index))
            {
                ErrorWaale.AddLast(TestIndexBox);
            }

            if (TestTypeBox.SelectedItem == null)
            {
                ErrorWaale.AddLast(TestTypeBox);
            }

            if (ErrorWaale.Count != 0)
            {
                return;
            }

            if (obtained < 0 || obtained > totalmark)
            {
                ErrorWaale.AddLast(MarksObtainedBox);
            }

            if (totalmark <= 0 || totalmark < obtained)
            {
                ErrorWaale.AddLast(TotalMarksBox);
            }

            if (TypeExists(index))
            {
                ErrorWaale.AddLast(TestTypeBox);
                ErrorWaale.AddLast(TestIndexBox);
            }
        }

        protected override void ClearAddGrid()
        {
            ItemToChange = null;
            AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

            TestIndexBox.Text =
            MarksObtainedBox.Text =
            TotalMarksBox.Text =
            DescriptionBox.Text = "";
            TestTypeBox.SelectedItem = null;
            DateBox.SelectedDate = DateTime.Now;
        }

        protected override Grid Header()
        {
            return GenerateHeader(("Test Name", 1), ("Date", 1), ("Description", 2), ("Marks", 1));
        }

        protected override void InitializeAddGrid(params FrameworkElement[] AddViewGridControls)
        {
            DateBox = AddViewGridControls[0] as DatePicker;
            TestTypeBox = AddViewGridControls[1] as ComboBox;
            TestIndexBox = AddViewGridControls[2] as TextBox;
            MarksObtainedBox = AddViewGridControls[3] as TextBox;
            TotalMarksBox = AddViewGridControls[4] as TextBox;
            DescriptionBox = AddViewGridControls[5] as TextBox;
            AddButton = AddViewGridControls[6] as Button;
        }

        protected override void ItemToChangeUpdate()
        {
            ItemToChange.UpdateData(DateBox.SelectedDate.Value.DateTime,
                (TestType)Enum.Parse(typeof(TestType), TestTypeBox.SelectedItem as string),
                int.Parse(TestIndexBox.Text),
                float.Parse(MarksObtainedBox.Text),
                float.Parse(TotalMarksBox.Text),
                DescriptionBox.Text);
        }

        protected override IOrderedEnumerable<ETestItem> OrderList()
        {
            return lists.OrderBy(a => a.DayOfTest);
        }

        protected override void SetAddGrid_ItemToChange()
        {
            TestIndexBox.Text = ItemToChange.TestIndex.ToString();
            MarksObtainedBox.Text = ItemToChange.MarksObtained.ToString();
            TotalMarksBox.Text = ItemToChange.TotalMarks.ToString();
            DescriptionBox.Text = ItemToChange.Description;
            TestTypeBox.SelectedIndex = (int)ItemToChange.TypeOfTest;
            DateBox.SelectedDate = ItemToChange.DayOfTest;
        }

        protected override void SetContentDialog()
        {
            contentDialog.Title = ItemToChange.TypeOfTest.ToString().Replace('_', ' ') + " " + ItemToChange.TestIndex;
            contentDialog.Content =
                string.Format("{0} was conducted on {1} and {2} marks were obtained from {3} marks",
                contentDialog.Title.ToString(),
                ItemToChange.DayOfTest.ToString("dd/mm/yyyy"),
                ItemToChange.MarksObtained,
                ItemToChange.TotalMarks);
        }
    }
}
