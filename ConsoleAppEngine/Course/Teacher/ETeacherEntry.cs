using ConsoleAppEngine.Abstracts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    public class ETeacherEntry : EElementItemBase
    {

        public string Name { get; private set; }
        public string[] Phone { get; private set; }
        public string[] Email { get; private set; }
        public string Address { get; private set; }
        public string Website { get; private set; }
        public string OtherInfo { get; private set; }

        internal readonly TextBlock NameViewBlock;
        internal readonly TextBlock SiteViewBlock;


        public ETeacherEntry(string name, string[] phone, string[] email, string address, string website, string otherInfo)
        {
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 2));

            NameViewBlock = controls[0] as TextBlock;
            SiteViewBlock = controls[1] as TextBlock;

            Update(name, phone, email, address, website, otherInfo);
        }

        internal void Update(string name, string[] phone, string[] email, string address, string website, string otherInfo)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            Website = website;
            OtherInfo = otherInfo;

            NameViewBlock.Text = Name;
            SiteViewBlock.Text = website;
            SiteViewBlock.IsTextSelectionEnabled = true;
        }

        internal override object PointerOverObject => null;
    }
}
