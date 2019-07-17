using ConsoleAppEngine.Course;

namespace ConsoleAppEngine.Contacts
{
    public class AllContacts
    {
        public static AllContacts Instance = new AllContacts();

        public ETeachers TeacherEntry { get; internal set; } = new ETeachers();
        public EStudents StudentEntry { get; internal set; } = new EStudents();

        private AllContacts()
        {

        }
    }
}
