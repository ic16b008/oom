using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Task4;

namespace Task4_3
{
    class SerializationExample
    {
        public static void Run(IVertebrate[] items)
        {
            var animal = items[0];

            // 1. serialize a single book to a JSON string
            Console.WriteLine(JsonConvert.SerializeObject(animal));

            // 2. ... with nicer formatting
            Console.WriteLine(JsonConvert.SerializeObject(animal, Formatting.Indented));

            // 3. serialize all items
            var settings = new JsonSerializerSettings() { Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.Auto };
            Console.WriteLine(JsonConvert.SerializeObject(items, settings));

            // 4. store json string to file "items.json" on your Desktop
            var text = JsonConvert.SerializeObject(items, settings);
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filename = Path.Combine(desktop, "items.json");
            File.WriteAllText(filename, text);

            // 5. deserialize items from "items.json"
            var textFromFile = File.ReadAllText(filename);
            var itemsFromFile = JsonConvert.DeserializeObject<IVertebrate[]>(textFromFile, settings);
            foreach (var x in itemsFromFile)
            {
                Console.WriteLine($"{x.family} {x.species} {x.count}");
            }
        }
    }
}