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
    protected int points;  
    private bool isComplete;  

    protected Goal(string name, string description, int points)  
    {  
        Name = name;  
        Description = description;  
        this.points = points;  
        isComplete = false;  
    }  

    public virtual void CompleteGoal()  
    {  
        if (!isComplete)  
        {  
            isComplete = true;  
            Console.WriteLine($"Goal '{Name}' completed! You've earned {points} points.");  
        }  
    }  

    public bool IsComplete => isComplete;  
    public abstract string GetGoalDetails();  
    public int GetPoints() => points; // Expose points to derived classes  
    
    // Adding setter for isComplete to help with loading saved goals
    protected void SetComplete(bool value) => isComplete = value;
}  

// Simple goal that can be marked complete  
[Serializable]  
public class SimpleGoal : Goal  
{  
    public SimpleGoal(string name, string description, int points)   
        : base(name, description, points) { }  

    public override void CompleteGoal()
    {
        base.CompleteGoal(); // Call the base method  
    }

    public override string GetGoalDetails()  
    {  
        return IsComplete ? $"[X] {Name}: {Description} - {GetPoints()} points" : $"[ ] {Name}: {Description}";  
    }  
}  

// Eternal goal that can be recorded multiple times without completion  
[Serializable]  
public class EternalGoal : Goal  
{  
    private int timesRecorded;

    public EternalGoal(string name, string description, int points)  
        : base(name, description, points)
    {
        timesRecorded = 0;
    }

    public override void CompleteGoal()  
    {  
        // Award points every time this goal is recorded  
        timesRecorded++;
        int pointsEarned = 100; // Users earn 100 points each time they are recorded
        points += pointsEarned;
        Console.WriteLine($"Recorded progress for '{Name}'. You've earned {pointsEarned} points.");  
    }  

    public override string GetGoalDetails()  
    {  
        return $"[ ] {Name}: {Description} - Recorded {timesRecorded} times - Total Points: {GetPoints()}";  
    }  
}  

// Checklist goal that requires a specific number of completions  
[Serializable]  
public class ChecklistGoal : Goal  
{  
    public int RequiredCompletes { get; }  
    private int currentCompletes;  
    private readonly int bonusPoints;
    private readonly int pointsPerCompletion;

    public ChecklistGoal(string name, string description, int points, int requiredCompletes)   
        : base(name, description, points)  
    {  
        RequiredCompletes = requiredCompletes;  
        currentCompletes = 0;
        bonusPoints = 500;
        pointsPerCompletion = 50;
    }  

    public override void CompleteGoal()  
    {  
        if (!IsComplete)  
        {  
            currentCompletes++;
            int pointsEarned = pointsPerCompletion;
            
            // Check if the required completions have been met  
            if (currentCompletes >= RequiredCompletes)  
            {  
                pointsEarned += bonusPoints; // Award a bonus for completing all tasks
                SetComplete(true);
                Console.WriteLine($"Congratulations! You've completed the checklist goal '{Name}'.");  
                Console.WriteLine($"You've earned {bonusPoints} bonus points!");
            }  

            points += pointsEarned;
            Console.WriteLine($"Recorded completion for '{Name}'. You've earned {pointsPerCompletion} points.");  
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already complete!");
        }
    }  

    public override string GetGoalDetails()  
    {  
        return IsComplete ?   
            $"[X] {Name}: {Description} - Completed {currentCompletes}/{RequiredCompletes} - Total Points: {GetPoints()}" :  
            $"[ ] {Name}: {Description} - Completed {currentCompletes}/{RequiredCompletes}";  
    }
    
    public int GetCurrentCompletes() => currentCompletes;
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
        Console.WriteLine($"Goal '{goal.Name}' added successfully.");  
    }  

    public void RecordGoal(Goal goal)  
    {  
        int previousPoints = goal.GetPoints();
        goal.CompleteGoal();  
        int pointsEarned = goal.GetPoints() - previousPoints;
        Score += pointsEarned;
        Console.WriteLine($"Your total score is now: {Score}");
    }  

    public void DisplayGoals()  
    {  
        Console.WriteLine($"{Name}'s Goals:");  
        if (Goals.Count == 0)
        {
            Console.WriteLine("No goals have been created yet.");
            return;
        }
        
        for (int i = 0; i < Goals.Count; i++)  
        {  
            Console.WriteLine($"{i+1}. {Goals[i].GetGoalDetails()}");  
        }  
        Console.WriteLine($"Total Score: {Score}");  
    }  

    // Save user data to a file  
    public void SaveProgress(string fileName = "goals.dat")  
    {  
        try
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))  
            {  
                BinaryFormatter formatter = new BinaryFormatter();  
                formatter.Serialize(stream, this);  
            }  
            Console.WriteLine($"Progress saved successfully to {fileName}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving progress: {ex.Message}");
        }
    }
    
    // Load user data from a file
    public static User LoadProgress(string fileName = "goals.dat")
    {
        try
        {
            if (File.Exists(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    User user = (User)formatter.Deserialize(stream);
                    Console.WriteLine($"Progress loaded successfully for {user.Name}.");
                    return user;
                }
            }
            else
            {
                Console.WriteLine("No saved progress found.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading progress: {ex.Message}");
            return null;
        }
    }
}

// Main program class
public class Program
{
    public static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Blue; // Set text color to blue
        
        User currentUser = null;
        
        // Try to load existing user
        currentUser = User.LoadProgress();
        
        // If no user exists, create a new one
        if (currentUser == null)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            currentUser = new User(name);
        }
        
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n===== Eternal Quest Goal Tracker =====");
            Console.WriteLine($"Current Score: {currentUser.Score} points");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Goal Completion");
            Console.WriteLine("4. Save Progress");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    CreateNewGoal(currentUser);
                    break;
                case "2":
                    currentUser.DisplayGoals();
                    break;
                case "3":
                    RecordGoalCompletion(currentUser);
                    break;
                case "4":
                    currentUser.SaveProgress();
                    break;
                case "5":
                    running = false;
                    currentUser.SaveProgress(); // Auto-save on exit
                    Console.WriteLine("Thank you for using Eternal Quest! Goodbye.");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
    
    private static void CreateNewGoal(User user)
    {
        Console.WriteLine("\n=== Goal Types ===");
        Console.WriteLine("1. Simple Goal (Complete once)");
        Console.WriteLine("2. Eternal Goal (Can be recorded multiple times)");
        Console.WriteLine("3. Checklist Goal (Complete a specific number of times)");
        Console.Write("Select a goal type: ");
        
        string choice = Console.ReadLine();
        
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        
        Console.Write("Enter base points for this goal: ");
        if (!int.TryParse(Console.ReadLine(), out int points))
        {
            Console.WriteLine("Invalid points value. Using default of 100 points.");
            points = 100;
        }
        
        Goal newGoal = null;
        
        switch (choice)
        {
            case "1":
                newGoal = new SimpleGoal(name, description, points);
                break;
            case "2":
                newGoal = new EternalGoal(name, description, points);
                break;
            case "3":
                Console.Write("How many times does this goal need to be completed? ");
                if (!int.TryParse(Console.ReadLine(), out int requiredCompletes) || requiredCompletes <= 0)
                {
                    Console.WriteLine("Invalid number. Using default of 3 completions.");
                    requiredCompletes = 3;
                }
                newGoal = new ChecklistGoal(name, description, points, requiredCompletes);
                break;
            default:
                Console.WriteLine("Invalid goal type. Goal not created.");
                return;
        }
        
        user.AddGoal(newGoal);
    }
    
    private static void RecordGoalCompletion(User user)
    {
        if (user.Goals.Count == 0)
        {
            Console.WriteLine("You don't have any goals to record progress for.");
            return;
        }
        
        Console.WriteLine("\n=== Your Goals ===");
        for (int i = 0; i < user.Goals.Count; i++)
        {
            Goal goal = user.Goals[i];
            string status = (goal is EternalGoal) ? "Eternal" : 
                           (goal.IsComplete ? "Completed" : "Incomplete");
            Console.WriteLine($"{i+1}. {goal.Name} - {status}");
        }
        
        Console.Write("Enter the number of the goal you completed: ");
        if (int.TryParse(Console.ReadLine(), out int goalIndex) && goalIndex > 0 && goalIndex <= user.Goals.Count)
        {
            Goal selectedGoal = user.Goals[goalIndex - 1];
            
            if (selectedGoal.IsComplete && !(selectedGoal is EternalGoal))
            {
                Console.WriteLine("This goal is already complete!");
            }
            else
            {
                user.RecordGoal(selectedGoal);
            }
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }
}