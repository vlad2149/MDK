using System;
using System.Collections.Generic;

namespace Practice2.Task4
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, string> contacts = new Dictionary<string, string>();
            while (true)
            {
                Console.WriteLine("1 - Добавить, 2 - Поиск, 3 - Просмотр, выход - Завершить");
                Console.Write("Команда: ");
                string command = Console.ReadLine() ?? "";

                if (command == "выход")
                {
                    break;
                }
                if (command == "1")
                {
                    Console.Write("Имя: ");
                    string name = Console.ReadLine() ?? "";
                    Console.Write("Телефон: ");
                    string phone = Console.ReadLine() ?? "";
                    if (name != "")
                    {
                        contacts[name] = phone;
                        Console.WriteLine("Контакт сохранён.");
                    }
                }
                else if (command == "2")
                {
                    Console.Write("Имя для поиска: ");
                    string name = Console.ReadLine() ?? "";
                    if (contacts.ContainsKey(name))
                    {
                        Console.WriteLine(name + ": " + contacts[name]);
                    }
                    else
                    {
                        Console.WriteLine("Контакт не найден.");
                    }
                }
                else if (command == "3")
                {
                    foreach (KeyValuePair<string, string> contact in contacts)
                    {
                        Console.WriteLine(contact.Key + ": " + contact.Value);
                    }
                }
                else
                {
                    Console.WriteLine("Неизвестная команда.");
                }
            }
        }
    }
}
