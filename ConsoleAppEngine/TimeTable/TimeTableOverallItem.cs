using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.AllEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.TimeTable
{
    internal class TimeTableOverallItem : EElementItemBase
    {
        internal readonly TextBlock CourseViewBlock;
        internal readonly TextBlock TypeViewBlock;
        internal readonly TextBlock TeacherViewBlock;
        internal readonly TextBlock DaysViewBlock;
        internal readonly TextBlock HourViewBlock;

        internal readonly ETimeTableItem eTimeTableItem;
        internal readonly CourseEntry courseEntry;

        public TimeTableOverallItem(CourseEntry entry, ETimeTableItem timeTableItem)
        {
            courseEntry = entry;
            eTimeTableItem = timeTableItem;

            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));
            CourseViewBlock = controls[0] as TextBlock;
            TypeViewBlock = controls[1] as TextBlock;
            TeacherViewBlock = controls[2] as TextBlock;
            DaysViewBlock = controls[3] as TextBlock;
            HourViewBlock = controls[4] as TextBlock;

            CourseViewBlock.Text = courseEntry.Title;
            TypeViewBlock.Text = eTimeTableItem.EntryType.ToString();
            TeacherViewBlock.Text = string.Join(", ", (from a in eTimeTableItem.Teachers where a != null select a.Name).ToArray());
            DaysViewBlock.Text = ETimeTableItem.GetDayListString(eTimeTableItem.WeekDays);
            HourViewBlock.Text = string.Join(" ", Array.ConvertAll(eTimeTableItem.Hours, (x) => x.ToString()));
        }

    }
}
