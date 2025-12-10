using System;
using System.Collections.Generic;
using System.Text;
using Trade;
using Fight;
using Characters;
using SaveTheWorld;
using System.Security;

namespace Maps
{
    public class Map
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public List<string> Locations { get; set; } = new();

        public Map(string name, int size)
        {
            Name = name;
            Size = size;
            Locations = new List<string>();
        }

        public void AddLocation(string location)
        {
            Locations.Add(location);
            Console.WriteLine($"{location} was added to map {Name}.");
        }
        public void DisplayMap()
        {
            int option = 0;

            while (option != 3)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Show Map");
                Console.WriteLine("2. Walk");
                Console.WriteLine("3. Exit");
                Console.Write("Your choice: ");
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid input.");
                    Console.WriteLine();
                    continue;
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Showing Map:");
                        Console.WriteLine($"Map: {Name}, Size: {Size}");
                        Console.WriteLine("Locations:");
                        foreach (var location in Locations)
                        {
                            Console.WriteLine($"- {location}");
                        }
                        watchMap(5); // Call only once
                        break;

                    case 2:
                        if (Locations.Count > 0)
                        {
                            MoveToLocation(Locations[Random.Shared.Next(Locations.Count)]);
                            Console.WriteLine("You are walking through the map...");
                        }
                        else
                        {
                            Console.WriteLine("No locations available.");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Exiting map view.");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine();
            }
        }
        public void GenerateRandomMap(int numberOfLocations)
        {
            string[] possibleLocations = { "Village", "City", "Forest", "Mountain", "Lake", "River", "Cave", "Ruins", "Temple", "Castle", "Island", "Desert", "Swamp", "Hill", "Valley", "Monastery", "Fortress", "Harbor", "Market", "Arena",
            "Tower", "Maze", "Garden", "Bridge", "Path", "Watchtower", "Field", "Grove", "Shrine", "Orchard", "Vineyard", "Cemetery", "Library", "Laboratory", "Workshop", "Forge", "Bakery", "Tavern", "Inn",
            "Marketplace", "Citadel", "Keep", "Cathedral", "Theater", "Museum", "Park", "Plaza", "Boulevard", "Promenade", "Parkland", "Botanical Garden", "Aquarium", "Zoo", "Planetarium", "Observatory" };

            for (int i = 0; i < numberOfLocations; i++)
            {
                string location = possibleLocations[Random.Shared.Next(possibleLocations.Length)];
                AddLocation(location);
            }

        }
        public List<string> SerializeMap()
        {
            return Locations;
        }
        public static Map DeserializeMap(List<string> mapData)
        {
            Map map = new Map("Loaded Map", mapData.Count);
            map.Locations = new List<string>(mapData); // Kopie erstellen
            return map;
        }

        public void ClearMap()
        {
            Locations.Clear();
            Console.WriteLine($"Map {Name} has been reset.");
        }
        public void MoveToLocation(string location)
        {
            if (Locations.Contains(location))
            {
                Console.WriteLine($"You move to {location} on map {Name}.");
            }
            else
            {
                Console.WriteLine($"{location} is not available on map {Name}.");
            }
        }
        public void watchMap(int size)
        {
            for (int i = 0; i < size; i++)
            {

                for (int j = 0; j < size; j++)
                {
                    Console.Write("[ ]");
                }
                Console.WriteLine();
            }
        }
    }
}