using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    internal class MyQueue : MyStructs
    {
        private int _head = 0; //Индекс первого элемента очереди FIFO первый зашел, первый вышел
        private int _tail = 0; //Индекс последнего элемента 

        public void Enqueue(int value) // Добавить в конец очереди
        {
            if (_count + _head == _items.Length)
                Extension();

            if (_head > _count/2)
                Compact();

            _items[_head + _count] = value;
            _count++;
        }

        public int Dequeue()    // Удаление первого элемента из очереди (FIFO). Вместо копирования массива смещаем индекс _head — логическая граница "начала" очереди.
        {
            IsEmptyCheck();
            int outer = _items[_head];
            _items[_head] = 0;
            _head++;
            _count--;
            return outer;
        }

        public int Peek()
        {
            IsEmptyCheck();
            return _items[_head];
        }

        private void Compact ()
        {
            for (int i = 0; i < _count; i++)
            {
                _items[i] = _items[_head+i];
            }
            _head = 0;
        }

        public override void Print()
        {
            _tail = _head+_count;
            Console.Write("[ ");
            for (int i = _head; i < _tail; i++)
            {
                Console.Write($"{_items[i]} ");
            }
            Console.WriteLine("]");
        }

    }
}
