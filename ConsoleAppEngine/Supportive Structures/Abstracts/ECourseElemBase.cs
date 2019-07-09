using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace ConsoleAppEngine.Abstracts
{
    [Serializable]
    public abstract partial class EElementBase<T> : ISerializable where T : EElementItemBase
    {
        private EElementBase(LinkedList<T> List)
        {
            lists = List;
            contentDialog = new ContentDialog()
            {
                PrimaryButtonText = "Modify",
                SecondaryButtonText = "Delete",
                CloseButtonText = "Ok"
            };
        }

        public EElementBase() : this(new LinkedList<T>())
        {

        }

        #region Serialization

        protected EElementBase(SerializationInfo info, StreamingContext context) : this((LinkedList<T>)info.GetValue(nameof(lists), typeof(LinkedList<T>)))
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(lists), lists, typeof(LinkedList<T>));
        }

        #endregion
    }

    public abstract partial class EElementBase<T> where T : EElementItemBase
    {
        public readonly LinkedList<T> lists;
        protected T ItemToChange { get; set; }
        protected Grid ViewGrid;
        protected Grid AddGrid;
        protected ListView ViewList;
        protected Button AddButton;
        protected readonly ContentDialog contentDialog;
        protected AppBarButton ViewCommand;
        protected AppBarButton AddCommand;

        protected abstract void AddNewItem();
        protected abstract Grid Header();
        protected abstract void InitializeAddGrid(params FrameworkElement[] AddViewGridControls);
        protected abstract void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale);
        protected abstract void ClearAddGrid();
        protected abstract void ItemToChangeUpdate();
        protected abstract void SetContentDialog();
        protected abstract void SetAddGrid_ItemToChange();
        protected abstract IOrderedEnumerable<T> OrderList();
        public abstract void DestructViews();

        public virtual void PostDeleteTasks()
        {
            // Kept empty and virtual because some childs may not use it
        }

        public void InitializeViews(Grid viewGrid, Grid addGrid, AppBarButton viewCommand, AppBarButton addCommand, params FrameworkElement[] AddViewGridControls)
        {
            ViewGrid = viewGrid;
            AddGrid = addGrid;
            ViewCommand = viewCommand;
            AddCommand = addCommand;

            FillViewGrid();
            InitializeAddGrid(AddViewGridControls);
            SetEvents();

            ViewGrid.Visibility = Visibility.Visible;
            AddGrid.Visibility = Visibility.Collapsed;
        }

        private void SetEvents()
        {
            ViewCommand.Click += (sender, e) =>
            {
                AddGrid.Visibility = Visibility.Collapsed;
                ViewGrid.Visibility = Visibility.Visible;
                // Clear AddGrid when last item was set to modify but not modified
                if (AddButton.Content.ToString() == "Modify")
                {
                    ClearAddGrid();
                }
            };
            AddCommand.Click += (sender, e) =>
            {
                ViewGrid.Visibility = Visibility.Collapsed;
                AddGrid.Visibility = Visibility.Visible;
                ClearAddGrid();
            };
            AddButton.Click += (sender, e) =>
            {
                try
                {
                    // This will throw an exception when any control have invalid data
                    AbstractCheckInputs();
                }
                catch
                {
                    return;
                }

                // This code will be called only when all inputs are Valid
                if (AddButton.Content.ToString() == "Add")
                {
                    AddNewItem();
                }
                else if (AddButton.Content.ToString() == "Modify")
                {
                    ItemToChangeUpdate();
                    UpdateList();
                }

                // Change View to ViewGrid
                ViewGrid.Visibility = Visibility.Visible;
                AddGrid.Visibility = Visibility.Collapsed;

                // Clear all the AddGrid Values
                ClearAddGrid();
                ItemToChange = null;
                return;
            };
            ViewList.SelectionChanged += async (sender, e) =>
            {
                // No item is selected or programmatically selected item was unselected
                if (ViewList.SelectedItem == null)
                {
                    return;
                }

                // Maps Selected item to ItemToChange
                foreach (var a in lists)
                {
                    if (a.GetView == ViewList.SelectedItem)
                    {
                        ItemToChange = a;
                        break;
                    }
                }

                // Unselect the selected item
                ViewList.SelectedItem = null;

                // PointerOverObject checking 
                if (ItemToChange.PointerOverObject != null &&
                ItemToChange.PointerOverObject is ButtonBase x && x.IsPointerOver)
                {
                    return;
                }

                // Update ContentDialog
                SetContentDialog();

                switch (await contentDialog.ShowAsync())
                {
                    // DELETE
                    case ContentDialogResult.Secondary:
                        // Remove Selected item
                        ViewList.Items.Remove(ItemToChange.GetView);
                        lists.Remove(ItemToChange);
                        ItemToChange.IsDeleted = true;

                        // Call for operations after deleting
                        PostDeleteTasks();
                        ItemToChange = null;
                        break;

                    // MODIFY
                    case ContentDialogResult.Primary:

                        ViewGrid.Visibility = Visibility.Collapsed;
                        AddGrid.Visibility = Visibility.Visible;

                        AddButton.Content = "Modify";
                        // Fill ViewGrid
                        SetAddGrid_ItemToChange();
                        break;
                }
            };
        }

        private void AbstractCheckInputs()
        {
            LinkedList<Control> controls_cando = new LinkedList<Control>();

            LinkedList<Control> controls_err = new LinkedList<Control>();

            CheckInputs(controls_cando, controls_err);

            // Set every Control border to default
            foreach (Control x in controls_cando)
            {
                x.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            }

            AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));

            // Set invalid controls border to red
            foreach (Control x in controls_err)
            {
                x.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }

            if (controls_err.Count != 0)
            {
                AddButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                throw new Exception();
            }
        }

        protected void FillViewGrid()
        {
            Grid header = Header();
            Grid.SetRow(header, 0);
            ViewList = new ListView();
            Grid.SetRow(ViewList, 1);

            ViewGrid.Children.Add(header);
            ViewGrid.Children.Add(ViewList);

            UpdateList();
        }

        protected void UpdateList()
        {
            // If current instaance of ViewList is null
            if (ViewList == null)
            {
                return;
            }

            // Remove deleted items from list
            foreach (var a in (from x in lists where x.IsDeleted == true select x).ToArray())
            {
                lists.Remove(a);
            }

            // order all elements
            List<T> v = OrderList().ToList();
            lists.Clear();

            // Update list by oedered elements
            foreach (var x in v)
            {
                lists.AddLast(x);
            }

            // Clear ViewList
            ViewList.Items.Clear();

            // Fill ViewList with new ordering
            foreach (var a in from a in lists select a.GetView)
            {
                ViewList.Items.Add(a);
            }
        }

        protected static Grid GenerateHeader(params (string Name, double Width)[] Input)
        {
            Grid grid = new Grid() { Margin = new Thickness(10, 10, 10, 10) };
            int i = 0;
            foreach ((string Name, double Width) in Input)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Width, GridUnitType.Star) });
                TextBlock temp = new TextBlock()
                {
                    Text = Name,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold
                };
                Grid.SetColumn(temp, i++);
                grid.Children.Add(temp);
            }

            return grid;
        }
    }
}