using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    public enum CourseEntryType
    {
        Lecture,
        Tutorial,
        Lab,
        Quiz,
        MidSem,
        Compre,
        ExamDistribution
    }
    public static partial class Extensions
    {
        public static LinkedList<T> RemoveDuplicates<T>(this LinkedList<T> abcd) where T : class
        {
            var x = abcd.ToArray();
            abcd.Clear();
            foreach (T val in x)
                if (!abcd.Contains(val))
                    abcd.AddLast(val);
            return abcd;
        }
        public static LinkedList<T> AddLast<T>(this LinkedList<T> abcd, LinkedList<T> ToAdd) where T : class
        {
            abcd.AddLast(ToAdd.First);
            return abcd;
        }
        public static LinkedList<T> Sort<T>(this LinkedList<T> abcd)
        {
            var temp = abcd.ToArray();
            Array.Sort(temp);
            abcd.Clear();
            foreach (var x in temp)
                abcd.AddLast(x);
            return abcd;

        }
    }

    public class Course
    {
        private readonly AllCourses CoursesList;
        private readonly AllTeachers TeacherList;

        public readonly string Number;
        public readonly string Title;
        private readonly LinkedList<(LinkedList<Teacher> Teachers, CourseEntryType EntryType, RoomLocation Location, DayTime DayTime, uint Section)> Entries
            = new LinkedList<(LinkedList<Teacher>, CourseEntryType, RoomLocation, DayTime, uint)>();

        // Local Lists
        private readonly LinkedList<Teacher> teachers = new LinkedList<Teacher>();
        private readonly LinkedList<RoomLocation> roomLocations = new LinkedList<RoomLocation>();
        private readonly LinkedList<(DayTime Timing, CourseEntryType EntryType)> timings = new LinkedList<(DayTime Timing, CourseEntryType EntryType)>();
        public LinkedList<Teacher> Teachers
        {
            get
            {
                teachers.Clear();

                foreach ((LinkedList<Teacher> Teachers, CourseEntryType, RoomLocation, DayTime, uint) tempi in Entries)
                    foreach (Teacher tempj in tempi.Teachers)
                        teachers.AddLast(tempj);

                return teachers.RemoveDuplicates();
            }
        }
        public LinkedList<RoomLocation> RoomLocations
        {
            get
            {
                roomLocations.Clear();
                foreach ((LinkedList<Teacher>, CourseEntryType, RoomLocation Location, DayTime, uint) temp in Entries)
                    roomLocations.AddLast(temp.Location);

                return roomLocations.RemoveDuplicates();
            }
        }
        public LinkedList<(DayTime Time, CourseEntryType EntryType)> Timings
        {
            get
            {
                timings.Clear();
                foreach ((LinkedList<Teacher>, CourseEntryType EntryType, RoomLocation, DayTime DayTime, uint) temp in Entries)
                    timings.AddLast((temp.DayTime, temp.EntryType));

                return timings;
            }
        }


        public Course(string ID, string NAME, AllCourses ReferenceCourseList, AllTeachers ReferenceTeacherList)
        {
            Number = ID;
            Title = NAME;
            CoursesList = ReferenceCourseList;
            TeacherList = ReferenceTeacherList;
            CoursesList.AllCoursesList.AddLast(this);
        }
        public void AddCourseTiming(CourseEntryType entryType, RoomLocation roomLocation, DayTime dayTime, LinkedList<Teacher> TeacherList, uint Section)
        {

            foreach ((LinkedList<Teacher>, CourseEntryType, RoomLocation, DayTime DayTime, uint) temp in Entries)
                if (temp.DayTime.Intersect(dayTime))
                    throw new ArgumentException("Time collides..");
            Entries.AddLast((TeacherList, entryType, roomLocation, dayTime, Section));
        }
        public void AddTeacherToTiming(Teacher t, DayTime dt)
        {
            if (!TeacherList.List.Contains(t))
                TeacherList.List.AddLast(t);
            foreach ((LinkedList<Teacher> Teachers, CourseEntryType, RoomLocation, DayTime DayTime, uint) temp in Entries)
                if (temp.DayTime == dt)
                {
                    if (!temp.Teachers.Contains(t))
                        temp.Teachers.AddLast(t);
                    break;
                }
        }

        public LinkedList<CourseTimeEntry> GetTimeEntries()
        {
            LinkedList<CourseTimeEntry> result = new LinkedList<CourseTimeEntry>();
            foreach ((LinkedList<Teacher> Teachers, CourseEntryType EntryType, RoomLocation Location, DayTime DayTime, uint Section) temp in Entries)
                result.AddLast(
                    new CourseTimeEntry()
                    {
                        Course = this,
                        CourseEntryType = temp.EntryType,
                        DayTime = temp.DayTime,
                        RoomLocation = temp.Location,
                        TeacherList = temp.Teachers,
                        SectionNo = temp.Section
                    });

            return result;
        }
        public LinkedList<Teacher> GetTeachers(CourseEntryType ctype)
        {
            LinkedList<CourseEntryType> temp = new LinkedList<CourseEntryType>();
            temp.AddLast(ctype);
            return GetTeachers(ctype);
        }
        public LinkedList<Teacher> GetTeachers(LinkedList<CourseEntryType> ctype)
        {
            LinkedList<Teacher> result = new LinkedList<Teacher>();

            foreach ((LinkedList<Teacher> Teachers, CourseEntryType EntryType, RoomLocation, DayTime, uint) temp in Entries)
                if (ctype.Contains(temp.EntryType))
                    result.AddLast(temp.Teachers);
            return result;
        }
    }
}