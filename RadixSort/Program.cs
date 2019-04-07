using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RadixSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            //TestArray_List(seed);
            //TestArray_ListFile(seed);
            //Analysis.AnalysisArray_List(seed, 6);
            Analysis.AnalysisFILEArray_List(seed, 6);
            Console.ReadKey();
        }
        public static void RadixSortArray(DataArray data)
        {
            int i, j;
            long[] temp = new long[data.Lenght];
            long[] duom = new long[data.Lenght];
            for (int z = 0; z < data.Lenght; z++)
            {
                duom[z] = BitConverter.ToInt64(BitConverter.GetBytes(data[z]), 0);
            }
            for (int shift = (int)Math.Log10(duom[0] + 1); shift > -1; --shift)
            {
                j = 0;
                for (i = 0; i < duom.Length; ++i)
                {
                    bool move = (duom[i] << shift) >= 0;
                    if (shift == 0 ? !move : move)
                        duom[i - j] = duom[i];
                    else
                        temp[j++] = duom[i];
                }
                Array.Copy(temp, 0, duom, duom.Length - j, j);
            }
            for (int z = 0; z < duom.Length; z++)
                data[z] = BitConverter.ToDouble(BitConverter.GetBytes(duom[z]), 0);
        }

        public static void RadixSortList(DataList mylist)
        {
            int i, j;
            long[] temp = new long[mylist.Lenght];
            long[] duom = new long[mylist.Lenght];
            for (int z = 0; z < mylist.Lenght; z++)
            {
                duom[z] = BitConverter.ToInt64(BitConverter.GetBytes(mylist.ReturnValueByIndex(z)), 0);
            }
            for (int shift = (int)Math.Log10(duom[0] + 1); shift > -1; --shift)
            {
                j = 0;
                for (i = 0; i < duom.Length; ++i)
                {
                    bool move = (duom[i] << shift) >= 0;
                    if (shift == 0 ? !move : move)
                        duom[i - j] = duom[i];
                    else
                        temp[j++] = duom[i];
                }
                Array.Copy(temp, 0, duom, duom.Length - j, j);
            }
            double value;
            for (int z = 0; z < duom.Length; z++)
            {
                value = BitConverter.ToDouble(BitConverter.GetBytes(duom[z]), 0);
                mylist.InsertNewValueInIndex(z, value);
            }
        }

        public static void TestArray_List(int seed)
        {
            int n = 12;
            MyDataArray myArray = new MyDataArray(n, seed);
            Console.WriteLine("---Array---");
            myArray.Print(n);
            Console.WriteLine();
            Console.WriteLine("---RadixSortedArray");
            RadixSortArray(myArray);
            myArray.Print(n);
            Console.WriteLine();
            MyDataList mylist = new MyDataList(n, seed);
            Console.WriteLine("---List---");
            mylist.Print(n);
            Console.WriteLine();
            Console.WriteLine("---RadixSortedList---");
            RadixSortList(mylist);
            mylist.Print(n);
            Console.WriteLine();
        }

        public static void TestArray_ListFile(int seed)
        {
            int n = 12;
            string filename = @"myTestArray.dat";
            MyFileArray myarray = new MyFileArray(filename, n, seed);
            using (myarray.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                Console.WriteLine("---FileArray---");
                myarray.Print(n);
                Console.WriteLine();
                Console.WriteLine("---RadixSortedFileArray---");
                RadixSortArray(myarray);
                myarray.Print(n);
                Console.WriteLine();
            }
            filename = @"myTestList.dat";
            MyFileList mylist = new MyFileList(filename, n, seed);
            using (mylist.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                Console.WriteLine("---FileList---");
                mylist.Print(n);
                Console.WriteLine();
                Console.WriteLine("---RadixSortedFileList---");
                RadixSortList(mylist);
                mylist.Print(n);
                Console.WriteLine();
            }
        }
    }
    abstract class DataArray
    {
        protected int lenght;
        public int Lenght { get { return lenght; } }
        public abstract double this[int index] { get; set; }
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(" {0:F5} ", this[i]);
            }
            Console.WriteLine();
        }
    }

    abstract class DataList
    {
        protected int lenght;
        public int Lenght { get { return lenght; } }
        public abstract double Head();
        public abstract double Next();
        public abstract double ReturnValueByIndex(int index);
        public abstract void InsertNewValueInIndex(int index, double value);
        public void Print(int n)
        {
            Console.Write(" {0:F5} ", Head());
            for (int i = 1; i < n; i++)
            {
                Console.Write(" {0:F5} ", Next());
            }
            Console.WriteLine();
        }
    }
}
