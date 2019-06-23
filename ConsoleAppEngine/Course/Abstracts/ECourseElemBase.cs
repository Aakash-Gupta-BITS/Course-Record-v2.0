using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace ConsoleAppEngine.Course.Abstracts
{
    public abstract class ECourseElemBase<T> where T : ECourseElemItemBase
    {
        protected readonly LinkedList<T> lists = new LinkedList<T>();
        protected T ItemToChange { get; set; }

        protected Grid ViewGrid;
        protected Grid AddGrid;
        protected ListView ViewList;
        protected Button AddButton;
        protected readonly ContentDialog contentDialog = new ContentDialog()
        {
            PrimaryButtonText = "Modify",
            SecondaryButtonText = "Delete",
            CloseButtonText = "Ok"
        };
        protected AppBarButton ViewCommand;
        protected AppBarButton AddCommand;

        protected abstract void AddNewItem();
        protected abstract Grid Header();
        protected abstract void InitializeAddGrid();
        protected abstract void CheckInputs();
        protected abstract void ClearAddGrid();
        protected abstract void ItemToChangeUpdate();
        protected abstract void SetContentDialog();
        protected abstract void SetAddGrid_ItemToChange();
        protected abstract IOrderedEnumerable<T> OrderList();
        public abstract void DestructViews();

        public void InitializeViews(Grid viewGrid, Grid addGrid, AppBarButton viewCommand, AppBarButton addCommand)
        {
            ViewGrid = viewGrid;
            AddGrid = addGrid;
            ViewCommand = viewCommand;
            AddCommand = addCommand;

            FillViewGrid();
            InitializeAddGrid();
            SetEvents();
        }

        void SetEvents()
        {
            ViewCommand.Click += (object sender, RoutedEventArgs e) =>
            {
                AddGrid.Visibility = Visibility.Collapsed;
                ViewGrid.Visibility = Visibility.Visible;
                if (AddButton.Content.ToString() == "Modify")
                    ClearAddGrid();
            };
            AddCommand.Click += (object sender, RoutedEventArgs e) =>
            {
                ViewGrid.Visibility = Visibility.Collapsed;
                AddGrid.Visibility = Visibility.Visible;
                AddButton.Content = "Add";
            };
            AddButton.Click += (object sender, RoutedEventArgs e) =>
            {
                try
                {
                    CheckInputs();
                }
                catch
                {
                    return;
                }
                if (AddButton.Content.ToString() == "Add")
                    AddNewItem();
                else if (AddButton.Content.ToString() == "Modify")
                {
                    ItemToChangeUpdate();
                    UpdateList();
                }
                ViewGrid.Visibility = Visibility.Visible;
                AddGrid.Visibility = Visibility.Collapsed;
                ClearAddGrid();
                ItemToChange = null;
                return;
            };
            ViewList.SelectionChanged += async (object sender, SelectionChangedEventArgs e) =>
            {
                if (ViewList.SelectedItem == null) return;

                foreach (var a in lists)
                    if (a.GetView == ViewList.SelectedItem)
                    {
                        ItemToChange = a;
                        break;
                    }

                ViewList.SelectedItem = null;

                if (ItemToChange.PointerOverObject != null && (ItemToChange.PointerOverObject as ButtonBase).IsPointerOver)
                    return;

                SetContentDialog();

                switch (await contentDialog.ShowAsync())
                {
                    // DELETE
                    case ContentDialogResult.Secondary:
                        ViewList.Items.Remove(ItemToChange.GetView);
                        lists.Remove(ItemToChange);
                        ItemToChange.IsDeleted = true;
                        break;

                    // MODIFY
                    case ContentDialogResult.Primary:

                        ViewGrid.Visibility = Visibility.Collapsed;
                        AddGrid.Visibility = Visibility.Visible;

                        AddButton.Content = "Modify";
                        SetAddGrid_ItemToChange();
                        break;
                }
            };
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
            if (ViewList == null)
                return;
            foreach (var a in (from x in lists where x.IsDeleted == true select x).ToArray())
                lists.Remove(a);

            List<T> v = OrderList().ToList();
            lists.Clear();
            foreach (var x in v)
                lists.AddLast(x);

            ViewList.Items.Clear();

            foreach (var a in (from a in lists select a.GetView))
                ViewList.Items.Add(a);
        }
    }

}
