using System;
using System.Collections.Generic;

namespace Practice2.Task5
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, int> words = new Dictionary<string, int>();
            while (true)
            {
                Console.Write("Введите слово (или выход): ");
                string word = Console.ReadLine() ?? "";
                if (word == "выход")
                {
                    break;
                }
                if (word == "")
                {
                    continue;
                }
                if (words.ContainsKey(word))
                {
                    words[word]++;
                }
                else
                {
                    words[word] = 1;
                }

                foreach (KeyValuePair<string, int> item in words)
                {
                    Console.WriteLine(item.Key + ": " + item.Value);
                }
            }
        }
    }
}
