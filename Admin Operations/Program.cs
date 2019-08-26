using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Runtime.Serialization.Formatters.Binary;
using SautinSoft;

namespace Admin_Operations
{
    class Page
    {
        public static string DirLocation => PDFLocation.Substring(0, PDFLocation.LastIndexOf("\\"));
        public static string PDFLocation { get; set; }

        public int PageNumber { get; set; }

        public string BinExportLocation => Path.Combine(DirLocation, "Bin Files", PageNumber + ".bin");
        public string ExcelExportLocation => Path.Combine(DirLocation, "Excel Files", PageNumber + ".xls");

        public LinkedList<string[]> Contents { get; private set; }

        public void GenerateExcel(PdfFocus pdfFocus)
        {
            Console.WriteLine("Saving Contents to {0}.xls", PageNumber);
            pdfFocus.ToExcel(ExcelExportLocation, PageNumber, PageNumber);
        }

        public void LoadInMemory(Application xlApp)
        {
            Console.WriteLine("Loading {0}.xls in memory", PageNumber);
            Workbook xlWorkBook = xlApp.Workbooks.Open(ExcelExportLocation);

            int count = 0;

            foreach (Worksheet xlWorkSheet in xlWorkBook.Sheets)
            {
                // Process only one page
                if (count != 0)
                    break;
                ++count;

                Range xlRange = xlWorkSheet.UsedRange;
                int totalRows = xlRange.Rows.Count;
                int totalColumns = xlRange.Columns.Count;

                Contents = new LinkedList<string[]>();

                // Row 1 to 6 are extra rows
                for (int i = 7; i <= totalRows; i++)
                {
                    string[] output = new string[totalColumns];
                    for (int j = 1; j <= totalColumns; ++j)
                        output[j - 1] = (xlRange.Cells[i, j] as Range).Text.ToString().Replace("\n", "");

                    bool NonEmpty = false;
                    foreach (var x in output)
                        if (x != "")
                        {
                            NonEmpty = true;
                            break;
                        }

                    if (NonEmpty)
                        Contents.AddLast(output);
                }
                Marshal.ReleaseComObject(xlWorkSheet);
            }

            xlWorkBook.Close();
            Marshal.ReleaseComObject(xlWorkBook);
        }

        public void SaveBin()
        {
            Console.WriteLine("Saving {0}.bin to Storage", PageNumber);
            using (Stream m = new FileStream(BinExportLocation, FileMode.Create, FileAccess.Write))
                new BinaryFormatter().Serialize(m, Contents);
        }
    }

    class PDFParse
    {
        readonly string DirLocation;
        readonly PdfFocus pdfFocus;
        readonly Application xlApp;

        Page[] CompleteList { get; set; }
        public string[][] Table;

        public PDFParse(string PDFLocation, int FromPage, int ToPage)
        {
            Console.WriteLine("Loading Parsers");

            pdfFocus = new PdfFocus();
            pdfFocus.OpenPdf(PDFLocation);

            xlApp = new Application();

            if (xlApp == null)
                throw new Exception("Excel not installed!!");


            // Creating Directories
            DirLocation = PDFLocation.Substring(0, PDFLocation.LastIndexOf("\\"));
            string loc1 = Path.Combine(DirLocation, "Bin Files");
            string loc2 = Path.Combine(DirLocation, "Excel Files");
            if (!Directory.Exists(loc1))
                Directory.CreateDirectory(loc1);
            if (!Directory.Exists(loc2))
                Directory.CreateDirectory(loc2);

            
            // Initialize Pages
            CompleteList = new Page[ToPage - FromPage + 1];
            Page.PDFLocation = PDFLocation;

            for (int i = FromPage; i <= ToPage; ++i)
                CompleteList[i - FromPage] = new Page { PageNumber = i };

            Console.WriteLine("Parsers Loaded");
        }

        public void A_SaveExcel()
        {
            foreach (var x in CompleteList)
                x.GenerateExcel(pdfFocus);
        }

        public void B_LoadExcel()
        {
            foreach (var x in CompleteList)
                x.LoadInMemory(xlApp);
        }

        public void C_SaveBins()
        {
            foreach (var x in CompleteList)
                x.SaveBin();
        }

        public void D_MergePages()
        {
            int Rows = 0;
            foreach (Page p in CompleteList)
                Rows += p.Contents.Count;

            Table = new string[Rows][];

            int i = 0;
            foreach (Page p in CompleteList)
                foreach (string[] row in p.Contents)
                    Table[i++] = row;
        }

        public void E_SaveFinalTable()
        {
            using (Stream m = new FileStream(Path.Combine(DirLocation, "final.bin"), FileMode.Create, FileAccess.Write))
                new BinaryFormatter().Serialize(m, Table);
        }

        public void F_LoadFinalTable()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (var s = new FileStream(Path.Combine(DirLocation, "final.bin"), FileMode.OpenOrCreate, FileAccess.Read))
            {
                Table = formatter.Deserialize(s) as string[][];
            }
        }

        public void SaveTxt()
        {
            string output = "";

            foreach (var x in Table)
            {
                foreach (var y in x)
                {
                    output += y + "\t";
                }
                output += "\n";
            }

            File.WriteAllText(Path.Combine(DirLocation, "output.txt"), output);
        }

        public void End()
        {
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }

    class Program
    {
        static void Main()
        {
            string FileLoc;
            do
            {
                Console.Write("Enter File Location : ");
                FileLoc = Console.ReadLine();

            } while (!File.Exists(FileLoc) || !FileLoc.EndsWith(".pdf"));

            PDFParse p = new PDFParse(FileLoc, 1, 48);

            // p.A_SaveExcel();
            // p.B_LoadExcel();
            // p.C_SaveBins();
            // p.D_MergePages();
            // p.E_SaveFinalTable();

            p.F_LoadFinalTable();

            MainIterator iter = new MainIterator(p.Table);

            p.End();
        }
    }
}