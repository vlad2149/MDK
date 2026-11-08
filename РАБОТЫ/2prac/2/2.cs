using System;

namespace Practice2.Task2
{
    class Program
    {
        static void Main()
        {
            Console.Write("Количество студентов: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Количество предметов: ");
            int m = Convert.ToInt32(Console.ReadLine());
            if (n <= 0 || m <= 0)
            {
                Console.WriteLine("Количество студентов и предметов должно быть больше нуля.");
                return;
            }

            int[,] grades = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Студент " + (i + 1));
                for (int j = 0; j < m; j++)
                {
                    Console.Write("Оценка по предмету " + (j + 1) + ": ");
                    grades[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < m; j++)
                {
                    sum += grades[i, j];
                }
                Console.WriteLine("Средняя оценка студента " + (i + 1) + ": " + (sum / m).ToString("F2"));
            }
        }
    }
}
