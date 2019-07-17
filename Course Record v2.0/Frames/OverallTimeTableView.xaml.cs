using ConsoleAppEngine.TimeTable;
using Windows.UI.Xaml.Controls;

namespace Course_Record_v2._0.Frames
{
    public sealed partial class OverallTimeTableView : Page
    {
        public OverallTimeTableView()
        {
            this.InitializeComponent();
            TimeTableOverallView view = new TimeTableOverallView();
            view.InitializeList();
            view.Sort();
            view.InitializeViews();
            foreach (var x in view.ViewItems)
            {
                ViewList.Items.Add(x);
            }
        }
    }
}
