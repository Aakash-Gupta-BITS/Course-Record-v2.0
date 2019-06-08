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
        internal LinkedList<Course> CourseList => AllCourses.AllCoursesList;

        private readonly LinkedList<CourseTimeEntry> timeList = new LinkedList<CourseTimeEntry>();
        public LinkedList<CourseTimeEntry> TimeList
        {
            get
            {
                timeList.Clear();
                foreach (Course s in CourseList)
                    foreach (CourseTimeEntry ct in s.GetTimeEntries())
                        timeList.AddLast(ct);
                timeList.Sort();
                return timeList;
            }
        }

        public TimeTable(AllCourses courses) => AllCourses = courses;

        private void Add(Course c)
        {
            foreach (Course i in CourseList)
                foreach (CourseTimeEntry j in i.GetTimeEntries())
                    foreach (CourseTimeEntry k in c.GetTimeEntries())
                        if (j.DayTime.Intersect(k.DayTime))
                            throw new Exception("Time Collides");
            CourseList.AddLast(c);
        }
    }
}