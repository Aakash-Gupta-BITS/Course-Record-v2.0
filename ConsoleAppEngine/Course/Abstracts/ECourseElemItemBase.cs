using System;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course.Abstracts
{
    public abstract class ECourseElemItemBase : IComparable
    {
        internal bool IsDeleted = false;
        internal readonly ListViewItem GetView = new ListViewItem();
        internal abstract object PointerOverObject { get; }

        public abstract int CompareTo(object obj);
    }
}
