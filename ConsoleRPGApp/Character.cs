using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maps;
using Trade;
using Fight;
using SaveTheWorld;
using System.Security.Cryptography.X509Certificates;


namespace Characters
{


    public class Character
    {
        public string Name { get; set; }
        public string Klasse { get; set; } = "";
        public bool physical { get; set; }
        public bool magical { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Strength { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Defense { get; set; }
        public int Stamina { get; set; }
        public int Luck { get; set; }
        public int Charisma { get; set; }
        public int Wisdom { get; set; }
        public int Armor { get; set; }
        public int Speed { get; set; }
        public double CriticalChance { get; set; }
        public double CriticalDamage { get; set; }
        /*public int FireResistance { get; set; }
        public int IceResistance { get; set; }
        public int LightningResistance { get; set; }
        public int PoisonResistance { get; set; }
        */
        public double HealthRegen { get; set; }
        public double ManaRegen { get; set; }
        public double AttackSpeed { get; set; }
        public int SpellPower { get; set; }
        public int MaxHealth { get; set; } = 1000;
        public List<Spell> Spells { get; set; } = new();
        //public int BlockChance { get; set; }
        //public int Evasion { get; set; }
        public Item? EquippedWeapon { get; set; }
        public Item? EquippedArmor { get; set; }
        public Item? EquippedAccessory { get; set; }
        public int InventoryCount { get; set; }
        public List<Item> Inventory { get; set; } = new();
        public List<Item> EquippedItems { get; set; } = new();
        public List<Item> Consumables { get; set; } = new();
        public List<Item> QuestItems { get; set; } = new();
        public List<Item> CraftingMaterials { get; set; } = new();
        public List<Item> MiscItems { get; set; } = new();
        public List<Item> Loot { get; set; } = new();
        public Map CurrentMap { get; set; }

        public Character(string name)
        {
            Name = name;
            Health = 100;
            Mana = 50;
            Strength = 10;
            Level = 1;
            Experience = 0;
            Gold = 0;
            Agility = Random.Shared.Next(5, 15);
            Intelligence = Random.Shared.Next(5, 15);
            Defense = Random.Shared.Next(5, 15);
            Stamina = Random.Shared.Next(5, 15);
            Luck = Random.Shared.Next(1, 10);
            Charisma = Random.Shared.Next(1, 10);
            Wisdom = Random.Shared.Next(1, 10);
            Armor = Random.Shared.Next(1, 10);
            Speed = Random.Shared.Next(1, 10);
            CriticalChance = Random.Shared.Next(1, 11);
            CriticalDamage = 150;
            /*FireResistance = 0;
            IceResistance = 0;
            LightningResistance = 0;
            PoisonResistance = 0;
            */
            MaxHealth = 100;
            HealthRegen = Random.Shared.Next(1, 5);
            ManaRegen = Random.Shared.Next(1, 5);
            AttackSpeed = Random.Shared.Next(1, 3);
            SpellPower = Random.Shared.Next(1, 3);
            Spells = new List<Spell>();
            //BlockChance = 0;
            //Evasion = 5;
            Inventory = new List<Item>();
            EquippedItems = new List<Item>();
            Consumables = new List<Item>();
            QuestItems = new List<Item>();
            CraftingMaterials = new List<Item>();
            MiscItems = new List<Item>();
            Loot = new List<Item>();
            CurrentMap = new Map("Abenteuerland", 10);


        }
        public void DisplayStats()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Klasse}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Mana: {Mana}");
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Intelligence: {Intelligence}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Luck: {Luck}");
            Console.WriteLine($"Critical Chance: {CriticalChance}%");
            Console.WriteLine($"Critical Damage: {CriticalDamage}%");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Experience: {Experience}");
            Console.WriteLine("Spells:");
            foreach (var spell in Spells)
            {
                Console.WriteLine($"- {spell.Name} (Cost: {spell.ManaCost} MP)");

            }
            foreach (var invent in Inventory)
            {
                Console.WriteLine($"{invent.Name}");
            }
        }
        public void LevelUp()
        {
            Level++;
            Health += 20;
            Mana += 10;
            Strength += 5;
            Agility += 3;
            Intelligence += 3;
            Defense += 2;
            Stamina += 2;
            Luck += 1;
            Charisma += 1;
            Wisdom += 1;
            Armor += 1;
            Speed += 1;
            CriticalChance += 1;
            HealthRegen += 1;
            ManaRegen += 1;
            AttackSpeed += 1;
            SpellPower += 1;
            MaxHealth += 20;
            Console.WriteLine($"{Name} is now Level {Level}!");
        }

        // Lern-Methoden für Standardzauber (z.B. für Magier)
        public void LearnDefaultSpells()
        {
            if (Klasse == "Mage")
            {
                Spells.Add(new Spell("Fireball", 15, baseDamage: 25, isHealing: false));
                Spells.Add(new Spell("Lightning", 20, baseDamage: 35, isHealing: false));
                Spells.Add(new Spell("Heal", 10, isHealing: true, healAmount: 25));
            }
        }

        // Zauber wirken
        public void CastSpell(Spell spell, Character target)
        {
            if (Mana < spell.ManaCost)
            {
                Console.WriteLine($"Not enough Mana for {spell.Name}!");
                return;
            }
            Mana -= spell.ManaCost;

            if (spell.IsHealing)
            {
                int heal = spell.HealAmount + (int)(Wisdom * 0.5) + SpellPower;
                Health += heal;
                Console.WriteLine($"{Name} casts {spell.Name} and heals {heal} Health!");
                return;
            }

            int damage = spell.BaseDamage + Intelligence * SpellPower - target.Defense;
            if (damage < 1) damage = 1;
            target.Health -= damage;
            Console.WriteLine($"{Name} casts {spell.Name} on {target.Name} and deals {damage} damage!");
        }

        public void AttackPhysical(Character target)
        {
            int baseDmg = (int)Math.Round(10.0 / Math.Max(1, target.Armor)) + Strength;
            baseDmg = Math.Max(1, baseDmg);

            int roll = Random.Shared.Next(1, 101);
            bool isCrit = roll <= (int)Math.Round(Math.Min(100, CriticalChance));
            double mult = isCrit ? (CriticalDamage / 100.0) : 1.0;
            int total = (int)Math.Round(baseDmg * mult);

            target.Health -= total;
            if (isCrit)
                Console.WriteLine($"{Name} attacks {target.Name} and critically hits for {total} damage!");
            else
                Console.WriteLine($"{Name} attacks {target.Name} and deals {total} damage!");
        }

        public void AttackMagical(Character target)
        {
            int baseDmg = (int)Math.Round(10.0 / Math.Max(1, target.Defense)) + Intelligence;
            baseDmg = Math.Max(1, baseDmg);
            int roll = Random.Shared.Next(1, 101);
            bool isCrit = roll <= (int)Math.Round(CriticalChance);
            double mult = isCrit ? (CriticalDamage / 100.0) : 1.0;
            int total = (int)Math.Round(baseDmg * mult);

            baseDmg = 10 / target.Defense + Intelligence;
            target.Health -= baseDmg;
            if (isCrit)
                Console.WriteLine($"{Name} attacks {target.Name} and critically hits for {total} damage!");
            else
                Console.WriteLine($"{Name} attacks {target.Name} and deals {total} damage!");

        }
        public void PlayerDefend()
        {
            Console.WriteLine($"{Name} defends and reduces next damage by half!");
            // Implement here the logic for damage reduction for the next attack
        }
        public void AttemptFlee()
        {
            int fleeChance = 50 + (Agility - 10) * 5; // Base chance plus agility bonus
            int roll = Random.Shared.Next(1, 101);
            if (roll <= fleeChance)
            {
                Console.WriteLine($"{Name} fled successfully!");
                // Implement here the logic to leave the combat
            }
            else
            {
                Console.WriteLine($"{Name} could not flee!");
                // The combat continues
            }
        }
        public void PlayerUseItem()
        {
            Console.WriteLine("Which item do you want to use?");
            for (int i = 0; i < Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Inventory[i].Name}");
            }
            string choice = Console.ReadLine();
            if (int.TryParse(choice, out int index) && index > 0 && index <= Inventory.Count)
            {
                Item selectedItem = Inventory[index - 1];
                Inventar.UseItem(selectedItem.Name, this);
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }
    }
    class CharakterErstellung
    {
        public static Character CreateCharacter()
        {
            Console.WriteLine("Enter your character's name:");
            string name = Console.ReadLine();
            Character character = new Character(name);

            Console.WriteLine("Choose a class for your character:");
            Console.WriteLine("1. Warrior");
            Console.WriteLine("2. Mage");
            Console.WriteLine("3. Rogue");
            Console.WriteLine("4. Healer");
            string classChoice = Console.ReadLine();

            switch (classChoice)
            {
                case "1":
                    character.Klasse = "Warrior";
                    character.Strength += 5;
                    character.Health += 20;
                    character.physical = true;
                    character.magical = false;
                    break;
                case "2":
                    character.Klasse = "Mage";
                    character.Intelligence += 5;
                    character.Mana += 20;
                    character.physical = false;
                    character.magical = true;
                    character.LearnDefaultSpells(); // Learn default spells for Mage
                    break;
                case "3":
                    character.Klasse = "Rogue";
                    character.Agility += 5;
                    character.CriticalChance += 5;
                    character.physical = true;
                    character.magical = false;
                    break;
                case "4":
                    character.Klasse = "Healer";
                    character.Wisdom += 5;
                    character.Mana += 15;
                    character.physical = false;
                    character.magical = true;
                    character.Spells.Add(new Spell("Heal", 10, isHealing: true, healAmount: 30)); // Add Heal spell
                    break;
                default:
                    Console.WriteLine("Invalid selection, default class Warrior assigned.");
                    character.Klasse = "Warrior";
                    character.Strength += 5;
                    character.Health += 20;
                    character.physical = true;
                    character.magical = false;
                    break;
            }

            Console.WriteLine($"Character created: {character.Name}, Class: {character.Klasse}");
            return character;
        }
    }
}