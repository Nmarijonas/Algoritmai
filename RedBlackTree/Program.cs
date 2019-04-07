using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            TestasOP(6);

            Console.ReadKey();
        }

        public static void CreateData(string filename, int duomskc)
        {
            if (File.Exists(filename))
                File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Append)))
                {
                    writer.Write(4);
                    for (int j = 0; j < duomskc; j++)
                    {
                        writer.Write(j * 2);
                        writer.Write((j + 1) * 12 + 4);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        public static void TestasOP(int kiek)
        {
            int duomskc = 200;
            for (int i = 0; i < kiek; i++)
            {
                RedBlackTree medis = new RedBlackTree();
                int[] mass = new int[duomskc];
                GenerateInts(medis, duomskc, mass);
                LaikoTyrimas(medis, mass);
                duomskc = duomskc * 2;
            }

            Console.WriteLine("---PABAIGA---");

        }

        public static void GenerateInts(RedBlackTree medis, int kiek, int[] mass)
        {
            int j = 0;
            for (int i = 4; i < kiek; i++)
            {
                medis.Add(i * 2);
                mass[j] = i * 2;
            }
        }

        public static void LaikoTyrimas(RedBlackTree medis, int[] mass)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < mass.Length; i++)
            {
                medis.Find(mass[i]);
            }
            timer.Stop();
            Console.WriteLine("{0,-10}{1}", mass.Length, timer.Elapsed);


        }


        public class RedBlackTree
        {
            private class Node
            {
                // Fields
                private int item; // Generic object held by each node
                private Node left, right, parent; // Links to children and parent
                private bool red = true; // Color of node
                                         // Constructor
                public Node(int item, Node parent)
                {
                    this.item = item;
                    this.parent = parent;
                    left = right = null;
                }
                // Properties
                public Node Left
                {
                    get { return left; }
                    set { left = value; }
                }
                public Node Right
                {
                    get { return right; }
                    set { right = value; }
                }
                public Node Parent
                {
                    get { return parent; }
                    set { parent = value; }
                }
                public int Item
                {
                    get { return item; }
                    set { item = value; }
                }

                public bool Red
                {
                    get { return red; }
                    set { red = value; }
                }

            }
            private Node root;
            private int numberElements;
            private Node insertedNode;


            public bool Find(int element)
            {
                Node nodas = root;
                while (nodas != null)
                {
                    if (nodas.Item < element)
                    {
                        nodas = nodas.Left;
                    }
                    else if (nodas.Item > element)
                    {
                        nodas = nodas.Right;
                    }
                    else if (nodas.Item == element)
                    {
                        return true;
                    }
                }
                return false;
            }

            /*public void DisplayTree()
            {
                if (root == null)
                {
                    Console.WriteLine("Nothing in the tree!");
                    return;
                }
                if (root != null)
                {
                    Display(root);
                }
            }*/

            /*private void Display(Node current)
            {
                if (current != null)
                {
                    Display(current.Left);
                    Console.Write("({0}) ", current.Item);
                    Display(current.Right);
                }
            }*/


            public void Add(int item)
            {
                root = InsertNode(root, item, null);
                numberElements++;
                if (numberElements > 2)
                {
                    Node parent, grandParent, greatGrandParent;
                    GetNodesAbove(insertedNode, out parent,
                    out grandParent, out greatGrandParent);
                    FixTreeAfterInsertion(insertedNode, parent,
                    grandParent, greatGrandParent);
                }
            }

            private Node InsertNode(Node node, int item, Node parent)
            {
                if (node == null)
                {
                    Node newNode = new Node(item, parent);
                    if (numberElements > 0)
                    {
                        newNode.Red = true;
                    }
                    else
                    {
                        newNode.Red = false;
                    }
                    insertedNode = newNode;
                    return newNode;
                }
                else if (item.CompareTo(node.Item) < 0)
                {
                    node.Left = InsertNode(node.Left, item, node);
                    return node;
                }
                else if (item.CompareTo(node.Item) > 0)
                {
                    node.Right = InsertNode(node.Right, item, node);
                    return node;
                }
                else
                {
                    throw new InvalidOperationException(
                    "Cannot add duplicate object.");
                }
            }
            private void GetNodesAbove(Node curNode, out Node parent,
                out Node grandParent, out Node greatGrandParent)
            {
                parent = null;
                grandParent = null;
                greatGrandParent = null; if (curNode != null)
                {
                    parent = curNode.Parent;
                }
                if (parent != null)
                {
                    grandParent = parent.Parent;
                }
                if (grandParent != null)
                {
                    greatGrandParent = grandParent.Parent;
                }
            }

            private void RightRotate(ref Node node)
            {
                Node nodeParent = node.Parent;
                Node left = node.Left;
                Node temp = left.Right;
                left.Right = node;
                node.Parent = left;
                node.Left = temp; if (temp != null)
                {
                    temp.Parent = node;
                }
                if (left != null)
                {
                    left.Parent = nodeParent;
                }
                node = left;
            }

            private void LeftRotate(ref Node node)
            {
                Node nodeParent = node.Parent;
                Node right = node.Right;
                Node temp = right.Left;
                right.Left = node;
                node.Parent = right;
                node.Right = temp; if (temp != null)
                {
                    temp.Parent = node;
                }
                if (right != null)
                {
                    right.Parent = nodeParent;
                }
                node = right;
            }
            private void FixTreeAfterInsertion(Node child, Node parent,
                        Node grandParent, Node greatGrandParent)
            {
                if (grandParent != null)
                {
                    Node uncle = (grandParent.Right == parent) ? grandParent.Left : grandParent.Right;
                    if (uncle != null && parent.Red && uncle.Red)
                    {
                        uncle.Red = false;
                        parent.Red = false;
                        grandParent.Red = true;
                        Node higher = null;
                        Node stillHigher = null;
                        if (greatGrandParent != null)
                        {
                            higher = greatGrandParent.Parent;
                        }
                        if (higher != null)
                        {
                            stillHigher = higher.Parent;
                        }
                        FixTreeAfterInsertion(grandParent, greatGrandParent,
                        higher, stillHigher);
                    }
                    else if (uncle == null || parent.Red && !uncle.Red)
                    {
                        if (grandParent.Right == parent && parent.Right == child)
                        { // right-right case
                            parent.Red = false;
                            grandParent.Red = true; if (greatGrandParent != null)
                            {
                                if (greatGrandParent.Right == grandParent)
                                {
                                    LeftRotate(ref grandParent);
                                    greatGrandParent.Right = grandParent;
                                }
                                else
                                {
                                    LeftRotate(ref grandParent);
                                }
                                greatGrandParent.Left = grandParent;
                            }
                        }
                        else
                        {
                            LeftRotate(ref root);
                        }
                    }
                    else if (grandParent.Left == parent &&
                  parent.Left == child)
                    { // left-left case
                        parent.Red = false;
                        grandParent.Red = true; if (greatGrandParent != null)
                        {
                            if (greatGrandParent.Right == grandParent)
                            {
                                RightRotate(ref grandParent);
                                greatGrandParent.Right = grandParent;
                            }
                            else
                            {
                                RightRotate(ref grandParent);
                                greatGrandParent.Left = grandParent;
                            }
                        }
                        else
                        {
                            RightRotate(ref root);
                        }
                    }
                    else if (grandParent.Right == parent && parent.Left == child)
                    {// right-left case
                        child.Red = false;
                        grandParent.Red = true;
                        RightRotate(ref parent);
                        grandParent.Right = parent; if (greatGrandParent != null)
                        {
                            if (greatGrandParent.Right == grandParent)
                            {
                                LeftRotate(ref grandParent);
                                greatGrandParent.Right = grandParent;
                            }
                            else
                            {
                                LeftRotate(ref grandParent);
                                greatGrandParent.Left = grandParent;
                            }
                        }
                        else
                        {
                            LeftRotate(ref root);
                        }
                    }
                    else if (grandParent.Left == parent &&
                  parent.Right == child)
                    {// left-right case
                        child.Red = false;
                        grandParent.Red = true;
                        LeftRotate(ref parent);
                        grandParent.Left = parent; if (greatGrandParent != null)
                        {
                            if (greatGrandParent.Right == grandParent)
                            {
                                RightRotate(ref grandParent);
                                greatGrandParent.Right = grandParent;
                            }
                            else
                            {
                                RightRotate(ref grandParent); greatGrandParent.Left = grandParent;
                            }
                        }
                        else
                        {
                            RightRotate(ref root);
                        }
                    }
                }
                if (root.Red)
                {
                    root.Red = false;
                }
            }
        }
    }
}
