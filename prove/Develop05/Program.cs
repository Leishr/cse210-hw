using System;
using System.Collections.Generic;

namespace EternalQuestSimple
{
    class Program
    {
        static List<Goal> goals = new List<Goal>();
        static int totalPoints = 0;

        static void Main()
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

            Goal goal = type switch
            {
                "1" => new SimpleGoal { Name = name, Points = points },
                "2" => new EternalGoal { Name = name, Points = points },
                "3" => CreateChecklistGoal(name, points),
                _ => null
            };

            if (goal != null)
            {
                goals.Add(goal);
                Console.WriteLine("Goal added!");
            }
            else Console.WriteLine("Invalid type.");
        }

        static ChecklistGoal CreateChecklistGoal(string name, int points)
        {
            Console.Write("Target times: ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("Bonus points: ");
            int bonus = int.Parse(Console.ReadLine());

            return new ChecklistGoal
            {
                Name = name,
                Points = points,
                TargetTimes = target,
                BonusPoints = bonus
            };
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
