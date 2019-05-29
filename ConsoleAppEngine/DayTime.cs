using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    public class DayTime : IComparable
    {
        private readonly DateTime dateTime;
        private readonly DateTime finalDateTime;
        public DayOfWeek DayOfWeek => dateTime.DayOfWeek;
        public int StartHour => dateTime.Hour;
        public int StartMinute => dateTime.Minute;
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
        public DayTime Copy() => new DayTime(dateTime, finalDateTime);
        private void Verify()
        {
            if (finalDateTime < dateTime)
                throw new ArgumentException(string.Format("Initial time : {0} is greater than Final time : {1}", StartHour + ":" + StartMinute, EndHour + ":" + EndMinute));
        }

        // Changes
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

        // Comparison
        public bool Intersect(DayTime dt)
        {
            if (DayOfWeek != dt.DayOfWeek)
                return false;

            if ((dateTime < dt.finalDateTime && dateTime > dt.dateTime) ||
                (finalDateTime < dt.finalDateTime && finalDateTime > dt.dateTime) ||
                (dt.dateTime < finalDateTime && dt.dateTime > dateTime) ||
                (dt.finalDateTime < finalDateTime && dt.finalDateTime > dateTime))
                return true;

            return false;
        }
        public static bool Intersect(DayTime lhs, DayTime rhs) => lhs.Intersect(rhs);
        public static bool operator <(DayTime lhs, DayTime rhs) => lhs.dateTime < rhs.dateTime;
        public static bool operator>(DayTime lhs, DayTime rhs) => lhs.dateTime > rhs.dateTime;
        public static bool operator ==(DayTime lhs, DayTime rhs) => lhs.Equals(rhs);
        public static bool operator !=(DayTime lhs, DayTime rhs) => !lhs.Equals(rhs);

        public int CompareTo(object obj)
        {
            DayTime dt = obj as DayTime;
            if (dt == null)
                throw new Exception();

            if (this < dt)
                return -1;
            else if (this > dt)
                return 1;
            else if (this == dt)
                return 0;
            return -2;
        }

        public override string ToString() =>
            string.Format("{0}\t{1}:{2}\t{3}:{4}",
                DayOfWeek,
                StartHour < 10 ? "0" + StartHour : StartHour.ToString(),
                StartMinute < 10 ? "0" + StartMinute : StartMinute.ToString(),
                EndHour < 10 ? "0" + EndHour : EndHour.ToString(),
                EndMinute < 10 ? "0" + EndMinute : EndMinute.ToString());
        public override bool Equals(object obj)
        {
            return obj is DayTime time &&
                   DayOfWeek == time.DayOfWeek &&
                   StartHour == time.StartHour &&
                   StartMinute == time.StartMinute &&
                   EndHour == time.EndHour &&
                   EndMinute == time.EndMinute;
        }
        public override int GetHashCode()
        {
            var hashCode = -60057006;
            hashCode = hashCode * -1521134295 + DayOfWeek.GetHashCode();
            hashCode = hashCode * -1521134295 + StartHour.GetHashCode();
            hashCode = hashCode * -1521134295 + StartMinute.GetHashCode();
            hashCode = hashCode * -1521134295 + EndHour.GetHashCode();
            hashCode = hashCode * -1521134295 + EndMinute.GetHashCode();
            return hashCode;
        }

    }
}