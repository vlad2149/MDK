using System;
using System.Collections.Generic;
using System.IO;

namespace EnterpriseEquipmentDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Equipment> registrationQueue = new List<Equipment>
            {
                new Equipment { ModelName = "Сервер SRV-200", SerialNumber = "SN-99482" },
                new Equipment { ModelName = "Коммутатор SW-10", SerialNumber = "" },
                new Equipment { ModelName = "Маршрутизатор RT-5", SerialNumber = "SN-11023" }
            };

            Console.WriteLine("Запуск регистрации оборудования...");
            RegisterEquipmentBatch(registrationQueue);
            Console.WriteLine("Регистрация завершена.");
        }

        static void RegisterEquipmentBatch(List<Equipment> items)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (desktop == "")
            {
                desktop = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop");
            }
            Directory.CreateDirectory(desktop);
            string logPath = Path.Combine(desktop, "equipment_errors.log");

            foreach (Equipment item in items)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(item.SerialNumber))
                    {
                        throw new ArgumentException("Серийный номер оборудования не указан.");
                    }
                    RegisterInSystem(item);
                }
                catch (ArgumentException ex)
                {
                    string message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | Модель: {item.ModelName} | Сбой: {ex.Message}";
                    using (StreamWriter writer = new StreamWriter(logPath, true))
                    {
                        writer.WriteLine(message);
                    }
                    Console.WriteLine(message);
                }
            }
            Console.WriteLine("Файл ошибок: " + logPath);
        }

        static void RegisterInSystem(Equipment eq)
        {
            Console.WriteLine($"[СИСТЕМА] Оборудование '{eq.ModelName}' (S/N: {eq.SerialNumber}) успешно внесено в реестр.");
        }
    }
}
