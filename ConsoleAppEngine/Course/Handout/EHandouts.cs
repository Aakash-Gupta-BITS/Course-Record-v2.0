using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace ConsoleAppEngine
{
    public class EHandouts
    {
        LinkedList<EHandoutItem> lists = new LinkedList<EHandoutItem>();
        ListView viewlist;

        public void ChangeListView(ListView l)
        {
            if (viewlist != null)
                viewlist.Items.Clear();

            viewlist = l;
            foreach (EHandoutItem e in lists)
                if (e.IsDeleted != true)
                    e.GenerateTappedEvent(viewlist);

            UpdateList();
        }

        public void AddHandout(TextBox Lecture, TextBox Topic, TextBox Description, CheckBox Done, Button Add)
        {
            int lecture;
            if (!int.TryParse(Lecture.Text, out lecture))
            {
                Lecture.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                return;
            }
            else
                Lecture.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));

            foreach (int lec in (from a in lists where a.LectureNo == lecture select a.LectureNo).ToArray())
            {
                Add.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                return;
            }

            string topic = Topic.Text;
            string desc = Description.Text;
            bool done = Done.IsChecked == true;

            lists.AddLast(new EHandoutItem(lecture, topic, done, desc));
            Add.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
        }

        public void AddHandout(EHandoutItem handoutItem)
        {
            lists.AddLast(handoutItem);
            UpdateList();
        }

        public void UpdateList()
        {
            if (viewlist == null) return;
            var query = (from x in lists where x.IsDeleted == true select x).ToArray();
            foreach (var a in (query))
                lists.Remove(a);
            FillListView();
        }

        private void FillListView()
        {
            viewlist.Items.Clear();
            foreach (var a in (from a in lists where a.IsDeleted == false select a.GetView))
                viewlist.Items.Add(a);
        }
    }
}
