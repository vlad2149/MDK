using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FileReaderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (desktop == "")
            {
                desktop = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop");
            }
            string fullPath = Path.Combine(desktop, "lorem.txt");
            if (!File.Exists(fullPath))
            {
                Console.WriteLine("Файл lorem.txt не найден. Сначала запустите проект ls.");
                return;
            }

            Stopwatch sw = Stopwatch.StartNew();
            Dictionary<char, int> counts = new Dictionary<char, int>();
            using (StreamReader reader = new StreamReader(fullPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        char symbol = line[i];
                        if (counts.ContainsKey(symbol))
                        {
                            counts[symbol]++;
                        }
                        else
                        {
                            counts[symbol] = 1;
                        }
                    }
                }
            }
            sw.Stop();
            foreach (KeyValuePair<char, int> count in counts)
            {
                Console.WriteLine($"'{count.Key}': {count.Value}");
            }
            Console.WriteLine("Время чтения: " + sw.ElapsedMilliseconds + " мс");
        }
    }
}
