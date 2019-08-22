using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Operations
{
    public enum CompreSession
    {
        AM,
        PM
    }

    public class TimeEntry
    {
        public int Section { get; set; }
        public string Teacher { get; set; }
        public string Room { get; set; }
        public LinkedList<DayOfWeek> Days { get; set; }
        public LinkedList<int> Hours { get; set; }
    }

    public class Course
    {
        public string COM_COD { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public (int L, int P, int U) Credits { get; set; }
        public (DateTime date, CompreSession session) CompreTiming { get; set; }

        public Course(LinkedList<string[]> Entry)
        {
            COM_COD = Entry.First.Value[0];
            Number = Entry.First.Value[1];
            Title = Entry.First.Value[2];
            Credits = (int.Parse(Entry.First.Value[3]), int.Parse(Entry.First.Value[4]), int.Parse(Entry.First.Value[5]));
        }
    }

    public class MainParser
    {
        readonly LinkedList<LinkedList<string[]>> Inputs;

        public LinkedList<Course> Courses { get; private set; }

        public MainParser(string[][] input)
        {
            for (int i = 0; i < input.GetLength(0); ++i)
            {
                if (input[i][0] == "")
                    throw new Exception();

                int upto = i;
                // Test remaining for this loop
                for (int j = i; j < input.GetLength(0) - 1; ++j)
                    if (input[j + 1][0] == "")
                        ++upto;
                    else
                        break;

                LinkedList<string[]> temp = new LinkedList<string[]>();
                for (int j = i; j <= upto; ++j)
                {
                    temp.AddLast(input[j]);
                }

                Inputs.AddLast(temp);

                i = upto;
            }

        }
    }
}
