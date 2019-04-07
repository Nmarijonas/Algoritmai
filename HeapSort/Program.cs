using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HeapSort
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

        public static class HeapSortArray
        {
            private static int heapSize;
            public static void BuildHeap(DataArray arr)
            {
                heapSize = arr.Lenght - 1;
                for (int i = heapSize / 2; i >= 0; i--)
                {
                    Heapify(arr, i);
                }
            }

            public static void Heapify(DataArray arr, int i)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int largest = i;
                if (left <= heapSize && arr[left] > arr[i])
                {
                    largest = left;
                }
                if (right <= heapSize && arr[right] > arr[largest])
                {
                    largest = right;
                }
                if (largest != i)
                {
                    arr.Swap(i, largest);
                    Heapify(arr, largest);
                }
            }

            public static void HeapSortas(DataArray arr)
            {
                BuildHeap(arr);
                for (int i = arr.Lenght - 1; i >= 0; i--)
                {
                    arr.Swap(0, i);
                    heapSize--;
                    Heapify(arr, 0);
                }
            }
        }

        public static class HeapSortList
        {
            private static int heapSize;
            public static void BuildHeap(DataList mylist)
            {
                heapSize = mylist.Lenght - 1;
                for (int i = heapSize / 2; i >= 0; i--)
                {
                    Heapify(mylist, i);
                }
            }

            public static void Heapify(DataList mylist, int i)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int maxHeap = i;
                if (left <= heapSize &&
                   mylist.ReturnValueByIndex(left) > mylist.ReturnValueByIndex(i))
                {
                    maxHeap = left;
                }
                if (right <= heapSize &&
                    mylist.ReturnValueByIndex(right) > mylist.ReturnValueByIndex(maxHeap))
                {
                    maxHeap = right;
                }

                if (maxHeap != i)
                {
                    mylist.Swap(i, maxHeap);
                    Heapify(mylist, maxHeap);
                }
            }

            public static void HeapSortas(DataList mylist)
            {
                BuildHeap(mylist);
                for (int i = mylist.Lenght - 1; i >= 0; i--)
                {
                    mylist.Swap(0, i);
                    heapSize--;
                    Heapify(mylist, 0);
                }
            }
        }
        public static void TestArray_List(int seed)
        {
            int n = 12;
            MyDataArray myArray = new MyDataArray(n, seed);
            Console.WriteLine("---Array---");
            myArray.Print(n);
            Console.WriteLine();
            Console.WriteLine("---HeapSortedArray---");
            HeapSortArray.HeapSortas(myArray);
            myArray.Print(n);
            Console.WriteLine();
            MyDataList myList = new MyDataList(n, seed);
            Console.WriteLine("---List---");
            myList.Print(n);
            Console.WriteLine();
            Console.WriteLine("---HeapSortedList---");
            HeapSortList.HeapSortas(myList);
            myList.Print(n);
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
                Console.WriteLine("---HeapSortedFileArray---");
                HeapSortArray.HeapSortas(myarray);
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
                Console.WriteLine("---HeapSortedFileList---");
                HeapSortList.HeapSortas(mylist);
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
        public abstract void Swap(int a, int b);
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
        public abstract void Swap(int a, int b);
        public abstract double ReturnValueByIndex(int index);
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
