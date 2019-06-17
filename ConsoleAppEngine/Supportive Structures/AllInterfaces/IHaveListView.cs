using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Collections;

namespace ConsoleAppEngine.AllInterfaces
{
    public abstract class HaveList<T>
    {
        public LinkedList<T> List { get; set; }
        private LinkedList<ListViewItem> ViewItem;

        private void ResetViewList()
        {
            ViewItem = new LinkedList<ListViewItem>();
            foreach (T item in List)
                ViewItem.AddLast(new ListViewItem() { Content = item.ToString() });
        }
        

        IEnumerator GetEnumerator() => ViewItem.GetEnumerator();

    }
}
