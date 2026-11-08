using System;
using System.Collections.Generic;
using System.IO;

namespace EnterpriseStorageDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] warehouseStock = { "Корпус A-10", "Плата ЦП-v2", "Модуль ОЗУ-8G", "Блок питания-500W" };
            int[] requestedIndices = { 0, 2, 1, 4, 3 };

            Console.WriteLine("Запуск процедуры отгрузки компонентов со склада...");
            List<string> dispatchList = ExtractCargo(warehouseStock, requestedIndices);
            Console.WriteLine("Успешно подготовлено к отгрузке: " + dispatchList.Count);
        }

        static List<string> ExtractCargo(string[] stock, int[] indices)
        {
            List<string> order = new List<string>();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (desktop == "")
            {
                desktop = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop");
            }
            Directory.CreateDirectory(desktop);
            string logPath = Path.Combine(desktop, "warehouse_errors.log");

            for (int i = 0; i < indices.Length; i++)
            {
                int targetIndex = indices[i];
                try
                {
                    string item = stock[targetIndex];
                    order.Add(item);
                    Console.WriteLine("[OK] Компонент '" + item + "' добавлен в заказ.");
                }
                catch (IndexOutOfRangeException ex)
                {
                    string message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | Индекс: {targetIndex} | Ошибка: {ex.Message}";
                    using (StreamWriter writer = new StreamWriter(logPath, true))
                    {
                        writer.WriteLine(message);
                    }
                    Console.WriteLine(message);
                }
            }
            Console.WriteLine("Файл ошибок: " + logPath);
            return order;
        }
    }
}
