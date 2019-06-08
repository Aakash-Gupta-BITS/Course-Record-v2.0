using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppEngine;

namespace Course_Record_v2._0
{
    public class Globals
    {
        public readonly AllTeachers Teachers = new AllTeachers();
        public readonly AllCourses Courses = new AllCourses();
        public readonly TimeTable TimeTable;

        public Globals()
        {
            TimeTable = new TimeTable(Courses);
        }
    }
}
