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
    public partial class EHandouts : ISerializable
    {
        #region DisplayBoxes

        private TextBox LectureBox;
        private TextBox TopicBox;
        private TextBox DescriptionBox;
        private CheckBox DoneByMeBox;

        #endregion

        #region Serialization

        public EHandouts() : base()
        {

        }

        protected EHandouts(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion
    }

    public partial class EHandouts : EElementBase<EHandoutItem>
    {
        public override void DestructViews()
        {
            base.DestructViews();

            LectureBox = null;
            TopicBox = null;
            DescriptionBox = null;
            DoneByMeBox = null;
        }

        protected override EHandoutItem AddNewItem()
        {
            return new EHandoutItem(
                int.Parse(LectureBox.Text),
                TopicBox.Text,
                DoneByMeBox.IsChecked == true,
                DescriptionBox.Text);
        }

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
            Controls.AddLast(LectureBox);

            if (!int.TryParse(LectureBox.Text, out int lecture) || lecture <= 0)
            {
                ErrorWaale.AddLast(LectureBox);
            }

            foreach (var x in (from a in lists where a != ItemToChange && a.IsDeleted == false select a.LectureNo))
            {
                if (x == lecture)
                {
                    ErrorWaale.AddLast(LectureBox);
                }
            }
        }

        protected override void ClearAddGrid()
        {
            base.ClearAddGrid();

            LectureBox.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));

            LectureBox.Text =
            TopicBox.Text =
            DescriptionBox.Text = "";
            DoneByMeBox.IsChecked = false;
        }

        protected override Grid Header()
        {
            return GenerateHeader(("Lecture No", 1), ("Topic", 3), ("Done By Me", 0.5));
        }

        protected override void InitializeViews(params FrameworkElement[] AddViewGridControls)
        {
            LectureBox = AddViewGridControls[0] as TextBox;
            TopicBox = AddViewGridControls[1] as TextBox;
            DescriptionBox = AddViewGridControls[2] as TextBox;
            DoneByMeBox = AddViewGridControls[3] as CheckBox;
            AddButton = AddViewGridControls[4] as Button;
        }

        protected override void ItemToChangeUpdate()
        {
            ItemToChange.UpdateDataWithViews(
                int.Parse(LectureBox.Text),
                TopicBox.Text,
                DoneByMeBox.IsChecked == true,
                DescriptionBox.Text);
        }

        protected override IOrderedEnumerable<EHandoutItem> OrderList()
        {
            return lists.OrderBy(a => a.LectureNo);
        }

        protected override void SetAddGrid_ItemToChange()
        {
            LectureBox.Text = ItemToChange.LectureNo.ToString();
            DescriptionBox.Text = ItemToChange.Description;
            TopicBox.Text = ItemToChange.Topic;
            DoneByMeBox.IsChecked = ItemToChange.DoneByMe;
        }

        protected override void SetContentDialog()
        {
            contentDialog.Title = ItemToChange.Topic;
            contentDialog.Content = ItemToChange.Description;
        }
    }
}