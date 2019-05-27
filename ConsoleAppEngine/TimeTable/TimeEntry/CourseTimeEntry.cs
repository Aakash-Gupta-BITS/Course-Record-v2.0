using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    public class CourseTimeEntry : AbstractTimeEntry
    {
        public LinkedList<Teacher> TeacherList { get; set; }
        public Course Course { get; set; }
        public CourseEntryType CourseEntryType { get; set; }
        public RoomLocation RoomLocation { get; set; }
    }
}
