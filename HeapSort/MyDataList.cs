using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
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

        public override void Swap(int posnode1, int posnode2)
        {
            Node ithNode = head;
            for (int z = 0; z < posnode1; z++)
            {
                ithNode = ithNode.nextNode;
            }
            Node jthNode = head;
            for (int q = 0; q < posnode2; q++)
            {
                jthNode = jthNode.nextNode;
            }
            double data = ithNode.duom;
            ithNode.duom = jthNode.duom;
            jthNode.duom = data;
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
    }
}
