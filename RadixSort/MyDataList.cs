using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixSort
{
    class MyDataList : DataList
    {
        private class Node
        {
            public Node nextNode { get; set; }
            public double duom { get; set; }
            public Node(double data)
            {
                this.duom = data;
            }
        }
        Node head;
        Node current;
        public MyDataList(int n, int seed)
        {
            lenght = n;
            Random rng = new Random(seed);
            head = new Node(rng.NextDouble());
            current = head;
            for (int i = 1; i < lenght; i++)
            {
                current.nextNode = new Node(rng.NextDouble());
                current = current.nextNode;
            }
            current.nextNode = null;
        }

        public override double ReturnValueByIndex(int index)
        {
            Node current = head;
            int count = 0;
            while (current != null)
            {
                if (count == index)
                    return current.duom;
                count++;
                current = current.nextNode;
            }
            return 0;
        }

        public override double Head()
        {
            current = head;
            return current.duom;
        }
        public override double Next()
        {
            current = current.nextNode;
            return current.duom;
        }

        public override void InsertNewValueInIndex(int index, double value)
        {
            Node ithNode = head;
            for (int z = 0; z < index; z++)
            {
                ithNode = ithNode.nextNode;
            }
            ithNode.duom = value;
        }
    }
}
