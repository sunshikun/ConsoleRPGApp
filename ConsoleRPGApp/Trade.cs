using System;
using System.Collections.Generic;
using System.Linq;
using Maps;
using Fight;
using Characters;
using SaveTheWorld;

namespace Trade
{
    public class Shop
    {
        public static void OpenShop(Character player)
        {
            Console.WriteLine("Welcome to the Shop!");
            Item sword = new Item("Sword");
            Item staff = new Item("Staff");
            Item dagger = new Item("Dagger");
            Item potion = new Item("Health Potion");

            var shopItems = new List<Item> { sword, staff, dagger, potion };

            Console.WriteLine("Available Items:");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {shopItems[i].Name} (Price: 20 Gold)");
            }

            Console.WriteLine("Choose an item to buy (or 0 to leave):");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= shopItems.Count)
            {
                if (player.Gold >= 20)
                {
                    player.Gold -= 20;
                    Inventar.AddItem(shopItems[choice - 1], player);
                    Console.WriteLine($"You bought {shopItems[choice - 1].Name}!");
                }
                else
                {
                    Console.WriteLine("Not enough Gold!");
                }
            }
            else
            {
                Console.WriteLine("Leaving Shop.");
            }
        }
    }
    public class Inventar
    {

        public static void AddItem(Item item, Character player)
        {
            player.Inventory.Add(item);
            Console.WriteLine($"{item.Name} was added to inventory.");
        }

        public static void ShowInventory(Character player)
        {
            Console.WriteLine("Inventory:");
            foreach (var item in player.Inventory)
            {
                Console.WriteLine($"- {item.Name}: Damage {item.Damage}, Heal {item.HealAmount}");
            }
        }

        public static void UseItem(string itemName, Character player)
        {
            var item = player.Inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                if (item.Name == "Health Potion")
                {
                    player.MaxHealth = 1000;
                    player.Health += item.HealAmount;
                    if (player.Health > player.MaxHealth) player.Health = player.MaxHealth; // Max value for health
                    Console.WriteLine($"{player.Name} uses {item.Name} and restores {item.HealAmount} Health.");
                    player.Inventory.Remove(item);
                }
                else
                {
                    Console.WriteLine($"{item.Name} cannot be used.");
                }
            }
            else
            {
                Console.WriteLine($"{itemName} is not in inventory.");
            }

        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int CriticalChance { get; set; }
        public int AttackSpeed { get; set; }
        public int MagicDamage { get; set; }
        public int SpellPower { get; set; }
        public int HealAmount { get; set; }

        public Item(string name)
        {
            Name = name;
            Damage = Random.Shared.Next(5, 15);
            CriticalChance = Random.Shared.Next(1, 10);
            AttackSpeed = Random.Shared.Next(1, 3);
            MagicDamage = Random.Shared.Next(5, 15);
            SpellPower = Random.Shared.Next(1, 3);
            HealAmount = 20;
        }
        public void AddRandomAttackItem()
        {
            string[] names = { "Sword", "Staff", "Dagger", "Armor", "Ring", "Amulet", "Bow", "Wand", "Axe", "Hammer", "Shield", "Helm", "Boots", "Gloves", "Belt", "Cloak", "Talisman", "Warhammer", "Spear", "Mace", "Katana" };

            Name = names[Random.Shared.Next(names.Length)];
            Damage = Random.Shared.Next(5, 15);
            CriticalChance = Random.Shared.Next(1, 10);
            AttackSpeed = Random.Shared.Next(1, 3);
            MagicDamage = Random.Shared.Next(5, 15);
            SpellPower = Random.Shared.Next(1, 3);

        }
        public void AddRandomConsumableItem()
        {
            string[] names = { "Health Potion", "Mana Potion", "Strength Potion", "Speed Elixir", "Defense Potion", "Power Elixir", "Regeneration Potion", "Lucky Charm", "Invisibility Potion", "Fire Resistance Potion" };

            Name = names[Random.Shared.Next(names.Length)];

        }
        public void AddRandomBuffItem()
        {
            string[] names = { "Strength Ring", "Defense Amulet", "Speed Cloak", "Power Belt", "Lucky Medal", "Regeneration Staff", "Fire Resistance Shield", "Ice Resistance Helm", "Lightning Resistance Gloves", "Poison Resistance Boots" };

            Name = names[Random.Shared.Next(names.Length)];

        }
        public void AddRandomQuestItem()
        {
            string[] names = { "Amulet of Power", "Key of Destiny", "Map of Lost Treasure", "Ring of Wisdom", "Crystal of Time", "Book of Secrets", "Staff of Elements", "Medal of the Hero", "Seal of Darkness", "Talisman of Light" };

            Name = names[Random.Shared.Next(names.Length)];
        }
        public void AddRandomCraftingMaterial()
        {
            string[] names = { "Iron", "Wood", "Leather", "Cloth", "Crystal", "Gold", "Silver", "Copper", "Diamond", "Ruby", "Sapphire", "Emerald", "Obsidian", "Mithril", "Adamantium", "Dragon Skin", "Phoenix Feather", "Ivory", "Demon Blood", "Ghost Ash" };

            Name = names[Random.Shared.Next(names.Length)];
        }
        public void AddRandomMiscItem()
        {
            string[] names = { "Old Coin", "Enchanted Stone", "Mysterious Key", "Ancient Relic", "Strange Device", "Secret Scroll", "Faded Painting", "Enigmatic Statue", "Ancient Map", "Magic Mirror" };

            Name = names[Random.Shared.Next(names.Length)];
        }

    }
}