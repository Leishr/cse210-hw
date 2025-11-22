using System;
using System.Collections.Generic;

namespace EternalQuestSimple
{
    // Base class for all goals
    public abstract class Goal
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public abstract void RecordEvent();
        public abstract string GetStatus();
    }

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
            else
            {
                Console.WriteLine("This goal is already completed.");
            }
        }

        public override string GetStatus() => Completed ? "[X]" : "[ ]";
    }

    public class EternalGoal : Goal
    {
        public override void RecordEvent()
        {
            Console.WriteLine($"You recorded {Name} and earned {Points} points!");
        }

        public override string GetStatus() => "[∞]";
    }

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
            else
            {
                Console.WriteLine("Checklist goal already completed!");
            }
        }

        public override string GetStatus() => $"[{CompletedTimes}/{TargetTimes}]";
    }

    class Program
    {
        static List<Goal> goals = new List<Goal>();
        static int totalPoints = 0;

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n1. Show Goals  2. Add Goal  3. Record Event  4. Show Score  5. Exit");
                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowGoals(); break;
                    case "2": AddGoal(); break;
                    case "3": RecordEvent(); break;
                    case "4": ShowScore(); break;
                    case "5": exit = true; break;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void ShowGoals()
        {
            Console.WriteLine("\n--- Goals ---");
            for (int i = 0; i < goals.Count; i++)
                Console.WriteLine($"{i + 1}. {goals[i].GetStatus()} {goals[i].Name}");
        }

        static void AddGoal()
        {
            Console.WriteLine("1. Simple  2. Eternal  3. Checklist");
            Console.Write("Type: ");
            string type = Console.ReadLine();
            Console.Write("Goal name: ");
            string name = Console.ReadLine();
            Console.Write("Points: ");
            int points = int.Parse(Console.ReadLine());

            Goal goal = null;
            if (type == "1") goal = new SimpleGoal { Name = name, Points = points };
            else if (type == "2") goal = new EternalGoal { Name = name, Points = points };
            else if (type == "3")
            {
                Console.Write("Target times: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                goal = new ChecklistGoal { Name = name, Points = points, TargetTimes = target, BonusPoints = bonus };
            }
            else { Console.WriteLine("Invalid type."); return; }

            goals.Add(goal);
            Console.WriteLine("Goal added!");
        }

        static void RecordEvent()
        {
            ShowGoals();
            Console.Write("Select goal number: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index >= 0 && index < goals.Count)
            {
                Goal goal = goals[index];
                goal.RecordEvent();

                if (goal is ChecklistGoal checklist)
                {
                    totalPoints += checklist.Points;
                    if (checklist.CompletedTimes == checklist.TargetTimes)
                        totalPoints += checklist.BonusPoints;
                }
                else
                {
                    totalPoints += goal.Points;
                }
            }
            else Console.WriteLine("Invalid goal number.");
        }

        static void ShowScore()
        {
            Console.WriteLine($"\nTotal Points: {totalPoints}");
        }
    }
}
