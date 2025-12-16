using System;

namespace MindfulnessProgram
{
    public class ReflectionActivity : Activity
    {
        private static readonly string[] Prompts =
            { "Think of a time you helped someone.", "Think of a time you did something difficult." };
        private static readonly string[] Questions =
            { "Why was this meaningful?", "How did you feel?", "What did you learn?" };

        public ReflectionActivity() : base("Reflection Activity",
            "Reflect on times you showed strength and resilience.") { }

        protected override void RunActivity()
        {
            var rng = new Random();
            Console.WriteLine("Prompt:\n--- " + Prompts[rng.Next(Prompts.Length)] + " ---");
            Console.WriteLine("\nPress Enter when ready."); Console.ReadLine();
            var end = DateTime.Now.AddSeconds(DurationSeconds);
            while (DateTime.Now < end)
            {
                Console.WriteLine("> " + Questions[rng.Next(Questions.Length)]);
                Spinner(Math.Min(6, (int)Math.Ceiling((end - DateTime.Now).TotalSeconds)));
            }
        }
    }
}
