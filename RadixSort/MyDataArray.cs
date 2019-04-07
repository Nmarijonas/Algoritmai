﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixSort
{
    class MyDataArray : DataArray
    {
        double[] duom;
        public MyDataArray(int n, int seed)
        {
            duom = new double[n];
            lenght = n;
            Random rng = new Random(seed);
            for (int i = 0; i < lenght; i++)
            {
                duom[i] = rng.NextDouble();
            }
        }

        public override double this[int index]
        {
            get { return duom[index]; }
            set { duom[index] = value; }
        }
    }
}
