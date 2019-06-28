using ConsoleAppEngine.AllEnums;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    public class CourseEntry
    {
        private readonly (BranchType branchtype, string branchstring) ID;
        public readonly string Title;
        private readonly byte LectureUnits;
        private readonly byte PracticalUnits;
        public readonly ETeacherEntry IC;

        public readonly NavigationViewItem navigationViewItem = new NavigationViewItem();

        public readonly EHandouts HandoutEntry = new EHandouts();
        public readonly EBooks BookEntry = new EBooks();
        public readonly ETests TestEntry = new ETests();
        public readonly ETeachers TeacherEntry = new ETeachers();
        public readonly ECourseTimeTable TimeEntry = new ECourseTimeTable();
        public readonly EEvents EventEntry = new EEvents();

        public CourseEntry((BranchType branchtype, string branchstring) id, string title, byte lectureUnits, byte practicalUnits, ETeacherEntry iC)
        {
            Title = title;
            ID = id;
            LectureUnits = lectureUnits;
            PracticalUnits = practicalUnits;
            IC = iC;

            navigationViewItem.Content = Title;

        }
    }
}
