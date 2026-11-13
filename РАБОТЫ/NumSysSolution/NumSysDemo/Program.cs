using System;
using NumSysLib;

namespace NumSysDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Adapter adapter = new Adapter();

            try
            {
                Console.Write("Введите число: ");
                string value = Console.ReadLine() ?? "";

                Console.Write("Введите исходное основание (2, 8, 10 или 16): ");
                int fromBase = int.Parse(Console.ReadLine() ?? "");

                Console.Write("Введите основание результата (2, 8, 10 или 16): ");
                int toBase = int.Parse(Console.ReadLine() ?? "");

                string result = adapter.Convert(value, fromBase, toBase);
                Console.WriteLine("Результат: " + result);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка ввода данных. Основание должно быть целым числом.");
            }
            catch (ArgumentException error)
            {
                Console.WriteLine("Ошибка ввода данных: " + error.Message);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка ввода данных. Введено слишком большое число.");
            }
        }
    }
}
