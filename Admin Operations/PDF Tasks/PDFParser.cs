using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Runtime.Serialization.Formatters.Binary;
using SautinSoft;

namespace Admin_Operations.PDF_Tasks
{
    class PDFParser
    {
        readonly string DirLocation;
        readonly PdfFocus pdfFocus;
        readonly Application xlApp;

        Page[] CompleteList { get; set; }
        public string[][] Table;

        public PDFParser(string PDFLocation, int FromPage, int ToPage)
        {
            DirLocation = PDFLocation.Substring(0, PDFLocation.LastIndexOf("\\"));

            if (File.Exists(Path.Combine(DirLocation, "final.bin")))
            {
                Console.WriteLine("Loading Existing Table");
                LoadTable();
                Console.WriteLine("Table Loaded");
                return;
            }

            Console.WriteLine("Loading Parsers");

            pdfFocus = new PdfFocus();
            pdfFocus.OpenPdf(PDFLocation);

            xlApp = new Application();

            if (xlApp == null)
                throw new Exception("Excel not installed!!");


            // Creating Directories
            string loc1 = Path.Combine(DirLocation, "Excel Files");
            if (!Directory.Exists(loc1))
                Directory.CreateDirectory(loc1);


            // Initialize Pages
            CompleteList = new Page[ToPage - FromPage + 1];
            Page.PDFLocation = PDFLocation;

            for (int i = FromPage; i <= ToPage; ++i)
                CompleteList[i - FromPage] = new Page { PageNumber = i };

            Console.WriteLine("Parsers Loaded");
        }

        public void A_SaveExcel()
        {
            if (pdfFocus == null)
                return;

            foreach (var x in CompleteList)
                x.GenerateExcel(pdfFocus);
        }

        public void B_LoadExcels()
        {
            if (xlApp == null)
                return;

            foreach (var x in CompleteList)
                x.LoadInMemory(xlApp);

            int Rows = 0;
            foreach (Page p in CompleteList)
                Rows += p.Contents.Count;

            Table = new string[Rows][];

            int i = 0;
            foreach (Page p in CompleteList)
                foreach (string[] row in p.Contents)
                    Table[i++] = row;

            SaveTable();
        }

        public void SaveTable()
        {
            using (Stream m = new FileStream(Path.Combine(DirLocation, "final.bin"), FileMode.Create, FileAccess.Write))
                new BinaryFormatter().Serialize(m, Table);
        }

        public void LoadTable()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (var s = new FileStream(Path.Combine(DirLocation, "final.bin"), FileMode.OpenOrCreate, FileAccess.Read))
            {
                Table = formatter.Deserialize(s) as string[][];
            }
        }

        public void End()
        {
            if (xlApp == null)
                return;

            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}