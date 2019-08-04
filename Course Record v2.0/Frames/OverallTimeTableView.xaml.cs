using ConsoleAppEngine.TimeTable;
using System.Collections.Generic;
using System;
using System.Linq;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Contacts;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0.Frames
{
    public sealed partial class OverallTimeTableView : Page
    {
        readonly bool[,] arr = new bool[6, 10];

        public OverallTimeTableView()
        {
            this.InitializeComponent();
        }

        public Button GetView((string Title, TimeTableEntryType Type, LinkedList<ETeacherEntry> Teachers, DayOfWeek WeekDay, uint Hour, uint hours, string Room) Entry)
        {
            StackPanel panel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Stretch };

            panel.Children.Add(new TextBlock
            {
                Text = Entry.Title,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.Wrap
            });

            panel.Children.Add(new TextBlock
            {
                Text = Entry.Type.ToString() + "\n" + Entry.Room,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextWrapping = TextWrapping.Wrap,
                HorizontalTextAlignment = TextAlignment.Center
            });

            Button button = new Button
            {
                BorderBrush = new SolidColorBrush(Colors.Gray),
                BorderThickness = new Thickness(1),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = new SolidColorBrush(Colors.Transparent),
                Content = panel,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };

            Grid.SetRow(button, (int)Entry.WeekDay + 1);
            Grid.SetColumn(button, (int)Entry.Hour);
            Grid.SetColumnSpan(button, (int)Entry.hours);

            for (int i  = 0; i < Entry.hours; ++i)
            {
                arr[(int)Entry.WeekDay - 1, (int)Entry.Hour + i - 1] = true;
            }

            Flyout flyout = new Flyout
            {
                OverlayInputPassThroughElement = button
            };
            button.Click += (sender, e) =>
            {
                flyout.ShowAt(sender as FrameworkElement);
            };

            flyout.Content = new TextBlock
            {
                Text = string.Format(
                    "{0} - {1}\n" +
                    "Teacher\t: {2}\n" +
                    "Room\t: {3}\n",
                    Entry.Title,
                    Entry.Type.ToString(),
                    string.Join(',', from t in Entry.Teachers select t.Name),
                    Entry.Room)
            };

            return button;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TimeTableOverallView view = new TimeTableOverallView();
            view.InitializeList();
            foreach (var x in view.InitialList)
                _View.Children.Add(GetView(x));

            for (int i = 2; i < 8; ++i)
            {
                for (int j = 1; j < 11; ++j)
                {
                    if (arr[i - 2, j - 1])
                        continue;

                    Button b = new Button
                    {
                        BorderBrush = new SolidColorBrush(Colors.Gray),
                        BorderThickness = new Thickness(1),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Background = new SolidColorBrush(Colors.Transparent)
                    };

                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);

                    _View.Children.Add(b);
                }
            }
        }
    }
}