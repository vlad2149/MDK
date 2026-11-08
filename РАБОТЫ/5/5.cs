using System;

namespace Practice1.Task5
{
    class Program
    {
        static void Main()
        {
            int[,] first = new int[3, 3];
            int[,] second = new int[3, 3];

            Console.WriteLine("Введите 3 строки по 3 числа через пробел:");
            for (int i = 0; i < 3; i++)
            {
                string[] parts = (Console.ReadLine() ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 3)
                {
                    Console.WriteLine("В каждой строке должно быть ровно 3 числа.");
                    return;
                }

                for (int j = 0; j < 3; j++)
                {
                    if (!int.TryParse(parts[j], out first[i, j]))
                    {
                        Console.WriteLine("Нужно вводить целые числа.");
                        return;
                    }
                    second[j, i] = first[i, j];
                }
            }

            Console.WriteLine("Было:");
            PrintMatrix(first);
            Console.WriteLine("Стало:");
            PrintMatrix(second);
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
