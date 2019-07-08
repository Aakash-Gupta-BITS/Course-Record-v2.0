using ConsoleAppEngine.AllEnums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public class CourseEntry : ISerializable
    {
        internal readonly (CourseType branchtype, string branchstring) ID;
        public readonly string Title;
        internal readonly byte LectureUnits;
        internal readonly byte PracticalUnits;
        public readonly ETeacherEntry IC;

        public readonly NavigationViewItem navigationViewItem = new NavigationViewItem();

        public readonly EBooks BookEntry = new EBooks();
        public readonly EStudents CTLog = new EStudents();
        public readonly EEvents EventEntry = new EEvents();
        public readonly EHandouts HandoutEntry = new EHandouts();
        public readonly ETeachers TeacherEntry = new ETeachers();
        public readonly ETests TestEntry = new ETests();
        public readonly ECourseTimeTable TimeEntry = new ECourseTimeTable();

        public CourseEntry((CourseType branchtype, string branchstring) id, string title, byte lectureUnits, byte practicalUnits, ETeacherEntry iC)
        {
            Title = title;
            ID = id;
            LectureUnits = lectureUnits;
            PracticalUnits = practicalUnits;
            IC = iC;

            navigationViewItem.Content = Title;
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

        protected CourseEntry(SerializationInfo info, StreamingContext context)
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

            navigationViewItem = new NavigationViewItem();
            navigationViewItem.Content = Title;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(ID.branchtype), ID.branchtype, typeof(CourseType));
            info.AddValue(nameof(ID.branchstring), ID.branchstring);
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(LectureUnits), LectureUnits);
            info.AddValue(nameof(PracticalUnits), PracticalUnits);

            info.AddValue(nameof(BookEntry), BookEntry, typeof(EBooks));
            info.AddValue(nameof(EventEntry), EventEntry, typeof(EEvents));
            info.AddValue(nameof(HandoutEntry), HandoutEntry, typeof(EHandouts));
            info.AddValue(nameof(TestEntry), TestEntry, typeof(ETests));
            info.AddValue(nameof(TeacherEntry), TeacherEntry, typeof(ETeachers));
            info.AddValue(nameof(TimeEntry), TimeEntry, typeof(ECourseTimeTable));
        }
    }
}