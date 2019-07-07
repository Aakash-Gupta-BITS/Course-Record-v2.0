using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;

namespace ConsoleAppEngine.Database
{
    public static class GetAllFromHDD
    {
        private static bool IsOperationCompleted = false;
        public static bool Completed => IsOperationCompleted;

        static AllCourses allCourses;
        static AllContacts allContacts;

        private delegate Tuple<AllCourses, AllContacts> GetOperation();

        public static void Initialize()
        {
            GetOperation op = new GetOperation(GetFromHDD);

            IAsyncResult result = op.BeginInvoke(new AsyncCallback(OnOperationComplete), null);
        }

        private static Tuple<AllCourses, AllContacts> GetFromHDD()
        {
            return new Tuple<AllCourses, AllContacts>(allCourses, allContacts);
        }

        static void OnOperationComplete(IAsyncResult asyncResult)
        {

            IsOperationCompleted = true;
        }
    }
}
