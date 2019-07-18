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

        public NavigationViewItem navigationViewItem;
        public TextBlock IdViewBlock;
        public TextBlock TitleViewBlock;
        public TextBlock ICViewBlock;

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
            ID = ((CourseType)info.GetValue(nameof(ID.branchtype), typeof(CourseType)), (string)info.GetValue(nameof(ID.branchstring), typeof(string)));
            Title = info.GetString(nameof(Title));
            LectureUnits = info.GetByte(nameof(LectureUnits));
            PracticalUnits = info.GetByte(nameof(PracticalUnits));
            IC = new ETeacherEntry("", default, default, default, default, default);


            BookEntry = info.GetValue(nameof(BookEntry), typeof(EBooks)) as EBooks;
        }

        public void InitializeViews()
        {
            FrameworkElement[] controls = GenerateViews(ref GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));
            IdViewBlock = controls[0] as TextBlock;
            TitleViewBlock = controls[1] as TextBlock;
            ICViewBlock = controls[2] as TextBlock;

            navigationViewItem = new NavigationViewItem
            {
                Content = Title
            };

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

            info.AddValue(nameof(BookEntry), BookEntry, typeof(EBooks));
        }
        #endregion

        public CourseEntry((CourseType branchtype, string branchstring) id, string title, byte lectureUnits, byte practicalUnits, ETeacherEntry iC)
        {
            FrameworkElement[] controls = GenerateViews(ref GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));
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