using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using Windows.Storage;

namespace ConsoleAppEngine
{
    public class Teacher
    {
        public readonly string Name;
        public readonly LinkedList<string> PhoneNumbers = new LinkedList<string>();
        public readonly LinkedList<string> EmailId = new LinkedList<string>();
        public readonly RoomLocation ChamberLocation = new RoomLocation();
        public readonly DayTime ChamberTiming;

        internal CheckBox DisplayListViewItem = new CheckBox();
        public bool? IsSelected => DisplayListViewItem.IsChecked;

        public Teacher(string name, RoomLocation roomLocation = null,  DayTime ChamberTimings = null)
        {
            Name = name;
            ChamberLocation = roomLocation;
            ChamberTiming = ChamberTimings;
            DisplayListViewItem.Content = Name;
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
