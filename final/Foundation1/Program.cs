using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthSeconds { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();

    public Video(string title, string author, int lengthSeconds)
    {
        Title = title;
        Author = author;
        LengthSeconds = lengthSeconds;
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video v1 = new Video("How to Build a Robot", "TechMaster", 540);
        v1.AddComment(new Comment("Alice", "Great tutorial!"));
        v1.AddComment(new Comment("Bob", "This helped me a lot."));
        v1.AddComment(new Comment("Evan", "Can you make a follow-up video?"));
        videos.Add(v1);

        // Video 2
        Video v2 = new Video("Cooking the Perfect Steak", "ChefMike", 300);
        v2.AddComment(new Comment("Sarah", "Yum!"));
        v2.AddComment(new Comment("Hannah", "Trying this tonight."));
        v2.AddComment(new Comment("Dave", "Your tips always work!"));
        videos.Add(v2);

        // Video 3
        Video v3 = new Video("Travel Tips for Japan", "ExploreWorld", 720);
        v3.AddComment(new Comment("Tony", "Super helpful!"));
        v3.AddComment(new Comment("Mei", "Can't wait to visit Tokyo."));
        v3.AddComment(new Comment("Carlos", "Please cover Kyoto next!"));
        videos.Add(v3);

        // Display everything
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (Comment c in video.Comments)
            {
                Console.WriteLine($"  - {c.CommenterName}: {c.Text}");
            }

            Console.WriteLine(new string('-', 50));
        }
    }
}
