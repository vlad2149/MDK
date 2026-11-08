using System;
using System.Collections.Generic;

namespace Practice2.Task3
{
    class Program
    {
        static void Main()
        {
            List<int> numbers = new List<int>();

            while (true)
            {
                Console.Write("Введите целое число: ");
                int number = Convert.ToInt32(Console.ReadLine());
                if (numbers.Contains(number))
                {
                    Console.WriteLine("Число повторилось. Ввод завершён.");
                    break;
                }
                numbers.Add(number);
            }

            Console.WriteLine("Массив до первого повторения:");
            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();
        }
    }
}
