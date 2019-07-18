using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Contacts;
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
    public partial class ETimeTable : ISerializable
    {
        #region DisplayBoxes

        private ComboBox EntryTypeBox;
        private TextBox SectionBox;
        private readonly ComboBox[] TeachersBox = new ComboBox[3];
        private TextBox RoomBox;
        private TextBox DaysBox;
        private TextBox HoursBox;

        #endregion

        public ETeachers EquivalentTeacherEntry;

        public void SetTeachersEntry(ETeachers t)
        {
            EquivalentTeacherEntry = t;
        }

        private LinkedList<ETeacherEntry> GenerateTeacherFromAddGrid()
        {
            LinkedList<ETeacherEntry> eTeachers = new LinkedList<ETeacherEntry>();

            foreach (var x in TeachersBox)
            {
                if (x.SelectedIndex == 0)
                {
                    continue;
                }

                foreach (var teacher in EquivalentTeacherEntry.lists)
                {
                    if (teacher.Name == x.SelectedItem as string)
                    {
                        eTeachers.AddLast(teacher);
                    }
                }
            }

            eTeachers = new LinkedList<ETeacherEntry>(eTeachers.OrderBy(a => a.Name));

            return eTeachers;
        }

        public void AddTimeEntry(ETimeTableItem eTimeTableItem)
        {
            lists.AddLast(eTimeTableItem);
            UpdateList();
        }

        #region Serialization

        public ETimeTable() : base()
        {

        }

        protected ETimeTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion
    }

    public partial class ETimeTable : EElementBase<ETimeTableItem>
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

            EntryTypeBox = null;
            SectionBox = null;
            TeachersBox[0] = null;
            TeachersBox[1] = null;
            TeachersBox[2] = null;
            RoomBox = null;
            DaysBox = null;
            HoursBox = null;
        }

        protected override void AddNewItem()
        {
            AddTimeEntry(new ETimeTableItem(
                (TimeTableEntryType)Enum.Parse(typeof(TimeTableEntryType), EntryTypeBox.SelectedItem.ToString().Replace(" ", "")),
                uint.Parse(SectionBox.Text),
                GenerateTeacherFromAddGrid(),
                RoomBox.Text,
                ETimeTableItem.GetDaysList(DaysBox.Text),
                Array.ConvertAll(HoursBox.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries), uint.Parse).Distinct().ToArray()));
        }

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
            const int MaxHour = 12;
            Controls.AddLast(EntryTypeBox);
            Controls.AddLast(SectionBox);
            Controls.AddLast(TeachersBox[0]);
            Controls.AddLast(DaysBox);
            Controls.AddLast(HoursBox);


            // Input Format Check
            if (EntryTypeBox.SelectedItem == null)
            {
                ErrorWaale.AddLast(EntryTypeBox);
            }

            if (!uint.TryParse(SectionBox.Text, out uint section))
            {
                ErrorWaale.AddLast(SectionBox);
            }

            if (TeachersBox[0].SelectedIndex == 0 && TeachersBox[1].SelectedIndex == 0 && TeachersBox[2].SelectedIndex == 0)
            {
                ErrorWaale.AddLast(TeachersBox[0]);
            }

            var arr1 = ETimeTableItem.GetDaysList(DaysBox.Text);
            var arr2 = Array.ConvertAll(HoursBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse).Distinct().ToArray();
            if (arr1.Count == 0)
            {
                ErrorWaale.AddLast(DaysBox);
            }

            if (arr2.Length == 0)
            {
                ErrorWaale.AddLast(HoursBox);
            }

            foreach (string x in DaysBox.Text.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (x != "M" && x != "T" && x != "W" && x != "TH" && x != "F" && x != "S")
                {
                    ErrorWaale.AddLast(DaysBox);
                    break;
                }
            }
            foreach (string x in HoursBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (!int.TryParse(x, out int a) || a <= 0 || a > MaxHour)
                {
                    ErrorWaale.AddLast(HoursBox);
                    break;
                }
            }

            if (ErrorWaale.Count != 0)
            {
                return;
            }


            // Cross Check
            var typeEntered = (TimeTableEntryType)Enum.Parse(typeof(TimeTableEntryType), EntryTypeBox.SelectedItem.ToString().Replace(" ", ""));
            var timingArray = from days in arr1
                              from hours in arr2
                              select new { days, hours };
            foreach (var entry in (from a in lists where a != ItemToChange select a))
            {
                if (entry.EntryType == typeEntered)
                {
                    ErrorWaale.AddLast(EntryTypeBox);
                }

                if (entry.Section == section && entry.EntryType == typeEntered)
                {
                    ErrorWaale.AddLast(SectionBox);
                }

                var curtimeArray = from days in entry.WeekDays
                                   from hours in entry.Hours
                                   select new { days, hours };
                foreach (var x in timingArray)
                {
                    foreach (var y in curtimeArray)
                    {
                        if (x.days == y.days && x.hours == y.hours)
                        {
                            ErrorWaale.AddLast(DaysBox);
                            ErrorWaale.AddLast(HoursBox);
                        }
                    }
                }
            }
        }

        protected override void ClearAddGrid()
        {
            ItemToChange = null;
            AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

            EntryTypeBox.SelectedItem = null;
            TeachersBox[0].SelectedIndex = 0;
            TeachersBox[1].SelectedIndex = 0;
            TeachersBox[2].SelectedIndex = 0;
            SectionBox.Text =
            RoomBox.Text =
            DaysBox.Text =
            HoursBox.Text = "";
        }

        protected override Grid Header()
        {
            return GenerateHeader(("Type", 1), ("Teacher Name", 1), ("Days", 1), ("Hour", 1));
        }

        protected override void InitializeAddGrid(params FrameworkElement[] AddViewGridControls)
        {
            EntryTypeBox = AddViewGridControls[0] as ComboBox;
            SectionBox = AddViewGridControls[1] as TextBox;
            TeachersBox[0] = AddViewGridControls[2] as ComboBox;
            TeachersBox[1] = AddViewGridControls[3] as ComboBox;
            TeachersBox[2] = AddViewGridControls[4] as ComboBox;
            RoomBox = AddViewGridControls[5] as TextBox;
            DaysBox = AddViewGridControls[6] as TextBox;
            HoursBox = AddViewGridControls[7] as TextBox;
            AddButton = AddViewGridControls[8] as Button;
        }

        protected override void ItemToChangeUpdate()
        {
            ItemToChange.UpdateData((TimeTableEntryType)Enum.Parse(typeof(TimeTableEntryType), EntryTypeBox.SelectedItem.ToString().Replace(" ", "")),
                uint.Parse(SectionBox.Text),
                GenerateTeacherFromAddGrid(),
                RoomBox.Text,
                ETimeTableItem.GetDaysList(DaysBox.Text),
                Array.ConvertAll(HoursBox.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries), uint.Parse).Distinct().ToArray());
        }

        protected override IOrderedEnumerable<ETimeTableItem> OrderList()
        {
            return lists.OrderBy(a => a.EntryType);
        }

        protected override void SetAddGrid_ItemToChange()
        {
            EntryTypeBox.SelectedIndex = (int)ItemToChange.EntryType;
            SectionBox.Text = ItemToChange.Section.ToString();

            foreach (var y in TeachersBox)
            {
                y.SelectedIndex = 0;
            }

            var arr = ItemToChange.Teachers.ToArray();

            for (int i = 0; i < arr.Length; ++i)
            {
                TeachersBox[i].SelectedItem = arr[i].Name;
            }

            RoomBox.Text = ItemToChange.Room;
            DaysBox.Text = ETimeTableItem.GetDayListString(ItemToChange.WeekDays);
            HoursBox.Text = string.Join(' ', ItemToChange.Hours);
        }

        protected override void SetContentDialog()
        {
            contentDialog.Title = ItemToChange.EntryType.ToString();

            contentDialog.Content = string.Format(
                "Section\t: {0}\n" +
                "Teacher\t: {1}\n" +
                "Room\t: {2}\n" +
                "Timing\t: {3}",
                ItemToChange.Section,
                string.Join(", ", (from a in ItemToChange.Teachers where a != null select a.Name).ToArray()),
                ItemToChange.Room,
                ETimeTableItem.GetDayListString(ItemToChange.WeekDays) + "\t" + ItemToChange.HourViewBlock.Text);
        }
    }
}
