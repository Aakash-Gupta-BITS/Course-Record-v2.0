using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    class CourseTimeEntry : AbstractTimeEntry
    {
        public readonly LinkedList<Teacher> Teachers;
        public readonly Course Course;
        public readonly RoomLocation Location;

        public CourseTimeEntry(Course s, RoomLocation location)
        {
            Location = location;
            Course = s;
        }
        public void AddTeacher(Teacher teacher) => Teachers.AddLast(teacher);
        
    }
}
