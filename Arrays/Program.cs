using System.IO.Pipes;
using Structures.Binary_Search_Tree;
using Structures.HashTable;
using Structures.LinkedList;

namespace Structures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Динамические массивы");
            MyArray array = new MyArray();
            array.Add(1);
            array.Add(2);
            array.Add(3);
            array.Add(4);
            array.Remove(1);
            array.Print();
            array.Print(2);
            Console.WriteLine("Аналог стека:");
            MyStack stack = new MyStack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Pop();
            stack.Peek();
            stack.Print();

            Console.WriteLine("Очередь:");
            MyQueue q = new MyQueue();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Print();
            q.Peek();
            q.Dequeue();
            q.Print();

            Console.WriteLine("Список:");
            MySinglyLinkedList linkedList = new MySinglyLinkedList();
            linkedList.AddFirst(1);
            linkedList.AddLast(2);
            linkedList.AddLast(3);
            linkedList.RemoveLast();
            linkedList.AddFirst(5);
            linkedList.AddLast(6);
            linkedList.RemoveFirst();
            linkedList.Print();

            Console.WriteLine("Хеш Таблица:");
            var table = new MyHashTable();

            table.Add("apple", 1);
            table.Add("banana", 2);
            table.Add("orange", 3);
            table.Add("apple", 10); // коллизия, перезапишет

            Console.WriteLine(table.Get("apple")); // 10

            table.Print();


            Console.WriteLine("Бинарное дерево поиска:");
            BinarySearchTree bst = new BinarySearchTree();
            bst.Insert(3);
            bst.Insert(6);
            bst.Insert(5);
            var b = bst.Contains(5);
            bst.PrintInOrder();
            bst.Remove(5);
            bst.PrintInOrder();



        }
    }
}
