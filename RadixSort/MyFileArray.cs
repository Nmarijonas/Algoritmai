using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RadixSort
{
    class MyFileArray : DataArray
    {
        public MyFileArray(string filename, int n, int seed)
        {
            double[] duom = new double[n];
            lenght = n;
            Random rng = new Random(seed);
            for (int i = 0; i < lenght; i++)
            {
                duom[i] = rng.NextDouble();
            }
            if (File.Exists(filename))
                File.Delete(filename);
            try
            {
                using (BinaryWriter rasyti = new BinaryWriter(File.Open(filename, FileMode.Append)))
                {
                    for (int i = 0; i < lenght; i++)
                    {
                        rasyti.Write(duom[i]);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public FileStream fs { get; set; }
        public override double this[int index]
        {
            get
            {
                Byte[] data = new Byte[8];
                fs.Seek(8 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 8);
                double result = BitConverter.ToDouble(data, 0);
                return result;
            }
            set
            {
                Byte[] data;
                fs.Seek(8 * index, SeekOrigin.Begin);
                data = BitConverter.GetBytes(value);
                fs.Write(data, 0, 8);
            }
        }
    }
}
