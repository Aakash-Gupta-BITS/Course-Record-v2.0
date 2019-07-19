using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Windows.Storage;

namespace ConsoleAppEngine.Globals
{
    public static class HDDSync
    {
        public static void GetAllFromHDD()
        {
            string CourseDirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Courses");

            if (!Directory.Exists(CourseDirectoryLocation))
            {
                return;
            }

            BinaryFormatter formatter = new BinaryFormatter();

            foreach (string file in Directory.GetFiles(CourseDirectoryLocation))
            {
                using (var s = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    AllCourses.Instance.CoursesList.AddLast(formatter.Deserialize(s) as CourseEntry);
                }
            }

            string ContactDirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Contacts");

            if (!Directory.Exists(ContactDirectoryLocation))
            {
                return;
            }
            
            using (var s = new FileStream(Path.Combine(ContactDirectoryLocation, "Teachers" + ".bin"), FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (s.Length == 0)
                {
                    AllContacts.Instance.TeacherEntry = new ETeachers();
                }
                else
                {
                    AllContacts.Instance.TeacherEntry = new BinaryFormatter().Deserialize(s) as ETeachers;
                }
            }
            using (var s = new FileStream(Path.Combine(ContactDirectoryLocation, "Students" + ".bin"), FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (s.Length == 0)
                {
                    AllContacts.Instance.StudentEntry = new EStudents();
                }
                else
                {
                    AllContacts.Instance.StudentEntry = new BinaryFormatter().Deserialize(s) as EStudents;
                }
            }
            
            foreach (CourseEntry course in AllCourses.Instance.lists)
            {
                var finalteachers = new LinkedList<ETeacherEntry>();
                var mainiterator = AllContacts.Instance.TeacherEntry.lists.First;
                var tempiterator = course.TeacherEntry.lists.First;

                while (tempiterator != null)
                {
                    while (tempiterator.Value.Name != mainiterator.Value.Name)
                        mainiterator = mainiterator.Next;

                    finalteachers.AddLast(mainiterator.Value);

                    mainiterator = mainiterator.Next;
                    tempiterator = tempiterator.Next;

                }
                course.TeacherEntry.lists.Clear();
                foreach (var x in finalteachers)
                    course.TeacherEntry.lists.AddLast(x);
            }

        }

        public static void AddAllToHDD()
        {
            string CourseDirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Courses");

            if (!Directory.Exists(CourseDirectoryLocation))
            {
                Directory.CreateDirectory(CourseDirectoryLocation);
            }

            foreach (CourseEntry e in AllCourses.Instance.CoursesList)
            {
                using (Stream m = new FileStream(Path.Combine(CourseDirectoryLocation, e.Title + ".bin"), FileMode.Create, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(m, e);
                }
            }

            string ContactsDirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Contacts");

            if (!Directory.Exists(ContactsDirectoryLocation))
            {
                Directory.CreateDirectory(ContactsDirectoryLocation);
            }

            using (Stream m = new FileStream(Path.Combine(ContactsDirectoryLocation, "Teachers" + ".bin"), FileMode.Create, FileAccess.Write))
            {
                new BinaryFormatter().Serialize(m, AllContacts.Instance.TeacherEntry);
            }

            using (Stream m = new FileStream(Path.Combine(ContactsDirectoryLocation, "Students" + ".bin"), FileMode.Create, FileAccess.Write))
            {
                new BinaryFormatter().Serialize(m, AllContacts.Instance.StudentEntry);

            }
        }
    }
}
