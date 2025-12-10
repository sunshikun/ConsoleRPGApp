using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Linq;

using Maps;
using Trade;
using Fight;
using Characters;
using SaveTheWorld;


namespace ConsoleRPGApp
{
    class Program
        {
        static void Main(string[] args)
        {
                        /*Test trade = new Test();
                        trade.test1();*/
                        SaveGameManager saveSystem = new SaveGameManager();
                        Map gameMap = new Map("Abenteuerland", 10);
                        saveSystem.ListFilesInFolder("savegames");
                        Einleitung.Intro();
                        Character player = null;
                        Console.WriteLine("Möchtest du einen alten Spielstand laden? (j/n)");
                        string loadChoice = Console.ReadLine();
                        if (loadChoice?.ToLower() == "j")
                        {
                                Console.WriteLine("Gib den Namen deines Charakters ein:");
                                string name = Console.ReadLine();
                                Console.WriteLine("Gib das Level deines Charakters ein:");
                                string levelInput = Console.ReadLine();
                                int level = 1;
                                int.TryParse(levelInput, out level);
                                Console.WriteLine("Gib die Klasse deines Charakters ein:");
                                string klasse = Console.ReadLine();
                                
                                string savePath = $"savegames/{name}_level{level}.json";
                                if (File.Exists(savePath))
                                {
                                        player = new Character(name);
                                        SaveSystem.LoadIntoCharacter(savePath, player);
                                        Console.WriteLine("Spielstand geladen!");
                                        gameMap = player.CurrentMap;
                                }
                                else
                                {
                                        Console.WriteLine("Kein Spielstand gefunden, neuer Charakter wird erstellt.");
                                        player = CharakterErstellung.CreateCharacter();
                                        gameMap.GenerateRandomMap(5);
                                        player.CurrentMap = gameMap;
                                }
                        }
                        else
                        {
                                player = CharakterErstellung.CreateCharacter();
                                gameMap.GenerateRandomMap(5);
                                player.CurrentMap = gameMap;
                        }
                        bool playing = true;
                        while (playing)
                        {
                                
                                Console.WriteLine("Was möchtest du tun?");
                                Console.WriteLine($"Charakter: {player.Name}, Klasse: {player.Klasse}, Level: {player.Level}, Gesundheit: {player.Health}, Mana: {player.Mana}, Erfahrung: {player.Experience}, Gold: {player.Gold}");
                                Console.WriteLine("Money: " + player.Gold);
                                Console.WriteLine("1. Kampf starten");
                                Console.WriteLine("2. Map anzeigen und optionen");
                                Console.WriteLine("3. Statistiken anzeigen");
                                Console.WriteLine("4. Inventar anzeigen");
                                Console.WriteLine("5. Items verwenden");
                                Console.WriteLine("6. Shop besuchen");
                                Console.WriteLine("7. Mitglieder verwalten ");
                                Console.WriteLine("8. Spiel speichern");
                                Console.WriteLine("9. Spiel laden");
                                Console.WriteLine("0. Beenden");
                                string choice = Console.ReadLine();

                                switch (choice)
                                {
                                        case "1":

                                                Character monster = Monster.CreateMonster(player.Level);
                                                Kampf.StartBattle(player, monster);
                                                break;
                                        case "2":
                                                gameMap.DisplayMap();


                                                break;
                                        case "3":
                                                player.DisplayStats();
                                                break;
                                        case "4":
                                                Inventar.ShowInventory(player);
                                                break;

                                        case "5":
                                                Console.WriteLine("Welchen Gegenstand möchtest du verwenden?");
                                                int x = 1;

                                                foreach (var item in player.Inventory)
                                                {

                                                        Console.WriteLine($"{x}. {item.Name}");
                                                        x++;


                                                }
                                                Console.WriteLine($"");
                                                string choice2 = Console.ReadLine();

                                                // parse the user's choice and use the selected item by name
                                                if (!int.TryParse(choice2, out int selected))
                                                {
                                                        Console.WriteLine("Ungültige Auswahl, bitte eine Zahl eingeben.");
                                                }
                                                else
                                                {
                                                        selected -= 1; // convert to 0-based index
                                                        if (selected >= 0 && selected < player.Inventory.Count)
                                                        {
                                                                var itemName = player.Inventory[selected].Name;
                                                                Trade.Inventar.UseItem(itemName, player);
                                                        }
                                                        else
                                                        {
                                                                Console.WriteLine("Ungültige Auswahl, Index außerhalb des Bereichs.");
                                                        }
                                                }
                                                break;
                                        case "6":
                                                Shop.OpenShop(player);
                                                break;
                                        case "7":
                                                Mitglieder member = new Mitglieder("", 0, "");
                                                member.Create_Random_Member();
                                                member.DisplayInfo();
                                                break;
                                        case "8":
                                                SaveSystem.SaveCharacter($"savegames/{player.Name}_level{player.Level}.json", player);
                                                System.Console.WriteLine("Spiel gespeichert!");
                                                break;
                                        case "9":
                                                SaveSystem.LoadIntoCharacter($"savegames/{player.Name}_level{player.Level}.json", player);
                                                System.Console.WriteLine("Spiel geladen!");
                                                break;
                                        case "0":
                                                playing = false;
                                                Console.WriteLine("Danke fürs Spielen!");
                                                break;
                                        default:
                                                Console.WriteLine("Ungültige Auswahl, bitte versuche es erneut.");
                                                break;
                                }
                        }
        }
        class Einleitung
        {
            public static void Intro()
            {
                Console.WriteLine("Willkommen zu meinem RPG-Spiel!");
                Console.WriteLine("In diesem Spiel kannst du einen Charakter erstellen, gegen Monster kämpfen und Erfahrung sammeln.");
                Console.WriteLine("Viel Spaß!");
            }
        }          
    }
}