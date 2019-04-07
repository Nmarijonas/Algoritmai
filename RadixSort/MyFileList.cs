using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RadixSort
{
    class MyFileList : DataList
    {
        int PrevNode; int CurrentNode; int NextNode;
        public MyFileList(string filename, int n, int seed)
        {
            lenght = n;
            Random rand = new Random(seed);
            if (File.Exists(filename))
                File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Append)))
                {
                    writer.Write(4);
                    for (int j = 0; j < lenght; j++)
                    {
                        writer.Write(rand.NextDouble());
                        writer.Write((j + 1) * 12 + 4);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public FileStream fs { get; set; }

        public double Current()
        {
            Byte[] data = new Byte[8];
            fs.Seek(CurrentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            double result = BitConverter.ToDouble(data, 0);
            return result;
        }

        public override double Head()
        {
            Byte[] data = new Byte[12];
            fs.Seek(0, SeekOrigin.Begin);
            fs.Read(data, 0, 4);
            CurrentNode = BitConverter.ToInt32(data, 0);
            PrevNode = -1;
            fs.Seek(CurrentNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            double result = BitConverter.ToDouble(data, 0);
            NextNode = BitConverter.ToInt32(data, 8);
            return result;
        }

        public override double Next()
        {
            Byte[] data = new Byte[12];
            fs.Seek(NextNode, SeekOrigin.Begin);
            fs.Read(data, 0, 12);
            PrevNode = CurrentNode;
            CurrentNode = NextNode;
            double result = BitConverter.ToDouble(data, 0);
            NextNode = BitConverter.ToInt32(data, 8);
            return result;
        }

        public override double ReturnValueByIndex(int index)
        {
            Byte[] data = new Byte[8];
            fs.Seek(index * 12 + 4, SeekOrigin.Begin);
            fs.Read(data, 0, 8);
            return BitConverter.ToDouble(data, 0);
        }

        public override void InsertNewValueInIndex(int index, double value)
        {
            Byte[] data = new Byte[8];
            data = BitConverter.GetBytes(value);
            fs.Seek(index * 12 + 4, SeekOrigin.Begin);
            fs.Write(data, 0, 8);
        }
    }
}
