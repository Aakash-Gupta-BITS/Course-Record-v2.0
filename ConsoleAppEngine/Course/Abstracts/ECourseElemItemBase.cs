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

        protected static FrameworkElement[] GenerateViews(ListViewItem GetView, params (Type t, double Width)[] Input)
        {
            Grid grid = new Grid();
            FrameworkElement[] controls = new FrameworkElement[Input.Length];

            for (int i = 0; i < Input.Length; ++i)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Input[i].Width, GridUnitType.Star) });
                if (Input[i].t.Equals(typeof(string)))
                    controls[i] = new TextBlock()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        TextWrapping = TextWrapping.Wrap
                    };
                else if (Input[i].t.Equals(typeof(bool)))
                    controls[i] = new CheckBox()
                    {
                        Content = "",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        MinWidth = 32
                    };
                else if (Input[i].t.Equals(typeof(HyperlinkButton)))
                    controls[i] = new HyperlinkButton()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                Grid.SetColumn(controls[i], i);
                grid.Children.Add(controls[i]);
            }
            GetView.Content = grid;
            return controls;
        }
    }
}
