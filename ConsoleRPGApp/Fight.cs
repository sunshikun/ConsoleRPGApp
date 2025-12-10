using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maps;
using Trade;
using Characters;
using SaveTheWorld;

namespace Fight
{
    public class Kampf
    {

        public static void StartBattle(Character player, Character enemy)
        {

            Console.WriteLine($"A wild {enemy.Name} appears!");
            while (player.Health > 0 && enemy.Health > 0)
            {
                Console.WriteLine($"{player.Name}: {player.Health} HP, {player.Mana} MP");
                Console.WriteLine($"{enemy.Name}: {enemy.Health} HP");
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Attack");
                if (player.magical && player.Spells.Count > 0)
                {
                    Console.WriteLine("2. Cast Spell");
                }
                Console.WriteLine("3. Defend");
                Console.WriteLine("4. Flee");
                string action = "";
                while (string.IsNullOrEmpty(action) || (action != "1" && action != "2"))
                {
                    action = Console.ReadLine();
                    if (string.IsNullOrEmpty(action) || (action != "1" && action != "2"))
                    {
                        Console.WriteLine("Invalid input. Please choose 1 or 2.");
                    }
                }


                if (action == "1")
                {
                    if (player.physical)
                        player.AttackPhysical(enemy);
                    else if (player.magical)
                        player.AttackMagical(enemy);
                }
                else if (action == "2" && player.magical && player.Spells.Count > 0)
                {
                    Console.WriteLine("Choose a spell:");
                    for (int i = 0; i < player.Spells.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {player.Spells[i].Name} (Cost: {player.Spells[i].ManaCost} MP)");
                    }
                    int spellChoice;
                    if (int.TryParse(Console.ReadLine(), out spellChoice) && spellChoice >= 1 && spellChoice <= player.Spells.Count)
                    {
                        player.CastSpell(player.Spells[spellChoice - 1], enemy);
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection, you lose your turn!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid action, you lose your turn!");
                }

                if (enemy.Health > 0)
                {
                    if (enemy.physical)
                        enemy.AttackPhysical(player);
                    else if (enemy.magical)
                        enemy.AttackMagical(player);
                }
            }

            if (player.Health <= 0)
            {
                Console.WriteLine("You were defeated...");
            }
            else
            {
                Console.WriteLine($"You defeated the {enemy.Name}!");
                player.Experience += 30;
                player.Gold += 20;
                Console.WriteLine($"You gain 30 Experience and 20 Gold.");
                if (player.Experience >= player.Level * 100)
                {
                    player.LevelUp();
                }
            }

        }
    }
    public class Spell
    {
        public string Name { get; set; }
        public int ManaCost { get; set; }
        public int BaseDamage { get; set; }
        public bool IsHealing { get; set; }
        public int HealAmount { get; set; }

        public Spell(string name, int manaCost, int baseDamage = 0, bool isHealing = false, int healAmount = 0)
        {
            Name = name;
            ManaCost = manaCost;
            BaseDamage = baseDamage;
            IsHealing = isHealing;
            HealAmount = healAmount;
        }
    }
    public class Mitglieder
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Role { get; set; }

        public Mitglieder(string name, int level, string role)
        {
            Name = name;
            Level = level;
            Role = role;
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Level: {Level}, Role: {Role}");
        }
        public void LevelUp()
        {
            Level++;
            Console.WriteLine($"{Name} is now Level {Level}!");
        }
        public void Create_Random_Member()
        {
            string[] names = { "Arin", "Borin", "Cirin", "Dorin", "Erin", "Farin", "Gorin", "Harin", "Irin", "Jorin", "Kirin", "Lorin", "Marin", "Narin", "Orin", "Parin", "Qirin", "Rorin", "Sarin", "Tarin" };
            string[] roles = { "Warrior", "Mage", "Rogue", "Healer", "Thief", "Archer", "Paladin", "Druid", "Warlock", "Berserker", "Assassin", "Monk", "Hunter", "Cleric", "Necromancer", "Alchemist", "Runemaster", "Guardian", "Collector", "Seer" };
            Name = names[Random.Shared.Next(names.Length)];
            Level = Random.Shared.Next(1, 5);
            Role = roles[Random.Shared.Next(roles.Length)];
        }

    }
    public class Monster
    {
        public static Character CreateMonster(int level)
        {
            string[] monsterNames = { "Goblin", "Orc", "Troll", "Skeleton", "Zombie", "Demon", "Giant", "Dragon", "Vampire", "Werewolf", "Ghoul", "Harpy", "Minotaur", "Golem",
                "Phantom", "Banshee", "Chimera", "Hydra", "Mummy", "Sphinx", "Imp", "Griffin", "Kobold", "Necromancer", "Shadow", "Wyrm", "Elemental", "Dragonling", "Lich", "Hellhound",
                "Shadowrunner", "Fire Spirit", "Ice Giant", "Lightning Demon", "Poisoner", "Nightmare", "Soul Eater", "Time Walker", "Dimension Walker", "Dream Wever", "Star Walker",
                "Mist Spirit", "Darkness Bringer", "Light Bringer", "Storm Caller", "Earth Breaker", "Water Mage", "Fire Mage", "Ice Mage", "Lightning Mage" };
            Random rand = new Random();
            string name = monsterNames[rand.Next(monsterNames.Length)];
            Character monster = new Character(name)
            {
                Level = level,
                Health = 50 + (level * 10),
                Strength = 5 + (level * 2),
                Defense = 3 + level,
                physical = true,
                magical = false,
                Inventory = new List<Item>()
            };
            return monster;
        }
    }
}
