using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    public class CourseTimeEntry : AbstractTimeEntry, IComparable
    {
        public LinkedList<Teacher> TeacherList { get; set; }
        public Course Course { get; set; }
        public CourseEntryType CourseEntryType { get; set; }
        public RoomLocation RoomLocation { get; set; }

        public static bool operator ==(CourseTimeEntry lhs, CourseTimeEntry rhs) => lhs.Equals(rhs);
        public static bool operator !=(CourseTimeEntry lhs, CourseTimeEntry rhs) => !lhs.Equals(rhs);

        public int CompareTo(object obj) => this.DayTime.CompareTo((obj as CourseTimeEntry).DayTime);
        public override bool Equals(object obj)
        {
            return obj is CourseTimeEntry entry &&
                   EqualityComparer<LinkedList<Teacher>>.Default.Equals(TeacherList, entry.TeacherList) &&
                   EqualityComparer<Course>.Default.Equals(Course, entry.Course) &&
                   CourseEntryType == entry.CourseEntryType &&
                   DayTime == entry.DayTime &&
                   EqualityComparer<RoomLocation>.Default.Equals(RoomLocation, entry.RoomLocation);
        }
        public override int GetHashCode()
        {
            var hashCode = 1976764702;
            hashCode = hashCode * -1521134295 + EqualityComparer<LinkedList<Teacher>>.Default.GetHashCode(TeacherList);
            hashCode = hashCode * -1521134295 + EqualityComparer<Course>.Default.GetHashCode(Course);
            hashCode = hashCode * -1521134295 + CourseEntryType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<RoomLocation>.Default.GetHashCode(RoomLocation);
            return hashCode;
        }
    }
}
