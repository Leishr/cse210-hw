using System;

public class EternalGoal : Goal
{
    public override void RecordEvent()
    {
        Console.WriteLine($"You recorded {Name} and earned {Points} points!");
    }

    public override string GetStatus() => "[∞]";
}
