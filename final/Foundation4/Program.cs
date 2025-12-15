using System;
using System.Collections.Generic;

// Base abstract class
abstract class Activity
{
    private string _date;
    private double _minutes;

    public Activity(string date, double minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    // Encapsulation: getters
    public string Date { get { return _date; } }
    public double Minutes { get { return _minutes; } }

    // Abstract methods to be overridden
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Summary method
    public virtual string GetSummary()
    {
        return $"{Date} {this.GetType().Name} ({Minutes} min) - " +
               $"Distance {GetDistance():0.00} miles, " +
               $"Speed {GetSpeed():0.00} mph, " +
               $"Pace {GetPace():0.00} min per mile";
    }
}

// Running class
class Running : Activity
{
    private double _distance; // in miles

    public Running(string date, double minutes, double distance) 
        : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (_distance / Minutes) * 60;
    }

    public override double GetPace()
    {
        return Minutes / _distance;
    }
}

// Cycling class
class Cycling : Activity
{
    private double _speed; // in mph

    public Cycling(string date, double minutes, double speed)
        : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (_speed * Minutes) / 60;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        return 60 / _speed;
    }
}

// Swimming class
class Swimming : Activity
{
    private int _laps;

    public Swimming(string date, double minutes, int laps)
        : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        // Convert laps to miles (50 meters per lap)
        return (_laps * 50.0 / 1000) * 0.62;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60;
    }

    public override double GetPace()
    {
        return Minutes / GetDistance();
    }
}

// Main program
class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>()
        {
            new Running("03 Nov 2022", 30, 3.0),
            new Cycling("03 Nov 2022", 45, 12.0),
            new Swimming("03 Nov 2022", 60, 40)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
