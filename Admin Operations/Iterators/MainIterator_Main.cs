using System.Collections.Generic;
using System.Linq;
using System.Collections;

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

        public bool[,] RoomBookings(string room)
        {
            bool[,] Output = new bool[6, 10];

            foreach (var c in CourseIterationList)
                foreach (var x in c)
                    foreach (var s in x)
                        if (s.Room == room)
                            foreach (var a in from hr in s.Hours
                                              from day in s.Days
                                              select ((int)day - 1, hr - 1))

                                Output[a.Item1, a.Item2] = true;
                       
            return Output;
        }
    }
}