using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    internal class MyStack : MyStructs
    {
        public void Push(int value)
        {
            if (_count == _items.Length)  // Просто метод расширения массива удвоением
            {
                Extension();
            }
            _items[_count] = value;
            _count++;
        }

        public int Pop()
        {
            IsEmptyCheck();
            int pop = _items[_count-1];
            _items[_count] = 0;
            _count--;
            return pop;
        }

        public int Peek()
        { 
            IsEmptyCheck();
            return _items[_count-1]; 
        }
    }
}
