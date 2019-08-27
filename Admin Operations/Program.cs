using System;
using System.IO;
using Admin_Operations.PDF_Tasks;
using Admin_Operations.Iterators;

namespace Admin_Operations
{
    class Program
    {
        static void Main()
        {
            string FileLoc;
            do
            {
                Console.Write("Enter File Location : ");
                FileLoc = Console.ReadLine().ToLower();
            } while (!File.Exists(FileLoc) || !FileLoc.EndsWith(".pdf"));


        Start1:
            Console.Write("From Page : ");
            int.TryParse(Console.ReadLine(), out int from);
            if (from == default)
                goto Start1;


            Start2:
            Console.Write("To Page : ");
            int.TryParse(Console.ReadLine(), out int to);
            if (to == default)
                goto Start2;


            PDFParser p = new PDFParser(FileLoc, from, to);

            p.A_SaveExcel();
            p.B_LoadExcels();


            MainIterator iter = new MainIterator(p.Table);

            p.End();
        }
    }
}