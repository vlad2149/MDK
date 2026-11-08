using System;

namespace Practice2.Task1
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите размер массива N: ");
            int n = Convert.ToInt32(Console.ReadLine());
            if (n <= 0)
            {
                Console.WriteLine("Размер массива должен быть больше нуля.");
                return;
            }

            int[] numbers = new int[n];
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                Console.Write("Число " + (i + 1) + ": ");
                numbers[i] = Convert.ToInt32(Console.ReadLine());
                sum += numbers[i];
            }

            Console.WriteLine("Массив в обратном порядке:");
            for (int i = n - 1; i >= 0; i--)
            {
                Console.Write(numbers[i] + " ");
            }
            Console.WriteLine();

            double average = sum / n;
            int closest = numbers[0];
            for (int i = 1; i < n; i++)
            {
                if (Math.Abs(numbers[i] - average) < Math.Abs(closest - average))
                {
                    closest = numbers[i];
                }
            }

            Console.WriteLine("Среднее арифметическое: " + average.ToString("F2"));
            Console.WriteLine("Ближайшее к среднему число: " + closest);
        }
    }
}
