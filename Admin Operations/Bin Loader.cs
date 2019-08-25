using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Main = Admin_Operations.Iterators.MainIterator;

namespace Admin_Operations
{
    public enum ClassType
    {
        Main,
        Tutorial,
        Practical,
        UnKnown
    }

    public enum CompreSession
    {
        AM,
        PM
    }

    public class Section
    {
        public static string[][] AllData => Main.AllData;
        public int FromIndex { get; private set; }
        public int ToIndex { get; private set; }

        public CourseSubDivisions CourseSubDivisions { get; internal set; }
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


    }

    public class CourseSubDivisions
    {
        public static string[][] AllData => Main.AllData;
        public int FromIndex { get; private set; }
        public int ToIndex { get; private set; }

        public Course ParentCourse { get; internal set; }
        public ClassType Class { get; private set; }
        public LinkedList<Section> Sections = new LinkedList<Section>();

        public CourseSubDivisions(int from, int to, Course c)
        {
            FromIndex = from;
            ToIndex = to;
            ParentCourse = c;

            switch (FromIndex == ParentCourse.FromIndex)
            {
                case true:
                    Class = ClassType.Main;
                    break;
                case false:
                    Enum.TryParse<ClassType>(AllData[FromIndex][2], out ClassType ct);
                    Class = ct; 
                    break;
            }
        }
    }

    public class Course
    {
        public static string[][] AllData => Main.AllData;
        public int FromIndex { get; private set; }
        public int ToIndex { get; private set; }

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


        public Course(int from, int to)
        {
            FromIndex = from;
            ToIndex = to;

            // Not tested completely
            LinkedList<(int start, int end)> Ranges = new LinkedList<(int start, int end)>();

            Ranges.AddLast((FromIndex, FromIndex));

            for (int i = FromIndex + 1; i <= ToIndex; ++i)
            {
                if (AllData[i][2] != "")
                    Ranges.AddLast((i, i));
                else
                    Ranges.Last.Value = (Ranges.Last.Value.start, Ranges.Last.Value.end + 1);
            }

            foreach (var (start, end) in Ranges)
                Divisons.AddLast(new CourseSubDivisions(start, end, this));
        }
    }
}