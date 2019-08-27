using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Operations.Iterators
{
    public partial class CourseSubDivisions : IEnumerable<Section>
    {
        public IEnumerator<Section> GetEnumerator()
        {
            return ((IEnumerable<Section>)Sections).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Section>)Sections).GetEnumerator();
        }
    }
}
