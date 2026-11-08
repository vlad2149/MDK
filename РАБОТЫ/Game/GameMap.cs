using System.Collections.Generic;
using Game.Models;

namespace Game.World
{
    class GameMap
    {
        public List<Location> Locations;
        public int Index;

        public GameMap()
        {
            Locations = new List<Location>
            {
                new Location("Общага", "Пора выбраться из общаги.",
                    new Enemy("Прокрастинация", 35, 6), new int[,]
                    {
                        { 1, 1, 1 },
                        { 1, 2, 1 },
                        { 1, 1, 3 }
                    }),
                new Location("Колледж", "На пути к аудитории стоит преподаватель.",
                    new Enemy("Препод", 50, 9), new int[,]
                    {
                        { 1, 2, 1 },
                        { 1, 1, 1 },
                        { 1, 1, 3 }
                    }),
                new Location("Аудитория", "Впереди сложная контрольная.",
                    new Enemy("Контрольная", 55, 10), new int[,]
                    {
                        { 1, 1, 2 },
                        { 1, 1, 1 },
                        { 1, 1, 3 }
                    }),
                new Location("Сессия", "Последнее испытание.",
                    new Enemy("Экзамен", 70, 12), new int[,]
                    {
                        { 1, 1, 1 },
                        { 2, 1, 1 },
                        { 1, 1, 3 }
                    })
            };
        }

        public Location Current()
        {
            return Locations[Index];
        }

        public bool IsLast()
        {
            return Index == Locations.Count - 1;
        }

        public void Next()
        {
            Index++;
        }
    }
}
