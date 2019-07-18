using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public class ETimeTableItem : EElementItemBase, ISerializable
    {
        #region Properties

        public TimeTableEntryType EntryType { get; private set; }
        public uint Section { get; private set; }
        public LinkedList<ETeacherEntry> Teachers { get; private set; }
        public string Room { get; private set; }
        public LinkedList<DayOfWeek> WeekDays { get; private set; }
        public uint[] Hours { get; private set; }

        #endregion

        #region DisplayItems

        internal TextBlock TypeViewBlock { get; private set; }
        internal TextBlock TeacherViewBlock { get; private set; }
        internal TextBlock DaysViewBlock { get; private set; }
        internal TextBlock HourViewBlock { get; private set; }

        #endregion

        #region Serialization

        protected ETimeTableItem(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            EntryType = (TimeTableEntryType)info.GetValue(nameof(EntryType), typeof(TimeTableEntryType));
            Section = (uint)info.GetValue(nameof(Section), typeof(uint));
            Teachers = info.GetValue(nameof(Teachers), typeof(LinkedList<ETeacherEntry>)) as LinkedList<ETeacherEntry>;
            Room = info.GetValue(nameof(Room), typeof(string)) as string;
            WeekDays = (info.GetValue(nameof(WeekDays), typeof(LinkedList<DayOfWeek>))) as LinkedList<DayOfWeek>;
            Hours = (info.GetValue(nameof(Hours), typeof(List<uint>)) as List<uint>).ToArray();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(EntryType), EntryType, typeof(TimeTableEntryType));
            info.AddValue(nameof(Section), Section, typeof(uint));
            info.AddValue(nameof(Teachers), Teachers, typeof(LinkedList<ETeacherEntry>));
            info.AddValue(nameof(Room), Room, typeof(string));
            info.AddValue(nameof(WeekDays), WeekDays, typeof(LinkedList<DayOfWeek>));
            info.AddValue(nameof(Hours), Hours.ToList(), typeof(List<uint>));
        }

        #endregion

        public ETimeTableItem(TimeTableEntryType entryType, uint section, LinkedList<ETeacherEntry> teacher, string room, LinkedList<DayOfWeek> weekDays, uint[] hours)
        {
            UpdateData(entryType, section, teacher, room, weekDays, hours);
        }

        internal void UpdateData(TimeTableEntryType entryType, uint section, LinkedList<ETeacherEntry> teacher, string room, LinkedList<DayOfWeek> weekDays, uint[] hours)
        {
            EntryType = entryType;
            Section = section;
            Teachers = teacher;
            Room = room;
            WeekDays = new LinkedList<DayOfWeek>(weekDays.OrderBy(a => a)) as LinkedList<DayOfWeek>;

            Array.Sort(hours);
            Hours = hours;
        }

        internal void UpdateDataWithViews(TimeTableEntryType entryType, uint section, LinkedList<ETeacherEntry> teacher, string room, LinkedList<DayOfWeek> weekDays, uint[] hours)
        {
            UpdateData(entryType, section, teacher, room, weekDays, hours);
            UpdateViews();
        }

        internal override void InitializeViews()
        {
            FrameworkElement[] controls = GenerateViews(ref GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));

            TypeViewBlock = controls[0] as TextBlock;
            TeacherViewBlock = controls[1] as TextBlock;
            DaysViewBlock = controls[2] as TextBlock;
            HourViewBlock = controls[3] as TextBlock;

            UpdateViews();
        }

        internal override void UpdateViews()
        {
            TypeViewBlock.Text = EntryType.ToString();
            TeacherViewBlock.Text = string.Join(", ", (from a in Teachers where a != null select a.Name).ToArray());
            DaysViewBlock.Text = GetDayListString(WeekDays);
            HourViewBlock.Text = string.Join(" ", Array.ConvertAll(Hours, x => x.ToString()));
        }

        internal override void DestructViews()
        {
            base.DestructViews();

            TypeViewBlock = null;
            TeacherViewBlock = null;
            DaysViewBlock = null;
            HourViewBlock = null;
        }

        #region Helpers

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
            LinkedList<string> list = new LinkedList<string>();

            foreach (DayOfWeek w in input)
            {
                switch (w)
                {
                    case DayOfWeek.Monday:
                        list.AddLast("M");
                        break;
                    case DayOfWeek.Tuesday:
                        list.AddLast("T");
                        break;
                    case DayOfWeek.Wednesday:
                        list.AddLast("W");
                        break;
                    case DayOfWeek.Thursday:
                        list.AddLast("Th");
                        break;
                    case DayOfWeek.Friday:
                        list.AddLast("F");
                        break;
                    case DayOfWeek.Saturday:
                        list.AddLast("S");
                        break;
                }
            }

            return string.Join(" ", list.Distinct().ToArray());
        }
        #endregion
    }
}