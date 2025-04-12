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
    public int Points { get; protected set; }  
    public bool IsComplete { get; protected set; }  

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

    public abstract string GetGoalDetails();  
}  

// Simple goal that can be marked complete  
[Serializable]  
public class SimpleGoal : Goal  
{  
    public SimpleGoal(string name, string description, int points)   
        : base(name, description, points) { }  

    public override void CompleteGoal()  
    {  
        base.CompleteGoal(); // Call the base method to mark complete  
    }  

    public override string GetGoalDetails()  
    {  
        return IsComplete ? $"[X] {Name}: {Description} - {Points} points" : $"[ ] {Name}: {Description}";  
    }  
}  

// Eternal goal that can be recorded multiple times without completion  
[Serializable]  
public class EternalGoal : Goal  
{  
    public EternalGoal(string name, string description, int points)  
        : base(name, description, points) { }  

    public override void CompleteGoal()  
    {  
        // Award points every time this goal is recorded  
        Points += 100; // Players earn 100 points each time they are recorded  
        Console.WriteLine($"Recorded progress for '{Name}'. You've earned 100 extra points.");  
    }  

    public override string GetGoalDetails()  
    {  
        return $"{Name}: {Description} - Total Points: {Points}";  
    }  
}  

// Checklist goal that requires a specific number of completions  
[Serializable]  
public class ChecklistGoal : Goal  
{  
    public int RequiredCompletes { get; }  
    public int CurrentCompletes { get; private set; }  
    private const int BonusPoints = 500;  

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
            Points += 50; // Award points for each completion  

            // Check if the required completions have been met  
            if (CurrentCompletes == RequiredCompletes)  
            {  
                Points += BonusPoints; // Award a bonus for completing all tasks  
                IsComplete = true;  
            }  

            Console.WriteLine($"Recorded completion for '{Name}'. You've earned 50 points.");  
        }  
    }  

    public override string GetGoalDetails()  
    {  
        return IsComplete ?   
            $"[X] {Name}: {Description} - Completed {CurrentCompletes}/{RequiredCompletes} - Total Points: {Points}" :  
            $"[ ] {Name}: {Description} - Completed {CurrentCompletes}/{RequiredCompletes}";  
    }  
}  

// User class that represents the person using the program  
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
    }  

    public void RecordGoal(Goal goal)  
    {  
        goal.CompleteGoal();  
        Score += goal.Points;  
    }  

    public void DisplayGoals()  
    {  
        Console.WriteLine($"{Name}'s Goals:");  
        foreach (var goal in Goals)  
        {  
            Console.WriteLine(goal.GetGoalDetails());  
        }  
        Console.WriteLine($"Total Score: {Score}");  
    }  

    // Save user data to a file  
    public void SaveProgress()  
    {  
        using (FileStream stream = new FileStream($"{Name}_goals.dat", FileMode.Create))  
        {  
            IFormatter formatter = new BinaryFormatter();  
            formatter.Serialize(stream, this);  
        }  
        Console.WriteLine("Progress saved successfully.");  
    }  

    // Load user data from a file  
    public static User LoadProgress(string name)  
    {  
        using (FileStream stream = new FileStream($"{name}_goals.dat", FileMode.Open))  
        {  
            IFormatter formatter = new BinaryFormatter();  
            return (User)formatter.Deserialize(stream);  
        }  
    }  
}  

// Program entry point  
class Program  
{  
    static void Main(string[] args)  
    {  
        Console.WriteLine("Welcome to the Eternal Quest Program!");  

        User user = new User("Alice"); // Create a user instance  
        bool running = true;  

        while (running)  
        {  
            Console.WriteLine("\nOptions:");  
            Console.WriteLine("1. Add a Simple Goal");  
            Console.WriteLine("2. Add an Eternal Goal");  
            Console.WriteLine("3. Add a Checklist Goal");  
            Console.WriteLine("4. Record a Goal Completion");  
            Console.WriteLine("5. Display All Goals");  
            Console.WriteLine("6. Save Progress");  
            Console.WriteLine("7. Load Progress");  
            Console.WriteLine("8. Exit");  
            Console.Write("Choose an option: ");  

            string choice = Console.ReadLine();  

            switch (choice)  
            {  
                case "1":  
                    user.AddGoal(new SimpleGoal("Run a Marathon", "Finish a marathon in under 4 hours", 1000));  
                    break;  
                case "2":  
                    user.AddGoal(new EternalGoal("Read Scriptures Daily", "Read scriptures every day", 0));  
                    break;  
                case "3":  
                    user.AddGoal(new ChecklistGoal("Attend Temple", "Attend the temple 10 times", 0, 10));  
                    break;  
                case "4":  
                    Console.Write("Enter the goal's name to record completion: ");  
                    string goalName = Console.ReadLine();  
                    Goal goalToRecord = user.Goals.Find(g => g.Name.Equals(goalName, StringComparison.OrdinalIgnoreCase));  
                    if (goalToRecord != null)  
                    {  
                        user.RecordGoal(goalToRecord);  
                    }  
                    else  
                    {  
                        Console.WriteLine("Goal not found!");  
                    }  
                    break;  
                case "5":  
                    user.DisplayGoals();  
                    break;  
                case "6":  
                    user.SaveProgress();  
                    break;  
                case "7":  
                    Console.WriteLine("Enter your name to load progress: ");  
                    string name = Console.ReadLine();  
                    user = User.LoadProgress(name);  
                    break;  
                case "8":  
                    running = false;  
                    Console.WriteLine("Exiting the program.");  
                    break;  
                default:  
                    Console.WriteLine("Invalid option, try again.");  
                    break;  
            }  
        }  
    }  
}  