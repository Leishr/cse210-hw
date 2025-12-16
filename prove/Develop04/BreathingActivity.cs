using System;

namespace MindfulnessProgram
{
    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing Activity",
            "Relax by breathing in and out slowly. Focus on your breathing.") { }

        protected override void RunActivity()
        {
            var end = DateTime.Now.AddSeconds(DurationSeconds);
            bool inBreath = true;
            while (DateTime.Now < end)
            {
                int count = Math.Min(inBreath ? 4 : 6, (int)Math.Ceiling((end - DateTime.Now).TotalSeconds));
                Console.Write(inBreath ? "Breathe in... " : "Breathe out... ");
                Countdown(count); Console.WriteLine(); inBreath = !inBreath;
                Spinner(1);
            }
        }
    }
}
