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
    public class ECourseTimeTable : ECourseElemBase<ETimeTableItem>
    {
        ComboBox EntryTypeBox;
        TextBox SectionBox;
        ComboBox TeachersBox;
        TextBox RoomBox;
        TextBox DaysBox;
        TextBox HoursBox;

        public void AddTimeEntry(ETimeTableItem eTimeTableItem)
        {
            lists.AddLast(eTimeTableItem);
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

            EntryTypeBox = null;
            SectionBox = null;
            TeachersBox = null;
            RoomBox = null;
            DaysBox = null;
            HoursBox = null;
        }

        protected override void AddNewItem()
        {

        }

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
            throw new NotImplementedException();
        }

        protected override void ClearAddGrid()
        {
            ItemToChange = null;
            AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

            EntryTypeBox.SelectedItem = 
            TeachersBox.SelectedItem = null;
            SectionBox.Text =
            RoomBox.Text =
            DaysBox.Text =
            HoursBox.Text = "";
        }

        protected override Grid Header() => GenerateHeader(("Type", 1), ("Teacher Name", 1), ("Timing", 2));

        protected override void InitializeAddGrid(params FrameworkElement[] AddViewGridControls)
        {
            EntryTypeBox = AddViewGridControls[0] as ComboBox;
            SectionBox = AddViewGridControls[1] as TextBox;
            TeachersBox = AddViewGridControls[2] as ComboBox;
            RoomBox = AddViewGridControls[3] as TextBox;
            DaysBox = AddViewGridControls[4] as TextBox;
            HoursBox = AddViewGridControls[5] as TextBox;
            AddButton = AddViewGridControls[6] as Button;
        }

        protected override void ItemToChangeUpdate()
        {
            throw new NotImplementedException();
        }

        protected override IOrderedEnumerable<ETimeTableItem> OrderList() => lists.OrderBy(a => a.EntryType);

        protected override void SetAddGrid_ItemToChange()
        {
            throw new NotImplementedException();
        }

        protected override void SetContentDialog()
        {
            contentDialog.Title = ItemToChange.EntryType;

            string content = string.Format(
                "Section\t: {0}\n" +
                "Teacher\t: {1}\n" +
                "Room\t: {2}\n" +
                "Timing\t:{3}",
                ItemToChange.Section,
                ItemToChange.Teacher.Name,
                ItemToChange.Room,
                ItemToChange.Hours);
        }
    }
}
