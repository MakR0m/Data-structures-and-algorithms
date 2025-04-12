using System.Collections.Concurrent;

namespace Algorithms
{
    internal class Program
    {
        // Алгоритм — последовательность действий решения задачи.
        // Скорость (время выполнения) и объем расходуемой памяти - критерии эффективности алгоритма.
        // Время выполнения зависит от желаза, поэтому на общий случай оценивают по О-нотации (Big O) (асимптотическая сложность)
        // Доступ по индексу arr[1] - сложность О(1) (одно действие (константное время)). Перебор всех элементов массива - О(n). Цикл в цикле с перебором всех элементов - О(n^2). 
        // Основные задачи - поиск и сортировка
        static void Main(string[] args)
        {
            int[] arr = { 5, 15, 1, 74, 2, 10 };
            Array.Sort(arr);
            var i = BinarySearch(3, arr);
            var result = FindMinMax(arr);
            Console.WriteLine($"min:{result.min} max:{result.max}");

            int n = arr.Where(x => x > 5).First();

            int[] arr1 = { 5, 15, 1, 74, 2, 10 };
            BubbleSort(arr1);
            int[] arr2 = { 5, 15, 1, 74, 2, 10 };
            InsertionSort(arr2);
            int[] arr3 = { 5, 15, 1, 74, 2, 10 };
            QuickSort(arr3, 0, arr.Length-1);

            int[] data = { 8, 3, 5, 1, 4 };
            int[] sorted = MergeSort(data);

            Console.WriteLine(string.Join(",", sorted)); // Первый параметр - разделитель. Второй коллекция. Джойн перебирает элементы коллекции, выводит и соединяет разделителем.  Использует стрингбилдер
        }

        

        private static int LinearSearch(int target, int[] ints) //Линейный поиск (поиск по всем элементам массива перебором). Возвращает индекс массива, где лежит элемент. Сложность О(n)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                if (ints[i] == target)
                    return i;
            }
            return -1;
        }

        private static int BinarySearch(int target, int[] ints)  //Работает только на отсортированных массивах. Делит массив пополам и отбрасывает ненужную часть. O(log n) сложность уменьшается в два раза на каждом шаге
        {
            int leftLimit = 0;
            int rightLimit = ints.Length - 1;
            while (leftLimit <= rightLimit)
            {
                int center = (leftLimit + rightLimit) / 2;
                if (target == ints[center])
                    return center;
                else
                    if (target < ints[center])    // если таргет не равен значению в центре, значит нужно сместить на единицу, а не равнять центру, потому что если бы равен был центру, то мы бы нашли значение
                         rightLimit = center - 1;
                else
                         leftLimit = center + 1;
            }
            return -1;
        }

        private static (int min, int max) FindMinMax(int[] ints)
        {
            int min = ints[0], max = ints[0]; 
            for (int i = 1; i < ints.Length; i++)
            {
                if (min > ints[i])
                    min = ints[i];
                if (max < ints[i]) 
                    max = ints[i];
            }
            return (min, max);
        }

        private static int FindFirstGreater(int[] ints, int x)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                if (ints[i] > x)
                    return ints[i];
            }
            return -1;
        }

        private static List<int> FindAllIndices(int[] ints, int x)
        {
            var indices = new List<int>();
            for (int i = 0; i < ints.Length; i++)
            {
                if (x == ints[i])
                    indices.Add(i);
            }
            return indices;
        }

        private static void BubbleSort(int[] ints)   // Внейшний цикл сокращает внутренний (т.к. часть пузырьков в процессе уже наверху). Флаг добавлен, чтобы досрочно завершить сортировку если в последнем проходе никто не менялся
        {
            int buffer;                              //BubbleSort меняет соседние элементы местами
            bool notSwapped;
            int nMax = ints.Length - 1;
            for (int n = 0; n < nMax; n++)
            {
                notSwapped = true;
                for (int i = 0; i < ints.Length-n-1; i++)
                {
                    if (ints[i] > ints[i+1])
                    {
                        buffer = ints[i];
                        ints[i] = ints[i+1];
                        ints[i+1] = buffer;
                        notSwapped = false;
                    }
                }
                if (notSwapped)
                    return;
            }
        }
                                                        //Сортировка вставками
        private static void InsertionSort(int[] ints)   // Запоминаем текущий элемент (например 3-й), смотрим соседние слева (сначала ближний). Пробегаемся по всем элементам слева, пока левые больше текущего, когда нет - вставляем
        {                                               // Пробежались и определили для него место
            for (int i = 1; i < ints.Length; i++)
            {
                int current = ints[i];
                int j = i - 1;
                while (j >= 0 && current < ints[j])
                {
                    ints[j+1] = ints[j];
                    j--;
                }
                ints[j+1] = current;
            }
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            if (left >= right)
                return;

            int pivot = array[(left + right) / 2];
            int index = Partition(array, left, right, pivot);
            QuickSort(array, left, index - 1);
            QuickSort(array, index, right);
        }

        private static int Partition(int[] array, int left, int right, int pivot)
        {
            while (left <= right)
            {
                while (array[left] < pivot) left++;
                while (array[right] > pivot) right--;

                if (left <= right)
                {
                    (array[left], array[right]) = (array[right], array[left]);
                    left++;
                    right--;
                }
            }
            return left;
        }

        private static int[] MergeSort (int[] array)
        {
            // Базовый случай: 0 или 1 элемент — массив уже отсортирован
            if (array.Length <= 1)
                return array;

            // Находим середину
            int mid = array.Length / 2;

            // Разделяем массив на левую и правую части
            int[] left = array[..mid];  // вырезает подмассив
            int[] right = array[mid..];

            // Рекурсивно сортируем каждую часть
            int[] sortedLeft = MergeSort(left);
            int[] sortedRight = MergeSort(right);

            // Объединяем отсортированные части
            return Merge(sortedLeft, sortedRight);

        }

        private static int[] Merge(int[] left, int[] right)
        {
            int[] result = new int[left.Length + right.Length];
            int i = 0, l = 0, r = 0;

            // Пока в обоих массивах есть элементы
            while (l < left.Length && r < right.Length)
            {
                if (left[l] < right[r])
                {
                    result[i++] = left[l++];
                }
                else
                {
                    result[i++] = right[r++];
                }
            }

            // Добавляем оставшиеся элементы (если остались)
            while (l < left.Length)
                result[i++] = left[l++];

            while (r < right.Length)
                result[i++] = right[r++];

            return result;
        }

    }
}
