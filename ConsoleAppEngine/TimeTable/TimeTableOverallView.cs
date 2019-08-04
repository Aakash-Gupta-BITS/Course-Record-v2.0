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
        public readonly LinkedList<(string Title, TimeTableEntryType Type, LinkedList<ETeacherEntry> Teachers, DayOfWeek WeekDay, uint Hour, uint hours, string Room)> InitialList =
            new LinkedList<(string Title, TimeTableEntryType Type, LinkedList<ETeacherEntry> Teachers, DayOfWeek WeekDay, uint Hour, uint hours, string Room)>();

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
                        (uint)count,
                        arr[i, j].Time.Room));

                    j += count;
                }
            }
        }
        
        /*
        public void Sort()
        {
            var list = InitialList.OrderBy(a => a.WeekDay).ThenBy(a => a.Hour).ToArray();
            InitialList.Clear();
            foreach (var x in list)
            {
                InitialList.AddLast(x);
            }
        }*/
    }
}