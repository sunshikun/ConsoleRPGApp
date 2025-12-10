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
            
            Console.WriteLine($"Ein wildes {enemy.Name} erscheint!");
            while (player.Health > 0 && enemy.Health > 0)
            {
                Console.WriteLine($"{player.Name}: {player.Health} HP, {player.Mana} MP");
                Console.WriteLine($"{enemy.Name}: {enemy.Health} HP");
                Console.WriteLine("Wähle eine Aktion:");
                Console.WriteLine("1. Angreifen");
                if (player.magical && player.Spells.Count > 0)
                {
                    Console.WriteLine("2. Zauber wirken");
                }
                Console.WriteLine("3. Verteidigen");
                Console.WriteLine("4. Fliehen");
                string action = "";
                while (string.IsNullOrEmpty(action)|| (action != "1" && action != "2"))
                {
                    action = Console.ReadLine();
                    if (string.IsNullOrEmpty(action) || (action != "1" && action != "2"))
                    {
                        Console.WriteLine("Ungültige Eingabe. Bitte wähle 1 oder 2.");
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
                    Console.WriteLine("Wähle einen Zauber:");
                    for (int i = 0; i < player.Spells.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {player.Spells[i].Name} (Kosten: {player.Spells[i].ManaCost} MP)");
                    }
                    int spellChoice;
                    if (int.TryParse(Console.ReadLine(), out spellChoice) && spellChoice >= 1 && spellChoice <= player.Spells.Count)
                    {
                        player.CastSpell(player.Spells[spellChoice - 1], enemy);
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Auswahl, du verlierst deinen Zug!");
                    }
                }
                else
                {
                    Console.WriteLine("Ungültige Aktion, du verlierst deinen Zug!");
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
                Console.WriteLine("Du wurdest besiegt...");
            }
            else
            {
                Console.WriteLine($"Du hast das {enemy.Name} besiegt!");
                player.Experience += 30;
                player.Gold += 20;
                Console.WriteLine($"Du erhältst 30 Erfahrung und 20 Gold.");
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
            Console.WriteLine($"Name: {Name}, Level: {Level}, Rolle: {Role}");
        }
        public void LevelUp()
        {
            Level++;
            Console.WriteLine($"{Name} ist jetzt Level {Level}!");
        }
        public void Create_Random_Member()
        {
            string[] names = { "Arin", "Borin", "Cirin", "Dorin", "Erin", "Farin", "Gorin", "Harin", "Irin", "Jorin", "Kirin", "Lorin", "Marin", "Narin", "Orin", "Parin", "Qirin", "Rorin", "Sarin", "Tarin" };
            string[] roles = { "Krieger", "Magier", "Schurke", "Heiler", "Dieb", "Bogenschütze", "Paladin", "Druide", "Hexenmeister", "Berserker", "Assassine", "Mönch", "Jäger", "Kleriker", "Nekromant", "Alchemist", "Runenmeister", "Wächter", "Sammler", "Seher" };
            Name = names[Random.Shared.Next(names.Length)];
            Level = Random.Shared.Next(1, 5);
            Role = roles[Random.Shared.Next(roles.Length)];
        }

    }
    public class Monster
    {
        public static Character CreateMonster(int level)
        {
            string[] monsterNames = { "Goblin", "Ork", "Troll", "Skelett", "Zombie", "Dämon", "Riese", "Drache", "Vampir", "Werwolf", "Ghul", "Harpyie", "Minotaurus", "Golem",
                "Phantom", "Banshee", "Chimäre", "Hydra", "Mumie", "Sphinx", "Wichtel", "Greif", "Kobold", "Nekromant", "Schatten", "Wyrm", "Elementar", "Drachenling", "Lich", "Dämonenhund",
                "Schattenläufer", "Feuergeist", "Eisriese", "Blitzdämon", "Giftmischer", "Nachtmahr", "Seelenfresser", "Zeitwandler", "Dimensionswanderer", "Traumweber", "Sternenwandler",
                "Nebelgeist", "Dunkelheitsbringer", "Lichtbringer", "Sturmrufer", "Erdbrecher", "Wassermagier", "Feuerzauberer", "Eismagier", "Blitzmagier" };
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
