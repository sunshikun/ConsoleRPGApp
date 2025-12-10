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
            Console.WriteLine($"{location} wurde zur Karte {Name} hinzugefügt.");
        }
        public void DisplayMap()
        {
            int option = 0;

            while (option != 3)
            {
                Console.WriteLine("Wähle eine Option:");
                Console.WriteLine("1. Karte anzeigen");
                Console.WriteLine("2. laufen");
                Console.WriteLine("3. Beenden");
                Console.Write("Deine Wahl: ");
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Ungültige Eingabe.");
                    Console.WriteLine();
                    continue;
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Karte wird angezeigt:");
                        Console.WriteLine($"Karte: {Name}, Größe: {Size}");
                        Console.WriteLine("Orte:");
                        foreach (var location in Locations)
                                {
                                    Console.WriteLine($"- {location}");
                                }
                        watchMap(5); // Nur einmal aufrufen
                        break;

                    case 2:
                        if (Locations.Count > 0)
                        {
                            MoveToLocation(Locations[Random.Shared.Next(Locations.Count)]);
                            Console.WriteLine("Du läufst durch die Karte...");
                        }
                        else
                        {
                            Console.WriteLine("Keine Orte vorhanden.");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Beende Kartenanzeige.");
                        break;
                    default:
                        Console.WriteLine("Ungültige Option.");
                        break;
                }

                Console.WriteLine();
            }
        }
        public void GenerateRandomMap(int numberOfLocations)
        {
            string[] possibleLocations = { "Dorf", "Stadt", "Wald", "Berg", "See", "Fluss", "Höhle", "Ruine", "Tempel", "Schloss", "Insel", "Wüste", "Sumpf", "Hügel", "Tal", "Kloster", "Festung", "Hafen", "Markt", "Arena",
            "Turm", "Labyrinth", "Garten", "Brücke", "Pfad", "Wachturm", "Feld", "Hain", "Schrein", "Obsthain", "Weinberg", "Friedhof", "Bibliothek", "Labor", "Werkstatt", "Schmiede", "Bäckerei", "Taverne", "Gasthaus",
            "Marktplatz", "Zitadelle", "Burg", "Kathedrale", "Theater", "Museum", "Park", "Plaza", "Boulevard", "Promenade", "Parkanlage", "Botanischer Garten", "Aquarium", "Zoo", "Planetarium", "Observatorium" };

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
            Map map = new Map("Geladene Karte", mapData.Count);
            map.Locations = new List<string>(mapData); // Kopie erstellen
            return map;
        }

        public void ClearMap()
        {
            Locations.Clear();
            Console.WriteLine($"Karte {Name} wurde zurückgesetzt.");
        }
        public void MoveToLocation(string location)
        {
            if (Locations.Contains(location))
            {
                Console.WriteLine($"Du bewegst dich zu {location} auf der Karte {Name}.");
            }
            else
            {
                Console.WriteLine($"{location} ist nicht auf der Karte {Name} vorhanden.");
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