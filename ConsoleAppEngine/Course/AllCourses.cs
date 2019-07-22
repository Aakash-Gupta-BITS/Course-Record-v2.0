using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Globals;
using ConsoleAppEngine.Contacts;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public partial class AllCourses : EElementBase<CourseEntry>, ISerializable
    {
        public static AllCourses Instance = new AllCourses();

        public AllContacts Contacts => AllContacts.Instance;

        public LinkedList<CourseEntry> CoursesList => lists;

        #region DisplayBoxes

        public NavigationView NavView;

        private ComboBox TypeBox;
        private TextBox IdBox;
        private TextBox TitleBox;
        private TextBox LectureBox;
        private TextBox PracticalBox;
        private ComboBox ICBox;

        #endregion

        #region Serialization

        private AllCourses() : base()
        {

        }

        protected AllCourses(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion

        #region ChangeTasks

        public override void PostAddTasks(CourseEntry e)
        {
            var list = NavView.MenuItems.ToArray();
            NavView.MenuItems.Clear();

            for (int i = 0; i < 5; ++i)
            {
                NavView.MenuItems.Add(list[i]);
            }
            foreach (var item in CoursesList)
            {
                NavView.MenuItems.Add(item.CourseNavigationItem);
            }

            for (int i = list.Length - 4; i < list.Length; ++i)
            {
                NavView.MenuItems.Add(list[i]);
            }

            HDDSync.SaveCourseToHdd(e);
        }

        public override void PostDeleteTasks(CourseEntry element)
        {
            NavView.MenuItems.Remove(element.CourseNavigationItem);
            File.Delete(Path.Combine(HDDSync.CourseDirectoryLocation, element.Title + ".bin"));
        }

        public override void PostModifyTasks(CourseEntry element)
        {
            string[] files = Directory.GetFiles(HDDSync.CourseDirectoryLocation);
            string[] Finalfiles = Array.ConvertAll(Instance.lists.ToArray(), a => Path.Combine(HDDSync.CourseDirectoryLocation, a.Title + ".bin"));

            foreach (var file in files)
                if (!Finalfiles.Contains(file))
                {
                    File.Delete(file);
                    break;
                }

            using (Stream m = new FileStream(Path.Combine(HDDSync.CourseDirectoryLocation, element.Title + ".bin"), FileMode.Create, FileAccess.Write))
            {
                new BinaryFormatter().Serialize(m, element);
            }
        }

        #endregion
    }

    public partial class AllCourses : EElementBase<CourseEntry>
    {
        public override void DestructViews()
        {
            base.DestructViews();

            TypeBox = null;
            IdBox = null;
            TitleBox = null;
            LectureBox = null;
            PracticalBox = null;
            ICBox = null;

            NavView = null;
        }

        protected override CourseEntry AddNewItem()
        {
            ETeacherEntry eTeacher = null;
            foreach (var y in Contacts.TeacherEntry.lists)
            {
                if (y.Name == ICBox.SelectedItem as string)
                {
                    eTeacher = y;
                    break;
                }
            }

            return new CourseEntry(
                ((CourseType)Enum.Parse(typeof(CourseType), TypeBox.SelectedItem as string), IdBox.Text),
                TitleBox.Text,
                byte.Parse(LectureBox.Text),
                byte.Parse(PracticalBox.Text),
                eTeacher);
        }

        protected override void CheckInputs(LinkedList<Control> Controls, LinkedList<Control> ErrorWaale)
        {
            Controls.AddLast(TypeBox);
            Controls.AddLast(IdBox);
            Controls.AddLast(TitleBox);
            Controls.AddLast(LectureBox);
            Controls.AddLast(PracticalBox);
            Controls.AddLast(ICBox);

            if (TypeBox.SelectedItem == null)
            {
                ErrorWaale.AddLast(TypeBox);
            }

            if (IdBox.Text == "")
            {
                ErrorWaale.AddLast(IdBox);
            }

            if (TitleBox.Text == "")
            {
                ErrorWaale.AddLast(TitleBox);
            }

            if (!byte.TryParse(LectureBox.Text, out _))
            {
                ErrorWaale.AddLast(LectureBox);
            }

            if (!byte.TryParse(PracticalBox.Text, out _))
            {
                ErrorWaale.AddLast(PracticalBox);
            }

            if (ICBox.SelectedItem == null)
            {
                ErrorWaale.AddLast(ICBox);
            }

            if (ErrorWaale.Count != 0)
            {
                return;
            }

            foreach (var y in (from x in CoursesList where x != ItemToChange select x))
            {
                if (y.Title == TitleBox.Text)
                {
                    ErrorWaale.AddLast(TitleBox);
                    break;
                }
                if (y.ID.branchstring == IdBox.Text && y.ID.branchtype.ToString() == TypeBox.Text)
                {
                    ErrorWaale.AddLast(IdBox);
                    ErrorWaale.AddLast(TypeBox);
                    break;
                }
            }

        }

        protected override void ClearAddGrid()
        {
            base.ClearAddGrid();

            TypeBox.BorderBrush =
            IdBox.BorderBrush =
            TitleBox.BorderBrush =
            LectureBox.BorderBrush =
            PracticalBox.BorderBrush =
            ICBox.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));

            TypeBox.SelectedItem = null;
            IdBox.Text =
            TitleBox.Text =
            LectureBox.Text =
            PracticalBox.Text = "";
            ICBox.SelectedItem = null;
        }

        protected override Grid Header()
        {
            return GenerateHeader(("Id", 1), ("Title", 1), ("IC", 1));
        }

        protected override void InitializeViews(params FrameworkElement[] AddViewGridControls)
        {
            TypeBox = AddViewGridControls[0] as ComboBox;
            IdBox = AddViewGridControls[1] as TextBox;
            TitleBox = AddViewGridControls[2] as TextBox;
            LectureBox = AddViewGridControls[3] as TextBox;
            PracticalBox = AddViewGridControls[4] as TextBox;
            ICBox = AddViewGridControls[5] as ComboBox;
            AddButton = AddViewGridControls[6] as Button;
        }

        protected override void ItemToChangeUpdate()
        {
            ETeacherEntry eTeacher = null;
            foreach (var y in Contacts.TeacherEntry.lists)
            {
                if (y.Name == ICBox.SelectedItem as string)
                {
                    eTeacher = y;
                    break;
                }
            }

            ItemToChange.UpdateDataWithViews(
                ((CourseType)Enum.Parse(typeof(CourseType), TypeBox.SelectedItem as string), IdBox.Text),
                TitleBox.Text,
                byte.Parse(LectureBox.Text),
                byte.Parse(PracticalBox.Text),
                eTeacher);
        }

        protected override IOrderedEnumerable<CourseEntry> OrderList()
        {
            return CoursesList.OrderBy(a => a.Title.ToUpper());
        }

        protected override void SetAddGrid_ItemToChange()
        {
            TypeBox.SelectedItem = ItemToChange.ID.branchtype.ToString();
            IdBox.Text = ItemToChange.ID.branchstring;
            TitleBox.Text = ItemToChange.Title;
            LectureBox.Text = ItemToChange.LectureUnits.ToString();
            PracticalBox.Text = ItemToChange.PracticalUnits.ToString();
            ICBox.SelectedItem = ItemToChange.IC.Name;
        }

        protected override void SetContentDialog()
        {
            contentDialog.Title = ItemToChange.Title;
            contentDialog.Content = string.Format(
                "Id\t\t:\t{0}\n" +
                "Lecture Units\t:\t{1}\n" +
                "Practical Units\t:\t{2}\n" +
                "IC\t\t:\t{3}",
                ItemToChange.ID.branchtype.ToString() + " " + ItemToChange.ID.branchstring,
                ItemToChange.LectureUnits,
                ItemToChange.PracticalUnits,
                ItemToChange.IC.Name);
        }
    }
}
