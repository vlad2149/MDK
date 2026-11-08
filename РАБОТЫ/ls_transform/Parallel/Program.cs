using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ParallelFileTransformDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (desktop == "")
            {
                desktop = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Desktop");
            }
            string fullPath = Path.Combine(desktop, "lorem.txt");
            string outputPath = Path.Combine(desktop, "lorem_output_parallel.txt");
            if (!File.Exists(fullPath))
            {
                Console.WriteLine("Файл lorem.txt не найден. Сначала запустите проект ls.");
                return;
            }

            Stopwatch sw = Stopwatch.StartNew();
            Channel<byte[]> readChannel = Channel.CreateBounded<byte[]>(4);
            Channel<byte[]> writeChannel = Channel.CreateBounded<byte[]>(4);

            Task readerTask = Task.Run(async () =>
            {
                try
                {
                    using (FileStream input = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer = new byte[65536];
                        int count;
                        while ((count = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            byte[] chunk = new byte[count];
                            Buffer.BlockCopy(buffer, 0, chunk, 0, count);
                            await readChannel.Writer.WriteAsync(chunk);
                        }
                    }
                }
                finally
                {
                    readChannel.Writer.TryComplete();
                }
            });

            Task workerTask = Task.Run(async () =>
            {
                try
                {
                    await foreach (byte[] chunk in readChannel.Reader.ReadAllAsync())
                    {
                        for (int i = 0; i < chunk.Length; i++)
                        {
                            byte b = chunk[i];
                            if (b >= 97 && b <= 122)
                            {
                                chunk[i] = (byte)(b & ~0x20);
                            }
                        }
                        await writeChannel.Writer.WriteAsync(chunk);
                    }
                }
                finally
                {
                    writeChannel.Writer.TryComplete();
                }
            });

            Task writerTask = Task.Run(async () =>
            {
                using (FileStream output = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    await foreach (byte[] chunk in writeChannel.Reader.ReadAllAsync())
                    {
                        await output.WriteAsync(chunk, 0, chunk.Length);
                    }
                }
            });

            await Task.WhenAll(readerTask, workerTask, writerTask);
            sw.Stop();
            Console.WriteLine("Результат: " + outputPath);
            Console.WriteLine($"Время обработки: {sw.ElapsedMilliseconds} мс ({sw.Elapsed.TotalSeconds:F2} сек)");
        }
    }
}
