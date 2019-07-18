﻿using ConsoleAppEngine.Abstracts;
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
    public partial class ETeachers : ISerializable
    {
        #region DisplayBoxes

        private TextBox NameBox;
        private TextBox Phone1Box, Phone2Box;
        private TextBox Email1Box, Email2Box;
        private TextBox AddressBox;
        private TextBox WebsiteBox;
        private TextBox OtherBox;

        #endregion

        private AllCourses allCourses => AllCourses.Instance;

        public void AddTeacher(ETeacherEntry t)
        {
            lists.AddLast(t);
            UpdateList();
        }

        public override void PostDeleteTasks()
        {
            foreach (CourseEntry s in allCourses.CoursesList)
            {
                s.TeacherEntry.lists.Remove(ItemToChange);
                s.SyncTimeTablewithTeachers();
            }
        }

        #region Serialization

        public ETeachers() : base()
        {

        }

        protected ETeachers(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion
    }

    public partial class ETeachers : EElementBase<ETeacherEntry>
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
            Email1Box = Email2Box =
            AddressBox =
            WebsiteBox =
            OtherBox = null;

            foreach (var x in lists)
                x.DestructViews();
        }

        protected override void AddNewItem()
        {
            AddTeacher(new ETeacherEntry(
                NameBox.Text,
                new string[] { Phone1Box.Text, Phone2Box.Text },
                new string[] { Email1Box.Text, Email2Box.Text },
                AddressBox.Text,
                WebsiteBox.Text,
                OtherBox.Text));
        }

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
            foreach (var x in (from a in lists where a != ItemToChange select a.Name))
            {
                if (NameBox.Text == x)
                {
                    Controls.AddLast(NameBox);
                    ErrorWaale.AddLast(NameBox);
                    break;
                }
            }
        }

        protected override void ClearAddGrid()
        {
            ItemToChange = null;
            AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

            NameBox.Text =
            Phone1Box.Text = Phone2Box.Text =
            Email1Box.Text = Email2Box.Text =
            AddressBox.Text =
            WebsiteBox.Text =
            OtherBox.Text = "";
        }

        protected override Grid Header()
        {
            return GenerateHeader(("Name", 1), ("Website", 2));
        }

        protected override void InitializeAddGrid(params FrameworkElement[] AddViewGridControls)
        {
            NameBox = AddViewGridControls[0] as TextBox;
            AddressBox = AddViewGridControls[1] as TextBox;
            Phone1Box = AddViewGridControls[2] as TextBox;
            Phone2Box = AddViewGridControls[3] as TextBox;
            Email1Box = AddViewGridControls[4] as TextBox;
            Email2Box = AddViewGridControls[5] as TextBox;
            WebsiteBox = AddViewGridControls[6] as TextBox;
            OtherBox = AddViewGridControls[7] as TextBox;
            AddButton = AddViewGridControls[8] as Button;

            foreach (var x in lists)
                x.InitializeViews();
        }

        protected override void ItemToChangeUpdate()
        {
            ItemToChange.UpdateDataWithViews(
                NameBox.Text,
                new string[] { Phone1Box.Text, Phone2Box.Text },
                new string[] { Email1Box.Text, Email2Box.Text },
                AddressBox.Text,
                WebsiteBox.Text,
                OtherBox.Text);
        }

        protected override IOrderedEnumerable<ETeacherEntry> OrderList()
        {
            return lists.OrderBy(a => a.Name);
        }

        protected override void SetAddGrid_ItemToChange()
        {
            NameBox.Text = ItemToChange.Name;
            Phone1Box.Text = ItemToChange.Phone[0];
            Phone2Box.Text = ItemToChange.Phone[1];
            Email1Box.Text = ItemToChange.Email[0];
            Email2Box.Text = ItemToChange.Email[1];
            AddressBox.Text = ItemToChange.Address;
            WebsiteBox.Text = ItemToChange.Website;
            OtherBox.Text = ItemToChange.OtherInfo;
        }

        protected override void SetContentDialog()
        {
            contentDialog.Title = ItemToChange.Name;
            contentDialog.Content =
                new TextBlock()
                {
                    Text = string.Format(
                        "{0}\n\n" +
                        "Phone   \t:\t{1}, {2}\n" +
                        "Email   \t:\t{3}, {4}\n" +
                        "Website \t:\t{5}\n" +
                        "Other Info :\t{6}",
                        ItemToChange.Address,
                        ItemToChange.Phone[0],
                        ItemToChange.Phone[1],
                        ItemToChange.Email[0],
                        ItemToChange.Email[1],
                        ItemToChange.Website,
                        ItemToChange.OtherInfo),
                    IsTextSelectionEnabled = true
                };
        }
    }
}
