using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HeapSort
{
    class Analysis
    {
        const int duomskc = 500;
        public static void AnalysisArray_List(int seed, int kiek)
        {
            int n = duomskc;
            Console.WriteLine("Array HeapSort");
            Console.WriteLine("N         RunTime");
            for (int i = 0; i < kiek; i++)
            {
                MyDataArray myarr = new MyDataArray(n, seed);
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Program.HeapSortArray.HeapSortas(myarr);
                timer.Stop();
                //myarr.Print(n);
                Console.WriteLine("{0,-10}{1}", n, timer.Elapsed);
                n = n * 2;
            }
            Console.WriteLine();
            n = duomskc;
            Console.WriteLine("List HeapSort");
            Console.WriteLine("N         RunTime");
            for (int i = 0; i < kiek; i++)
            {
                MyDataList mylist = new MyDataList(n, seed);
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Program.HeapSortList.HeapSortas(mylist);
                timer.Stop();
                //mylist.Print(n);
                Console.WriteLine("{0,-10}{1}", n, timer.Elapsed);
                n = n * 2;
            }
            Console.WriteLine();
        }

        public static void AnalysisFILEArray_List(int seed, int kiek)
        {
            string filename = @"myTestArray.dat";
            int n = duomskc;
            Console.WriteLine("FileArray HeapSort");
            for (int i = 0; i < kiek; i++)
            {
                MyFileArray myarray = new MyFileArray(filename, n, seed);
                Stopwatch timer = new Stopwatch();
                using (myarray.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
                {
                    timer.Start();
                    Program.HeapSortArray.HeapSortas(myarray);
                    timer.Stop();
                }
                //myarray.Print(n);
                Console.WriteLine("{0,-10}{1}", n, timer.Elapsed);
                n = n * 2;
            }
            Console.WriteLine();
            filename = @"myTestList.dat";
            n = duomskc;
            Console.WriteLine("FileList HeapSort");
            Console.WriteLine("N         RunTime");
            for (int i = 0; i < kiek; i++)
            {
                MyFileList mylist = new MyFileList(filename, n, seed);
                Stopwatch timer = new Stopwatch();
                using (mylist.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
                {
                    timer.Start();
                    Program.HeapSortList.HeapSortas(mylist);
                    timer.Stop();
                }
                //mylist.Print(n);
                Console.WriteLine("{0,-10}{1}", n, timer.Elapsed);
                n = n * 2;
            }
            Console.WriteLine();
            Console.WriteLine("---PABAIGA---");
        }
    }
}
