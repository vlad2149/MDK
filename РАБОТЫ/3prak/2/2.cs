using System;
using System.Collections.Generic;
using System.IO;

namespace EnterpriseDirectoryDemo
{
    class Employee
    {
        public string FullName { get; set; }
        public string Department { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> incomingPayloads = new List<Employee>
            {
                new Employee { FullName = "Иванов И.И.", Department = "IT" },
                null,
                new Employee { FullName = null, Department = "HR" },
                new Employee { FullName = "Петров П.П.", Department = "Finance" }
            };

            Console.WriteLine("Запуск пакетной обработки профилей сотрудников...");
            ProcessDirectoryUpdate(incomingPayloads);
            Console.WriteLine("Пакетная обработка завершена.");
        }

        static void ProcessDirectoryUpdate(List<Employee> employees)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (desktop == "")
            {
                desktop = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop");
            }
            Directory.CreateDirectory(desktop);
            string logPath = Path.Combine(desktop, "directory_errors.log");

            for (int i = 0; i < employees.Count; i++)
            {
                try
                {
                    Employee emp = employees[i];
                    string uppercaseName = emp.FullName.ToUpper();
                    string record = "Сотрудник: " + uppercaseName + " [Департамент: " + emp.Department + "]";
                    Console.WriteLine("[БАЗА ДАННЫХ] " + record);
                }
                catch (NullReferenceException ex)
                {
                    string message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | Индекс: {i} | Ошибка: {ex.Message}";
                    using (StreamWriter writer = new StreamWriter(logPath, true))
                    {
                        writer.WriteLine(message);
                    }
                    Console.WriteLine(message);
                }
            }
            Console.WriteLine("Файл ошибок: " + logPath);
        }
    }
}
