using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ConsoleAppEngine.Course.Abstracts;
using ConsoleAppEngine.AllEnums;

namespace ConsoleAppEngine.Course
{
    public class ETimeTableItem : ECourseElemItemBase
    {
        public TimeTableEntryType EntryType { get; private set; }
        public uint Section { get; private set; }
        public ETeacherEntry Teacher { get; private set; }
        public string Room { get; private set; }
        public DayOfWeek[] WeekDays { get; private set; }
        public int[] Hours { get; private set; }


        internal readonly TextBlock TypeViewBlock;
        internal readonly TextBlock TeacherViewBlock;
        internal readonly TextBlock TimingViewBlock;

        public ETimeTableItem(TimeTableEntryType entryType, uint section, ETeacherEntry teacher, string room, DayOfWeek[] weekDays, int[] hours)
        {
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 2));
            TypeViewBlock = controls[0] as TextBlock;
            TeacherViewBlock = controls[1] as TextBlock;
            TimingViewBlock = controls[2] as TextBlock;

            Update(entryType, section, teacher, room, weekDays, hours);
        }

        internal void Update(TimeTableEntryType entryType, uint section, ETeacherEntry teacher, string room, DayOfWeek[] weekDays, int[] hours)
        {
            EntryType = entryType;
            Section = section;
            Teacher = teacher;
            Room = room;
            WeekDays = weekDays;
            Hours = hours;

            TypeViewBlock.Text = EntryType.ToString();
            TeacherViewBlock.Text = teacher.Name;

            string output = "";
            foreach (DayOfWeek w in WeekDays)
            {
                switch (w)
                {
                    case DayOfWeek.Monday:
                        output += "M ";
                        break;
                    case DayOfWeek.Tuesday:
                        output += "T ";
                        break;
                    case DayOfWeek.Wednesday:
                        output += "W ";
                        break;
                    case DayOfWeek.Thursday:
                        output += "Th ";
                        break;
                    case DayOfWeek.Friday:
                        output += "F ";
                        break;
                    case DayOfWeek.Saturday:
                        output += "S ";
                        break;
                }
            }
            output += " ";
            foreach (int hrs in Hours)
                output += hrs.ToString() + " ";

            output = output.Substring(0, output.Length - 1);

            TimingViewBlock.Text = output;
        }

        internal override object PointerOverObject => null;
    }
}
