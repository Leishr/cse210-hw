using System;

public class ChecklistGoal : Goal
{
    public int CompletedTimes { get; set; } = 0;
    public int TargetTimes { get; set; }
    public int BonusPoints { get; set; }

    public override void RecordEvent()
    {
        if (CompletedTimes < TargetTimes)
        {
            CompletedTimes++;
            Console.WriteLine($"You earned {Points} points for {Name}!");
            if (CompletedTimes == TargetTimes)
                Console.WriteLine($"Checklist complete! Bonus {BonusPoints} points!");
        }
        else Console.WriteLine("Checklist goal already completed!");
    }

    public override string GetStatus() => $"[{CompletedTimes}/{TargetTimes}]";
}
