using System;  
using System.Collections.Generic;  
using System.IO;  

class Program  
{  
    static void Main(string[] args)  
    {  
        List<string> entries = new List<string>();  
        string[] prompts = {  
            "Who was the most interesting person I interacted with today?", 
            "What is the scripture you have read today?",
            "What was the best part of my day?",  
            "How did I see the hand of the Lord in my life today?",  
            "What was the strongest emotion I felt today?",  
            "If I had one thing I could do over today, what would it be?",  
            "What is something new I learned today?"  
        };  

        string option;  

        do  
        {  
            Console.WriteLine("Welcome to the Journal App!");  
            Console.WriteLine("1. Write");  
            Console.WriteLine("2. Display");  
            Console.WriteLine("3. Save");  
            Console.WriteLine("4. Load");  
            Console.WriteLine("5. Exit");  
            Console.Write("Please select an option: ");  
            option = Console.ReadLine();  

            if (option == "1")  
            {  
                Random random = new Random();  
                int index = random.Next(prompts.Length);  
                string prompt = prompts[index];  
                Console.WriteLine($"Prompt: {prompt}");  
                Console.Write("Your response: ");  
                string response = Console.ReadLine();  
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  
                string entry = $"{date} | Prompt: {prompt} | Response: {response}";  
                entries.Add(entry);  
                Console.WriteLine("Entry added!");  
            }  
            else if (option == "2")  
            {  
                Console.WriteLine("Your Journal Entries:");  
                if (entries.Count == 0)  
                {  
                    Console.WriteLine("No entries found.");  
                }  
                else  
                {  
                    foreach (string entry in entries)  
                    {  
                        Console.WriteLine(entry);  
                    }  
                }  
            }  
            else if (option == "3")  
            {  
                Console.Write("Enter filename to save journal: ");  
                string filename = Console.ReadLine();  
                File.WriteAllLines(filename, entries);  
                Console.WriteLine("Journal saved successfully.");  
            }  
            else if (option == "4")  
            {  
                Console.Write("Enter filename to load journal: ");  
                string filename = Console.ReadLine();  
                if (File.Exists(filename))  
                {  
                    entries.Clear();  
                    entries.AddRange(File.ReadAllLines(filename));  
                    Console.WriteLine("Journal loaded successfully.");  
                }  
                else  
                {  
                    Console.WriteLine("File not found.");  
                }  
            }  
            else if (option != "5")  
            {  
                Console.WriteLine("Invalid option, please try again.");  
            }  

        } while (option != "5");  

        Console.WriteLine("Exiting the program.");  
    }  
}  

// I have add: "What is the scripture you have read today?" - The code exceeds requirements by
 //integrating a spiritual reflection element with the scripture reading prompt. This addition enriches
 // the journal application by encouraging daily scripture study alongside personal reflection, creating
   //a more holistic journaling experience.