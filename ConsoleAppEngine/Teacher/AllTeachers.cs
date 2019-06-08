using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    public class AllTeachers
    {
        public LinkedList<Teacher> List = new LinkedList<Teacher>();

        public AllTeachers()
        {

        }

        public void AddTeacher(Teacher t)
        {
            if (!List.Contains(t))
                List.AddLast(t);
        }
    }
}
