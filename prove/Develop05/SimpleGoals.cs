using System;

public class SimpleGoal : Goal
{
    public bool Completed { get; set; } = false;

    public override void RecordEvent()
    {
        if (!Completed)
        {
            Completed = true;
            Console.WriteLine($"Goal completed! You earned {Points} points!");
        }
        else Console.WriteLine("This goal is already completed.");
    }

    public override string GetStatus() => Completed ? "[X]" : "[ ]";
}
