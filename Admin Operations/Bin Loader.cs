using System;
using System.Collections;
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

    public class CourseSubDivisions
    {
        public static string[][] AllData;
        public int FromIndex { get; internal set; }
        public int ToIndex { get; internal set; }
        public Course ParentCourse { get; internal set; }


    }

    public class Course
    {
        public static string[][] AllData;
        public int FromIndex { get; internal set; }
        public int ToIndex { get; internal set; }

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



        public Course()
        {

        }
    }
}