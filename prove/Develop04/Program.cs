using System;
using System.IO;
using System.Threading;

namespace MindfulnessProgram
{
    class Program
    {
        static void Main()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program\n-------------------\n1. Breathing\n2. Reflection\n3. Listing\n4. View log\n0. Quit\nChoose: ");
                var choice = Console.ReadLine();
                Activity act = choice switch
                {
                    "1" => new BreathingActivity(),
                    "2" => new ReflectionActivity(),
                    "3" => new ListingActivity(),
                    _ => null
                };
                if (act != null) act.Start();
                else if (choice == "4") ShowLog();
                else if (choice == "0") { running = false; Console.WriteLine("Goodbye!"); Thread.Sleep(1000); }
                else { Console.WriteLine("Invalid. Press Enter."); Console.ReadLine(); }
            }
        }

        static void ShowLog()
        {
            Console.Clear(); Console.WriteLine("Activity Log:");
            if (!File.Exists("activity_log.txt")) Console.WriteLine("(No entries yet.)");
            else
            {
                var lines = File.ReadAllLines("activity_log.txt");
                for (int i = lines.Length - 1; i >= 0; i--) Console.WriteLine(lines[i]);
            }
            Console.WriteLine("\nPress Enter."); Console.ReadLine();
        }
    }
}
