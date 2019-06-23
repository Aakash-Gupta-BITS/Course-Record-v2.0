using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Course.Abstracts
{
    public abstract class ECourseElemItemBase
    {
        internal bool IsDeleted = false;
        internal readonly ListViewItem GetView = new ListViewItem() { HorizontalContentAlignment = HorizontalAlignment.Stretch };
        internal abstract object PointerOverObject { get; }
    }
}
