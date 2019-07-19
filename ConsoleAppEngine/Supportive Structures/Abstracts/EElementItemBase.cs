using System;
using System.Runtime.Serialization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ConsoleAppEngine.Abstracts
{
    [Serializable]
    public abstract class EElementItemBase : ISerializable
    {
        #region Properties

        public bool IsDeleted;
        public ListViewItem GetView;
        

        #endregion

        public EElementItemBase()
        {
            GetView = new ListViewItem() { HorizontalContentAlignment = HorizontalAlignment.Stretch };
            IsDeleted = false;
        }

        #region Serialization

        protected EElementItemBase(SerializationInfo info, StreamingContext context) : this()
        {

        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }

        #endregion

        internal static FrameworkElement[] GenerateViews(ListViewItem GetView, params (Type t, double Width)[] Input)
        {
            Grid grid = new Grid();
            FrameworkElement[] controls = new FrameworkElement[Input.Length];

            for (int i = 0; i < Input.Length; ++i)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Input[i].Width, GridUnitType.Star) });

                if (Input[i].t.Equals(typeof(string)))
                {
                    controls[i] = new TextBlock()
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        TextWrapping = TextWrapping.Wrap
                    };
                }
                else if (Input[i].t.Equals(typeof(bool)))
                {
                    controls[i] = new CheckBox()
                    {
                        Content = "",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        MinWidth = 32
                    };
                }

                Grid.SetColumn(controls[i], i);
                grid.Children.Add(controls[i]);
            }
            GetView.Content = grid;
            return controls;
        }
    }
}
