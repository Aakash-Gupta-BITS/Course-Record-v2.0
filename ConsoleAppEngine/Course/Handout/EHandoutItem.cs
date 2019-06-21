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
        public readonly int LectureNo;
        public readonly string Topic;
        public readonly bool DoneByMe;
        public readonly string Description;

        internal bool IsDeleted = false;
        public readonly ListViewItem GetView = new ListViewItem();

        CheckBox ViewCheckBox = null;
        TappedEventHandler lastTappedEventHandler;

        public EHandoutItem(int lectureNo, string topic, bool doneByMe, string description)
        {
            LectureNo = lectureNo;
            Topic = topic;
            DoneByMe = doneByMe;
            Description = description;

            GenerateView();
        }

        public static Grid Header()
        {
            Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });

            TextBlock LectureNo = new TextBlock()
            {
                Text = "Lecture No",
                HorizontalAlignment = HorizontalAlignment.Left,
                TextWrapping = TextWrapping.Wrap,
                FontWeight = FontWeights.Bold
            };
            TextBlock Topic = new TextBlock()
            {
                Text = "Topic",
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold
            };
            TextBlock DonebyMe = new TextBlock()
            {
                Text = "Done by Me",
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = FontWeights.Bold
            };

            Grid.SetColumn(LectureNo, 0);
            Grid.SetColumn(Topic, 1);
            Grid.SetColumn(DonebyMe, 2);

            grid.Children.Add(LectureNo);
            grid.Children.Add(Topic);
            grid.Children.Add(DonebyMe);

            return grid;
        }

        internal void GenerateTappedEvent(ListView l)
        {
            Action<object, TappedRoutedEventArgs> action = async (object sender, TappedRoutedEventArgs e) =>
            {
                if (ViewCheckBox.IsPointerOver)
                    return;

                ContentDialog contentDialog = new ContentDialog
                {
                    Title = Topic,
                    Content = Description,
                    PrimaryButtonText = "Modify",
                    SecondaryButtonText = "Delete",
                    CloseButtonText = "Ok"
                };
                l.SelectedItem = null;
                switch (await contentDialog.ShowAsync())
                {
                    // Delete
                    case ContentDialogResult.Secondary:
                        l.Items.Remove(GetView);
                        IsDeleted = true;
                        break;
                }
            };

            if (lastTappedEventHandler != null)
                GetView.Tapped -= lastTappedEventHandler;

            lastTappedEventHandler = new TappedEventHandler(action);
            GetView.Tapped += lastTappedEventHandler;

        }

        private void GenerateView()
        {
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });

            TextBlock lecture = new TextBlock()
            {
                Text = LectureNo.ToString(),
                HorizontalAlignment = HorizontalAlignment.Left
            };
            TextBlock topic = new TextBlock()
            {
                Text = Topic,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            CheckBox donebyme = new CheckBox()
            {
                IsChecked = DoneByMe,
                Content = "",
                HorizontalAlignment = HorizontalAlignment.Center,
                MinWidth = 32
            };

            Grid.SetColumn(lecture, 0);
            Grid.SetColumn(topic, 1);
            Grid.SetColumn(donebyme, 2);

            grid.Children.Add(lecture);
            grid.Children.Add(topic);
            grid.Children.Add(donebyme);

            GetView.Content = grid;
            GetView.HorizontalContentAlignment = HorizontalAlignment.Stretch;

            ViewCheckBox = donebyme;
        }

        public override string ToString() => Topic;
    }
}