using System.Collections.Generic;

namespace Admin_Operations.Iterators
{
    public partial class MainIterator
    {
        public static string[][] AllData;

        LinkedList<Course> CourseIterationList { get; set; } = new LinkedList<Course>();

        public MainIterator(string[][] data)
        {
            AllData = data;

            // Not tested completely
            LinkedList<(int start, int end)> Ranges = new LinkedList<(int start, int end)>();

            Ranges.AddLast((0, 0));

            for (int i = 1; i < data.Length; ++i)
                if (AllData[i][0] != "")
                    Ranges.AddLast((i, i));
                else
                    Ranges.Last.Value = (Ranges.Last.Value.start, Ranges.Last.Value.end + 1);

            foreach (var (start, end) in Ranges)
                CourseIterationList.AddLast(new Course(start, end, this));
        }
    }
}
