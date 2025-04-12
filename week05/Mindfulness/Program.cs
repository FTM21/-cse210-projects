using System;  
using System.Collections.Generic;  
using System.Threading;  

class Program  
{  
    static void Main(string[] args)  
    {  
        Console.Clear();
        SetConsoleColor(ConsoleColor.Cyan, ConsoleColor.Black);
        Console.WriteLine("Hello World! This is the Mindfulness Project.");
        ResetConsoleColor();
        Thread.Sleep(1500);
        
        while (true)  
        {  
            Console.Clear();  
            DisplayHeader("Welcome to the Mindfulness Program!");  
            Console.WriteLine("Please select an activity:");  
            
            DisplayMenuItem("1", "Breathing Activity");  
            DisplayMenuItem("2", "Reflection Activity");  
            DisplayMenuItem("3", "Listing Activity");  
            DisplayMenuItem("0", "Exit");  
            
            SetConsoleColor(ConsoleColor.Yellow, ConsoleColor.Black);
            Console.Write("Enter your choice: ");  
            ResetConsoleColor();

            int choice;
            // Adding error handling for input
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                DisplayError("Invalid input. Please enter a number.");
                Thread.Sleep(2000);
                continue;
            }

            Activity activity = null;  

            switch (choice)  
            {  
                case 1:  
                    activity = new BreathingActivity();  
                    break;  
                case 2:  
                    activity = new ReflectionActivity();  
                    break;  
                case 3:  
                    activity = new ListingActivity();  
                    break;  
                case 0:  
                    SetConsoleColor(ConsoleColor.Cyan, ConsoleColor.Black);
                    Console.WriteLine("Thank you for using the Mindfulness Program. Goodbye!");
                    ResetConsoleColor();
                    Thread.Sleep(2000);
                    return;  
                default:  
                    DisplayError("Invalid choice. Please try again.");
                    Thread.Sleep(2000);
                    continue;  
            }  

            StartActivity(activity);  
        }  
    }  

    static void StartActivity(Activity activity)  
    {  
        if (activity == null) return;  

        activity.Start();  
        activity.Execute();  
        activity.End();  
    }
    
    static void DisplayHeader(string message)
    {
        SetConsoleColor(ConsoleColor.Magenta, ConsoleColor.Black);
        Console.WriteLine(new string('=', message.Length + 4));
        Console.WriteLine($"  {message}  ");
        Console.WriteLine(new string('=', message.Length + 4));
        ResetConsoleColor();
        Console.WriteLine();
    }
    
    static void DisplayMenuItem(string key, string description)
    {
        SetConsoleColor(ConsoleColor.DarkYellow, ConsoleColor.Black);
        Console.Write($"{key}. ");
        ResetConsoleColor();
        Console.WriteLine(description);
    }
    
    static void DisplayError(string message)
    {
        SetConsoleColor(ConsoleColor.Red, ConsoleColor.Black);
        Console.WriteLine(message);
        ResetConsoleColor();
    }
    
    static void SetConsoleColor(ConsoleColor foreground, ConsoleColor background)
    {
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
    }
    
    static void ResetConsoleColor()
    {
        Console.ResetColor();
    }
}  

public abstract class Activity  
{  
    protected int duration;
    protected string activityName;
    protected string description;
    
    public Activity(string name, string desc)
    {
        activityName = name;
        description = desc;
    }

    public virtual void Start()  
    {  
        Console.Clear();
        DisplayActivityHeader();
        Console.WriteLine(description);
        Console.WriteLine();
        
        SetConsoleColor(ConsoleColor.Yellow, ConsoleColor.Black);
        Console.Write("Enter duration in seconds: ");  
        ResetConsoleColor();
        
        // Adding error handling for duration input
        if (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
        {
            duration = 30; // Default value
            SetConsoleColor(ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine($"Invalid duration. Setting to default: {duration} seconds.");
            ResetConsoleColor();
        }
        
        SetConsoleColor(ConsoleColor.Green, ConsoleColor.Black);
        Console.WriteLine("\nPrepare to begin...");
        ResetConsoleColor();
        AnimateSpinner(3);  
    }  

    public abstract void Execute();  

    public virtual void End()  
    {  
        Console.WriteLine();
        SetConsoleColor(ConsoleColor.Green, ConsoleColor.Black);
        Console.WriteLine("Well done!");
        ResetConsoleColor();  
        AnimateSpinner(2);
        
        SetConsoleColor(ConsoleColor.Cyan, ConsoleColor.Black);
        Console.WriteLine($"You have completed the {activityName} ({duration} seconds).");
        ResetConsoleColor();  
        AnimateSpinner(3);  
    }  

    protected void AnimatePause(int seconds)  
    {  
        for (int i = 0; i < seconds; i++)  
        {  
            SetConsoleColor(ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.Write(".");
            ResetConsoleColor();
            Thread.Sleep(1000);  
        }  
        Console.WriteLine();  
    }
    
    protected void AnimateSpinner(int seconds)
    {
        string[] spinnerFrames = { "|", "/", "-", "\\" };
        
        for (int i = 0; i < seconds * 2; i++)
        {
            Console.Write("\r");
            SetConsoleColor(ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.Write($"[{spinnerFrames[i % 4]}]");
            ResetConsoleColor();
            Thread.Sleep(500);
        }
        Console.WriteLine();
    }
    
    protected void DisplayCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write("\r");
            SetConsoleColor(ConsoleColor.Yellow, ConsoleColor.Black);
            Console.Write($"Starting in: {i} ");
            ResetConsoleColor();
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
    
    protected void DisplayActivityHeader()
    {
        SetConsoleColor(ConsoleColor.Cyan, ConsoleColor.Black);
        Console.WriteLine(new string('*', activityName.Length + 14));
        Console.WriteLine($"*** {activityName} ***");
        Console.WriteLine(new string('*', activityName.Length + 14));
        ResetConsoleColor();
        Console.WriteLine();
    }
    
    protected static void SetConsoleColor(ConsoleColor foreground, ConsoleColor background)
    {
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
    }
    
    protected static void ResetConsoleColor()
    {
        Console.ResetColor();
    }
}  

public class BreathingActivity : Activity  
{  
    public BreathingActivity() 
        : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }
    
    public override void Execute()  
    {  
        Console.Clear();
        DisplayActivityHeader();
        SetConsoleColor(ConsoleColor.DarkCyan, ConsoleColor.Black);
        Console.WriteLine("Get comfortable and focus on your breathing...");
        ResetConsoleColor();
        DisplayCountdown(3);

        int remainingTime = duration;
        
        while (remainingTime > 0)
        {
            // Breathe in
            SetConsoleColor(ConsoleColor.Blue, ConsoleColor.Black);
            Console.Write("\rBreathe in...  ");
            ResetConsoleColor();
            for (int i = 4; i > 0 && remainingTime > 0; i--)
            {
                Console.Write("\r");
                SetConsoleColor(ConsoleColor.Blue, ConsoleColor.Black);
                Console.Write($"Breathe in... {i} ");
                ResetConsoleColor();
                Thread.Sleep(1000);
                remainingTime--;
            }
            
            if (remainingTime <= 0) break;
            
            // Breathe out
            SetConsoleColor(ConsoleColor.Green, ConsoleColor.Black);
            Console.Write("\rBreathe out... ");
            ResetConsoleColor();
            for (int i = 6; i > 0 && remainingTime > 0; i--)
            {
                Console.Write("\r");
                SetConsoleColor(ConsoleColor.Green, ConsoleColor.Black);
                Console.Write($"Breathe out... {i}");
                ResetConsoleColor();
                Thread.Sleep(1000);
                remainingTime--;
            }
        }
    }  
}  

public class ReflectionActivity : Activity  
{  
    private List<string> prompts = new List<string>  
    {  
        "Think of a time when you stood up for someone else.",  
        "Think of a time when you did something really difficult.",  
        "Think of a time when you helped someone in need.",  
        "Think of a time when you did something truly selfless."  
    };  

    private List<string> questions = new List<string>  
    {  
        "Why was this experience meaningful to you?",  
        "Have you ever done anything like this before?",  
        "How did you get started?",  
        "How did you feel when it was complete?",  
        "What made this time different than other times when you were not as successful?",  
        "What is your favorite thing about this experience?",  
        "What could you learn from this experience that applies to other situations?",  
        "What did you learn about yourself through this experience?",  
        "How can you keep this experience in mind in the future?"  
    };  
    
    public ReflectionActivity() 
        : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    public override void Execute()  
    {  
        Console.Clear();
        DisplayActivityHeader();
        
        SetConsoleColor(ConsoleColor.Yellow, ConsoleColor.Black);
        Console.WriteLine("Consider the following prompt:\n");
        ResetConsoleColor();
        
        SetConsoleColor(ConsoleColor.Green, ConsoleColor.Black);
        string prompt = GetRandomPrompt();
        Console.WriteLine($"--- {prompt} ---");
        ResetConsoleColor();
        
        Console.WriteLine("\nWhen you have something in mind, press enter to continue.");
        Console.ReadLine();
        
        SetConsoleColor(ConsoleColor.Cyan, ConsoleColor.Black);
        Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
        ResetConsoleColor();
        Console.Write("You may begin in: ");
        DisplayCountdown(3);
        Console.Clear();

        int timeElapsed = 0;
        Random random = new Random();
        List<int> usedQuestionIndices = new List<int>();
        
        while (timeElapsed < duration)  
        {  
            // Get a question that hasn't been used yet, if possible
            int index;
            if (usedQuestionIndices.Count < questions.Count)
            {
                do
                {
                    index = random.Next(questions.Count);
                } while (usedQuestionIndices.Contains(index));
                usedQuestionIndices.Add(index);
            }
            else
            {
                index = random.Next(questions.Count);
            }
            
            SetConsoleColor(ConsoleColor.Yellow, ConsoleColor.Black);
            Console.Write($"> {questions[index]} ");
            ResetConsoleColor();
            AnimateSpinner(Math.Min(10, duration - timeElapsed) / 2);
            timeElapsed += 10;
        }  
    }  

    private string GetRandomPrompt()  
    {  
        Random random = new Random();  
        return prompts[random.Next(prompts.Count)];  
    }  
}  

public class ListingActivity : Activity  
{  
    private List<string> prompts = new List<string>  
    {  
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };  
    
    public ListingActivity() 
        : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void Execute()  
    {  
        Console.Clear();
        DisplayActivityHeader();
        
        string prompt = GetRandomPrompt();
        SetConsoleColor(ConsoleColor.Yellow, ConsoleColor.Black);
        Console.WriteLine("Consider the following prompt:");
        ResetConsoleColor();
        
        SetConsoleColor(ConsoleColor.Green, ConsoleColor.Black);
        Console.WriteLine($"--- {prompt} ---");
        ResetConsoleColor();
        
        Console.WriteLine("\nYou will have a few seconds to think before you need to start listing...");
        Console.Write("Get ready to begin in: ");
        DisplayCountdown(5);
        
        SetConsoleColor(ConsoleColor.Cyan, ConsoleColor.Black);
        Console.WriteLine("Go! List as many items as you can:");
        ResetConsoleColor();
        
        DateTime startTime = DateTime.Now;
        List<string> items = new List<string>();
        
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            SetConsoleColor(ConsoleColor.Yellow, ConsoleColor.Black);
            Console.Write("> ");
            ResetConsoleColor();
            
            // Start reading with a timeout
            string item = ReadLineWithTimeout(startTime);
            
            if (item == null) // Timeout reached
                break;
                
            if (string.IsNullOrWhiteSpace(item))
                continue;
                
            items.Add(item);
            
            // Show time remaining every few items
            int timeLeft = duration - (int)(DateTime.Now - startTime).TotalSeconds;
            if (items.Count % 3 == 0 && timeLeft > 0)
            {
                SetConsoleColor(ConsoleColor.DarkCyan, ConsoleColor.Black);
                Console.WriteLine($"({timeLeft} seconds remaining)");
                ResetConsoleColor();
            }
            
            // Check if time is up after each entry
            if ((DateTime.Now - startTime).TotalSeconds >= duration)
                break;
        }
        
        Console.WriteLine();
        SetConsoleColor(ConsoleColor.Green, ConsoleColor.Black);
        Console.WriteLine($"You listed {items.Count} items!");
        ResetConsoleColor();
    }  

    private string GetRandomPrompt()  
    {  
        Random random = new Random();  
        return prompts[random.Next(prompts.Count)];  
    }
    
    private string ReadLineWithTimeout(DateTime startTime)
    {
        string result = "";
        int cursorLeft = Console.CursorLeft;
        int cursorTop = Console.CursorTop;
        bool timeExpired = false;
        
        Task<string> readTask = Task.Run(() => Console.ReadLine());
        
        while (!readTask.IsCompleted)
        {
            if ((DateTime.Now - startTime).TotalSeconds >= duration)
            {
                timeExpired = true;
                break;
            }
            Thread.Sleep(100);
        }
        
        if (timeExpired)
        {
            Console.WriteLine("\nTime's up!");
            return null;
        }
        
        return readTask.Result;
    }
}