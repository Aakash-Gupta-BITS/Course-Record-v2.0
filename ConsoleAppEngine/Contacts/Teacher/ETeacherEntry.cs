using ConsoleAppEngine.Abstracts;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public class ETeacherEntry : EElementItemBase, ISerializable
    {
        #region Properties

        public string Name { get; private set; }
        public string[] Phone { get; private set; }
        public string[] Email { get; private set; }
        public string Address { get; private set; }
        public string Website { get; private set; }
        public string OtherInfo { get; private set; }

        #endregion

        #region DisplayItems
        internal readonly TextBlock NameViewBlock;
        internal readonly TextBlock SiteViewBlock;
        #endregion

        #region Serialization

        protected ETeacherEntry(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Name = (string)info.GetValue(nameof(Name), typeof(string));
            Phone = (info.GetValue(nameof(Phone), typeof(List<string>)) as List<string>).ToArray();
            Email = (info.GetValue(nameof(Email), typeof(List<string>)) as List<string>).ToArray();
            Address = (string)info.GetValue(nameof(Address), typeof(string));
            Website = (string)info.GetValue(nameof(Website), typeof(string));
            OtherInfo = (string)info.GetValue(nameof(OtherInfo), typeof(string));


            // YE COPY PASTE THA, isse upar ka dekh Dekh liya. to studen tmein implement kar
            // 
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 2));

            NameViewBlock = controls[0] as TextBlock;
            SiteViewBlock = controls[1] as TextBlock;

            NameViewBlock.Text = Name;
            SiteViewBlock.Text = Website;
            SiteViewBlock.IsTextSelectionEnabled = true;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name, typeof(string));
            info.AddValue(nameof(Phone), new List<string>(Phone), typeof(List<string>));
            info.AddValue(nameof(Email), new List<string>(Email), typeof(List<string>));
            info.AddValue(nameof(Address), Address, typeof(string));
            info.AddValue(nameof(Website), Website, typeof(string));
            info.AddValue(nameof(OtherInfo), OtherInfo, typeof(string));
        }

        #endregion


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
            SiteViewBlock.Text = Website;
            SiteViewBlock.IsTextSelectionEnabled = true;
        }

        
    }
}
