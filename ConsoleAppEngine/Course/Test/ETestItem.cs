using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ConsoleAppEngine.Course.Abstracts;
using ConsoleAppEngine.AllEnums;

namespace ConsoleAppEngine.Course
{
    class ETestItem
    {
        public DateTime DayOfTest { get; private set; }
        public TestType TypeOfTest { get; private set; }
        public float MarkksObtained { get; private set; }
        public float TotalMarks { get; private set; }
        public string Description { get; private set; }

        
    }
}
