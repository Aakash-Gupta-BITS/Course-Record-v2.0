using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    class TimeTableNode<T> where T : AbstractTimeEntry
    {
        public readonly DayTime DayTime;
        public readonly T value;

        public TimeTableNode(DayTime dt, T VAL)
        {
            DayTime = dt.Copy();
            value = VAL;
        }


        public static bool Intersect(TimeTableNode<T> lhs, TimeTableNode<T> rhs) => lhs.DayTime.Intersect(rhs.DayTime);
        public bool Intersect(TimeTableNode<T> t) => Intersect(this, t);
        public static bool operator <(TimeTableNode<T> lhs, TimeTableNode<T> rhs) => lhs.DayTime < rhs.DayTime;
        public static bool operator >(TimeTableNode<T> lhs, TimeTableNode<T> rhs) => lhs.DayTime > rhs.DayTime;
    }
}