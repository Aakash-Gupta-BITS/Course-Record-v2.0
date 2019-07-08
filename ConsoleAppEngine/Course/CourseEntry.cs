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

        public readonly EHandouts HandoutEntry = new EHandouts();
        public readonly EBooks BookEntry = new EBooks();
        public readonly ETests TestEntry = new ETests();
        public readonly ETeachers TeacherEntry = new ETeachers();
        public readonly ECourseTimeTable TimeEntry = new ECourseTimeTable();
        public readonly EEvents EventEntry = new EEvents();
        public readonly EStudents CTLog = new EStudents();

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
                foreach (var y in x.Teacher)
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

            navigationViewItem = new NavigationViewItem();

            HandoutEntry = new EHandouts();
            TestEntry = new ETests();
            TeacherEntry = new ETeachers();
            TimeEntry = new ECourseTimeTable();
            EventEntry = new EEvents();
            CTLog = new EStudents();

            ID = ((CourseType)info.GetValue(nameof(ID.branchtype), typeof(CourseType)), (string)info.GetValue(nameof(ID.branchstring), typeof(string)));
            Title = info.GetString(nameof(Title));
            LectureUnits = info.GetByte(nameof(LectureUnits));
            PracticalUnits = info.GetByte(nameof(PracticalUnits));

            BookEntry = (EBooks)info.GetValue(nameof(BookEntry), typeof(EBooks));
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
        }
    }
}