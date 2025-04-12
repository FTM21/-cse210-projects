using System;  
using System.Collections.Generic;  
using System.Threading;  

class Program  
{  
    static void Main(string[] args)  
    {  
        // Testing Assignment class  
        Assignment assignment = new Assignment("Samuel Bennett", "Multiplication");  
        Console.WriteLine(assignment.GetSummary());  

        // Testing MathAssignment class  
        MathAssignment mathAssignment = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");  
        Console.WriteLine(mathAssignment.GetSummary());  
        Console.WriteLine(mathAssignment.GetHomeworkList());  

        // Testing WritingAssignment class  
        WritingAssignment writingAssignment = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");  
        Console.WriteLine(writingAssignment.GetSummary());  
        Console.WriteLine(writingAssignment.GetWritingInformation());  
    }  
}  

// Base Assignment class  
public class Assignment  
{  
    protected string _studentName;  
    protected string _topic;  

    public Assignment(string studentName, string topic)  
    {  
        _studentName = studentName;  
        _topic = topic;  
    }  

    public virtual string GetSummary()  
    {  
        return $"{_studentName} - {_topic}";  
    }  
}  

// MathAssignment class inheriting from Assignment  
public class MathAssignment : Assignment  
{  
    private string _section;  
    private string _problems;  

    public MathAssignment(string studentName, string topic, string section, string problems)  
        : base(studentName, topic)  
    {  
        _section = section;  
        _problems = problems;  
    }  

    public string GetHomeworkList()  
    {  
        return $"Section {_section} Problems {_problems}";  
    }  
}  

// WritingAssignment class inheriting from Assignment  
public class WritingAssignment : Assignment  
{  
    private string _title;  

    public WritingAssignment(string studentName, string topic, string title)  
        : base(studentName, topic)  
    {  
        _title = title;  
    }  

    public string GetWritingInformation()  
    {  
        return $"{_title} by {_studentName}";  
    }  
}  