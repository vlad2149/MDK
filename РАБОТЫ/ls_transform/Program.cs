using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FileTransformDemo
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
            string outputPath = Path.Combine(desktop, "lorem_output.txt");
            if (!File.Exists(fullPath))
            {
                Console.WriteLine("Файл lorem.txt не найден. Сначала запустите проект ls.");
                return;
            }

            Stopwatch sw = Stopwatch.StartNew();
            using (StreamReader reader = new StreamReader(fullPath, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(outputPath, false, Encoding.UTF8))
            {
                int nextChar;
                while ((nextChar = reader.Read()) != -1)
                {
                    char symbol = (char)nextChar;
                    if (char.IsLower(symbol))
                    {
                        symbol = char.ToUpperInvariant(symbol);
                    }
                    writer.Write(symbol);
                }
            }
            sw.Stop();
            Console.WriteLine("Результат: " + outputPath);
            Console.WriteLine($"Время обработки: {sw.ElapsedMilliseconds} мс ({sw.Elapsed.TotalSeconds:F2} сек)");
        }
    }
}
