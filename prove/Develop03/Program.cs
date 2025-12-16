using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = new List<Scripture>()
        {
            new Scripture(new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
            new Scripture(new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture(new Reference("Mosiah", 2, 17),
                "When ye are in the service of your fellow beings ye are only in the service of your God.")
        };

        Random random = new Random();
        bool keepGoing = true;

        while (keepGoing)
        {
            Console.Clear();
            Scripture scripture = scriptures[random.Next(scriptures.Count)];
            while (!scripture.AllHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit, 'new' for another scripture:");
                string input = Console.ReadLine()?.ToLower();

                if (input == "quit") { keepGoing = false; break; }
                else if (input == "new") { break; }
                else { scripture.HideRandomWords(3); }
            }

            if (scripture.AllHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nAll words are hidden! Press Enter to try another scripture or type 'quit' to exit:");
                string choice = Console.ReadLine()?.ToLower();
                if (choice == "quit") keepGoing = false;
            }
        }

        Console.WriteLine("Goodbye!");
    }
}
