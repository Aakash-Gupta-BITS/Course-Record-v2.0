using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    public class TimeTable
    {
        internal readonly AllCourses AllCourses;
        internal LinkedList<CourseEntry> CourseList => AllCourses.CoursEntryList;

        private readonly LinkedList<CourseTimeEntry> timeList = new LinkedList<CourseTimeEntry>();

        public TimeTable(AllCourses courses) => AllCourses = courses;

    }
}