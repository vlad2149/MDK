using System;
using System.Buffers;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FastWriterDemo
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
            string fullPath = Path.Combine(desktop, "lorem_fast.txt");
            string text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos\n";
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            int bufferSize = 1572864;
            long iterations = 1_000_000;
            ArrayPool<byte> pool = ArrayPool<byte>.Shared;
            byte[] buffer = pool.Rent(bufferSize);
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    int offset = 0;
                    for (long i = 0; i < iterations; i++)
                    {
                        if (offset + bytes.Length > bufferSize)
                        {
                            stream.Write(buffer, 0, offset);
                            offset = 0;
                        }
                        Buffer.BlockCopy(bytes, 0, buffer, offset, bytes.Length);
                        offset += bytes.Length;
                    }
                    if (offset > 0)
                    {
                        stream.Write(buffer, 0, offset);
                    }
                }
            }
            finally
            {
                pool.Return(buffer);
            }

            sw.Stop();
            Console.WriteLine("Файл создан: " + fullPath);
            Console.WriteLine($"Время генерации: {sw.ElapsedMilliseconds} мс ({sw.Elapsed.TotalSeconds:F2} сек)");
        }
    }
}
