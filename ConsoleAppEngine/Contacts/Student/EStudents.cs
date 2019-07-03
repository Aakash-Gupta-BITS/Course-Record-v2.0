using ConsoleAppEngine.Abstracts;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System;
using ConsoleAppEngine.AllEnums;

namespace ConsoleAppEngine.Course
{
    public partial class EStudents
    {
        private TextBox NameBox;
        private TextBox Phone1Box, Phone2Box;
        private TextBox IdBox;
        private TextBox PersonalEmailBox;
        private TextBox HostelBox;
        private TextBox RoomBox;
        private TextBox OtherInput;

        private AllCourses allCourses;

        public void SetAllCourses(AllCourses s) => allCourses = s;

        public override void PostDeleteTasks()
        {
            foreach (CourseEntry s in allCourses.CoursesList)
            {
                s.CTLog.lists.Remove(ItemToChange);
            }
        }

        public void AddStudent(EStudentEntry studentEntry)
        {
            lists.AddLast(studentEntry);
            UpdateList();
        }

        private void SetValidId(out (int year, ExpandedBranch[] branch, int digits) val)
        {
            var Id = IdBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int year;
            int digits;
            ExpandedBranch br1, br2;

            try
            {
                year = int.Parse(Id[0]);
                digits = int.Parse(Id[2]);
                br1 = (ExpandedBranch)(int)(BranchId)Enum.Parse(typeof(BranchId), Id[1].Substring(0, 2));
                br2 = (ExpandedBranch)(int)(BranchId)Enum.Parse(typeof(BranchId), Id[1].Substring(2));
                if (year < 2012 || year > 2019 || digits < 0 || digits > 2000)
                    throw new Exception();
            }
            catch
            {
                val = (0, null, 0);
                return;
            }

            val = (year, new ExpandedBranch[] { br1, br2 }, digits);
            return;
        }
    }

    public partial class EStudents : EElementBase<EStudentEntry>
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

            NameBox =
            Phone1Box = Phone2Box =
            IdBox =
            PersonalEmailBox =
            HostelBox =
            RoomBox =
            OtherInput = null;
        }

        protected override void AddNewItem()
        {
            SetValidId(out var x);
            int.TryParse(RoomBox.Text, out int room);
            AddStudent(new EStudentEntry(
                NameBox.Text,
                x,
                new string[] { Phone1Box.Text, Phone2Box.Text },
                PersonalEmailBox.Text,
                HostelBox.Text,
                room,
                OtherInput.Text));
        }

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
            Controls.AddLast(NameBox);
            Controls.AddLast(OtherInput);
            if (NameBox.Text == "")
                ErrorWaale.AddLast(NameBox);
            foreach (var x in (from a in lists
                               where a != ItemToChange
                               select a))
                if (NameBox.Text == x.Name && OtherInput.Text == x.OtherInfo)
                    ErrorWaale.AddLast(OtherInput);
        }

        protected override void ClearAddGrid()
        {
            ItemToChange = null;
            AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

            NameBox.Text =
            Phone1Box.Text =
            Phone2Box.Text =
            IdBox.Text =
            PersonalEmailBox.Text =
            HostelBox.Text =
            RoomBox.Text =
            OtherInput.Text = "";
        }

        protected override Grid Header() => GenerateHeader(("Name", 1), ("Phone", 1), ("Email", 1));

        protected override void InitializeAddGrid(params FrameworkElement[] AddViewGridControls)
        {
            NameBox = AddViewGridControls[0] as TextBox;
            Phone1Box = AddViewGridControls[1] as TextBox;
            Phone2Box = AddViewGridControls[2] as TextBox;
            IdBox = AddViewGridControls[3] as TextBox;
            PersonalEmailBox = AddViewGridControls[4] as TextBox;
            HostelBox = AddViewGridControls[5] as TextBox;
            RoomBox = AddViewGridControls[6] as TextBox;
            OtherInput = AddViewGridControls[7] as TextBox;
            AddButton = AddViewGridControls[8] as Button;
        }

        protected override void ItemToChangeUpdate()
        {
            SetValidId(out var x);
            ItemToChange.Update(
                NameBox.Text,
                x,
                new string[] { Phone1Box.Text, Phone2Box.Text },
                PersonalEmailBox.Text,
                HostelBox.Text,
                int.Parse(RoomBox.Text),
                OtherInput.Text);
        }

        protected override IOrderedEnumerable<EStudentEntry> OrderList() => lists.OrderBy(a => a.Name);

        protected override void SetAddGrid_ItemToChange()
        {
            NameBox.Text = ItemToChange.Name;
            Phone1Box.Text = ItemToChange.Phone[0];
            Phone2Box.Text = ItemToChange.Phone[1];
            if (ItemToChange.Year != 0)
                IdBox.Text = ItemToChange.Year + " " + ((BranchId)(int)ItemToChange.Branch[0]).ToString() + ((BranchId)(int)ItemToChange.Branch[1]).ToString() + " " + ItemToChange.Digits.ToString().PadRight(4, '0');
            else
                IdBox.Text = "";
            PersonalEmailBox.Text = ItemToChange.PersonalMail;
            HostelBox.Text = ItemToChange.Hostel;
            RoomBox.Text = ItemToChange.Room.ToString();
            OtherInput.Text = ItemToChange.OtherInfo;
        }

        protected override void SetContentDialog()
        {
            contentDialog.Title = ItemToChange.Name;

            contentDialog.Content =
                new TextBlock()
                {
                    Text = string.Format(
                        "{6}\n\n" +
                        "Contact\t\t:\t{0}\n" +
                        "Id\t\t:\t{1}\n" +
                        "BITS Email Id\t:\t{2}\n" +
                        "Personal Email Id\t:\t{3}\n" +
                        "Hostel\t\t:\t{4}\n" +
                        "Room No\t\t:\t{5}\n",
                        string.Join(", ", ItemToChange.Phone),
                        ItemToChange.Year == 0 ? "" : ItemToChange.Year + " " + ((BranchId)(int)ItemToChange.Branch[0]).ToString() + ((BranchId)(int)ItemToChange.Branch[1]).ToString() + " " + ItemToChange.Digits.ToString().PadLeft(4, '0'),
                        ItemToChange.Year == 0 ? "" : string.Format(@"f{0}{1}@pilani.bits-pilani.ac.in", ItemToChange.Year, ItemToChange.Digits.ToString().PadLeft(4, '0')),
                        ItemToChange.PersonalMail,
                        ItemToChange.Hostel,
                        ItemToChange.Room,
                        ItemToChange.OtherInfo),
                    IsTextSelectionEnabled = true
                };
        }
    }
}