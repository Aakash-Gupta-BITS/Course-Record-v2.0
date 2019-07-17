﻿using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public class EStudentEntry : EElementItemBase, ISerializable
    {
        #region Properties
        public string Name { get; private set; }
        public int Year { get; private set; }
        public ExpandedBranch[] Branch { get; private set; }
        public int Digits { get; private set; }
        public string[] Phone { get; private set; }
        public string PersonalMail { get; private set; }
        public string Hostel { get; private set; }
        public int Room { get; private set; }
        public string OtherInfo { get; private set; }
        #endregion

        #region DisplayItems
        internal readonly TextBlock NameViewBlock;
        internal readonly TextBlock PhoneViewBlock;
        internal readonly TextBlock EmailViewBlock;
        #endregion

        #region Serialization
        protected EStudentEntry(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Name = (string)info.GetValue(nameof(Name), typeof(string));
            Year = (int)info.GetValue(nameof(Year), typeof(int));
            Branch = (info.GetValue(nameof(Branch), typeof(List<ExpandedBranch>)) as List<ExpandedBranch>).ToArray();
            Digits = (int)info.GetValue(nameof(Digits), typeof(int));
            Phone = (info.GetValue(nameof(Phone), typeof(List<string>)) as List<string>).ToArray();
            PersonalMail = (string)info.GetValue(nameof(PersonalMail), typeof(string));
            Hostel = (string)info.GetValue(nameof(Hostel), typeof(string));
            Room = (int)info.GetValue(nameof(Room), typeof(int));
            OtherInfo = (string)info.GetValue(nameof(OtherInfo), typeof(string));



            #region DND
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 1));
            NameViewBlock = controls[0] as TextBlock;
            PhoneViewBlock = controls[1] as TextBlock;
            EmailViewBlock = controls[2] as TextBlock;

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
            #endregion
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name, typeof(string));
            info.AddValue(nameof(Year), Year, typeof(int));
            info.AddValue(nameof(Branch), new List<ExpandedBranch>(Branch), typeof(List<ExpandedBranch>));
            info.AddValue(nameof(Digits), Digits, typeof(int));
            info.AddValue(nameof(Phone), new List<string>(Phone), typeof(List<string>));
            info.AddValue(nameof(PersonalMail), PersonalMail, typeof(string));
            info.AddValue(nameof(Hostel), Hostel, typeof(string));
            info.AddValue(nameof(Room), Room, typeof(int));
            info.AddValue(nameof(OtherInfo), OtherInfo, typeof(string));

        }

        #endregion

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


    }
}