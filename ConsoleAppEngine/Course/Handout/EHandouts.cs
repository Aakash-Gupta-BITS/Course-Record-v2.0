using ConsoleAppEngine.Abstracts;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ConsoleAppEngine.Course
{
    // Code Class Specific Code comes here
    public partial class EHandouts
    {
        private TextBox LectureBox;
        private TextBox TopicBox;
        private TextBox DescriptionBox;
        private CheckBox DoneByMeBox;

        public void AddHandout(EHandoutItem handoutItem)
        {
            lists.AddLast(handoutItem);
            UpdateList();
        }

    }

    // All Overrides come here
    public partial class EHandouts : EElementBase<EHandoutItem>
    {
        public override void DestructViews()
        {
            // Clear all views
            ViewGrid.Children.Clear();
            AddGrid.Children.Clear();
            ViewList.Items.Clear();

            // Dereference all views
            ViewGrid = null;
            AddGrid = null;
            ViewList = null;
            AddButton = null;
            ViewCommand = null;
            AddCommand = null;

            LectureBox = null;
            TopicBox = null;
            DescriptionBox = null;
            DoneByMeBox = null;
        }

        protected override void AddNewItem()
        {
            // Add new Item to lists
            AddHandout(new EHandoutItem(
                int.Parse(LectureBox.Text),
                TopicBox.Text,
                DoneByMeBox.IsChecked == true,
                DescriptionBox.Text));
        }

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
            Controls.AddLast(LectureBox);

            // Valid Lecture Check
            if (!int.TryParse(LectureBox.Text, out int lecture) || lecture <= 0)
            {
                ErrorWaale.AddLast(LectureBox);
            }

            // Lecture Repeatition check
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
            // This code is same for all childs.
            ItemToChange = null;
            LectureBox.BorderBrush =
            AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

            // This code is class specific.
            LectureBox.Text =
            TopicBox.Text =
            DescriptionBox.Text = "";
            DoneByMeBox.IsChecked = false;
        }

        protected override Grid Header()
        {
            // Lecture:Topic:Done :: 1:1:0.5
            return GenerateHeader(("Lecture No", 1), ("Topic", 3), ("Done By Me", 0.5));
        }

        protected override void InitializeAddGrid(params FrameworkElement[] AddViewGridControls)
        {
            // Mappings
            LectureBox = AddViewGridControls[0] as TextBox;
            TopicBox = AddViewGridControls[1] as TextBox;
            DescriptionBox = AddViewGridControls[2] as TextBox;
            DoneByMeBox = AddViewGridControls[3] as CheckBox;
            AddButton = AddViewGridControls[4] as Button;
        }

        protected override void ItemToChangeUpdate()
        {
            // Update the selected item
            ItemToChange.Update(int.Parse(LectureBox.Text), TopicBox.Text, DoneByMeBox.IsChecked == true, DescriptionBox.Text);
        }

        protected override IOrderedEnumerable<EHandoutItem> OrderList()
        {
            // Orderby LectureNo
            return lists.OrderBy(a => a.LectureNo);
        }

        protected override void SetAddGrid_ItemToChange()
        {
            // Filling AddGrid by ItemToChange
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