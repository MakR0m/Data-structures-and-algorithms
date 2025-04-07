using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    internal class MyArray : MyStructs
    {

        //TO DO: добавление, удаление по индексу, вывести все элементы, вывести элемент по индексу.

        public void Add(int item)  // Добавление.
        {
            
            if (_count == _items.Length)
            {
                Extension();
            }
            _items[_count] = item;
            _count++;
        }

        public void Remove(int index)   // Удаление
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));
            try
            {
                _count--;
                int[] newItems = new int[_count];
                for (int i = 0; i < newItems.Length; i++)  // заполняем новый массив значениями старого до индекса и со смещением на единицу после удаляемого
                {
                    if (i < index)
                        newItems[i] = _items[i];
                    else
                        newItems[i] = _items[i + 1];
                }
                _items = newItems;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

	}
}
