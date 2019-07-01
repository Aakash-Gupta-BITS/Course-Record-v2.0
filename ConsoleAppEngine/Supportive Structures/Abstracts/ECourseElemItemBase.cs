using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Abstracts
{
    public abstract class EElementItemBase
    {
        /// <summary>
        /// When an item is removed, it is set true and GC will collect later
        /// </summary>
        public bool IsDeleted = false;
        /// <summary>
        /// ListViewItem to be displayed in View
        /// </summary>
        public readonly ListViewItem GetView = new ListViewItem() { HorizontalContentAlignment = HorizontalAlignment.Stretch };
        /// <summary>
        /// An item in GetView, when clicked on it, will not display contentDialog
        /// </summary>
        internal abstract object PointerOverObject { get; }

        /// <summary>
        /// Fill the given ListViewItem with current Entry and returns array of controls added
        /// </summary>
        /// <param name="GetView">ViewItem in which the current object view will be filled.</param>
        /// <param name="Input">Decides the type of view and the width in * of Grid.Column.</param>
        /// <returns></returns>
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
                else if (Input[i].t.Equals(typeof(HyperlinkButton)))
                {
                    controls[i] = new HyperlinkButton()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                }

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
