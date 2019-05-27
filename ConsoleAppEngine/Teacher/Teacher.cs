using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine
{
    public class Teacher
    {
        public readonly string Name;
        public readonly LinkedList<string> PhoneNumbers = new LinkedList<string>();
        public readonly LinkedList<string> EmailId = new LinkedList<string>();
        public readonly RoomLocation ChamberLocation = new RoomLocation();
        public readonly DayTime ChamberTiming;
        public LinkedList<Course> CoursesMaintained => throw new NotImplementedException();
        public LinkedList<CourseTimeEntry> TimeTableEntries => throw new NotImplementedException();

        public Teacher(string name, RoomLocation roomLocation = null,  DayTime ChamberTimings = null)
        {
            Name = name;
            ChamberLocation = roomLocation;
            ChamberTiming = ChamberTimings;
        }

        public void AddPhone(string input)
        {
            if (!PhoneNumbers.Contains(input))
                PhoneNumbers.AddLast(input);
        }
        public void AddEmail(string email)
        {
            email = email.Replace(" ", "");
            if (!EmailId.Contains(email))
                EmailId.AddLast(email);
        }
    }
}
