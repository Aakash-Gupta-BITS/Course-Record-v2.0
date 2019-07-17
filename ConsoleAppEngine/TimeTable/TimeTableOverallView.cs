using ConsoleAppEngine.Course;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.TimeTable
{
    public class TimeTableOverallView
    {
        public static readonly TimeTableOverallView Instance = new TimeTableOverallView();

        private ListView OverallView;
        private readonly LinkedList<TimeTableOverallItem> List = new LinkedList<TimeTableOverallItem>();

        private TimeTableOverallView() { }

        public void GenerateAllEntries(ListView View)
        {
            if (OverallView != null)
            {
                DestructViews();
            }

            OverallView = View;
            View.Items.Clear();
            foreach (CourseEntry e in AllCourses.Instance.CoursesList)
            {
                foreach (ETimeTableItem item in e.TimeEntry.lists)
                {
                    List.AddLast(new TimeTableOverallItem(e, item));
                }
            }

            foreach (var x in List)
            {
                View.Items.Add(x.GetView);
            }
        }

        public void DestructViews()
        {
            OverallView.Items.Clear();
            OverallView = null;

            List.Clear();
        }
    }
}
