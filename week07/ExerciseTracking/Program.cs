using System;
using System.Collections.Generic;

// Base class for all activities
public abstract class Activity
{
    // Shared attributes with encapsulation
    private DateTime _date;
    private int _minutes;

    // Properties with validation
    public DateTime Date 
    { 
        get { return _date; } 
        set { _date = value; }
    }

    public int Minutes 
    { 
        get { return _minutes; } 
        set { _minutes = value > 0 ? value : throw new ArgumentException("Minutes must be positive"); }
    }

    // Constructor
    public Activity(DateTime date, int minutes)
    {
        Date = date;
        Minutes = minutes;
    }

 
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

   
    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {GetType().Name} ({Minutes} min)- " +
               $"Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, " +
               $"Pace: {GetPace():F1} min per mile";
    }
}

// Derived class for running activity
public class Running : Activity
{
    private double _distance;

    public double Distance 
    { 
        get { return _distance; } 
        set { _distance = value > 0 ? value : throw new ArgumentException("Distance must be positive"); }
    }

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        Distance = distance;
    }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return (Distance / Minutes) * 60;
    }

    public override double GetPace()
    {
        return Minutes / Distance;
    }
}

// Derived class for cycling activity
public class Cycling : Activity
{
    private double _speed;

    public double Speed 
    { 
        get { return _speed; } 
        set { _speed = value > 0 ? value : throw new ArgumentException("Speed must be positive"); }
    }

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        Speed = speed;
    }

    public override double GetDistance()
    {
        return (Speed * Minutes) / 60;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        return 60 / Speed;
    }
}

// Derived class for swimming activity
public class Swimming : Activity
{
    private int _laps;
    private const double METERS_PER_LAP = 50;
    private const double METERS_PER_MILE = 1609.34;

    public int Laps 
    { 
        get { return _laps; } 
        set { _laps = value > 0 ? value : throw new ArgumentException("Laps must be positive"); }
    }

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        Laps = laps;
    }

    public override double GetDistance()
    {
        
        return Laps * METERS_PER_LAP / METERS_PER_MILE;
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


class Program
{
    static void Main()
    {
        // Create a list of activities
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 4), 45, 15.0),
            new Swimming(new DateTime(2022, 11, 5), 20, 20)
        };

        // Display summary for each activity
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}