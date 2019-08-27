using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Admin_Operations.Iterators
{
    [DebuggerDisplay("{Class}")]
    public partial class CourseSubDivisions
    {
        public static string[][] AllData => MainIterator.AllData;
        public int FromIndex { get; private set; }
        public int ToIndex { get; private set; }


        public ClassType Class { get; private set; }
        public LinkedList<Section> Sections = new LinkedList<Section>();

        public CourseSubDivisions(int from, int to, Course c)
        {
            FromIndex = from;
            ToIndex = to;

            switch (FromIndex == c.FromIndex)
            {
                case true:
                    Class = ClassType.Main;
                    break;
                case false:
                    Enum.TryParse<ClassType>(AllData[FromIndex][2], out ClassType ct);
                    Class = ct;
                    break;
            }

            // Not tested completely
            LinkedList<(int start, int end)> Ranges = new LinkedList<(int start, int end)>();

            Ranges.AddLast((FromIndex, FromIndex));

            for (int i = FromIndex + 1; i <= ToIndex; ++i)
                if (AllData[i][7] != "")
                    Ranges.AddLast((i, i));
                else
                    Ranges.Last.Value = (Ranges.Last.Value.start, Ranges.Last.Value.end + 1);

            foreach (var (start, end) in Ranges)
                Sections.AddLast(new Section(start, end, this));
        }
    }
}
