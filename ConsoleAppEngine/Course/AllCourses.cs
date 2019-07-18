using ConsoleAppEngine.Abstracts;
using ConsoleAppEngine.AllEnums;
using ConsoleAppEngine.Contacts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ConsoleAppEngine.Course
{
    [Serializable]
    public partial class AllCourses : EElementBase<CourseEntry>, ISerializable
    {
        public static AllCourses Instance = new AllCourses();

        public AllContacts Contacts => AllContacts.Instance;

        #region DisplayBoxes
        public NavigationView NavView;

        private ComboBox TypeBox;
        private TextBox IdBox;
        private TextBox TitleBox;
        private TextBox LectureBox;
        private TextBox PracticalBox;
        private ComboBox ICBox;

        #endregion

        public LinkedList<CourseEntry> CoursesList => lists; //  new LinkedList<CourseEntry>();

        public void AddCourse(CourseEntry e)
        {
            CoursesList.AddLast(e);
            UpdateList();
        }

        #region Serialization

        private AllCourses() : base()
        {

        }

        protected AllCourses(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        #endregion

        #region ToMoveToHDDSync
        public static void AddToHdd()
        {
            string DirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Courses");

            if (!Directory.Exists(DirectoryLocation))
            {
                Directory.CreateDirectory(DirectoryLocation);
            }

            foreach (CourseEntry e in Instance.CoursesList)
            {
                using (Stream m = new FileStream(Path.Combine(DirectoryLocation, e.Title + ".bin"), FileMode.Create, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(m, e);
                }
            }
        }

        public static void AddToHdd_NewThread()
        {
            Thread thread = new Thread(new ThreadStart(AddToHdd))
            {
                Name = "Add Courses to Hdd",
                IsBackground = false
            };
            thread.Start();
        }

        public static void GetFromHdd()
        {
            string DirectoryLocation = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Database", "Courses");

            if (!Directory.Exists(DirectoryLocation))
            {
                return;
            }

            BinaryFormatter formatter = new BinaryFormatter();

            foreach (string file in Directory.GetFiles(DirectoryLocation))
            {
                using (var s = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    Instance.CoursesList.AddLast(formatter.Deserialize(s) as CourseEntry);
                }
            }
        }

        public static void GetFromHdd_NewThread()
        {
            Thread thread = new Thread(new ThreadStart(GetFromHdd))
            {
                Name = "Add Courses to Hdd",
                IsBackground = false
            };
            thread.Start();
        }

        #endregion
    }

    public partial class AllCourses : EElementBase<CourseEntry>
    {
        public override void DestructViews()
        {
            ViewGrid.Children.Clear();
            AddGrid.Children.Clear();
            ViewList.Items.Clear();

            ViewGrid = null;
            AddGrid = null;
            ViewList = null;
            AddButton = null;
            ViewCommand = null;
            AddCommand = null;

            TypeBox = null;
            IdBox = null;
            TitleBox = null;
            LectureBox = null;
            PracticalBox = null;
            ICBox = null;

            NavView = null;
        }

        protected override void AddNewItem()
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

            CourseEntry entry = new CourseEntry(
                ((CourseType)Enum.Parse(typeof(CourseType), TypeBox.SelectedItem as string), IdBox.Text),
                TitleBox.Text,
                byte.Parse(LectureBox.Text),
                byte.Parse(PracticalBox.Text),
                eTeacher);
            AddCourse(entry);

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
            ItemToChange = null;
            AddButton.BorderBrush =
            TypeBox.BorderBrush =
            IdBox.BorderBrush =
            TitleBox.BorderBrush =
            LectureBox.BorderBrush =
            PracticalBox.BorderBrush =
            ICBox.BorderBrush = new SolidColorBrush(Color.FromArgb(102, 255, 255, 255));
            AddButton.Content = "Add";

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

        protected override void InitializeAddGrid(params FrameworkElement[] AddViewGridControls)
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

            ItemToChange.UpdateData(
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
