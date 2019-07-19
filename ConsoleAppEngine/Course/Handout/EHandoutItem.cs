using ConsoleAppEngine.Abstracts;
using System;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public class EHandoutItem : EElementItemBase, ISerializable
    {
        #region Properties

        public int LectureNo { get; private set; }
        public string Topic { get; private set; }
        public bool DoneByMe { get; internal set; }
        public string Description { get; private set; }

        #endregion

        #region DisplayItems

        internal TextBlock LectureViewBlock { get; private set; }
        internal TextBlock TopicViewBlock { get; private set; }
        internal CheckBox DoneViewBox { get; private set; }

        #endregion

        #region Serialization

        protected EHandoutItem(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            LectureNo = (int)info.GetValue(nameof(LectureNo), typeof(int));
            Topic = (string)info.GetValue(nameof(Topic), typeof(string));
            DoneByMe = (bool)info.GetValue(nameof(DoneByMe), typeof(bool));
            Description = (string)info.GetValue(nameof(Description), typeof(string));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(LectureNo), LectureNo, typeof(int));
            info.AddValue(nameof(Topic), Topic, typeof(string));
            info.AddValue(nameof(DoneByMe), DoneByMe, typeof(bool));
            info.AddValue(nameof(Description), Description, typeof(string));
        }

        #endregion

        public EHandoutItem(int lectureNo, string topic, bool doneByMe, string description)
        {
            UpdateData(lectureNo, topic, doneByMe, description);
        }

        internal void UpdateData(int lectureNo, string topic, bool doneByMe, string description)
        {
            LectureNo = lectureNo;
            Topic = topic;
            DoneByMe = doneByMe;
            Description = description;
        }

        internal void UpdateDataWithViews(int lectureNo, string topic, bool doneByMe, string description)
        {
            UpdateData(lectureNo, topic, doneByMe, description);
            UpdateViews();
        }

        internal override void InitializeViews()
        {
            FrameworkElement[] controls = GenerateViews(ref GetView, (typeof(string), 1), (typeof(string), 3), (typeof(bool), 0.5));

            LectureViewBlock = controls[0] as TextBlock;
            TopicViewBlock = controls[1] as TextBlock;
            DoneViewBox = controls[2] as CheckBox;

            DoneViewBox.Click += (object sender, RoutedEventArgs e) => DoneByMe = DoneViewBox.IsChecked == true ? true : false;

            UpdateViews();
        }

        internal override void UpdateViews()
        {
            LectureViewBlock.Text = LectureNo.ToString();
            TopicViewBlock.Text = Topic;
            DoneViewBox.IsChecked = DoneByMe;
        }

        internal override void DestructViews()
        {
            base.DestructViews();

            LectureViewBlock = null;
            TopicViewBlock = null;
            DoneViewBox = null;
        }
    }
}