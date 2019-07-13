using ConsoleAppEngine.Course;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Windows.Storage;

namespace ConsoleAppEngine.Contacts
{
    public class AllContacts
    {
        public ETeachers TeacherEntry { get; private set; } = new ETeachers();
        public EStudents StudentEntry { get; private set; } = new EStudents();

        public void AddToHdd()
        {
            string DirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Contacts");

            if (!Directory.Exists(DirectoryLocation))
            {
                Directory.CreateDirectory(DirectoryLocation);
            }

            using (Stream m = new FileStream(Path.Combine(DirectoryLocation, "Teachers" + ".bin"), FileMode.Create, FileAccess.Write))
            {
                new BinaryFormatter().Serialize(m, TeacherEntry);
            }

            using (Stream m = new FileStream(Path.Combine(DirectoryLocation, "Students" + ".bin"), FileMode.Create, FileAccess.Write))
            {
                new BinaryFormatter().Serialize(m, StudentEntry);
            }

        }

        public void AddToHdd_NewThread()
        {
            Thread thread = new Thread(new ThreadStart(AddToHdd));
            thread.Name = "Add Contacts to Hdd";
            thread.IsBackground = false;
            thread.Start();
        }

        public void GetFromHdd()
        {
            string DirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Contacts");

            if (!Directory.Exists(DirectoryLocation))
            {
                return;
            }

            using (var s = new FileStream(Path.Combine(DirectoryLocation, "Teachers" + ".bin"), FileMode.OpenOrCreate, FileAccess.Read))
            {
                TeacherEntry = new BinaryFormatter().Deserialize(s) as ETeachers;
            }
            using (var s = new FileStream(Path.Combine(DirectoryLocation, "Students" + ".bin"), FileMode.OpenOrCreate, FileAccess.Read))
            {
                StudentEntry = new BinaryFormatter().Deserialize(s) as EStudents;
            }
        }

        public void GetFromHdd_NewThread()
        {
            Thread thread = new Thread(new ThreadStart(GetFromHdd));
            thread.Name = "Get Contacts from Hdd";
            thread.IsBackground = false;
            thread.Start();
        }
    }
}
