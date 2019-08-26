using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Operations.Iterators
{
    public partial class CourseSubDivisions : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return Sections.GetEnumerator();
        }
    }
}
