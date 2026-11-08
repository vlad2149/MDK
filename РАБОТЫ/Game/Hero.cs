using System;

namespace Game.Models
{
    class Hero
    {
        public string Name;
        public int Health;
        public int Damage;
        public int Potions;
        public int X;
        public int Y;

        public Hero(string name)
        {
            Name = name;
            Health = 100;
            Damage = 20;
            Potions = 2;
        }

        public void TakeDamage(int amount)
        {
            Health = Math.Max(0, Health - amount);
        }

        public void Heal(int amount)
        {
            Health = Math.Min(100, Health + amount);
        }

        public void Attack(Enemy monster)
        {
            monster.TakeDamage(Damage);
        }

        public bool Move(int dx, int dy, int[,] map)
        {
            int newX = X + dx;
            int newY = Y + dy;
            if (newX < 0 || newY < 0 || newY >= map.GetLength(0) || newX >= map.GetLength(1))
            {
                return false;
            }
            X = newX;
            Y = newY;
            return true;
        }
    }
}
