using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    class TimeTable<T> where T : AbstractTimeEntry
    {
        public readonly LinkedList<TimeTableNode<T>> List = new LinkedList<TimeTableNode<T>>();

        public TimeTable(TimeTableNode<T> val) => List.AddLast(val);
        public void Add(TimeTableNode<T> val)
        {
            foreach (TimeTableNode<T> temp in List)
                if (temp.Intersect(val))
                    throw new Exception("Time collides with pre existing time");

            if (val > List.Last.Value)
                List.AddLast(val);
            else if (val < List.First.Value)
                List.AddFirst(val);
            else
            {
                int IndexToAdd = 0;
                TimeTableNode<T>[] arr = List.ToArray();
                for (int i = 0; i < arr.Length - 1; ++i)
                    if (val > arr[i] && val < arr[i + 1])
                    {
                        IndexToAdd = i;
                        break;
                    }

                List.Clear();
                for (int i = 0; i <= IndexToAdd; ++i)
                    List.AddLast(arr[i]);
                List.AddLast(val);
                for (int i = IndexToAdd + 1; i < arr.Length; ++i)
                    List.AddLast(arr[i]);
            }
        }


        private LinkedList<TimeTableNode<T>> GenerateList(Func<TimeTableNode<T>, bool> function)
        {
            LinkedList<TimeTableNode<T>> result = new LinkedList<TimeTableNode<T>>();

            foreach (TimeTableNode<T> temp in List)
                switch (temp.value)
                {
                    case CourseTimeEntry ct:
                        if (function(temp))
                            result.AddLast(temp);
                        break;
                }
            return result;
        }
        public LinkedList<TimeTableNode<T>> GetCourseTimeByLocation(RoomLocation location) => GenerateList((TimeTableNode<T> temp) => (temp.value as CourseTimeEntry).Location.Equals(location));
        public LinkedList<TimeTableNode<T>> GetCourseTimeByDay(DayOfWeek day) => GenerateList((TimeTableNode<T> temp) => temp.DayTime.DayOfWeek == day);
        public LinkedList<TimeTableNode<T>> GetCourseTimeByCourse(Course c) => GenerateList((TimeTableNode<T> temp) => (temp.value as CourseTimeEntry).Course.Id == c.Id);


        public TimeTableNode<T>[] ToArray() => List.ToArray();
    }
}