using ConsoleAppEngine.Abstracts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    public class EHandoutItem : EElementItemBase
    {
        public int LectureNo { get; private set; }
        public string Topic { get; private set; }
        public bool DoneByMe { get; internal set; }
        public string Description { get; private set; }

        internal readonly TextBlock LectureViewBlock;
        internal readonly TextBlock TopicViewBlock;
        internal readonly CheckBox DoneViewBox;

        public EHandoutItem(int lectureNo, string topic, bool doneByMe, string description)
        {
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 3), (typeof(bool), 0.5));

            LectureViewBlock = controls[0] as TextBlock;
            TopicViewBlock = controls[1] as TextBlock;
            DoneViewBox = controls[2] as CheckBox;

            Update(lectureNo, topic, doneByMe, description);

            DoneViewBox.Click += (object sender, RoutedEventArgs e) => DoneByMe = DoneViewBox.IsChecked == true ? true : false;
        }

        internal void Update(int lectureNo, string topic, bool doneByMe, string description)
        {
            LectureNo = lectureNo;
            Topic = topic;
            DoneByMe = doneByMe;
            Description = description;

            LectureViewBlock.Text = LectureNo.ToString();
            TopicViewBlock.Text = topic;
            DoneViewBox.IsChecked = DoneByMe;
        }

        internal override object PointerOverObject => DoneViewBox;
    }
}