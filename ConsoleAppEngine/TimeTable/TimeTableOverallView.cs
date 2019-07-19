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
            foreach (var x in AllCourses.Instance.CoursesList)
            {
                foreach (var y in x.TimeEntry.lists)
                {
                    foreach (var z in y.WeekDays)
                    {
                        bool[] arr = new bool[20];
                        foreach (var temp in y.Hours)
                        {
                            arr[temp - 1] = true;
                        }

                        LinkedList<int> startindexes = new LinkedList<int>();
                        if (arr[0] == true)
                        {
                            startindexes.AddLast(0);
                        }

                        for (int i = 1; i < arr.Length - 1; ++i)
                        {
                            if (arr[i] == false && arr[i + 1] == true)
                            {
                                startindexes.AddLast(i + 1);
                            }
                        }

                        var finl = startindexes.ToArray();
                        for (int i = 0; i < finl.Length; ++i)
                        {
                            int Len = 0;
                            for (int j = i; j < arr.Length; ++j)
                            {
                                if (arr[j] == false)
                                {
                                    break;
                                }
                                else
                                {
                                    ++Len;
                                }
                            }

                            InitialList.AddLast((x.Title,
                                y.EntryType,
                                y.Teachers,
                                z,
                                (uint)finl[i],
                                (uint)Len));
                        }
                    }
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
