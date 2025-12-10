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
                Console.WriteLine("Willkommen im Shop!");
                Item sword = new Item("Schwert");
                Item staff = new Item("Zauberstab");
                Item dagger = new Item("Dolch");
                Item potion = new Item("Heiltrank");
                
                var shopItems = new List<Item> { sword, staff, dagger, potion };

                Console.WriteLine("Verfügbare Artikel:");
                for (int i = 0; i < shopItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {shopItems[i].Name} (Preis: 20 Gold)");
                }

                Console.WriteLine("Wähle einen Artikel zum Kauf (oder 0 zum Verlassen):");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= shopItems.Count)
                {
                    if (player.Gold >= 20)
                    {
                        player.Gold -= 20;
                        Inventar.AddItem(shopItems[choice - 1],player);
                        Console.WriteLine($"Du hast {shopItems[choice - 1].Name} gekauft!");
                    }
                    else
                    {
                        Console.WriteLine("Nicht genug Gold!");
                    }
                }
                else
                {
                    Console.WriteLine("Verlasse den Shop.");
                }
        }
    }
    public class Inventar
    {
        
        public static void AddItem(Item item, Character player)
        {
            player.Inventory.Add(item);
            Console.WriteLine($"{item.Name} wurde zum Inventar hinzugefügt.");
        }

        public static void ShowInventory(Character player)
        {
            Console.WriteLine("Inventar:");
            foreach (var item in player.Inventory)
                {
                    Console.WriteLine($"- {item.Name}: Schaden {item.Damage}, Heilung {item.HealAmount}");
                }
        }
        
        public static void UseItem(string itemName, Character player)
        {
            var item = player.Inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                if (item.Name == "Heiltrank")
                {
                    player.MaxHealth = 1000;
                    player.Health += item.HealAmount;
                    if (player.Health > player.MaxHealth) player.Health = player.MaxHealth; // Maximalwert für Gesundheit
                    Console.WriteLine($"{player.Name} verwendet {item.Name} und stellt {item.HealAmount} Gesundheit wieder her.");
                    player.Inventory.Remove(item);
                }
                else
                {
                    Console.WriteLine($"{item.Name} kann nicht verwendet werden.");
                }
            }
            else
            {
                Console.WriteLine($"{itemName} ist nicht im Inventar.");
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
            string[] names = { "Schwert", "Zauberstab", "Dolch", "Rüstung", "Ring", "Amulett", "Bogen", "Stab", "Axt", "Hammer", "Schild", "Helm", "Schuhe", "Handschuhe", "Gürtel", "Umhang", "Talisman", "Kriegshammer", "Speer", "Streitkolben", "Katana" };
            
            Name = names[Random.Shared.Next(names.Length)];
            Damage = Random.Shared.Next(5, 15);
            CriticalChance = Random.Shared.Next(1, 10);
            AttackSpeed = Random.Shared.Next(1, 3);
            MagicDamage = Random.Shared.Next(5, 15);
            SpellPower = Random.Shared.Next(1, 3);

        }
        public void AddRandomConsumableItem()
        {
            string[] names = { "Heiltrank", "Manatrank", "Stärketrank", "Geschwindigkeitselixier", "Schutztrank", "Kraftelixier", "Regenerationstrank", "Glücksbringer", "Unsichtbarkeitstrank", "Feuerresistenztrank" };
            
            Name = names[Random.Shared.Next(names.Length)];

        }
        public void AddRandomBuffItem()
        {
            string[] names = { "Stärkungsring", "Schutzamulett", "Geschwindigkeitsumhang", "Kraftgürtel", "Glücksmedaille", "Regenerationsstab", "Feuerresistenzschild", "Eisresistenzhelm", "Blitzresistenzhandschuhe", "Giftresistenzschuhe" };
            
            Name = names[Random.Shared.Next(names.Length)];

        }
        public void AddRandomQuestItem()
        {
            string[] names = { "Amulett der Macht", "Schlüssel des Schicksals", "Karte des verlorenen Schatzes", "Ring der Weisheit", "Kristall der Zeit", "Buch der Geheimnisse", "Stab der Elemente", "Medaille des Helden", "Siegel der Dunkelheit", "Talisman des Lichts" };
            
            Name = names[Random.Shared.Next(names.Length)];
        }
        public void AddRandomCraftingMaterial()
        {
            string[] names = { "Eisen", "Holz", "Leder", "Stoff", "Kristall", "Gold", "Silber", "Kupfer", "Diamant", "Rubin", "Saphir", "Smaragd", "Obsidian", "Mithril", "Adamantium", "Drachenhaut", "Phönixfeder", "Elfenbein", "Dämonenblut", "Geisterasche" };
            
            Name = names[Random.Shared.Next(names.Length)];
        }
        public void AddRandomMiscItem()
        {
            string[] names = { "Alte Münze", "Verzauberter Stein", "Mysteriöser Schlüssel", "Antikes Relikt", "Seltsamer Apparat", "Geheimnisvolle Schriftrolle", "Verblasstes Gemälde", "Rätselhafte Statue", "Uralte Karte", "Magischer Spiegel" };
            
            Name = names[Random.Shared.Next(names.Length)];
        }

    }
}