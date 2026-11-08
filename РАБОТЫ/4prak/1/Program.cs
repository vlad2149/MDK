using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeAnalytics
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = AppContext.BaseDirectory;
            string inputFile = Path.Combine(folder, "employee.txt");
            string logFile = Path.Combine(folder, "log.txt");
            string resultFile = Path.Combine(folder, "result.txt");

            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Файл employee.txt не найден: " + inputFile);
                return;
            }

            DataReader reader = new DataReader();
            List<Employee> employees = reader.ReadEmployees(inputFile, logFile);
            if (employees.Count == 0)
            {
                Console.WriteLine("В файле нет корректных данных о сотрудниках.");
                return;
            }

            decimal totalSalary = 0;
            Employee oldest = employees[0];
            foreach (Employee employee in employees)
            {
                totalSalary += employee.Salary;
                if (employee.Age > oldest.Age)
                {
                    oldest = employee;
                }
            }

            decimal averageSalary = totalSalary / employees.Count;
            using (StreamWriter writer = new StreamWriter(resultFile))
            {
                writer.WriteLine("Количество сотрудников: " + employees.Count);
                writer.WriteLine("Средняя зарплата: " + averageSalary.ToString("F2"));
                writer.WriteLine("Самый старший сотрудник: " + oldest.Name + ", возраст: " + oldest.Age);
            }
            Console.WriteLine("Обработано сотрудников: " + employees.Count);
            Console.WriteLine("Результат: " + resultFile);
            Console.WriteLine("Ошибки чтения: " + logFile);
        }
    }
}
