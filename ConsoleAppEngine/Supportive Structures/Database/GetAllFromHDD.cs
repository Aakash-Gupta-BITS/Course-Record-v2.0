using ConsoleAppEngine.Contacts;
using ConsoleAppEngine.Course;
using System;

namespace ConsoleAppEngine.Database
{
    public static class GetAllFromHDD
    {
        private static bool IsOperationCompleted = false;
        public static bool Completed => IsOperationCompleted;

        private static readonly AllCourses allCourses;
        private static readonly AllContacts allContacts;

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

        private static void OnOperationComplete(IAsyncResult asyncResult)
        {

            IsOperationCompleted = true;
        }
    }
}
