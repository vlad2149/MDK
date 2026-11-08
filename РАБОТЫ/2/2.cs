using System;

namespace Practice1.Task2
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите сумму в рублях: ");
            double rubles = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите текущий курс (сколько рублей за доллар): ");
            double rate = Convert.ToDouble(Console.ReadLine());

            if (rubles < 0 || rate <= 0)
            {
                Console.WriteLine("Сумма не может быть отрицательной, а курс должен быть больше нуля.");
                return;
            }

            double dollars = rubles / rate;
            Console.WriteLine("Получится долларов: " + dollars.ToString("F2"));
        }
    }
}
