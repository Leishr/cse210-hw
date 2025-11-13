using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MindfulnessProgram
{
    // Base class for shared behavior and attributes
    public abstract class Activity
    {
        private string _name;
        private string _description;
        private int _durationSeconds;

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        // Encapsulated properties
        public string Name => _name;
        public string Description => _description;

        public int DurationSeconds
        {
            get => _durationSeconds;
            private set
            {
                if (value < 0) _durationSeconds = 0;
                else _durationSeconds = value;
            }
        }

        // Common starting sequence
        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"=== {Name} ===");
            Console.WriteLine();
            Console.WriteLine(Description);
            Console.WriteLine();
            SetDurationFromUser();
            Console.WriteLine();
            Console.WriteLine("Prepare to begin...");
            ShowSpinner(3);
            Console.Clear();
            RunActivity();
            Finish();
        }

        // Ask user for duration in seconds
        private void SetDurationFromUser()
        {
            int seconds = 0;
            Console.Write("Enter the duration you want for this activity (in seconds): ");
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out seconds) && seconds >= 0)
                {
                    DurationSeconds = seconds;
                    break;
                }
                Console.Write("Invalid. Please enter a whole number of seconds (0 or greater): ");
            }
        }

        // Each derived class supplies its own RunActivity implementation
        protected abstract void RunActivity();

        // Common finishing sequence
        protected void Finish()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            ShowSpinner(3);
            Console.WriteLine($"You have completed the {Name} for {DurationSeconds} seconds.");
            // small pause before returning to menu
            ShowSpinner(3);
            // Log activity (extra feature)
            LogActivity();
        }

        // Simple spinner animation for a given number of seconds
        protected void ShowSpinner(int seconds)
        {
            string[] spin = { "|", "/", "-", "\\" };
            DateTime end = DateTime.Now.AddSeconds(seconds);
            int i = 0;
            while (DateTime.Now < end)
            {
                Console.Write(spin[i % spin.Length]);
                Thread.Sleep(250);
                Console.Write("\b");
                i++;
            }
        }

        // Show a countdown in seconds (display each second)
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        // Show countdown with display that replaces number (keeps nicer output)
        protected void ShowCountdownLine(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i + " ");
                Thread.Sleep(1000);
                Console.Write("\b\b");
            }
        }

        // Extra: write a simple log entry to a local file
        private void LogActivity()
        {
            try
            {
                string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {Name} - {DurationSeconds} seconds";
                File.AppendAllLines("activity_log.txt", new[] { logLine });
            }
            catch
            {
                // if logging fails, ignore (don't crash program)
            }
        }
    }

    // Breathing activity implementation
    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        protected override void RunActivity()
        {
            Console.WriteLine("Follow the prompts and breathe slowly.");
            Console.WriteLine();

            DateTime end = DateTime.Now.AddSeconds(DurationSeconds);
            bool breatheIn = true;

            while (DateTime.Now < end)
            {
                if (breatheIn)
                {
                    Console.Write("Breathe in... ");
                    // Show countdown for breath in (4 seconds), but never exceed time left
                    int secondsLeft = (int)Math.Ceiling((end - DateTime.Now).TotalSeconds);
                    int count = Math.Min(4, Math.Max(1, secondsLeft));
                    ShowCountdownLine(count);
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("Breathe out... ");
                    int secondsLeft = (int)Math.Ceiling((end - DateTime.Now).TotalSeconds);
                    int count = Math.Min(6, Math.Max(1, secondsLeft)); // out a bit longer
                    ShowCountdownLine(count);
                    Console.WriteLine();
                }

                breatheIn = !breatheIn;

                // small pause spinner between prompts
                ShowSpinner(1);
            }
        }
    }

    // Reflection activity implementation
    public class ReflectionActivity : Activity
    {
        private static readonly string[] prompts = new string[]
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private static readonly string[] questions = new string[]
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity() : base(
            "Reflection Activity",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        { }

        protected override void RunActivity()
        {
            Random rng = new Random();
            string prompt = prompts[rng.Next(prompts.Length)];
            Console.WriteLine("Consider the following prompt:");
            Console.WriteLine();
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine();
            Console.WriteLine("When you have something in mind, press Enter to continue.");
            Console.ReadLine();

            DateTime end = DateTime.Now.AddSeconds(DurationSeconds);

            // While time remains, show random reflection questions with spinner pauses
            while (DateTime.Now < end)
            {
                string q = questions[rng.Next(questions.Length)];
                Console.WriteLine("> " + q);
                // pause for a few seconds while showing spinner - choose 6 sec or time left
                int secondsLeft = (int)Math.Ceiling((end - DateTime.Now).TotalSeconds);
                int pause = Math.Min(6, Math.Max(1, secondsLeft));
                ShowSpinner(pause);
                Console.WriteLine();
            }
        }
    }

    // Listing (Enumeration) activity implementation
    public class ListingActivity : Activity
    {
        private static readonly string[] prompts = new string[]
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity() : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        { }

        protected override void RunActivity()
        {
            Random rng = new Random();
            string prompt = prompts[rng.Next(prompts.Length)];
            Console.WriteLine("List as many responses to the prompt as you can:");
            Console.WriteLine();
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine();

            Console.WriteLine("You will have a few seconds to think before starting...");
            ShowCountdownLine(5);
            Console.WriteLine();
            Console.WriteLine("Begin listing! Press Enter after each item.");

            DateTime end = DateTime.Now.AddSeconds(DurationSeconds);
            List<string> items = new List<string>();

            // Keep accepting input until time expires
            while (DateTime.Now < end)
            {
                // Check if time will expire while waiting for input.
                // We can't easily cancel Console.ReadLine() but we can do a brief check after.
                // Prompt the user for input. They can press Enter to submit.
                if (Console.KeyAvailable)
                {
                    // If there's a key available, read it to prevent blocking issues (not typical).
                }

                // Display prompt symbol
                Console.Write("> ");
                // Set a small timeout loop to allow expiration check; use ReadLine normally, but if user takes too long it will still wait.
                // Simpler approach: we let ReadLine block — that's acceptable for this assignment.
                string entry = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(entry))
                {
                    items.Add(entry.Trim());
                }

                // If time expired while they were entering, break
                if (DateTime.Now >= end) break;
            }

            Console.WriteLine();
            Console.WriteLine($"You listed {items.Count} item(s). Nice work!");
            Console.WriteLine("Items:");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");
            }
        }
    }

    // Program class with menu
    class Program
    {
        static void Main(string[] args)
        {
            var running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. View activity log (extra)");
                Console.WriteLine("0. Quit");
                Console.WriteLine();
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Activity activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        activity.Start();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        activity.Start();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        activity.Start();
                        break;
                    case "4":
                        ShowLog();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Goodbye. Take care!");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void ShowLog()
        {
            Console.Clear();
            Console.WriteLine("Activity Log (most recent first):");
            try
            {
                if (!File.Exists("activity_log.txt"))
                {
                    Console.WriteLine("(No log entries yet.)");
                }
                else
                {
                    var lines = File.ReadAllLines("activity_log.txt");
                    for (int i = lines.Length - 1; i >= 0; i--)
                    {
                        Console.WriteLine(lines[i]);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Unable to read log file.");
            }
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }
    }
}
