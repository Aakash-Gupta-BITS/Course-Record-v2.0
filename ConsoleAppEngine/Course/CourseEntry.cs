using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public class CourseEntry : EElementItemBase, ISerializable
    {
        #region Properties
        internal (CourseType branchtype, string branchstring) ID { get; private set; }
        public string Title { get; private set; }
        internal byte LectureUnits { get; private set; }
        internal byte PracticalUnits { get; private set; }
        public ETeacherEntry IC { get; private set; }
        #endregion

        #region DisplayItems

        public readonly NavigationViewItem navigationViewItem;
        public readonly TextBlock IdViewBlock;
        public readonly TextBlock TitleViewBlock;
        public readonly TextBlock ICViewBlock;

        #endregion

        #region Aggregates
        public readonly EBooks BookEntry = new EBooks();
        public readonly EStudents CTLog = new EStudents();
        public readonly EEvents EventEntry = new EEvents();
        public readonly EHandouts HandoutEntry = new EHandouts();
        public readonly ETeachers TeacherEntry = new ETeachers();
        public readonly ETests TestEntry = new ETests();
        public readonly ECourseTimeTable TimeEntry = new ECourseTimeTable();
        #endregion

        #region Serialization
        protected CourseEntry(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ID = ((CourseType)info.GetValue(nameof(ID.branchtype), typeof(CourseType)), (string)info.GetValue(nameof(ID.branchstring), typeof(string)));
            Title = info.GetString(nameof(Title));
            LectureUnits = info.GetByte(nameof(LectureUnits));
            PracticalUnits = info.GetByte(nameof(PracticalUnits));

            BookEntry = info.GetValue(nameof(BookEntry), typeof(EBooks)) as EBooks;
            CTLog = new EStudents();
            EventEntry = info.GetValue(nameof(EventEntry), typeof(EEvents)) as EEvents;
            HandoutEntry = info.GetValue(nameof(HandoutEntry), typeof(EHandouts)) as EHandouts;
            TestEntry = info.GetValue(nameof(TestEntry), typeof(ETests)) as ETests;
            TeacherEntry = info.GetValue(nameof(TeacherEntry), typeof(ETeachers)) as ETeachers;
            TimeEntry = info.GetValue(nameof(TimeEntry), typeof(ECourseTimeTable)) as ECourseTimeTable;
            IC = info.GetValue(nameof(IC), typeof(ETeacherEntry)) as ETeacherEntry;

            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));
            IdViewBlock = controls[0] as TextBlock;
            TitleViewBlock = controls[1] as TextBlock;
            ICViewBlock = controls[2] as TextBlock;

            navigationViewItem = new NavigationViewItem();
            navigationViewItem.Content = Title;

            IdViewBlock.Text = ID.branchtype.ToString() + " " + ID.branchstring;
            TitleViewBlock.Text = Title;
            ICViewBlock.Text = IC.Name;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(ID.branchtype), ID.branchtype, typeof(CourseType));
            info.AddValue(nameof(ID.branchstring), ID.branchstring);
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(LectureUnits), LectureUnits);
            info.AddValue(nameof(PracticalUnits), PracticalUnits);
            info.AddValue(nameof(IC), IC, typeof(ETeacherEntry));

            info.AddValue(nameof(BookEntry), BookEntry, typeof(EBooks));
            info.AddValue(nameof(EventEntry), EventEntry, typeof(EEvents));
            info.AddValue(nameof(HandoutEntry), HandoutEntry, typeof(EHandouts));
            info.AddValue(nameof(TestEntry), TestEntry, typeof(ETests));
            info.AddValue(nameof(TeacherEntry), TeacherEntry, typeof(ETeachers));
            info.AddValue(nameof(TimeEntry), TimeEntry, typeof(ECourseTimeTable));
        }
        #endregion

        public CourseEntry((CourseType branchtype, string branchstring) id, string title, byte lectureUnits, byte practicalUnits, ETeacherEntry iC)
        {
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));
            IdViewBlock = controls[0] as TextBlock;
            TitleViewBlock = controls[1] as TextBlock;
            ICViewBlock = controls[2] as TextBlock;

            navigationViewItem = new NavigationViewItem();

            Update(id, title, lectureUnits, practicalUnits, iC);
        }

        internal void Update((CourseType branchtype, string branchstring) id, string title, byte lectureUnits, byte practicalUnits, ETeacherEntry iC)
        {
            Title = title;
            ID = id;
            LectureUnits = lectureUnits;
            PracticalUnits = practicalUnits;
            IC = iC;

            navigationViewItem.Content = Title;

            IdViewBlock.Text = ID.branchtype.ToString() + " " + ID.branchstring;
            TitleViewBlock.Text = Title;
            ICViewBlock.Text = IC.Name;
        }

        public void SyncTimeTablewithTeachers()
        {
            LinkedList<ETimeTableItem> temp = new LinkedList<ETimeTableItem>();
            foreach (var x in TimeEntry.lists)
            {
                temp.AddLast(x);
                foreach (var y in x.Teachers)
                {
                    if (!TeacherEntry.lists.Contains(y))            // If teacher is not found
                    {
                        temp.Remove(x);
                        break;
                    }
                }
            }
            TimeEntry.lists.Clear();
            foreach (var x in temp)
            {
                TimeEntry.lists.AddLast(x);
            }
        }
    }
}