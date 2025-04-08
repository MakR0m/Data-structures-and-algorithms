using System.Net.Sockets;
using System.Text;

namespace Vulues_types_and_Reference_types
{
    //Когда использовать класс, когда рекорд, когда стракт
    // enum — если значения ограничены и логически предопределены (дни недели, статусы, роли, флаги и т.п.)
    // struct — если объект: маленький, часто копируется, не требует ссылочного поведения (координаты, цвета, временные точки)
    // class — почти во всех остальных случаях, объект с логикой
    // record - хранение данных, DTO, модели ответа API

    //C C#9/.NET 5+ record - это специальный вид class или struct, предназначенный для хранения неизменяемых данных, с автоматическим сравнением по значениям и поддержкой deconstruct.
    //Отличия от класса. Сравнение по значению, автосоздание ToString(), Equals(), GetHashCode(), поддержка with-выражения, иммутабельность по умолчанию  (у init)

    record Person1(string Name, int Age);         // Reference type
    record struct Point1(int X, int Y);           // Value type (C# 10+)/  У record struct оператор == не определён по умолчанию (в отличие от record class). Чтобы сравнить record struct, нужно использовать .Equals()


    enum DayOfWeek
    {
        Monday,    // 0   Используется для ограниченного набора значений. Значимый тип, копируется при передаче
        Tuesday,   // 1   По умолчанию enum основан на int, но можно задать другой тип (byte, short и тд)
        Wednesday, // 2   Значения можно приводить к числам и обратно
        Thursday = 10,
        Friday,    // 11
    }

    struct Point                         //Хранится в стеке (если не вложен в объект)
    {                                    //Копируется при передаче
        public int X;                    //Не может иметь наследование
        public int Y;                    //Подходит для маленьких НЕИЗМЕНЯЕМЫХ объектов (Point, DateTime, Color)
        public Point(int x, int y)       //Может иметь конструкторы и методы, но без явного конструктора по умолчанию
        {
            X = x;
            Y = y;
        } 

    }

    class Person                       //Хранится в куче, а в переменной — ссылка
    {                                  //Передаётся по ссылке
        public string Name;            //Поддерживает наследование, полиморфизм
        public int Age;                //Используется везде, где есть сложная логика или изменяемые данные
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            TypesAndStringWork();
            VarObjDyn();
            Nullable();
            ClassStructRecord();
            BoxUnbox();

        }

        private static void TypesAndStringWork()
        {
            //Копируются ли значения при передаче?	Только у value types
            //Где хранятся объекты классов?	В куче(heap), ссылка в стеке
            //string — это value или reference?	Reference(но ведёт себя как value *)
            //Нужно ли остерегаться side-effect'ов?	Да, при работе с ссылками (List, массивы и т.п.)
            //Особенности:
            //int — самый часто используемый тип целых чисел
            //float, double — числа с плавающей точкой(приближённые значения)
            //decimal — подходит для денег(высокая точность, медленнее по скорости)
            //char — один Unicode-символ
            //bool — логическое значение(true / false)

            int x = 10;
            long y = x; // Неявное преобразование (без потерь)
            long l = 1000000000;
            int i = (int)l; // Явное приведение (с возможной потерей)

            int.Parse("123");        // строка → int
            int.TryParse("abc", out int result); // безопасное преобразование

            char.IsDigit('5');       // true
            char.IsLetter('A');      // true

            bool.Parse("true");      // true

            int z = 2147483647;
            z = z + 1; // Вышли за диапазон и начали с первого значения в диапазоне -2147483648. integer overflow, по умолчанию в C# он не вызывает ошибку, а работает “по кругу”.

            Console.WriteLine(z);

            byte q = 100;
            int w = q;  //Неявное преобразование безопасно и выведет 100. int “шире” по диапазону, чем byte, и преобразование происходит неявно без потерь.

            int e = 300;
            byte r = (byte)e; //Выведет 44, потеря данных при явном преобразовании. byte хранит от 0 до 255.  300 % 256 = 44 → в byte остаётся “остаток по модулю”

            char c = 'A'; //Тип char в C# — это 16-битное число, представляющее Unicode-символ.
            int code = c;

            bool res = int.TryParse("abc", out int value);  // false, а value значением инта по умолчанию - 0. TryParse не кидает исключение, а возвращает false и пишет в value нулевое значение по умолчанию (default(int)).

            Console.WriteLine(res);
            Console.WriteLine(value);


            //String ведёт себя как value type в поведении (каждое изменение — создаёт новую строку). Его нельзя изменить после создания. string — неизменяемый (immutable)\

            string s1 = "Hello"; // Старое значение "Hello" по - прежнему существует(до тех пор, пока не будет удалено сборщиком мусора).
            s1 = s1 + " world";

            //Почему строки сделали неизменяемыми? Безопасность(могут использоваться как ключи в словарях). Потокобезопасность Упрощение кэширования и интернирования

            //s1 == s2          сравнение содержимого
            //s1.Equals(s2)     тоже сравнение содержимого
            //object.ReferenceEquals(s1, s2) сравнение по ссылке в памяти

            string s = "  Hello, world!  ";

            s.Trim();           // Удаление пробелов по краям
            s.ToUpper();        // В верхний регистр
            s.ToLower();        // В нижний регистр
            s.Substring(0, 5);  // "Hello"
            s.Contains("world"); // true
            s.StartsWith("He"); // true
            s.Replace("world", "C#"); // "Hello, C#!"


            string h = "";
            for (int k = 0; k < 1000; k++)
            {
                h += k; // создаётся 1000 строк!
            }
            // Конкатенация строк в цикле 1000 раз создает 1000 строк. Лучше использовать билдер

            var builder = new StringBuilder();
            for (int j = 0; j < 1000; j++)
            {
                builder.Append(i);
            }
            string f = builder.ToString();
        }
        
        private static void VarObjDyn()
        {
            //var - неявный, но строготипизированный. Тип определяется на этапе компиляции. Ничего общего с динамическими типами не имеет. Просто сахар
            //object - хранит любой тип, т.к. все наследуются от класса object, но при использовании нужно приведение
            //object o = 123; int x = (int)o;
            // InvalidCastExeption ошибка приведения.

            //dynamic - тип определяемый в рантайме. Во время выполнения

            //dynamic d = 5;
            //d.Method(); //Компилятор не проверяет, есть ли у обьекта метод или свойство.В случае ошибки выкидывает ошибку во время выполнения.
            //Console.WriteLine(d + 3); // 8

            //Когда использовать: var - когда тип очевиден, object - редко, обычно в универсальных коллекциях, старом API, dynamic - при работе с JSON, COM, Reflection, ExpandoObject, осторожно в бизнес логике (нет проверок)

            object obj = 10;
            dynamic dyn = obj;

            int result = dyn + 5; //dyn становится dynamic, но указывает на int. C# "разворачивает" это в рантайме, как будто 10 + 5
            Console.WriteLine(result);
        }

        private static void Nullable()
        {
            //По умолчанию значимые типы не могут быть null, у них есть значения по умолчанию.
            //Nullable<T> - int?, bool?

            int? age = null;
            if (age.HasValue)
                Console.WriteLine(age.Value);
            else
                Console.WriteLine("Возраст не найден");
            //Лучше и безопаснее использовать age?? или age.GetValueOrDefault();
            //Оператор ?? - "Если null, то..."
            int? age1 = null;
            int actualAge = age ?? 18; // если age == null, то возьмём 18
            //Оператор ??= - "Установи значение, если переменная null"
            string? name = null;
            name ??= "Гость";   // если имя пустое, то запиши "Гость"
            Console.WriteLine(name);

            //оператор ?. - безопасный доступ
            Person? p = null;
            Console.WriteLine(p?.Name); //Не вызовет NullReferenceException.Если p == null, выражение возвращает null, не упадёт с исключением.
            //Полезно в работе с базами данных(int? из SQL), при парсинге JSON, XML, когда есть опциональные поля, в моделях(например, пользователь без email)

            string? input = null;
            string result = input?.ToUpper() ?? "DEFAULT";  // result вернул пусто во время приведение к верхнему регистру, потому что стоит ?., а раз нал, то значение "DEFAULT"

            Console.WriteLine(result);

        }

        private static void ClassStructRecord()
        {
            DayOfWeek en = DayOfWeek.Monday; //Значения можно приводить к числам и обратно
            Console.WriteLine((int)en);
            DayOfWeek en1 = (DayOfWeek)10;
            Console.WriteLine(en1);

            var p1 = new Person1("Anna", 30);
            var p2 = new Person1("Anna", 30);

            Console.WriteLine(p1 == p2); // true! сравнение по значению

            var p3 = p1 with { Age = 31 };
            Console.WriteLine(p3); // Person { Name = Anna, Age = 31 }
        }
        
        private static void BoxUnbox()   
        {
            int i = 42;
            object o = i; // ← boxing происходит здесь. Преобразование value type (например, int, double) в object, то есть упаковка значения в ссылку. Значение 42 упаковывается в новый объект в куче (heap).
            int j = (int)o; // ← unboxing. Обратный процесс: извлечение value type из object

            // Boxing → создаёт объект в куче. Unboxing → требует точного приведения типа
            // Это неявное выделение памяти + может быть медленным, особенно в циклах или при работе с коллекциями

            // Boxing происходит: при присваивании value type переменной типа object или interface
            // При передаче value type в метод, принимающий object
            // При добавлении value type в старые коллекции (ArrayList, Hashtable - не дженерики хранят все как object). List<T> избегает лишней запаковки, распаковки

            //object o = 42; Unboxing требует точного указания типа, какой тип запаковали, тот и должны указать при распаковке, иначе ошибка.
            //int x = (int)o;    ✅
            //long y = (long)o;  ❌ InvalidCastException
        }
    }
}
