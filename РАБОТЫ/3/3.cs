using System;

namespace Practice1.Task3
{
    class Program
    {
        static void Main()
        {
            double[] temperatures = new double[7];
            double sum = 0;

            for (int i = 0; i < 7; i++)
            {
                Console.Write("Температура за день " + (i + 1) + ": ");
                temperatures[i] = Convert.ToDouble(Console.ReadLine());
                sum += temperatures[i];
            }

            double average = sum / temperatures.Length;
            Console.WriteLine("Средняя температура: " + average.ToString("F2"));
        }
    }
}
