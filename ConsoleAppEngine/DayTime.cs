using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    public class DayTime
    {
        private readonly DateTime dateTime;
        private readonly DateTime finalDateTime;

        public DayOfWeek DayOfWeek => dateTime.DayOfWeek;
        public int Hour => dateTime.Hour;
        public int Minute => dateTime.Minute;
        public int EndHour => (finalDateTime).Hour;
        public int EndMinute => finalDateTime.Minute;

        public DayTime(DateTime dt, TimeSpan sp)
        {
            dateTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            finalDateTime = dateTime + sp;
            Verify();
        }
        public DayTime(DayOfWeek dayOfWeek, int Hour, int Minute, TimeSpan sp)
        {
            dateTime = new DateTime(DateTime.Now.Year, 1, 1, Hour, Minute, 0);
            while (dateTime.DayOfWeek != dayOfWeek)
                dateTime.AddDays(1);
            finalDateTime = dateTime + sp;
            Verify();
        }
        private DayTime(DateTime dt1, DateTime dt2)
        {
            dateTime = dt1;
            finalDateTime = dt2;
        }
        private void Verify()
        {
            if (finalDateTime < dateTime)
                throw new ArgumentException(string.Format("Initial time : {0} is greater than Final time : {1}", Hour + ":" + Minute, EndHour + ":" + EndMinute));
        }

        public DayTime Copy() => new DayTime(dateTime, finalDateTime);

        public void ChangeDay(DayOfWeek newDayOfWeek)
        {
            while (DayOfWeek != newDayOfWeek)
            {
                dateTime.AddDays(1);
                finalDateTime.AddDays(1);
            }
        }
        public void ShiftHoursToRight(int hours_to_shift_right)
        {
            dateTime.AddHours(hours_to_shift_right);
            finalDateTime.AddHours(hours_to_shift_right);
        }

        public bool Intersect(DayTime dt)
        {
            if (DayOfWeek != dt.DayOfWeek)
                return false;

            if ((dateTime < dt.finalDateTime && dateTime > dt.dateTime) || (finalDateTime < dt.finalDateTime && finalDateTime > dt.dateTime))
                return true;

            return false;
        }

        public static bool Intersect(DayTime lhs, DayTime rhs) => lhs.Intersect(rhs);

        public static bool operator <(DayTime lhs, DayTime rhs) => lhs.dateTime < rhs.dateTime;
        public static bool operator>(DayTime lhs, DayTime rhs) => lhs.dateTime > rhs.dateTime;
    }
}