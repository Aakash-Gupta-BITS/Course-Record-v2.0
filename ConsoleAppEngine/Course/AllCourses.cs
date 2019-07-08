using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Windows.Storage;

namespace ConsoleAppEngine.Course
{
    public class AllCourses
    {
        public readonly LinkedList<CourseEntry> CoursesList = new LinkedList<CourseEntry>();

        public void AddCourse(CourseEntry e)
        {
            if (!Consistent(e))
            {
                throw new Exception();
            }

            CoursesList.AddLast(e);
            var list = CoursesList.OrderBy(a => a.Title.ToUpper()).ToArray();
            CoursesList.Clear();

            foreach (var y in list)
            {
                CoursesList.AddLast(y);
            }
        }

        public bool Consistent(CourseEntry e)
        {
            foreach (var y in CoursesList)
            {
                if (y.Title == e.Title ||
                    (y.ID.branchstring == e.ID.branchstring && y.ID.branchtype == e.ID.branchtype))
                {
                    return false;
                }
            }
            return true;
        }

        public void AddToHdd()
        {
            string DirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Courses");

            if (!Directory.Exists(DirectoryLocation))
            {
                Directory.CreateDirectory(DirectoryLocation);
            }

            foreach (CourseEntry e in CoursesList)
            {
                using (Stream m = new FileStream(Path.Combine(DirectoryLocation, e.Title + ".bin"), FileMode.Create, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(m, e);
                }
            }
        }

        public void AddToHdd_NewThread()
        {
            Thread thread = new Thread(new ThreadStart(AddToHdd));
            thread.Name = "Add Courses to Hdd";
            thread.IsBackground = false;
            thread.Start();
        }

        public void GetFromHdd()
        {
            string DirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Courses");

            if (!Directory.Exists(DirectoryLocation))
            {
                return;
            }

            BinaryFormatter formatter = new BinaryFormatter();

            foreach (string file in Directory.GetFiles(DirectoryLocation))
            {
                using (var s = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    CoursesList.AddLast(formatter.Deserialize(s) as CourseEntry);
                }
            }
        }

        public void GetFromHdd_NewThread()
        {
            Thread thread = new Thread(new ThreadStart(GetFromHdd));
            thread.Name = "Add Courses to Hdd";
            thread.IsBackground = false;
            thread.Start();
        }
    }
}
