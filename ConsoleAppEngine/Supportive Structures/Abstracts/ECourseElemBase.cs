using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace ConsoleAppEngine.Abstracts
{
    public abstract class EElementBase<T> where T : EElementItemBase
    {
        /// <summary>
        /// Main list in which entries will be added
        /// </summary>
        public readonly LinkedList<T> lists = new LinkedList<T>();
        /// <summary>
        /// Entry, which is selected in ViewGrid, will be modified or deleted
        /// </summary>
        protected T ItemToChange { get; set; }

        /// <summary>
        /// Grid displyed when user wants to see whole list
        /// </summary>
        protected Grid ViewGrid;
        /// <summary>
        /// Grid displayed when user wants to add or modify some entry
        /// </summary>
        protected Grid AddGrid;
        /// <summary>
        /// ListView in which entries will be displayed in ViewGrid
        /// </summary>
        protected ListView ViewList;
        /// <summary>
        /// Add or Modify button in AddGrid
        /// </summary>
        protected Button AddButton;
        /// <summary>
        /// ContentDialog presented when an entry in selected in ViewList in ViewGrid
        /// </summary>
        protected readonly ContentDialog contentDialog = new ContentDialog()
        {
            PrimaryButtonText = "Modify",
            SecondaryButtonText = "Delete",
            CloseButtonText = "Ok"
        };
        /// <summary>
        /// Command to display ViewGrid located in lower right corner
        /// </summary>
        protected AppBarButton ViewCommand;
        /// <summary>
        /// Command to display AddGrid located in lower right corner
        /// </summary>
        protected AppBarButton AddCommand;

        /// <summary>
        /// Called when Add Button is pressed, and all entries are valid.
        /// </summary>
        protected abstract void AddNewItem();
        /// <summary>
        /// Generates the header to display in ViewGrid.
        /// </summary>
        /// <returns>Generted header</returns>
        protected abstract Grid Header();
        /// <summary>
        /// Map controls of AddGrid presented in xaml file to the controls defined in this class.
        /// </summary>
        /// <param name="AddViewGridControls">Controls that will be mapped.</param>
        protected abstract void InitializeAddGrid(params FrameworkElement[] AddViewGridControls);
        /// <summary>
        /// Will be called automatically when an item is added or modified.
        /// </summary>
        /// <param name="Controls">Required to add all controls to this which are checked for validity.</param>
        /// <param name="ErrorWaale">Controls whose inputs are invalid.</param>
        protected abstract void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale);
        /// <summary>
        /// Will be called automatically to clead AddGrid.
        /// </summary>
        protected abstract void ClearAddGrid();
        /// <summary>
        /// Will be called when we have to modify item and all entries are valid
        /// </summary>
        protected abstract void ItemToChangeUpdate();
        /// <summary>
        /// Will be called when a ListViewItem is selected (Not called when clicked on PointerOverObject).
        /// </summary>
        protected abstract void SetContentDialog();
        /// <summary>
        /// Called to fill AddGrid controls when modification in an entry is required.
        /// </summary>
        protected abstract void SetAddGrid_ItemToChange();
        /// <summary>
        /// Used to order the ViewGrid and lists
        /// </summary>
        /// <returns>Enumerable which contains the updated ordering of entries in lists</returns>
        protected abstract IOrderedEnumerable<T> OrderList();
        /// <summary>
        /// Clear all the controls and remove all ListViewItems in ViewGrid other than lists.
        /// </summary>
        public abstract void DestructViews();

        /// <summary>
        /// Optional - Called when some tasks are required to be done when an item is deleted. (ItemToChange can be used for currently deleted item).
        /// </summary>
        public virtual void PostDeleteTasks()
        {
            // Kept empty and virtual because some childs may not use it
        }

        /// <summary>
        /// Must be called first to map each and every control from xaml file to this instance.
        /// </summary>
        /// <param name="viewGrid"></param>
        /// <param name="addGrid"></param>
        /// <param name="viewCommand"></param>
        /// <param name="addCommand"></param>
        /// <param name="AddViewGridControls"></param>
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

        /// <summary>
        /// Set all Click Events of current instance
        /// </summary>
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

        /// <summary>
        /// Called when AddButton is clicked to check item for validitty.
        /// </summary>
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

        /// <summary>
        /// Called automatically and once when ViewGrid is initialised.
        /// </summary>
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

        /// <summary>
        /// Update lists and ViewList when an item is added or modified
        /// </summary>
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

        /// <summary>
        /// Automatically generates the header based on Title and Width of column in header
        /// </summary>
        /// <param name="Input">Array of Name of column and corrsponding StarWidth</param>
        /// <returns></returns>
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