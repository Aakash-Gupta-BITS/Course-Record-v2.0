using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course
{
    public class ETestItem : EElementItemBase
    {
        public DateTime DayOfTest { get; private set; }
        public TestType TypeOfTest { get; private set; }
        public int TestIndex { get; private set; }
        public float MarksObtained { get; private set; }
        public float TotalMarks { get; private set; }
        public string Description { get; private set; }

        readonly TextBlock NameViewBlock;
        readonly TextBlock TimingsViewBlock;
        readonly TextBlock DescriptionViewBlock;
        readonly TextBlock MarksViewBlock;

        public ETestItem(DateTime dayOfTest, TestType typeOfTest, int testIndex, float marksObtained, float totalMarks, string description)
        {
            FrameworkElement[] controls = GenerateViews(GetView, (typeof(string), 1), (typeof(string), 1), (typeof(string), 2), (typeof(string), 1));

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

        internal override object PointerOverObject => null;
    }
}
