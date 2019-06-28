using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ConsoleAppEngine.Course.Abstracts;
using ConsoleAppEngine.AllEnums;

namespace ConsoleAppEngine.Course
{
    public class EEventItem : ECourseElemItemBase
    {
        public string Title { get; private set; }
        public DateTime Timing { get; private set; }
        public string Location { get; private set; }
        public string Description { get; private set; }

        internal readonly TextBlock TitleViewBlock;
        internal readonly TextBlock TimingViewBlock;
        internal readonly TextBlock LocationViewBlock;

        public EEventItem(string title, DateTime timing, string location, string description)
        {
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 2), (typeof(string), 1), (typeof(string), 1));
            TitleViewBlock = controls[0] as TextBlock;
            TimingViewBlock = controls[1] as TextBlock;
            LocationViewBlock = controls[2] as TextBlock;

            Update(title, timing, location, description);
        }

        internal void Update(string title, DateTime timing, string location, string description)
        {
            Title = title;
            Timing = timing;
            Location = location;
            Description = description;

            TitleViewBlock.Text = Title;
            TimingViewBlock.Text = Timing.ToString("MMM. dd, yyyy HH:mm");
            LocationViewBlock.Text = Location;
        }

        internal override object PointerOverObject => null;
    }
}
