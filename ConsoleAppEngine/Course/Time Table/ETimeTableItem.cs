using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    public class ETimeTableItem : EElementItemBase
    {
        public TimeTableEntryType EntryType { get; private set; }
        public uint Section { get; private set; }
        public ETeacherEntry[] Teacher { get; private set; } = new ETeacherEntry[3];
        public string Room { get; private set; }
        public LinkedList<DayOfWeek> WeekDays { get; private set; } = new LinkedList<DayOfWeek>();
        public uint[] Hours { get; private set; }

        internal readonly TextBlock TypeViewBlock;
        internal readonly TextBlock TeacherViewBlock;
        internal readonly TextBlock DaysViewBlock;
        internal readonly TextBlock HourViewBlock;

        public ETimeTableItem(TimeTableEntryType entryType, uint section, LinkedList<ETeacherEntry> teacher, string room, LinkedList<DayOfWeek> weekDays, uint[] hours)
        {
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));
            TypeViewBlock = controls[0] as TextBlock;
            TeacherViewBlock = controls[1] as TextBlock;
            DaysViewBlock = controls[2] as TextBlock;
            HourViewBlock = controls[3] as TextBlock;

            Update(entryType, section, teacher, room, weekDays, hours);
        }

        internal void Update(TimeTableEntryType entryType, uint section, LinkedList<ETeacherEntry> teacher, string room, LinkedList<DayOfWeek> weekDays, uint[] hours)
        {
            EntryType = entryType;
            Section = section;
            switch (teacher.Count)
            {
                case 0:
                    break;
                case 1:
                    Teacher[0] = teacher.First.Value;
                    break;
                case 2:
                    Teacher[0] = teacher.First.Value;
                    Teacher[1] = teacher.First.Next.Value;
                    break;
                default:
                    Teacher[0] = teacher.First.Value;
                    Teacher[1] = teacher.First.Next.Value;
                    Teacher[2] = teacher.First.Next.Next.Value;
                    break;
            }
            Room = room;

            WeekDays.Clear();
            DayOfWeek[] r = weekDays.ToArray();
            Array.Sort(r);
            foreach (var x in r.Distinct())
            {
                WeekDays.AddLast(x);
            }

            Array.Sort(hours);
            Hours = hours;

            TypeViewBlock.Text = EntryType.ToString();
            TeacherViewBlock.Text = string.Join(", ", (from a in Teacher where a != null select a.Name).ToArray());
            DaysViewBlock.Text = GetDayListString(WeekDays);
            HourViewBlock.Text = string.Join(" ", Array.ConvertAll(Hours, (x) => x.ToString()));
        }

        internal void DeleteTeacher(ETeacherEntry eTeacher)
        {
            for (int i = 0; i < Teacher.Length; ++i)
            {
                if (eTeacher == Teacher[i])
                {
                    Teacher[i] = null;
                    TeacherViewBlock.Text = string.Join(", ", (from a in Teacher where a != null select a.Name).ToArray());
                }
            }
        }

        internal override object PointerOverObject => null;

        internal static LinkedList<DayOfWeek> GetDaysList(string x)
        {
            LinkedList<DayOfWeek> list = new LinkedList<DayOfWeek>();
            foreach (string a in x.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries).Distinct())
            {
                switch (a)
                {
                    case "M":
                        list.AddLast(DayOfWeek.Monday);
                        break;
                    case "T":
                        list.AddLast(DayOfWeek.Tuesday);
                        break;
                    case "W":
                        list.AddLast(DayOfWeek.Wednesday);
                        break;
                    case "TH":
                        list.AddLast(DayOfWeek.Thursday);
                        break;
                    case "F":
                        list.AddLast(DayOfWeek.Friday);
                        break;
                    case "S":
                        list.AddLast(DayOfWeek.Saturday);
                        break;
                }
            }
            return list;
        }

        internal static string GetDayListString(LinkedList<DayOfWeek> input)
        {
            if (input.Count == 0)
            {
                return "";
            }

            string output = "";
            foreach (DayOfWeek w in input)
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

            output = output.Substring(0, output.Length - 1);
            return output;
        }
    }
}
