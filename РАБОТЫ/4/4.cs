using System;

namespace Practice1.Task4
{
    class Program
    {
        static void Main()
        {
            int[,] grades = new int[3, 5];
            double[] averages = new double[3];

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Оценки студента " + (i + 1) + ":");
                int sum = 0;

                for (int j = 0; j < 5; j++)
                {
                    Console.Write("Оценка " + (j + 1) + ": ");
                    grades[i, j] = Convert.ToInt32(Console.ReadLine());
                    sum += grades[i, j];
                }

                averages[i] = (double)sum / 5;
            }

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Средняя оценка студента " + (i + 1) + ": " + averages[i].ToString("F2"));
            }
        }
    }
}
