using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
    public class ListingActivity : Activity
    {
        private static readonly string[] Prompts =
            { "Who are people you appreciate?", "What are your strengths?", "Who did you help this week?" };

        public ListingActivity() : base("Listing Activity",
            "List as many good things in your life as you can.") { }

        protected override void RunActivity()
        {
            var rng = new Random();
            Console.WriteLine("Prompt:\n--- " + Prompts[rng.Next(Prompts.Length)] + " ---");
            Console.WriteLine("Think for a few seconds..."); Countdown(5); Console.WriteLine("Begin listing!");
            var end = DateTime.Now.AddSeconds(DurationSeconds);
            var items = new List<string>();
            while (DateTime.Now < end)
            {
                Console.Write("> ");
                string entry = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(entry)) items.Add(entry.Trim());
            }
            Console.WriteLine($"\nYou listed {items.Count} item(s):");
            for (int i = 0; i < items.Count; i++) Console.WriteLine($"{i + 1}. {items[i]}");
        }
    }
}
