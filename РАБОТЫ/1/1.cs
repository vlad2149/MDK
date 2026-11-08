using System;

namespace Practice1.Task1
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите длину прямоугольника: ");
            double length = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите ширину прямоугольника: ");
            double width = Convert.ToDouble(Console.ReadLine());

            if (length <= 0 || width <= 0)
            {
                Console.WriteLine("Стороны должны быть больше нуля.");
                return;
            }

            double area = length * width;
            double perimeter = 2 * (length + width);
            Console.WriteLine("Площадь: " + area);
            Console.WriteLine("Периметр: " + perimeter);
        }
    }
}
