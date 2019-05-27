using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    class TimeTable
    {
        public readonly LinkedList<AbstractTimeEntry> List = new LinkedList<AbstractTimeEntry>();

        public TimeTable(AbstractTimeEntry val) => List.AddLast(val);
        public void Add(Course c)
        {
            LinkedList<CourseTimeEntry> entry = c.GetTimeEntries();
            foreach (CourseTimeEntry x in entry)
                List.AddLast(x);
        }

        
        public AbstractTimeEntry[] ToArray() => List.ToArray();
    }
}