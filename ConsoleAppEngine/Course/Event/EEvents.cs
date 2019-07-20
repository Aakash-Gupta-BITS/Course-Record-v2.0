using ConsoleAppEngine.Abstracts;
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
    public partial class EEvents : EElementBase<EEventItem>, ISerializable
    {
        #region DisplayBoxes

        private TextBox TitleBox;
        private DatePicker DateBox;
        private TimePicker TimeBox;
        private TextBox LocationBox;
        private TextBox DescriptionBox;

        #endregion

        #region Serialization

        public EEvents() : base()
        {

        }

        protected EEvents(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion
    }

    public partial class EEvents : EElementBase<EEventItem>
    {
        public override void DestructViews()
        {
            base.DestructViews();

            TitleBox = null;
            LocationBox = null;
            DescriptionBox = null;
            DateBox = null;
            TimeBox = null;
        }

        protected override EEventItem AddNewItem()
        {
            return new EEventItem(
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

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
            Controls.AddLast(TitleBox);
            Controls.AddLast(LocationBox);

            if (TitleBox.Text == "")
            {
                ErrorWaale.AddLast(TitleBox);
            }

            if (LocationBox.Text == "")
            {
                ErrorWaale.AddLast(LocationBox);
            }
            foreach (var x in (from a in lists
                               where a != ItemToChange
                               select a))
            {
                if (LocationBox.Text == x.Location && DateBox.Date == x.Timing.Date && TimeBox.Time == x.Timing.TimeOfDay)
                {
                    ErrorWaale.AddLast(LocationBox);
                }
                if (TitleBox.Text == x.Title)
                {
                    ErrorWaale.AddLast(TitleBox);
                }
            }
        }

        protected override void ClearAddGrid()
        {
            base.ClearAddGrid();
            TitleBox.BorderBrush =
            LocationBox.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));

            TitleBox.Text = "";
            DateBox.SelectedDate = DateTime.Now;
            TimeBox.SelectedTime = DateTime.Now.TimeOfDay;
            LocationBox.Text =
            DescriptionBox.Text = "";
        }

        protected override Grid Header()
        {
            return GenerateHeader(("Title", 2), ("Timing", 1), ("Location", 1));
        }

        protected override void InitializeViews(params FrameworkElement[] AddViewGridControls)
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
            ItemToChange.UpdateDataWithViews(
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

        protected override IOrderedEnumerable<EEventItem> OrderList()
        {
            return lists.OrderBy(a => a.Timing);
        }

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