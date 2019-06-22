using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using ConsoleAppEngine;

namespace ConsoleAppEngine
{
    public class EHandoutItem
    {
        public int LectureNo { get; private set; }
        public string Topic { get; private set; }
        public bool DoneByMe { get; internal set; }
        public string Description { get; private set; }

        internal bool IsDeleted = false;
        internal readonly ListViewItem GetView = new ListViewItem();

        internal readonly TextBlock LectureViewBlock;
        internal readonly TextBlock TopicViewBlock;
        internal readonly CheckBox DoneViewBox;

        public EHandoutItem(int lectureNo, string topic, bool doneByMe, string description)
        {
            LectureNo = lectureNo;
            Topic = topic;
            DoneByMe = doneByMe;
            Description = description;

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });

            LectureViewBlock = new TextBlock()
            {
                Text = LectureNo.ToString(),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            TopicViewBlock = new TextBlock()
            {
                Text = Topic,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            DoneViewBox = new CheckBox()
            {
                IsChecked = DoneByMe,
                Content = "",
                HorizontalAlignment = HorizontalAlignment.Center,
                MinWidth = 32
            };

            Grid.SetColumn(LectureViewBlock, 0);
            Grid.SetColumn(TopicViewBlock, 1);
            Grid.SetColumn(DoneViewBox, 2);

            grid.Children.Add(LectureViewBlock);
            grid.Children.Add(TopicViewBlock);
            grid.Children.Add(DoneViewBox);

            GetView.Content = grid;
            GetView.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            DoneViewBox.Click += (object sender, RoutedEventArgs e) => DoneByMe = DoneViewBox.IsChecked == true ? true : false;
        }
        
        internal void Update(int lectureNo, string topic, bool doneByMe, string description)
        {
            LectureNo = lectureNo;
            Topic = topic;
            DoneByMe = doneByMe;
            Description = description;

            LectureViewBlock.Text = LectureNo.ToString();
            TopicViewBlock.Text = topic;
            DoneViewBox.IsChecked = DoneByMe;
        }
    }
}