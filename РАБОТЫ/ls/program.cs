using System;
using System.Diagnostics;
using System.IO;

namespace FileWriterDemo
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
            Directory.CreateDirectory(desktop);
            string fullPath = Path.Combine(desktop, "lorem.txt");
            string text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos\n";

            Stopwatch sw = Stopwatch.StartNew();
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                for (int i = 0; i < 1_000_000; i++)
                {
                    writer.Write(text);
                }
            }
            sw.Stop();
            Console.WriteLine("Файл создан: " + fullPath);
            Console.WriteLine($"Время записи: {sw.ElapsedMilliseconds} мс ({sw.Elapsed.TotalSeconds:F2} сек)");
        }
    }
}
