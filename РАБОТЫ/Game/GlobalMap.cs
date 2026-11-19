using System;
using System.Collections.Generic;

namespace Game.World
{
    class GlobalMap
    {
        public void Show(List<Location> locations, int currentIndex)
        {
            Console.WriteLine();
            Console.WriteLine("========== ГЛОБАЛЬНАЯ КАРТА ==========");

            for (int i = 0; i < locations.Count; i++)
            {
                if (i == currentIndex)
                {
                    Console.Write("[ГЕРОЙ: " + locations[i].Name + "]");
                }
                else if (i < currentIndex)
                {
                    Console.Write("[Пройдено: " + locations[i].Name + "]");
                }
                else
                {
                    Console.Write("[" + locations[i].Name + "]");
                }

                if (i < locations.Count - 1)
                {
                    Console.Write(" ---> ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("======================================");
            Console.WriteLine();
        }
    }
}
