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
                        Console.WriteLine("Do you want to load an old save game? (y/n)");
                        string loadChoice = Console.ReadLine();
                        if (loadChoice?.ToLower() == "y")
                        {
                                Console.WriteLine("Enter character name:");
                                string name = Console.ReadLine();
                                Console.WriteLine("Enter character level:");
                                string levelInput = Console.ReadLine();
                                int level = 1;
                                int.TryParse(levelInput, out level);
                                Console.WriteLine("Enter character class:");
                                string klasse = Console.ReadLine();

                                string savePath = $"savegames/{name}_level{level}.json";
                                if (File.Exists(savePath))
                                {
                                        player = new Character(name);
                                        SaveSystem.LoadIntoCharacter(savePath, player);
                                        Console.WriteLine("Game loaded!");
                                        gameMap = player.CurrentMap;
                                }
                                else
                                {
                                        Console.WriteLine("No save game found, creating new character.");
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

                                Console.WriteLine("What do you want to do?");
                                Console.WriteLine($"Character: {player.Name}, Class: {player.Klasse}, Level: {player.Level}, Health: {player.Health}, Mana: {player.Mana}, Experience: {player.Experience}, Gold: {player.Gold}");
                                Console.WriteLine("Money: " + player.Gold);
                                Console.WriteLine("1. Start Battle");
                                Console.WriteLine("2. Show Map and Options");
                                Console.WriteLine("3. Show Stats");
                                Console.WriteLine("4. Show Inventory");
                                Console.WriteLine("5. Use Item");
                                Console.WriteLine("6. Visit Shop");
                                Console.WriteLine("7. Manage Members");
                                Console.WriteLine("8. Save Game");
                                Console.WriteLine("9. Load Game");
                                Console.WriteLine("0. Quit");
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
                                                Console.WriteLine("Which item do you want to use?");
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
                                                        Console.WriteLine("Invalid selection, please enter a number.");
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
                                                                Console.WriteLine("Invalid selection, index out of range.");
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
                                                System.Console.WriteLine("Game saved!");
                                                break;
                                        case "9":
                                                SaveSystem.LoadIntoCharacter($"savegames/{player.Name}_level{player.Level}.json", player);
                                                System.Console.WriteLine("Game loaded!");
                                                break;
                                        case "0":
                                                playing = false;
                                                Console.WriteLine("Thanks for playing!");
                                                break;
                                        default:
                                                Console.WriteLine("Invalid selection, please try again.");
                                                break;
                                }
                        }
                }
                class Einleitung
                {
                        public static void Intro()
                        {
                                Console.WriteLine("Welcome to my RPG game!");
                                Console.WriteLine("In this game you can create a character, fight monsters and gain experience.");
                                Console.WriteLine("Have fun!");
                        }
                }
        }
}