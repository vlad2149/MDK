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
                new Location("Деревня у переправы", "У мельницы бродит волколак. За мельницей начинается тропа к старому замку.",
                    new Enemy("Волколак", 35, 6), "Охотничий нож", new int[,]
                    {
                        { 1, 1, 1 },
                        { 1, 2, 1 },
                        { 4, 1, 3 }
                    }),
                new Location("Тисовый лес", "На деревьях ещё видны зарубки королевских егерей. Тропу у последней зарубки сторожит леший.",
                    new Enemy("Леший", 50, 9), "Топор егеря", new int[,]
                    {
                        { 1, 2, 1 },
                        { 1, 1, 1 },
                        { 4, 1, 3 }
                    }),
                new Location("Колокольная башня", "Над тобой висит треснувший колокол. У лестницы стоит чёрный рыцарь с серебряным языком колокола на цепи.",
                    new Enemy("Чёрный рыцарь", 70, 12), "Боевой молот", new int[,]
                    {
                        { 1, 1, 1 },
                        { 2, 1, 1 },
                        { 4, 1, 3 }
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
