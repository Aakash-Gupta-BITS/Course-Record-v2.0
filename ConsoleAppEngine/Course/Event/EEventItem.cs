using ConsoleAppEngine.Abstracts;
using System;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public class EEventItem : EElementItemBase, ISerializable
    {
        #region Properties

        public string Title { get; private set; }
        public DateTime Timing { get; private set; }
        public string Location { get; private set; }
        public string Description { get; private set; }

        #endregion

        #region DisplayItems

        internal TextBlock TitleViewBlock { get; private set; }
        internal TextBlock TimingViewBlock { get; private set; }
        internal TextBlock LocationViewBlock { get; private set; }

        #endregion

        #region Serialization

        protected EEventItem(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Title = (string)info.GetValue(nameof(Title), typeof(string));
            Timing = (DateTime)info.GetValue(nameof(Timing), typeof(DateTime));
            Location = (string)info.GetValue(nameof(Location), typeof(string));
            Description = (string)info.GetValue(nameof(Description), typeof(string));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Title), Title, typeof(string));
            info.AddValue(nameof(Timing), Timing, typeof(DateTime));
            info.AddValue(nameof(Location), Location, typeof(string));
            info.AddValue(nameof(Description), Description, typeof(string));
        }

        #endregion

        #region Parameters


        #endregion

        public EEventItem(string title, DateTime timing, string location, string description)
        {
            UpdateData(title, timing, location, description);
        }

        internal void UpdateData(string title, DateTime timing, string location, string description)
        {
            Title = title;
            Timing = timing;
            Location = location;
            Description = description;
        }

        internal void UpdateDataWithViews(string title, DateTime timing, string location, string description)
        {
            UpdateData(title, timing, location, description);
            UpdateViews();
        }

        internal override void InitializeViews() 
        {
            FrameworkElement[] controls = GenerateViews(ref GetView, (typeof(string), 2), (typeof(string), 1), (typeof(string), 1));

            TitleViewBlock = controls[0] as TextBlock;
            TimingViewBlock = controls[1] as TextBlock;
            LocationViewBlock = controls[2] as TextBlock;

            UpdateViews();
        }

        internal override void UpdateViews()
        {
            TitleViewBlock.Text = Title;
            TimingViewBlock.Text = Timing.ToString("MMM. dd, yyyy HH:mm");
            LocationViewBlock.Text = Location;
        }

        internal override void DestructViews()
        {
            base.DestructViews();

            TitleViewBlock = null;
            TimingViewBlock = null;
            Location = null;
        }
    }
}
