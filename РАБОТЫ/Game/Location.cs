using Game.Models;

namespace Game.World
{
    class Location
    {
        public string Name;
        public string Description;
        public Enemy Monster;
        public string Item;
        public int[,] Map;

        public Location(string name, string description, Enemy monster, string item, int[,] map)
        {
            Name = name;
            Description = description;
            Monster = monster;
            Item = item;
            Map = map;
        }
    }
}
