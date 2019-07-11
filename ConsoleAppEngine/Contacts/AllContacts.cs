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
        public readonly ETeachers TeacherEntry = new ETeachers();
        public readonly EStudents StudentEntry = new EStudents();




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
           
            DirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Contacts");

            if (!Directory.Exists(DirectoryLocation))
            {
                Directory.CreateDirectory(DirectoryLocation);
            }

            
            
                using (Stream m = new FileStream(Path.Combine(DirectoryLocation, "Students" + ".bin"), FileMode.Create, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(m, StudentEntry);
                }
            
        }
    }
}
