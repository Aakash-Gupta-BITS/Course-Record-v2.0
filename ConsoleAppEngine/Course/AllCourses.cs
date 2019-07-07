using System.Collections.Generic;

namespace ConsoleAppEngine.Course
{
    public class AllCourses
    {
        public readonly LinkedList<CourseEntry> CoursesList = new LinkedList<CourseEntry>();

        public void AddCourse(CourseEntry e)
        {
            if (!Consistent(e))
                throw new System.Exception();
            CoursesList.AddLast(e);
        }

        public bool Consistent(CourseEntry e)
        {
            foreach (var y in CoursesList)
            {
                if (y.Title == e.Title ||
                    (y.ID.branchstring == e.ID.branchstring && y.ID.branchtype == e.ID.branchtype))
                    return false;
            }
            return true;
        }

    }
}
