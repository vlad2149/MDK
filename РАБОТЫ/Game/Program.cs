using System;
using Game.Models;
using Game.World;
using Game.Logging;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите имя персонажа: ");
            string name = Console.ReadLine() ?? "";
            if (name.Trim() == "")
            {
                name = "Странник";
            }

            Hero hero = new Hero(name);
            GameMap world = new GameMap();
            GlobalMap globalMap = new GlobalMap();
            using (Logger logger = new Logger("gameLog.txt"))
            {
                logger.Show("Немой колокол. Герой: " + hero.Name);
                logger.Show("Управление: W/A/S/D - ходить, M - глобальная карта, H - эликсир, Q - выход.");
                logger.Show("С тех пор как умолк замковый колокол, по ночам в деревню приходит нечисть. Пройди три локации, доберись до башни и ударь в колокол.");
                globalMap.Show(world.Locations, world.Index);
                bool playing = true;

                while (playing && hero.Health > 0)
                {
                    Location location = world.Current();
                    Draw(hero, location);
                    Console.Write("Действие: ");
                    string action = (Console.ReadLine() ?? "q").Trim().ToLowerInvariant();
                    logger.Write("Действие игрока: " + action);

                    if (action == "q")
                    {
                        logger.Show("Выход из игры.");
                        break;
                    }
                    if (action == "h")
                    {
                        UsePotion(hero, logger);
                        continue;
                    }
                    if (action == "m")
                    {
                        globalMap.Show(world.Locations, world.Index);
                        logger.Write("Игрок открыл глобальную карту.");
                        continue;
                    }

                    int dx = 0;
                    int dy = 0;
                    if (action == "w") dy = -1;
                    else if (action == "s") dy = 1;
                    else if (action == "a") dx = -1;
                    else if (action == "d") dx = 1;
                    else
                    {
                        logger.Show("Неизвестная команда.");
                        continue;
                    }

                    if (!hero.Move(dx, dy, location.Map))
                    {
                        logger.Show("Туда пройти нельзя.");
                        continue;
                    }
                    logger.Write($"Переход: {location.Name}; X={hero.X}; Y={hero.Y}");

                    int cell = location.Map[hero.Y, hero.X];
                    if (cell == 4)
                    {
                        hero.Damage += 5;
                        location.Map[hero.Y, hero.X] = 1;
                        logger.Show($"Найден предмет «{location.Item}». Урон героя увеличен до {hero.Damage}.");
                    }
                    if (cell == 2 && location.Monster.IsAlive())
                    {
                        playing = Fight(hero, location.Monster, logger);
                        if (!playing || hero.Health == 0)
                        {
                            continue;
                        }
                        location.Map[hero.Y, hero.X] = 1;
                        hero.Damage += 2;
                        logger.Show("Противник побеждён. Урон героя увеличен на 2.");
                    }
                    if (cell == 3)
                    {
                        if (location.Monster.IsAlive())
                        {
                            logger.Show("Сначала нужно победить противника в этой локации.");
                        }
                        else if (world.IsLast())
                        {
                            logger.Show("Победа! Ты возвращаешь серебряный язык на место и бьёшь в колокол. Внизу, у мельницы, впервые за много ночей открывают ставни.");
                            playing = false;
                        }
                        else
                        {
                            world.Next();
                            hero.X = 0;
                            hero.Y = 0;
                            hero.Heal(10);
                            logger.Show("Новая локация: " + world.Current().Name + ". Здоровье: " + hero.Health);
                            globalMap.Show(world.Locations, world.Index);
                        }
                    }
                }

                if (hero.Health == 0)
                {
                    logger.Show("Игра окончена. Здоровье героя закончилось.");
                }
            }
        }

        static bool UsePotion(Hero hero, Logger logger)
        {
            if (hero.Potions == 0)
            {
                logger.Show("Эликсиров больше нет.");
                return false;
            }
            if (hero.Health == 100)
            {
                logger.Show("Здоровье полное. Эликсир остался в запасе.");
                return false;
            }
            hero.Potions--;
            hero.Heal(25);
            logger.Show($"Герой выпил эликсир. Здоровье: {hero.Health}, эликсиров осталось: {hero.Potions}.");
            return true;
        }

        static bool Fight(Hero hero, Enemy monster, Logger logger)
        {
            logger.Show("На пути противник: " + monster.Name);
            while (hero.Health > 0 && monster.IsAlive())
            {
                Console.WriteLine($"Герой: {hero.Health} HP, {hero.Damage} урона | {monster.Name}: {monster.Health} HP");
                Console.Write("1 - атака, 2 - эликсир, 3 - защита, q - выход: ");
                string action = (Console.ReadLine() ?? "q").Trim().ToLowerInvariant();
                logger.Write("Ход в бою: " + action);
                bool defended = false;

                if (action == "q")
                {
                    logger.Show("Игрок вышел во время боя.");
                    return false;
                }
                else if (action == "1")
                {
                    hero.Attack(monster);
                    logger.Show($"{hero.Name} атакует. У противника осталось {monster.Health} HP.");
                }
                else if (action == "2")
                {
                    if (!UsePotion(hero, logger))
                    {
                        continue;
                    }
                }
                else if (action == "3")
                {
                    defended = true;
                    logger.Show("Герой защищается.");
                }
                else
                {
                    logger.Show("Неизвестное действие.");
                    continue;
                }

                if (monster.IsAlive())
                {
                    int before = hero.Health;
                    monster.Attack(hero, defended);
                    logger.Show(monster.Name + " атакует. Получено урона: " + (before - hero.Health));
                }
            }
            return true;
        }

        static void Draw(Hero hero, Location location)
        {
            Console.WriteLine();
            Console.WriteLine("=== " + location.Name + " ===");
            Console.WriteLine(location.Description);
            Console.WriteLine("Имя: " + hero.Name + ", здоровье: " + hero.Health + ", урон: " + hero.Damage + ", эликсиры: " + hero.Potions);
            Console.WriteLine("0 - герой, 1 - путь, 2 - противник, 3 - выход, 4 - предмет (+5 к урону)");
            for (int y = 0; y < location.Map.GetLength(0); y++)
            {
                for (int x = 0; x < location.Map.GetLength(1); x++)
                {
                    Console.Write(hero.X == x && hero.Y == y ? "0 " : location.Map[y, x] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
