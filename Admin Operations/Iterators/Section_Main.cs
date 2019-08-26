using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Admin_Operations.Iterators
{
    [DebuggerDisplay("{SectionNo}")]
    public class Section
    {
        public static string[][] AllData => MainIterator.AllData;
        public int FromIndex { get; private set; }
        public int ToIndex { get; private set; }

        public CourseSubDivisions ParentCourseSubDivisions { get; internal set; }

        public int SectionNo => int.Parse(AllData[FromIndex][7]);
        public string Room => AllData[FromIndex][11];
        public LinkedList<DayOfWeek> Days
        {
            get
            {
                LinkedList<DayOfWeek> temp = new LinkedList<DayOfWeek>();

                foreach (string x in AllData[FromIndex][12].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    switch (x)
                    {
                        case "M":
                            temp.AddLast(DayOfWeek.Monday);
                            break;
                        case "T":
                            temp.AddLast(DayOfWeek.Tuesday);
                            break;
                        case "W":
                            temp.AddLast(DayOfWeek.Wednesday);
                            break;
                        case "Th":
                            temp.AddLast(DayOfWeek.Thursday);
                            break;
                        case "F":
                            temp.AddLast(DayOfWeek.Friday);
                            break;
                        case "S":
                            temp.AddLast(DayOfWeek.Saturday);
                            break;
                    }
                }

                return temp;
            }
        }
        public LinkedList<int> Hours
        {
            get
            {
                LinkedList<int> temp = new LinkedList<int>();

                foreach (char x in AllData[FromIndex][14].Replace("10", ""))
                    temp.AddLast(int.Parse(x.ToString()));

                if (AllData[FromIndex][14].Contains("10"))
                    temp.AddLast(10);

                return temp;
            }
        }
        public LinkedList<string> Teachers
        {
            get
            {
                LinkedList<string> temp = new LinkedList<string>();

                for (int i = FromIndex; i <= ToIndex; ++i)
                    temp.AddLast(AllData[i][8]);

                return temp;
            }
        }

        public Section(int from, int to, CourseSubDivisions Parent)
        {
            FromIndex = from;
            ToIndex = to;
            ParentCourseSubDivisions = Parent;
        }
    }
}
