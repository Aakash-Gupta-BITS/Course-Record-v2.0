using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppEngine.AllEnums;

namespace ConsoleAppEngine.Course
{
    public class CourseEntry
    {
       // internal readonly AllCourses AllCourses;

        public readonly string Title;
        private readonly (BranchType branchtype, string branchstring) id;
        private readonly byte units;
        public readonly bool HaveTutorial;
       // public readonly Teacher IC;
        public readonly EHandouts HandoutEntry = new EHandouts();
        public readonly EBooks BookEntry = new EBooks();
        public readonly ETests TestEntry = new ETests();
        public readonly ETeachers TeacherEntry = new ETeachers();
        public readonly ECourseTimeTable TimeEntry = new ECourseTimeTable();
        

        private readonly (/*LinkedList<Teacher> Teachers,*/ uint Section, uint Room, DayOfWeek[] DaysOfWeek, byte[] Hours) LectureInfo;
        private readonly (/*LinkedList<Teacher> Teachers,*/ uint Section, uint Room, DayOfWeek[] DaysOfWeek, byte[] Hours) PracticalInfo;
        private readonly (/*LinkedList<Teacher> Teachers,*/ uint Section, uint Room, DayOfWeek[] DaysOfWeek, byte[] Hours) TutorialInfo;

        public BranchType Branch => 
            id.branchtype;
        public string Id =>
            id.branchtype + " " + id.branchstring;
        public int LectureUnits => 
            units & 0x0f;
        public int PracticalUnits => 
            units >> 4;
        public int TotalUnits =>
            LectureUnits + PracticalUnits;

 //       public LinkedList<Teacher> LectureTeachers =>
 //           LectureInfo == default ? default : LectureInfo.Teachers;
        public uint LectureSection => 
            LectureInfo == default ? default : LectureInfo.Section;
        public DayOfWeek[] LectureDays =>
            LectureInfo == default ? default : LectureInfo.DaysOfWeek;
        public byte[] LectureHours =>
            LectureInfo == default ? default : LectureInfo.Hours;
        public uint LectureRoom =>
            LectureInfo == default ? default : LectureInfo.Room;

//        public LinkedList<Teacher> PracticalTeachers => 
//            PracticalInfo == default ? default : PracticalInfo.Teachers;
        public uint PracticalSection =>
            PracticalInfo == default ? default : PracticalInfo.Section;
        public DayOfWeek[] PracticalDays =>
            PracticalInfo == default ? default : PracticalInfo.DaysOfWeek;
        public byte[] PracticalHours =>
            PracticalInfo == default ? default : PracticalInfo.Hours;
        public uint PracticalRoom =>
            PracticalInfo == default ? default : PracticalInfo.Room;

//        public LinkedList<Teacher> TutorialTeachers => 
//            TutorialInfo == default ? default : TutorialInfo.Teachers;
        public uint TutorialSection =>
            TutorialInfo == default ? default : TutorialInfo.Section;
        public DayOfWeek[] TutorialDays => 
            TutorialInfo == default ? default : TutorialInfo.DaysOfWeek;
        public byte[] TutorialHours => 
            TutorialInfo == default ? default : TutorialInfo.Hours;
        public uint TutorialRoom => 
            TutorialInfo == default ? default : TutorialInfo.Room;

        public CourseEntry(/*AllCourses allCourses,*/ string title, string id, int lectureUnits, int practicalUnits, bool haveTutorial, // Teacher ic,
             (/*LinkedList<Teacher> lecTeachers,*/ uint lecSec, uint lecRoom, DayOfWeek[] lecDays, byte[] lecHours) lectureInfo = default,
             (/*LinkedList<Teacher> tutTeachers,*/ uint tutSec, uint tutRoom, DayOfWeek[] tutDays, byte[] tutHours) tutorialInfo = default,
             (/*LinkedList<Teacher> pracTeachers,*/ uint pracSec, uint pracRoom, DayOfWeek[] pracDays, byte[] pracHours) practicalInfo = default)
        {
            // AllCourses = allCourses;
            Title = title;
            this.id = ((BranchType)Enum.Parse(typeof(BranchType), id.Split(' ')[0], true), id.Split(' ')[1]);
            units = (byte)((lectureUnits & 0x07) | ((practicalUnits & 0x07) << 4));
            HaveTutorial = haveTutorial;
            // IC = ic;

            LectureInfo = lectureInfo;
            PracticalInfo = practicalInfo;
            TutorialInfo = tutorialInfo;

            // AllCourses.CoursEntryList.AddLast(this);
        }

    }
}
