using System;
using System.Diagnostics;
using System.IO;

namespace FastFileTransformDemo
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
            string outputPath = Path.Combine(desktop, "lorem_output_fast.txt");
            if (!File.Exists(fullPath))
            {
                Console.WriteLine("Файл lorem.txt не найден. Сначала запустите проект ls.");
                return;
            }

            Stopwatch sw = Stopwatch.StartNew();
            int macroBufferSize = 1572864;
            int miniBufferSize = 64;

            using (FileStream source = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            using (FileStream target = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[macroBufferSize];
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int offset = 0; offset < bytesRead; offset += miniBufferSize)
                    {
                        int count = Math.Min(miniBufferSize, bytesRead - offset);
                        for (int i = 0; i < count; i++)
                        {
                            byte symbol = buffer[offset + i];
                            if (symbol >= 97 && symbol <= 122)
                            {
                                buffer[offset + i] = (byte)(symbol & ~0x20);
                            }
                        }
                    }
                    target.Write(buffer, 0, bytesRead);
                }
            }
            sw.Stop();
            Console.WriteLine("Результат: " + outputPath);
            Console.WriteLine($"Время обработки: {sw.ElapsedMilliseconds} мс ({sw.Elapsed.TotalSeconds:F2} сек)");
        }
    }
}
