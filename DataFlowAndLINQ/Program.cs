namespace DataFlowAndLINQ
{
    internal class Program
    {
        //LINQ (Language Integrated Query) — это способ работать с коллекциями (и не только) через запросоподобные выражения прямо в языке C#.
        //Работает с List, Array, Dictionary, IEnumerable, а также с XML, SQL и даже файлами.

        static void Main(string[] args)
        {
            var lst = EvenNumbers([5, 6, 7, 8, 9,]);

            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5 };
            var even = numbers.Where(n => n % 2 == 0);  // Первый вид записи LINQ
            var even1 = from x in numbers               // SQL подобный вид
                        where x % 2 == 0
                        select x;
             
            var query = numbers.Where(x => x % 2 == 0); // ещё не выполняется. LINQ не "бежит" по данным сразу — только когда ты начинаешь использовать результат, например, через foreach, .ToList(), First() и т.д.
            foreach (var x in query)                    // только тут выполняется
                Console.WriteLine(x);

            var words = new[] { "apple", "banana", "pear", "plum" };
            var shortWord = words
                .Where(w => w.Length <= 4)
                .OrderBy(w => w)
                .Select(w => w.ToUpper());
            foreach (var w in shortWord)
                Console.WriteLine(w);

            var groups = words.GroupBy(w => w[0]); // группировка по первой букве. 
            foreach (var group in groups)
            {
                Console.WriteLine($"Key: {group.Key}");
                foreach (var word in group)
                    Console.WriteLine($"{word}");
            }

            var result = words     // группировка по первой букве с проецированием в анонимные типы или DTO модели
                .GroupBy(w => w[0])
                .Select(g => new   //анонимный тип
                {
                    Letter = g.Key,
                    Count = g.Count(),
                    Words = g.ToList()
                });

        }

        private static void CollectionInterfaces() // Базовые интерфейсы коллекций
        {
            IEnumerable<int> list = new List<int>() { 1 }; // Не наследуется ни от кого и позволяет перебирать элементы. Основной интерфейс LINQ. Когда нужно вернуть "только для чтения"
            ICollection<int> list2 = new List<int>(); // Наследуется от IEnumerable<T> + кол-во, добавление и удаление (базовые операции с коллекцией). Когда нужны добавление/удаление
            IList<int> list3 = new List<int>(); // Наследуется от ICollection<T> + доступ по индексу. Когда нужна логика, основанная на позициях

            foreach (int i in list)  // GetEnumerator -> foreach
                Console.WriteLine(i);

            //Зачем разные интерфейсы? → Чем меньше обязанностей у типа, тем гибче код. Интерфейсы позволяют передавать только необходимые возможности.
        }

        public static IEnumerable<int> EvenNumbers(int[] numbers)
        {
            foreach (var n in numbers)
            {
                if (n % 2 == 0)
                    yield return n;  //возвращает значения по одному, без создания коллекции целиком. Это называется ленивой (отложенной) генерацией.
            }
            // Зачем нужен yield: Фильтрация данных "на лету", Обработка больших наборов данных без загрузки в память, Создание пользовательских итераторов без необходимости писать IEnumerator
            // Нельзя использовать yield: в async методах, в try-catch-блоках (но try-finally можно), с параметрами ref, out
            // Важно: yield ≠ возвращение массива или списка
        }
    }
}
