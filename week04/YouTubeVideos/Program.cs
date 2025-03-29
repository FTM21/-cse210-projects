using System;
using System.Collections.Generic;

// Comment class to track the commenter's name and the text of the comment
class Comment
{
    // Properties for the Comment class
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    // Constructor to initialize a Comment object
    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

// Video class to track information about a YouTube video
class Video
{
    // Properties for the Video class
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> _comments;

    // Constructor to initialize a Video object
    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        _comments = new List<Comment>();
    }

    // Method to add a comment to the video
    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    // Method to get the number of comments
    public int GetCommentCount()
    {
        return _comments.Count;
    }

    // Method to get all comments
    public List<Comment> GetComments()
    {
        return _comments;
    }
}

// Main program class
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("YouTube Videos Tracking Program");
        Console.WriteLine("==============================");

        // Create a list to store the videos
        List<Video> videos = new List<Video>();

        // Create the first video and add comments
        Video video1 = new Video("C# Programming Tutorial for Beginners", "YeshuaCoder", 1200);
        video1.AddComment(new Comment("Rita Dell300", "Great tutorial! Could you please make more videos?."));
        video1.AddComment(new Comment("Megan Schneider", "You have helped me to finish my project! Could you make a follow-up video on OOP concepts?"));
        video1.AddComment(new Comment("Zuleika Moroni6", "The explanation at 4:06-06:11 was very clear. Thanks!"));
        videos.Add(video1);

        // Create the second video and add comments
        Video video2 = new Video("10 places to visit in Europe on Summer", "travellerExtraordinaire", 480);
        video2.AddComment(new Comment("Globalwalker4", "I'm thinking about to visit Algarve, Portugal, thank you!"));
        video2.AddComment(new Comment("Hally Manner", "I have been in Marbella, Spain is a great place to go visit with friends!!."));
        video2.AddComment(new Comment("230Stephanie", "I have booked my flight to Algarve after this video, thank you!"));
        video2.AddComment(new Comment("Onkatbelzer23", "Could you please share more options of restaurants for these places. Thank you!"));
        videos.Add(video2);

        // Create the third video and add comments
        Video video3 = new Video("Tutorial: Stepping to moonwalk dance", "22Lover", 720);
        video3.AddComment(new Comment("Dizzy234", "Wow, great moves!!!"));
        video3.AddComment(new Comment("1Russel Maxinho", "These moves on 03:22-33 and 07:11-32 are really hard to do! Great hacks!."));
        video3.AddComment(new Comment("Waka00waka", "Amazing tutorial, keep this smile on your face."));
        videos.Add(video3);

        // Display information for each video
        foreach (Video video in videos)
        {
            Console.WriteLine("\n-----------------------------------");
            
            // Format the length as minutes:seconds
            int minutes = video.LengthInSeconds / 60;
            int seconds = video.LengthInSeconds % 60;
            
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {minutes}:{seconds:D2}");
            Console.WriteLine($"Number of comments: {video.GetCommentCount()}");
            
            Console.WriteLine("\nComments:");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.CommentText}");
            }
        }
        
        Console.WriteLine("\n-----------------------------------");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}