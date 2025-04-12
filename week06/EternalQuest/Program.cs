using System;  
using System.Collections.Generic;  
using System.IO;  
using System.Runtime.Serialization;  
using System.Runtime.Serialization.Formatters.Binary;  

// Base class for all goals  
[Serializable] // Marking the class as serializable for saving/loading  
public abstract class Goal  
{  
    public string Name { get; }  
    public string Description { get; }  
    protected int Points { get; private set; }  
    public bool IsComplete { get; private set; }  

    protected Goal(string name, string description, int points)  
    {  
        Name = name;  
        Description = description;  
        Points = points;  
        IsComplete = false;  
    }  

    public virtual void CompleteGoal()  
    {  
        if (!IsComplete)  
        {  
            IsComplete = true;  
            Console.WriteLine($"Goal '{Name}' completed! You've earned {Points} points.");  
        }  
    }  

    public int GetPoints() => Points; // Expose points to derived classes  

    public abstract string GetGoalDetails(); // Abstract method for derived classes to implement  
}  

// Simple goal class that can be marked complete  
[Serializable]  
public class SimpleGoal : Goal  
{  
    public SimpleGoal(string name, string description, int points)  
        : base(name, description, points) { }  

    public override string GetGoalDetails() =>  
        IsComplete ? $"[X] {Name}: {Description} - {GetPoints()} points" : $"[ ] {Name}: {Description}";  
}  

// Eternal goal class that can be recorded multiple times without completion  
[Serializable]  
public class EternalGoal : Goal  
{  
    public int TimesRecorded { get; private set; }  

    public EternalGoal(string name, string description, int points)  
        : base(name, description, points)  
    {  
        TimesRecorded = 0;  
    }  

    public override void CompleteGoal()  
    {  
        TimesRecorded++;  
        Points += 100; // Award points each time recorded  
        Console.WriteLine($"Recorded progress for '{Name}'. You've earned 100 points.");  
    }  

    public override string GetGoalDetails() =>  
        $"[ ] {Name}: {Description} - Recorded {TimesRecorded} times - Total Points: {GetPoints()}";  
}  

// Checklist goal class that requires a specific number of completions  
[Serializable]  
public class ChecklistGoal : Goal  
{  
    public int RequiredCompletes { get; }  
    private int CurrentCompletes { get; set; }  
    private const int BonusPoints = 500;  
    private const int PointsPerCompletion = 50;  

    public ChecklistGoal(string name, string description, int points, int requiredCompletes)  
        : base(name, description, points)  
    {  
        RequiredCompletes = requiredCompletes;  
        CurrentCompletes = 0;  
    }  

    public override void CompleteGoal()  
    {  
        if (!IsComplete)  
        {  
            CurrentCompletes++;  
            Points += PointsPerCompletion; // Award points for each completion  

            // Check for bonus points upon meeting completion requirements  
            if (CurrentCompletes >= RequiredCompletes)  
            {  
                Points += BonusPoints;  
                IsComplete = true; // Mark as complete  
                Console.WriteLine($"Congratulations! You've completed the checklist goal '{Name}'. You've earned an extra {BonusPoints} bonus points!");  
            }  

            Console.WriteLine($"Recorded completion for '{Name}'. You've earned {PointsPerCompletion} points.");  
        }  
        else  
        {  
            Console.WriteLine($"Goal '{Name}' is already complete!");  
        }  
    }  

    public override string GetGoalDetails() =>  
        IsComplete  
            ? $"[X] {Name}: {Description} - Completed {CurrentCompletes}/{RequiredCompletes} - Total Points: {GetPoints()}"  
            : $"[ ] {Name}: {Description} - Completed {CurrentCompletes}/{RequiredCompletes}";  
}  

// User class representing the person using the program  
[Serializable]  
public class User  
{  
    public string Name { get; }  
    public int Score { get; private set; }  
    public List<Goal> Goals { get; }  

    public User(string name)  
    {  
        Name = name;  
        Score = 0;  
        Goals = new List<Goal>();  
    }  

    public void AddGoal(Goal goal)  
    {  
        Goals.Add(goal);  
        Console.WriteLine($"Goal '{goal.Name}' added successfully.");  
    }  

    public void RecordGoal(Goal goal)  
    {  
        int previousPoints = goal.GetPoints();  
        goal.CompleteGoal();  
        Score += goal.GetPoints() - previousPoints; // Update score based on points earned  
        Console.WriteLine($"Your total score is now: {Score}");  
    }  

    public void Display