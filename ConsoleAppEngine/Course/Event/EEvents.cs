using ConsoleAppEngine.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ConsoleAppEngine.Course
{
    public partial class EEvents
    {
        TextBox TitleBox;
        DatePicker DateBox;
        TimePicker TimeBox;
        TextBox LocationBox;
        TextBox DescriptionBox;

        public void AddEvent(EEventItem eventItem)
        {
            lists.AddLast(eventItem);
            UpdateList();
        }
    }

    public partial class EEvents : EElementBase<EEventItem>
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

            TitleBox =
            LocationBox =
            DescriptionBox = null;
            DateBox = null;
            TimeBox = null;
        }

        protected override void AddNewItem()
        {
            AddEvent(new EEventItem(
                TitleBox.Text,
                new DateTime(
                    DateBox.SelectedDate.Value.Year,
                    DateBox.SelectedDate.Value.Month,
                    DateBox.SelectedDate.Value.Day,
                    TimeBox.SelectedTime.Value.Hours,
                    TimeBox.SelectedTime.Value.Minutes,
                    TimeBox.SelectedTime.Value.Seconds),
                LocationBox.Text,
                DescriptionBox.Text));
        }

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
        }

        protected override void ClearAddGrid()
        {
            ItemToChange = null;
            AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

            TitleBox.Text = "";
            DateBox.SelectedDate = DateTime.Now;
            TimeBox.SelectedTime = DateTime.Now.TimeOfDay;
            LocationBox.Text =
            DescriptionBox.Text = "";
        }

        protected override Grid Header() => GenerateHeader(("Title", 2), ("Timing", 1), ("Location", 1));

        protected override void InitializeAddGrid(params FrameworkElement[] AddViewGridControls)
        {
            TitleBox = AddViewGridControls[0] as TextBox;
            DateBox = AddViewGridControls[1] as DatePicker;
            TimeBox = AddViewGridControls[2] as TimePicker;
            LocationBox = AddViewGridControls[3] as TextBox;
            DescriptionBox = AddViewGridControls[4] as TextBox;
            AddButton = AddViewGridControls[5] as Button;
        }

        protected override void ItemToChangeUpdate()
        {
            ItemToChange.Update(
                TitleBox.Text,
                new DateTime(
                    DateBox.SelectedDate.Value.Year,
                    DateBox.SelectedDate.Value.Month,
                    DateBox.SelectedDate.Value.Day,
                    TimeBox.SelectedTime.Value.Hours,
                    TimeBox.SelectedTime.Value.Minutes,
                    TimeBox.SelectedTime.Value.Seconds),
                LocationBox.Text,
                DescriptionBox.Text);
        }

        protected override IOrderedEnumerable<EEventItem> OrderList() => lists.OrderBy(a => a.Timing);

        protected override void SetAddGrid_ItemToChange()
        {
            TitleBox.Text = ItemToChange.Title;
            DateBox.SelectedDate = ItemToChange.Timing.Date;
            TimeBox.SelectedTime = ItemToChange.Timing.TimeOfDay;
            LocationBox.Text = ItemToChange.Location;
            DescriptionBox.Text = ItemToChange.Description;
        }

        protected override void SetContentDialog()
        {
            contentDialog.Title = ItemToChange.Title;
            contentDialog.Content = string.Format(
                "{0}\n\n" +
                "{1}\n" +
                "{2}",
                ItemToChange.Description,
                ItemToChange.Location,
                ItemToChange.TimingViewBlock.Text);
        }
    }
}