using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Runtime.Serialization.Formatters.Binary;
using SautinSoft;

namespace Admin_Operations
{

    class PDFParse
    {
        readonly PdfFocus pdfFocus;
        readonly Application xlApp;

        public PDFParse(string location)
        {
            Console.WriteLine("Opening PDF from " + location);
            pdfFocus = new PdfFocus();
            pdfFocus.OpenPdf(location);

            Console.WriteLine("Creating Excel Object");
            xlApp = new Application();

            if (xlApp == null)
                throw new Exception("Excel not installed!!");
        }

        public string[,] GetPage(int PageNumber, string exportDir, string Format)
        {
            string fileloc = string.Format(Path.Combine(exportDir, string.Format(Format, PageNumber)));
            Console.WriteLine("Parsing Page {0} to excel...", PageNumber);
            pdfFocus.ToExcel(fileloc, PageNumber, PageNumber);
            Console.WriteLine("Parsing Page {0} Completed...", PageNumber);

            Console.WriteLine("Loading Page {0} to memory", PageNumber);

            Workbook xlWorkBook = xlApp.Workbooks.Open(fileloc);

            string[,] Output = null;

            int count = 0;

            foreach (Worksheet xlWorkSheet in xlWorkBook.Sheets)
            {
                if (count != 0)
                    break;
                ++count;
                Console.WriteLine("Opening Worksheet");
                Range xlRange = xlWorkSheet.UsedRange;
                int totalRows = xlRange.Rows.Count;
                int totalColumns = xlRange.Columns.Count;

                Output = new string[totalRows, totalColumns];

                for (int i = 7; i <= totalRows; i++)
                    for (int j = 1; j <= totalColumns; ++j)
                        Output[i - 7, j - 1] = (xlRange.Cells[i, j] as Range).Text.ToString().Replace("\n", "");

                Marshal.ReleaseComObject(xlWorkSheet);
            }

            xlWorkBook.Close();
            Marshal.ReleaseComObject(xlWorkBook);

            return Output;
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
            string DirLoc = "";
            string FileLoc;
            do
            {
                Console.Write("Enter File Location : ");
                FileLoc = Console.ReadLine();
                if (FileLoc.Contains("\\"))
                    DirLoc = FileLoc.Substring(0, FileLoc.LastIndexOf("\\"));

            } while (!File.Exists(FileLoc) || !FileLoc.EndsWith(".pdf"));

            string ExcelExportLocation = Path.Combine(DirLoc, "Excel Files");
            string BinExportLocation = Path.Combine(DirLoc, "Bin Files");


            if (!Directory.Exists(ExcelExportLocation))
                Directory.CreateDirectory(ExcelExportLocation);
            if (!Directory.Exists(BinExportLocation))
                Directory.CreateDirectory(BinExportLocation);

            PDFParse p = new PDFParse(FileLoc);

            for (int i = 1; i <= 48; ++i)
            {
                using (Stream m = new FileStream(Path.Combine(BinExportLocation, i + ".bin"), FileMode.Create, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(m, p.GetPage(i, ExcelExportLocation, "{0}.xls"));
                }
            }

            p.End();
        }
    }
}
