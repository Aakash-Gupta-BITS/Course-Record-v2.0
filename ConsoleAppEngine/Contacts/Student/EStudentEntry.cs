using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    public class EStudentEntry : EElementItemBase
    {
        public string Name { get; private set; }
        public int Year { get; private set; }
        public ExpandedBranch[] Branch { get; private set; }
        public int Digits { get; private set; }
        public string[] Phone { get; private set; }
        public string PersonalMail { get; private set; }
        public string Hostel { get; private set; }
        public int Room { get; private set; }
        public string OtherInfo { get; private set; }

        internal readonly TextBlock NameViewBlock;
        internal readonly TextBlock PhoneViewBlock;
        internal readonly TextBlock EmailViewBlock;

        public EStudentEntry(string name, (int year, ExpandedBranch[] branch, int digits) id, string[] phone, string personalMail, string hostel, int room, string other)
        {
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));
            NameViewBlock = controls[0] as TextBlock;
            PhoneViewBlock = controls[1] as TextBlock;
            EmailViewBlock = controls[2] as TextBlock;

            Update(name, id, phone, personalMail, hostel, room, other);
        }

        public void Update(string name, (int year, ExpandedBranch[] branch, int digits) id, string[] phone, string personalMail, string hostel, int room, string other)
        {
            Name = name;
            Year = id.year;
            Branch = id.branch;
            Digits = id.digits;
            Phone = phone;
            PersonalMail = personalMail;
            Hostel = hostel;
            Room = room;
            OtherInfo = other;

            NameViewBlock.Text = Name;
            PhoneViewBlock.Text = Phone.Length > 0 ? Phone[0] : "";

            if (Year != 0)
            {
                EmailViewBlock.Text = PersonalMail == "" ? string.Format(@"f{0}{1}@pilani.bits-pilani.ac.in", Year, Digits.ToString().PadLeft(4, '0')) : PersonalMail;
            }
            else
            {
                EmailViewBlock.Text = PersonalMail;
            }
        }

        internal override object PointerOverObject => null;
    }
}