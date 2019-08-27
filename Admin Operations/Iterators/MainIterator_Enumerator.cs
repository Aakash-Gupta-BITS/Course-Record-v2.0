using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Operations.Iterators
{
    public partial class MainIterator : IEnumerable<Course>
    {
        public IEnumerator<Course> GetEnumerator()
        {
            return ((IEnumerable<Course>)CourseIterationList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Course>)CourseIterationList).GetEnumerator();
        }
    }
}
