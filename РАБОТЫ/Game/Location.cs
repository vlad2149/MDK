using Game.Models;

namespace Game.World
{
    class Location
    {
        public string Name;
        public string Description;
        public Enemy Monster;
        public int[,] Map;

        public Location(string name, string description, Enemy monster, int[,] map)
        {
            Name = name;
            Description = description;
            Monster = monster;
            Map = map;
        }
    }
}
