using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Contacts;
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

        public (CourseType branchtype, string branchstring) ID { get; private set; }
        public string Title { get; private set; }
        public byte LectureUnits { get; private set; }
        public byte PracticalUnits { get; private set; }
        public ETeacherEntry IC { get; private set; }
        
        #endregion

        #region DisplayItems

        public NavigationViewItem CourseNavigationItem { get; private set; }
        internal TextBlock IdViewBlock { get; private set; }
        internal TextBlock TitleViewBlock { get; private set; }
        internal TextBlock ICViewBlock { get; private set; }

        #endregion

        #region Aggregates

        public readonly EBooks BookEntry = new EBooks();
        public readonly EStudents CTLog = new EStudents();
        public readonly EEvents EventEntry = new EEvents();
        public readonly EHandouts HandoutEntry = new EHandouts();
        public readonly ETeachers TeacherEntry = new ETeachers();
        public readonly ETests TestEntry = new ETests();
        public readonly ETimeTable TimeEntry = new ETimeTable();

        #endregion

        #region Serialization

        protected CourseEntry(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ID = ((CourseType)info.GetValue(nameof(ID.branchtype), typeof(CourseType)), 
                  (string)info.GetValue(nameof(ID.branchstring), typeof(string)));
            Title = info.GetString(nameof(Title));
            LectureUnits = info.GetByte(nameof(LectureUnits));
            PracticalUnits = info.GetByte(nameof(PracticalUnits));
            IC = info.GetValue(nameof(IC), typeof(ETeacherEntry)) as ETeacherEntry;

            BookEntry = info.GetValue(nameof(BookEntry), typeof(EBooks)) as EBooks;
            CTLog = new EStudents();
            EventEntry = info.GetValue(nameof(EventEntry), typeof(EEvents)) as EEvents;
            HandoutEntry = info.GetValue(nameof(HandoutEntry), typeof(EHandouts)) as EHandouts;
            TestEntry = info.GetValue(nameof(TestEntry), typeof(ETests)) as ETests;
            TeacherEntry = info.GetValue(nameof(TeacherEntry), typeof(ETeachers)) as ETeachers;
            TimeEntry = info.GetValue(nameof(TimeEntry), typeof(ETimeTable)) as ETimeTable;
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
            info.AddValue(nameof(TimeEntry), TimeEntry, typeof(ETimeTable));
        }

        #endregion

        public CourseEntry((CourseType branchtype, string branchstring) id, string title, byte lectureUnits, byte practicalUnits, ETeacherEntry iC)
        {
            UpdateData(id, title, lectureUnits, practicalUnits, iC);
        }

        internal void UpdateData((CourseType branchtype, string branchstring) id, string title, byte lectureUnits, byte practicalUnits, ETeacherEntry iC)
        {
            Title = title;
            ID = id;
            LectureUnits = lectureUnits;
            PracticalUnits = practicalUnits;
            IC = iC;
        }

        internal void UpdateDataWithViews((CourseType branchtype, string branchstring) id, string title, byte lectureUnits, byte practicalUnits, ETeacherEntry iC)
        {
            UpdateData(id, title, lectureUnits, practicalUnits, iC);
            UpdateViews();
        }

        internal override void InitializeViews()
        {
            FrameworkElement[] controls = GenerateViews(ref GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));

            IdViewBlock = controls[0] as TextBlock;
            TitleViewBlock = controls[1] as TextBlock;
            ICViewBlock = controls[2] as TextBlock;

            CourseNavigationItem = CourseNavigationItem ?? new NavigationViewItem();

            UpdateViews();
        }

        internal override void UpdateViews()
        {
            IdViewBlock.Text = ID.branchtype.ToString() + " " + ID.branchstring;
            TitleViewBlock.Text = Title;
            ICViewBlock.Text = IC.Name;
            CourseNavigationItem.Content = Title;
        }

        internal override void DestructViews()
        {
            base.DestructViews();

            IdViewBlock = null;
            TitleViewBlock = null;
            ICViewBlock = null;
            CourseNavigationItem = null;
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

        public void InitializeNavViewItem()
        {
            CourseNavigationItem = CourseNavigationItem ?? new NavigationViewItem();
            CourseNavigationItem.Content = Title;
        }
    }
}