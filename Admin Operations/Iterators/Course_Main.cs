using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Admin_Operations.Iterators
{
    [DebuggerDisplay("{Title}")]
    public partial class Course
    {
        public static string[][] AllData => MainIterator.AllData;
        public int FromIndex { get; private set; }
        public int ToIndex { get; private set; }

        public MainIterator ParentMainIterator { get; private set; }

        public string COM_COD => AllData[FromIndex][0];
        public string Number => AllData[FromIndex][1];
        public string Title => AllData[FromIndex][2];
        public (int L, int P, int U) Credits
        {
            get
            {
                string s;
                return (
                    int.Parse((s = AllData[FromIndex][4].Replace("-", "")) == "" ? "0" : s),
                    int.Parse((s = AllData[FromIndex][5].Replace("-", "")) == "" ? "0" : s),
                    int.Parse((s = AllData[FromIndex][6].Replace("-", "")) == "" ? "0" : s)
                );
            }
        }
        public LinkedList<CourseSubDivisions> Divisons = new LinkedList<CourseSubDivisions>();
        public (DateTime date, CompreSession session) CompreTiming
        {
            get
            {
                string[] times = AllData[FromIndex][16].Split(new char[] { ' ', '/' });
                return (new DateTime(2019, int.Parse(times[1]), int.Parse(times[0])), (times[2] == "AN" ? CompreSession.AM : CompreSession.PM));
            }
        }

        public Course(int from, int to, MainIterator iter)
        {
            FromIndex = from;
            ToIndex = to;
            ParentMainIterator = iter;

            // Not tested completely
            LinkedList<(int start, int end)> Ranges = new LinkedList<(int start, int end)>();

            Ranges.AddLast((FromIndex, FromIndex));

            for (int i = FromIndex + 1; i <= ToIndex; ++i)
                if (AllData[i][2] != "")
                    Ranges.AddLast((i, i));
                else
                    Ranges.Last.Value = (Ranges.Last.Value.start, Ranges.Last.Value.end + 1);

            foreach (var (start, end) in Ranges)
                Divisons.AddLast(new CourseSubDivisions(start, end, this));
        }
    }
}
