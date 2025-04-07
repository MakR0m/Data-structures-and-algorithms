using System.Net.Sockets;
using System.Text;

namespace Vulues_types_and_Reference_types
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TypesAndStringWork();
            // VarObjDyn();
            object obj = 10;
            dynamic dyn = obj;

            int result = dyn + 5; //dyn становится dynamic, но указывает на int. C# "разворачивает" это в рантайме, как будто 10 + 5
            Console.WriteLine(result);



        }

        private static void VarObjDyn()
        {
            //var - неявный, но строготипизированный. Тип определяется на этапе компиляции. Ничего общего с динамическими типами не имеет. Просто сахар
            //object - хранит любой тип, т.к. все наследуются от класса object, но при использовании нужно приведение
            //object o = 123; int x = (int)o;
            // InvalidCastExeption ошибка приведения.

            //dynamic - тип определяемый в рантайме. Во время выполнения

            dynamic d = 5;
            d.Method(); //Компилятор не проверяет, есть ли у обьекта метод или свойство.В случае ошибки выкидывает ошибку во время выполнения.
            Console.WriteLine(d + 3); // 8

            //Когда использовать: var - когда тип очевиден, object - редко, обычно в универсальных коллекциях, старом API, dynamic - при работе с JSON, COM, Reflection, ExpandoObject, осторожно в бизнес логике (нет проверок)
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
    }
}
