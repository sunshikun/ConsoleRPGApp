using System;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using Maps;
using Trade;
using Fight;
using Characters;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace SaveTheWorld
{
    public class SaveGameManager
    {
        // Alle Dateien im Ordner auslesen

        public void ListFilesInFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath);

                Console.WriteLine("Files in folder:");
                foreach (string file in files)
                {
                    Console.WriteLine(Path.GetFileName(file)); // Only file names
                }
            }
            else
            {
                Console.WriteLine("Folder does not exist!");
            }
        }
    }
    public sealed class SaveData
    {
        // ... (wie gehabt, unverändert)
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("Klasse")] public string? Klasse { get; set; }
        [JsonPropertyName("level")] public int Level { get; set; }
        [JsonPropertyName("experience")] public long Experience { get; set; }
        [JsonPropertyName("gold")] public int Gold { get; set; }
        [JsonPropertyName("health")] public int Health { get; set; }
        [JsonPropertyName("mana")] public int Mana { get; set; }
        [JsonPropertyName("strength")] public int Strength { get; set; }
        [JsonPropertyName("agility")] public int Agility { get; set; }
        [JsonPropertyName("intelligence")] public int Intelligence { get; set; }
        [JsonPropertyName("defense")] public int Defense { get; set; }
        [JsonPropertyName("stamina")] public int Stamina { get; set; }
        [JsonPropertyName("luck")] public int Luck { get; set; }
        [JsonPropertyName("charisma")] public int Charisma { get; set; }
        [JsonPropertyName("wisdom")] public int Wisdom { get; set; }
        [JsonPropertyName("armor")] public int Armor { get; set; }
        [JsonPropertyName("speed")] public int Speed { get; set; }
        [JsonPropertyName("critChance")] public double CriticalChance { get; set; }
        [JsonPropertyName("critDamage")] public double CriticalDamage { get; set; }
        [JsonPropertyName("hpRegen")] public double HealthRegen { get; set; }
        [JsonPropertyName("mpRegen")] public double ManaRegen { get; set; }
        [JsonPropertyName("attackSpeed")] public double AttackSpeed { get; set; }
        [JsonPropertyName("spellPower")] public int SpellPower { get; set; }
        [JsonPropertyName("physical")] public bool Physical { get; set; }
        [JsonPropertyName("magical")] public bool Magical { get; set; }
        [JsonPropertyName("equippedWeapon")] public string? EquippedWeapon { get; set; }
        [JsonPropertyName("equippedArmor")] public string? EquippedArmor { get; set; }
        [JsonPropertyName("equippedAccessory")] public string? EquippedAccessory { get; set; }
        [JsonPropertyName("inventory")] public List<string> Inventory { get; set; } = new();
        [JsonPropertyName("equippedItems")] public List<string> EquippedItems { get; set; } = new();
        [JsonPropertyName("consumables")] public List<string> Consumables { get; set; } = new();
        [JsonPropertyName("questItems")] public List<string> QuestItems { get; set; } = new();
        [JsonPropertyName("craftingMaterials")] public List<string> CraftingMaterials { get; set; } = new();
        [JsonPropertyName("miscItems")] public List<string> MiscItems { get; set; } = new();
        [JsonPropertyName("loot")] public List<string> Loot { get; set; } = new();
        [JsonPropertyName("mapData")] public List<string> MapData { get; set; } = new();
    }

    public class SaveDataSet
    {
        [JsonPropertyName("saves")]
        public List<SaveData> Saves { get; set; } = new();
    }

    public class SaveSystem
    {
        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        // Save multiple SaveData entries to a file
        public static void SaveAll(string path, IEnumerable<Character> characters)
        {
            var set = new SaveDataSet();
            foreach (var c in characters)
            {
                if (c == null) continue;
                set.Saves.Add(CharacterToSaveData(c));
            }
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, JsonSerializer.Serialize(set, JsonOpts));
        }

        // Load all SaveData entries from a file
        public static List<SaveData> LoadAll(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException("Save file missing.", path);
            var json = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(json)) throw new InvalidDataException("Save file is empty.");

            SaveDataSet? set = null;
            try
            {
                set = JsonSerializer.Deserialize<SaveDataSet>(json, JsonOpts);
            }
            catch (JsonException)
            {
                // fallback: try legacy migration for single save
                var single = MigrateFromLegacyArray(json);
                set = new SaveDataSet { Saves = new List<SaveData> { single } };
            }
            return set?.Saves ?? new List<SaveData>();
        }

        // Save a single character (for backward compatibility)
        public static void SaveCharacter(string path, Character c)
        {
            SaveAll(path, new[] { c });
        }

        // Load first character from file into Character object
        public static void LoadIntoCharacter(string path, Character c)
        {
            var saves = LoadAll(path);
            if (saves.Count == 0) throw new InvalidDataException("No save records found.");
            ApplyToCharacter(c, saves[0]);
        }

        private static SaveData CharacterToSaveData(Character c)
        {
            return new SaveData
            {
                Name = c.Name,
                Klasse = c.Klasse,
                Level = c.Level,
                Experience = c.Experience,
                Gold = c.Gold,
                Health = c.Health,
                Mana = c.Mana,
                Strength = c.Strength,
                Agility = c.Agility,
                Intelligence = c.Intelligence,
                Defense = c.Defense,
                Stamina = c.Stamina,
                Luck = c.Luck,
                Charisma = c.Charisma,
                Wisdom = c.Wisdom,
                Armor = c.Armor,
                Speed = c.Speed,
                CriticalChance = c.CriticalChance,
                CriticalDamage = c.CriticalDamage,
                HealthRegen = c.HealthRegen,
                ManaRegen = c.ManaRegen,
                AttackSpeed = c.AttackSpeed,
                SpellPower = c.SpellPower,
                Physical = c.physical,
                Magical = c.magical,
                EquippedWeapon = c.EquippedWeapon != null ? c.EquippedWeapon.Name : "None",
                EquippedArmor = c.EquippedArmor != null ? c.EquippedArmor.Name : "None",
                EquippedAccessory = c.EquippedAccessory != null ? c.EquippedAccessory.Name : "None",
                Inventory = ToNameList(c.Inventory),
                EquippedItems = ToNameList(c.EquippedItems),
                Consumables = ToNameList(c.Consumables),
                QuestItems = ToNameList(c.QuestItems),
                CraftingMaterials = ToNameList(c.CraftingMaterials),
                MiscItems = ToNameList(c.MiscItems),
                Loot = ToNameList(c.Loot),
                MapData = c.CurrentMap != null ? c.CurrentMap.SerializeMap() : new List<string>(),
            };
        }

        // ... (restliche Methoden wie gehabt, unverändert)
        private static void ApplyToCharacter(Character c, SaveData d)
        {
            // ... (wie gehabt)
            if (d.Name != null) c.Name = d.Name;
            if (d.Klasse != null) c.Klasse = d.Klasse;
            c.Level = d.Level;
            c.Experience = (int)Math.Min(int.MaxValue, d.Experience);
            c.Gold = d.Gold;
            c.Health = d.Health;
            c.Mana = d.Mana;
            c.Strength = d.Strength;
            c.Agility = d.Agility;
            c.Intelligence = d.Intelligence;
            c.Defense = d.Defense;
            c.Stamina = d.Stamina;
            c.Luck = d.Luck;
            c.Charisma = d.Charisma;
            c.Wisdom = d.Wisdom;
            c.Armor = d.Armor;
            c.Speed = d.Speed;
            c.CriticalChance = d.CriticalChance;
            c.CriticalDamage = d.CriticalDamage;
            c.HealthRegen = d.HealthRegen;
            c.ManaRegen = d.ManaRegen;
            c.AttackSpeed = d.AttackSpeed;
            c.SpellPower = d.SpellPower;
            c.physical = d.Physical;
            c.magical = d.Magical;
            c.EquippedWeapon = !string.IsNullOrWhiteSpace(d.EquippedWeapon) && d.EquippedWeapon != "None" ? new Item(d.EquippedWeapon) : null;
            c.EquippedArmor = !string.IsNullOrWhiteSpace(d.EquippedArmor) && d.EquippedArmor != "None" ? new Item(d.EquippedArmor) : null;
            c.EquippedAccessory = !string.IsNullOrWhiteSpace(d.EquippedAccessory) && d.EquippedAccessory != "None" ? new Item(d.EquippedAccessory) : null;
            ReplaceWith(c.Inventory, d.Inventory);
            ReplaceWith(c.EquippedItems, d.EquippedItems);
            ReplaceWith(c.Consumables, d.Consumables);
            ReplaceWith(c.QuestItems, d.QuestItems);
            ReplaceWith(c.CraftingMaterials, d.CraftingMaterials);
            ReplaceWith(c.MiscItems, d.MiscItems);
            ReplaceWith(c.Loot, d.Loot);

            if (d.MapData != null && d.MapData.Count > 0)
            {
                c.CurrentMap = Map.DeserializeMap(d.MapData);
            }
            else
            {
                c.CurrentMap = new Map("Abenteuerland", 10);  // Nur wenn KEINE Daten vorhanden
            }



        }

        private static void ReplaceWith(List<Item> target, List<string> names)
        {
            target.Clear();
            foreach (var n in names)
                if (!string.IsNullOrWhiteSpace(n))
                    target.Add(new Item(n));
        }

        private static List<string> ToNameList(IEnumerable<Item> items)
            => items.Where(x => x != null)
                    .Select(x => x.Name ?? x.ToString() ?? "")
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .ToList();

        private static SaveData MigrateFromLegacyArray(string json)
        {
            var node = JsonNode.Parse(json);
            var d = new SaveData();

            if (node is not JsonArray a)
                return d;

            int N(string i) => int.TryParse(i, out var v) ? v : 0;

            double Nd(string i) => double.TryParse(i, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var v) ? v : 0.0;
            bool Nb(string i) => i.Equals("true", StringComparison.OrdinalIgnoreCase) || i == "1";
            string S(int i) => (i >= 0 && i < a.Count && a[i] is not null) ? a[i]!.ToString() : "";

            d.Name = S(0);
            d.Klasse = S(1);
            d.Level = N(S(2));
            d.Experience = N(S(3));
            d.Gold = N(S(4));
            d.Health = N(S(5));
            d.Mana = N(S(6));
            d.Strength = N(S(7));
            d.Agility = N(S(8));
            d.Intelligence = N(S(9));
            d.Defense = N(S(10));
            d.Stamina = N(S(11));
            d.Luck = N(S(12));
            d.Charisma = N(S(13));
            d.Wisdom = N(S(14));
            d.Armor = N(S(15));
            d.Speed = N(S(16));
            d.CriticalChance = Nd(S(17));
            d.CriticalDamage = Nd(S(18));
            d.HealthRegen = Nd(S(19));
            d.ManaRegen = Nd(S(20));
            d.AttackSpeed = Nd(S(21));
            d.SpellPower = N(S(22));
            d.Physical = Nb(S(23));
            d.Magical = Nb(S(24));
            d.EquippedWeapon = S(26);
            d.EquippedArmor = S(27);
            d.EquippedAccessory = S(28);
            d.EquippedItems = new List<string>();
            if (a.Count > 29 && a[29] is JsonArray arr29)
            {
                foreach (var n in arr29)
                    if (n is JsonValue v && v.TryGetValue<string>(out var name)
                        && !string.IsNullOrWhiteSpace(name))
                        d.EquippedItems.Add(name);
            }
            d.Consumables = new List<string>();
            if (a.Count > 30 && a[30] is JsonArray arr30)
            {
                foreach (var n in arr30)
                    if (n is JsonValue v && v.TryGetValue<string>(out var name) && !string.IsNullOrWhiteSpace(name))
                        d.Consumables.Add(name);
            }
            d.QuestItems = new List<string>();
            if (a.Count > 31 && a[31] is JsonArray arr31)
            {
                foreach (var n in arr31)
                    if (n is JsonValue v && v.TryGetValue<string>(out var name) && !string.IsNullOrWhiteSpace(name))
                        d.QuestItems.Add(name);
            }
            d.CraftingMaterials = new List<string>();
            if (a.Count > 32 && a[32] is JsonArray arr32)
            {
                foreach (var n in arr32)
                    if (n is JsonValue v && v.TryGetValue<string>(out var name) && !string.IsNullOrWhiteSpace(name))
                        d.CraftingMaterials.Add(name);
            }
            d.MiscItems = new List<string>();
            if (a.Count > 33 && a[33] is JsonArray arr33)
            {
                foreach (var n in arr33)
                    if (n is JsonValue v && v.TryGetValue<string>(out var name) && !string.IsNullOrWhiteSpace(name))
                        d.MiscItems.Add(name);
            }
            d.Loot = new List<string>();
            if (a.Count > 34 && a[34] is JsonArray arr34)
            {
                foreach (var n in arr34)
                    if (n is JsonValue v && v.TryGetValue<string>(out var name) && !string.IsNullOrWhiteSpace(name))
                        d.Loot.Add(name);
            }
            else
            {
                // Fallback: take the last JsonArray before inventory as loot
                var lootArr = a.OfType<JsonArray>().ElementAtOrDefault(a.OfType<JsonArray>().Count() - 2);
                if (lootArr != null)
                {
                    foreach (var n in lootArr)
                        if (n is JsonValue v && v.TryGetValue<string>(out var name) && !string.IsNullOrWhiteSpace(name))
                            d.Loot.Add(name);
                }
            }
            d.Inventory = new List<string>();
            d.MapData = new List<string>();
            if (a.Count > 36 && a[36] is JsonArray arr36)
            {
                foreach (var n in arr36)
                    if (n is JsonValue v && v.TryGetValue<string>(out var name) && !string.IsNullOrWhiteSpace(name))
                        d.MapData.Add(name);
            }
            else
            {
                // Fallback: take the last JsonArray as map data if not found at expected index
                var mapArr = a.OfType<JsonArray>().LastOrDefault();
                if (mapArr != null)
                {
                    foreach (var n in mapArr)
                        if (n is JsonValue v && v.TryGetValue<string>(out var name) && !string.IsNullOrWhiteSpace(name))
                            d.MapData.Add(name);
                }
            }



            JsonArray? invArr = null;
            if (a.Count > 35 && a[35] is JsonArray arr35) invArr = arr35;
            invArr ??= a.OfType<JsonArray>().LastOrDefault();

            if (invArr != null)
            {
                foreach (var n in invArr)
                    if (n is JsonValue v && v.TryGetValue<string>(out var name) && !string.IsNullOrWhiteSpace(name))
                        d.Inventory.Add(name);
            }

            return d;
        }
    }
}
