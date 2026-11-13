using System;
using NumSysLib;

namespace NumSysIntegrationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Adapter adapter = new Adapter();

            Console.WriteLine("Проверка подключённой DLL-библиотеки");
            Console.WriteLine("11111111 (2) = " + adapter.Convert("11111111", 2, 16) + " (16)");
            Console.WriteLine("377 (8) = " + adapter.Convert("377", 8, 10) + " (10)");
            Console.WriteLine("255 (10) = " + adapter.Convert("255", 10, 2) + " (2)");
        }
    }
}
