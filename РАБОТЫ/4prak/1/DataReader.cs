using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeAnalytics
{
    class DataReader
    {
        public List<Employee> ReadEmployees(string filePath, string logPath)
        {
            List<Employee> employees = new List<Employee>();
            using (StreamReader reader = new StreamReader(filePath))
            using (StreamWriter log = new StreamWriter(logPath))
            {
                string line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    string[] parts = line.Split(';');
                    if (parts.Length != 3 || parts[0].Trim() == "" || parts[1].Trim() == "" || parts[2].Trim() == "")
                    {
                        log.WriteLine("Строка " + lineNumber + ": отсутствуют данные.");
                        continue;
                    }

                    int age;
                    decimal salary;
                    if (!int.TryParse(parts[1], out age) || !decimal.TryParse(parts[2], out salary))
                    {
                        log.WriteLine("Строка " + lineNumber + ": вместо числа указан текст.");
                        continue;
                    }

                    Employee employee = new Employee
                    {
                        Name = parts[0].Trim(),
                        Age = age,
                        Salary = salary
                    };
                    employees.Add(employee);
                }
            }
            return employees;
        }
    }
}
