using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.HashTable
{
    public class MyHashTable
    {
        private List<Entry>[] _buckets;
        private int _capacity;

        public MyHashTable(int capacity = 10)
        {
            _capacity = capacity;
            _buckets = new List<Entry>[capacity];
                
        }

        private int GetIndex(string key)
        {
            int hash = key.GetHashCode();
            int index = Math.Abs(hash % _capacity);
            return index;
        }


        public void Add(string key, int value)
        {
            int index = GetIndex(key);                  // Получаем индекс массива на основе хеша ключа, получаем остаток от деления хеша ключа на размер массива по модулю.
                                                        // Хеш функция используется для равномерного распределения Entry по массиву

            if (_buckets[index] == null)
                _buckets[index] = new List<Entry>();       //Если в массиве по данному индексу нет значения, то создаем новый список и кладем в него.

            foreach (Entry entry in _buckets[index])      //Ищем ключ: если есть значение - заменяем, если нет — добавляем
            {
                if (entry.Key == key)
                {
                    entry.Value = value;
                    return;
                }
            }

            _buckets[index].Add(new Entry(key, value));

        }

        public int Get(string key)
        {
            int index = GetIndex(key);
            var bucket = _buckets[index];

            if (bucket != null)
            {
                foreach (var entry in bucket)
                {
                    if (entry.Key == key)
                        return entry.Value;
                }
            }

            throw new KeyNotFoundException($"Ключ '{key}' не найден.");

        }

        public void Remove(string key)
        {
            int index = GetIndex(key);
            var bucket = _buckets[index];

            if (bucket != null)
            {
                for (int i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key == key)
                    {
                        bucket.RemoveAt(i);
                        return;
                    }
                }
            }

            throw new KeyNotFoundException($"Ключ '{key}' не найден.");

        }

        public void Print()
        {
            for (int i = 0; i < _capacity; i++)
            {
                Console.Write($"[{i}]: ");
                if (_buckets[i] != null)
                {
                    foreach (var entry in _buckets[i])
                    {
                        Console.Write($"({entry.Key}: {entry.Value}) ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
