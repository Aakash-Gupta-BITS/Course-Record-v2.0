using ConsoleAppEngine.Abstracts;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Contacts
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

        internal TextBlock NameViewBlock { get; private set; }
        internal TextBlock SiteViewBlock { get; private set; }

        #endregion

        #region Serialization

        protected ETeacherEntry(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Name = (string)
                info.GetValue(nameof(Name), typeof(string));
            Phone = (string[])
                info.GetValue(nameof(Phone), typeof(string[]));
            Email = (string[])
                info.GetValue(nameof(Email), typeof(string[]));
            Address = (string)
                info.GetValue(nameof(Address), typeof(string));
            Website = (string)
                info.GetValue(nameof(Website), typeof(string));
            OtherInfo = (string)
                info.GetValue(nameof(OtherInfo), typeof(string));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name, typeof(string));
            info.AddValue(nameof(Phone), Phone, typeof(string[]));
            info.AddValue(nameof(Email), Email, typeof(string[]));
            info.AddValue(nameof(Address), Address, typeof(string));
            info.AddValue(nameof(Website), Website, typeof(string));
            info.AddValue(nameof(OtherInfo), OtherInfo, typeof(string));
        }

        #endregion

        public ETeacherEntry(string name, string[] phone, string[] email, string address, string website, string otherInfo)
        {
            UpdateData(name, phone, email, address, website, otherInfo);
        }

        internal void UpdateData(string name, string[] phone, string[] email, string address, string website, string otherInfo)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            Website = website;
            OtherInfo = otherInfo;
        }

        internal void UpdateDataWithViews(string name, string[] phone, string[] email, string address, string website, string otherInfo)
        {
            UpdateData(name, phone, email, address, website, otherInfo);
            UpdateViews();
        }

        internal override void InitializeViews()
        {
            FrameworkElement[] controls = GenerateViews(ref GetView, (typeof(string), 1), (typeof(string), 2));

            NameViewBlock = controls[0] as TextBlock;
            SiteViewBlock = controls[1] as TextBlock;
            SiteViewBlock.IsTextSelectionEnabled = true;

            UpdateViews();
        }

        internal override void UpdateViews()
        {
            NameViewBlock.Text = Name;
            SiteViewBlock.Text = Website;
        }

        internal override void DestructViews()
        {
            base.DestructViews();

            NameViewBlock = null;
            SiteViewBlock = null;
        }

        public void InitializeTeacher()
        {
            InitializeViews();
        }

        public void DestroyTeacherViews()
        {
            DestructViews();
        }
    }
}