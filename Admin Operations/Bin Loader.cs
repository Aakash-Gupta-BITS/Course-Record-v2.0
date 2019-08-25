using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Operations
{
    public enum ClassType
    {
        Main,
        Tutorial,
        Praactical,
        UnKnown
    }

    public enum CompreSession
    {
        AM,
        PM
    }

    public class TimeEntry
    {
        public ClassType ClassType { get; set; }
        public int Section { get; set; }
        public string[] Teacher { get; set; }
        public string Room { get; set; }
        public LinkedList<DayOfWeek> Days { get; set; }
        public LinkedList<int> Hours { get; set; }

        public TimeEntry(string[][] Entries)
        {
            
            
            /*
            ClassType = typ;
            Section = sec;
            Teacher = name;
            Room = room;

            Days = new LinkedList<DayOfWeek>();
            foreach (string x in Array.ConvertAll(days.ToCharArray(), a => a.ToString()))
            {
                switch (x)
                {
                    case "M":
                        Days.AddLast(DayOfWeek.Monday);
                        break;
                    case "T":
                        Days.AddLast(DayOfWeek.Monday);
                        break;
                    case "W":
                        Days.AddLast(DayOfWeek.Monday);
                        break;
                    case "Th":
                        Days.AddLast(DayOfWeek.Monday);
                        break;
                    case "F":
                        Days.AddLast(DayOfWeek.Monday);
                        break;
                    case "S":
                        Days.AddLast(DayOfWeek.Monday);
                        break;
                }
            }

            Hours = new LinkedList<int>();
            foreach (char x in hours.Replace("10", ""))
            {
                Hours.AddLast(int.Parse(x.ToString()));
            }
            if (hours.Contains("10"))
                Hours.AddLast(10);*/
        }

        public static LinkedList<TimeEntry> GetTimeEntries(LinkedList<string[][]> Entries)
        {
            LinkedList<TimeEntry> Output = new LinkedList<TimeEntry>();

            foreach (var entry in Entries)
                Output.AddLast(new TimeEntry(entry));

            return Output;
        }
    }

    public class Course
    {
        public string COM_COD { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        public (int L, int P, int U) Credits { get; set; }
        public (DateTime date, CompreSession session) CompreTiming { get; set; }
        public LinkedList<(ClassType ctype, LinkedList<TimeEntry>)> Classes = new LinkedList<(ClassType ctype, LinkedList<TimeEntry>)>();

        public Course(string[][] Entry, int StartRow, int EndRow)
        {
            COM_COD = Entry[StartRow][0];
            Number = Entry[StartRow][1];
            Title = Entry[StartRow][2];

            string s;

            Credits = (
                int.Parse((s = Entry[StartRow][4].Replace("-", "")) == "" ? "0" : s),
                int.Parse((s = Entry[StartRow][5].Replace("-", "")) == "" ? "0" : s),
                int.Parse((s = Entry[StartRow][6].Replace("-", "")) == "" ? "0" : s)
                );

            string[] times = Entry[StartRow][16].Split(new char[] { ' ', '/' });
            CompreTiming = (new DateTime(2019, int.Parse(times[1]), int.Parse(times[0])), (times[2] == "AN" ? CompreSession.AM : CompreSession.PM));

            // Modification
            Entry[StartRow][2] = "Main";
            for (int i = StartRow + 1; i <= EndRow; ++i)
            {
                if (Entry[i][2] == "")
                    Entry[i][2] = Entry[i - 1][2];
            }
        }
    }
}
