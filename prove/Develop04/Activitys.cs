using System;
using System.IO;
using System.Threading;

namespace MindfulnessProgram
{
    public abstract class Activity
    {
        public string Name { get; }
        public string Description { get; }
        public int DurationSeconds { get; private set; }

        protected Activity(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Start()
        {
            Console.Clear();
            Console.WriteLine($"=== {Name} ===\n{Description}\n");

            // Ask user for duration
            DurationSeconds = GetDuration();

            Console.WriteLine("Prepare to begin...");
            Spinner(3); // show spinner for 3 seconds
            Console.Clear();

            // Run the activity
            RunActivity();

            Finish();
        }

        private int GetDuration()
        {
            int seconds;
            Console.Write("Enter duration (seconds): ");
            while (!int.TryParse(Console.ReadLine(), out seconds) || seconds < 0)
            {
                Console.Write("Invalid. Enter 0 or greater: ");
            }
            return seconds;
        }

        protected abstract void RunActivity();

        private void Finish()
        {
            Console.WriteLine("\nWell done!");
            Spinner(3);
            Console.WriteLine($"Completed {Name} for {DurationSeconds} seconds.");
            Spinner(3);

            // Log to file
            try
            {
                string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {Name} - {DurationSeconds} seconds";
                File.AppendAllLines("activity_log.txt", new[] { logLine });
            }
            catch { }
        }

        // Spinner animation for a number of seconds
        protected void Spinner(int seconds)
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

        // Countdown display
        protected void Countdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i + " ");
                Thread.Sleep(1000);
                Console.Write("\b\b");
            }
        }
    }
}
