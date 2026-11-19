using System;

namespace Game.Models
{
    class Enemy
    {
        public string Name;
        public int Health;
        public int Damage;

        public Enemy(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public void TakeDamage(int amount)
        {
            Health = Math.Max(0, Health - amount);
        }

        public void Attack(Hero hero, bool defended)
        {
            int amount = defended ? Damage / 2 : Damage;
            hero.TakeDamage(amount);
        }
    }
}
