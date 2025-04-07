using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.LinkedList
{
    internal class MySinglyLinkedList
    {
        private MyNode? _head; // Первый узел
        private int _count;

        public MySinglyLinkedList()
        {
            _head = null;
            _count = 0;
        }


        public void AddFirst(int value)   // Добавление в горлову списка.
        {
            MyNode node = new MyNode(value);
            if (_head != null)
                node.Next = _head;
            _head = node;
            _count++;
        }

        public void AddLast(int value) // Добавление в конец списка
        {
            MyNode? previous;
            MyNode? last;

            if (_head != null)
            {
                (previous, last) = FindLastAndPrevious();
                last.Next = new MyNode(value);
                _count++;
            }
            else AddFirst(value);

        }

        public void RemoveFirst()  // Удаление из головы списка
        {
            if (_head == null)
                return;

            if (_head.Next != null)
                _head = _head.Next;
            else
                _head = null;
            _count--;
        }

        public void RemoveLast()  // Удаление с конца списка
        {
            MyNode? previous;
            MyNode? last;

            if (_head != null)
            {
                (previous, last) = FindLastAndPrevious();
                if (previous != null)
                {
                    previous.Next = null;
                }
                _count--;
            }
            else RemoveFirst();
        }

        public void Print()
        {
            MyNode node = _head;
            while (node != null)
            {
                Console.Write(node.Value + " ");
                node = node.Next;
            }
        }

        private (MyNode? previous, MyNode? last) FindLastAndPrevious()   // Поиск последнего элемента в списке
        {
            if (_head != null)
            {
                MyNode? previous = null;
                MyNode last = _head;

                while (last.Next != null)
                {
                    previous = last;
                    last = last.Next;
                }
                return (previous, last);
            }
            else return (null, null);

        }

    }
}
