using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    public abstract class MyStructs
    {
        protected int[] _items;
        protected int _count;

        public int Count { get { return _count; } }

        public MyStructs()
        {
            _items = new int[4];
            _count = 0;
        }

        protected void Extension()
        {
            int[] newItems = new int[_items.Length * 2];
            Array.Copy(_items, newItems, _count);
            _items = newItems;
        }


        public virtual void Print()
        {
            try
            {
                Console.Write("[ ");

                for (int i = 0; i < _count; i++)
                {
                    Console.Write($"{_items[i]} ");
                }

                Console.WriteLine("]");
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public void Print(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            Console.WriteLine(_items[index]);
        }

        protected void IsEmptyCheck()
        {
            if (_count < 1)
                throw new InvalidOperationException();
        }
    }
}
