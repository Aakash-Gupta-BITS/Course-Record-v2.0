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
        public static AllContacts Instance = new AllContacts();

        public ETeachers TeacherEntry { get; internal set; } = new ETeachers();
        public EStudents StudentEntry { get; internal set; } = new EStudents();

        private AllContacts()
        {

        }
    }
}
