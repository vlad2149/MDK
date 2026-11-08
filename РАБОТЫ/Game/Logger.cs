using System;
using System.IO;

namespace Game.Logging
{
    class Logger : IDisposable
    {
        private StreamWriter writer;

        public Logger(string filePath)
        {
            writer = new StreamWriter(filePath, false);
            writer.AutoFlush = true;
        }

        public void Write(string message)
        {
            writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }

        public void Show(string message)
        {
            Console.WriteLine(message);
            Write(message);
        }

        public void Dispose()
        {
            writer.Dispose();
        }
    }
}
