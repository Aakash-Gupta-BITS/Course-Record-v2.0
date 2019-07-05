using System;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Abstracts
{
    [Serializable]
    public abstract class EElementItemBase
    {
        [NonSerialized]
        public bool IsDeleted = false;
        [NonSerialized]
        public readonly ListViewItem GetView = new ListViewItem() { HorizontalContentAlignment = HorizontalAlignment.Stretch };
        internal abstract object PointerOverObject { get; }

        protected static FrameworkElement[] GenerateViews(ListViewItem GetView, params (Type t, double Width)[] Input)
        {
            // This grid will be added in GetView beause 
            // 1. We have multiple controls to be added in single ListViewItem
            // 2. Controls will be separated on basis of width of columns
            Grid grid = new Grid();
            FrameworkElement[] controls = new FrameworkElement[Input.Length];

            for (int i = 0; i < Input.Length; ++i)
            {
                // Add Column for current control
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Input[i].Width, GridUnitType.Star) });

                // Current Control is TextBox
                if (Input[i].t.Equals(typeof(string)))
                {
                    controls[i] = new TextBlock()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        TextWrapping = TextWrapping.Wrap
                    };
                }
                // Current control is CheckBox
                else if (Input[i].t.Equals(typeof(bool)))
                {
                    controls[i] = new CheckBox()
                    {
                        Content = "",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        MinWidth = 32
                    };
                }
                // Current control is Hyperlink (Not used anywhere, but can be used later)
               /* else if (Input[i].t.Equals(typeof(HyperlinkButton)))
                {
                    controls[i] = new HyperlinkButton()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                }*/

                // Add Control to grid
                Grid.SetColumn(controls[i], i);
                grid.Children.Add(controls[i]);
            }

            // Add Grid to ViewItem
            GetView.Content = grid;

            // Return the controls generated
            return controls;
        }
    }
}
