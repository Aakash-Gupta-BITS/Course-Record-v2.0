using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.TimeTable
{
    public class TimeTableOverallView
    {
        internal LinkedList<(string Title, TimeTableEntryType Type, LinkedList<ETeacherEntry> Teachers, DayOfWeek WeekDay, uint Hour, uint hours)> InitialList =
            new LinkedList<(string Title, TimeTableEntryType Type, LinkedList<ETeacherEntry> Teachers, DayOfWeek WeekDay, uint Hour, uint hours)>();

        public LinkedList<ListViewItem> ViewItems = new LinkedList<ListViewItem>();

        public void InitializeList()
        {
            (CourseEntry Course, ETimeTableItem Time)[,] arr = new (CourseEntry, ETimeTableItem)[6, 10];

            foreach (var Course in AllCourses.Instance.CoursesList)
            {
                foreach (var TimeEntry in Course.TimeEntry.lists)
                {
                    foreach (var Timing in from Hour in TimeEntry.Hours
                                           from Day in TimeEntry.WeekDays
                                           select (Hour, Day))
                    {
                        arr[(int)Timing.Day - 1, Timing.Hour - 1] = (Course, TimeEntry);
                    }
                }
            }

            for (int i = 0; i < arr.GetLength(0); ++i)
            {
                for (int j = 0; j < arr.GetLength(1);)
                {
                    if (arr[i, j] == (null, null))
                    {
                        ++j;
                        continue;
                    }
                    int count = 1;
                    for (int k = j + 1; k < arr.GetLength(1); ++k)
                    {
                        if (arr[i, j].Course == arr[i, k].Course && arr[i, j].Time == arr[i, k].Time)
                        {
                            ++count;
                        }
                        else
                        {
                            break;
                        }
                    }

                    InitialList.AddLast((arr[i, j].Course.Title,
                        arr[i, j].Time.EntryType,
                        arr[i, j].Time.Teachers,
                        (DayOfWeek)(i + 1),
                        (uint)(j + 1),
                        (uint)count));

                    j += count;
                }
            }
        }

        public void Sort()
        {
            var list = InitialList.OrderBy(a => a.WeekDay).ThenBy(a => a.Hour).ToArray();
            InitialList.Clear();
            foreach (var x in list)
            {
                InitialList.AddLast(x);
            }
        }

        public void InitializeViews()
        {
            DayOfWeek currentday = DayOfWeek.Sunday;
            foreach (var x in InitialList)
            {
                if (currentday != x.WeekDay)
                {
                    ViewItems.AddLast(new NavigationViewItemSeparator());
                    ViewItems.AddLast(new ListViewItem { Content = new TextBlock { Text = x.WeekDay.ToString(), FontWeight = new FontWeight { Weight = 5 } } });
                }
                currentday = x.WeekDay;
                ListViewItem item = new ListViewItem() { HorizontalContentAlignment = HorizontalAlignment.Stretch };
                FrameworkElement[] controls = EElementItemBase.GenerateViews(ref item, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));
                (controls[0] as TextBlock).Text = x.Title;
                (controls[1] as TextBlock).Text = x.Type.ToString();
                (controls[2] as TextBlock).Text = string.Join(", ", (from a in x.Teachers where a != null select a.Name).ToArray());
                (controls[3] as TextBlock).Text = string.Format("{0} {1}:00 - {2}:50", x.WeekDay, 7 + x.Hour, 6 + x.Hour + x.hours);
                ViewItems.AddLast(item);
            }
            ViewItems.AddLast(new NavigationViewItemSeparator());
        }
    }
}