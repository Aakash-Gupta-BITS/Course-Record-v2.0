using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using System;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public class ETestItem : EElementItemBase, ISerializable
    {
        #region Properties

        public DateTime DayOfTest { get; private set; }
        public TestType TypeOfTest { get; private set; }
        public int TestIndex { get; private set; }
        public float MarksObtained { get; private set; }
        public float TotalMarks { get; private set; }
        public string Description { get; private set; }

        #endregion

        #region DisplayItems

        private readonly TextBlock NameViewBlock;
        private readonly TextBlock TimingsViewBlock;
        private readonly TextBlock DescriptionViewBlock;
        private readonly TextBlock MarksViewBlock;

        #endregion

        #region Serialization

        protected ETestItem(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DayOfTest = (DateTime)info.GetValue(nameof(DayOfTest), typeof(DateTime));
            TypeOfTest = (TestType)info.GetValue(nameof(TypeOfTest), typeof(TestType));
            TestIndex = (int)info.GetValue(nameof(TestIndex), typeof(int));
            MarksObtained = (float)info.GetValue(nameof(MarksObtained), typeof(float));
            TotalMarks = (float)info.GetValue(nameof(TotalMarks), typeof(float));
            Description = (string)info.GetValue(nameof(Description), typeof(string));

            FrameworkElement[] controls = GenerateViews(ref GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 2), (typeof(string), 1));

            NameViewBlock = controls[0] as TextBlock;
            TimingsViewBlock = controls[1] as TextBlock;
            DescriptionViewBlock = controls[2] as TextBlock;
            MarksViewBlock = controls[3] as TextBlock;

            NameViewBlock.Text = TypeOfTest.ToString() + " " + TestIndex;
            TimingsViewBlock.Text = DayOfTest.ToString("dd/MM/yyyy");
            DescriptionViewBlock.Text = Description;
            MarksViewBlock.Text = MarksObtained + "/" + TotalMarks;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(DayOfTest), DayOfTest, typeof(DateTime));
            info.AddValue(nameof(TypeOfTest), TypeOfTest, typeof(TestType));
            info.AddValue(nameof(TestIndex), TestIndex, typeof(int));
            info.AddValue(nameof(MarksObtained), MarksObtained, typeof(float));
            info.AddValue(nameof(TotalMarks), TotalMarks, typeof(float));
            info.AddValue(nameof(Description), Description, typeof(string));
        }

        #endregion


        public ETestItem(DateTime dayOfTest, TestType typeOfTest, int testIndex, float marksObtained, float totalMarks, string description)
        {
            FrameworkElement[] controls = GenerateViews(ref GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 2), (typeof(string), 1));

            NameViewBlock = controls[0] as TextBlock;
            TimingsViewBlock = controls[1] as TextBlock;
            DescriptionViewBlock = controls[2] as TextBlock;
            MarksViewBlock = controls[3] as TextBlock;

            Update(dayOfTest, typeOfTest, testIndex, marksObtained, totalMarks, description);
        }

        internal void Update(DateTime dayOfTest, TestType typeOfTest, int testIndex, float marksObtained, float totalMarks, string description)
        {
            DayOfTest = dayOfTest;
            TypeOfTest = typeOfTest;
            TestIndex = testIndex;
            MarksObtained = marksObtained;
            TotalMarks = totalMarks;
            Description = description;

            NameViewBlock.Text = TypeOfTest.ToString() + " " + TestIndex;
            TimingsViewBlock.Text = DayOfTest.ToString("dd/MM/yyyy");
            DescriptionViewBlock.Text = Description;
            MarksViewBlock.Text = MarksObtained + "/" + TotalMarks;
        }


    }
}
