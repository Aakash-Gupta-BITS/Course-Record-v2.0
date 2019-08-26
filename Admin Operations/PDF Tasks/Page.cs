using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using SautinSoft;

namespace Admin_Operations.PDF_Tasks
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
    }
}
