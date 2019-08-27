using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Operations.Iterators
{
    public partial class Course : IEnumerable<CourseSubDivisions>
    {
        public IEnumerator<CourseSubDivisions> GetEnumerator()
        {
            return ((IEnumerable<CourseSubDivisions>)Divisons).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<CourseSubDivisions>)Divisons).GetEnumerator();
        }
    }
}
