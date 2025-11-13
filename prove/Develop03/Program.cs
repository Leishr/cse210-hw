using System;
using System.Collections.Generic;

/*
CSE 210 - Programming with Classes
Unit 03: Scripture Memorizer
Author: Haydan Leis

--- Description ---
This program helps users memorize scriptures by displaying a passage
and gradually hiding random words each time the user presses Enter.

--- Encapsulation ---
Each class (Word, Reference, Scripture) hides its internal data using private fields
and provides public methods for controlled access and modification.

--- Exceeding Requirements ---
✅ Includes a library of multiple scriptures chosen randomly.
✅ Only hides words that are still visible (smarter hiding logic).
✅ Lets the user start a new scripture without restarting the program.
✅ Clean, object-oriented design using encapsulation principles.
*/

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = new List<Scripture>()
        {
            new Scripture(new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
            new Scripture(new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            new Scripture(new Reference("Mosiah", 2, 17),
                "When ye are in the service of your fellow beings ye are only in the service of your God.")
        };

        Random random = new Random();
        bool keepGoing = true;

        while (keepGoing)
        {
            Console.Clear();
            Scripture scripture = scriptures[random.Next(scriptures.Count)];
            while (!scripture.AllHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit, 'new' for another scripture:");
                string input = Console.ReadLine()?.ToLower();

                if (input == "quit")
                {
                    keepGoing = false;
                    break;
                }
                else if (input == "new")
                {
                    break;
                }
                else
                {
                    scripture.HideRandomWords(3); // hides 3 words per press
                }
            }

            if (scripture.AllHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nAll words are hidden! Press Enter to try another scripture or type 'quit' to exit:");
                string choice = Console.ReadLine()?.ToLower();
                if (choice == "quit")
                    keepGoing = false;
            }
        }

        Console.WriteLine("Goodbye!");
    }
}

public class Reference
{
    private string _book;
    private int _chapter;
    private int _verseStart;
    private int? _verseEnd; // Nullable to handle single or range

    // Constructor for a single verse
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verse;
    }

    // Constructor for a verse range
    public Reference(string book, int chapter, int verseStart, int verseEnd)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verseStart;
        _verseEnd = verseEnd;
    }

    public string GetDisplayText()
    {
        if (_verseEnd.HasValue)
            return $"{_book} {_chapter}:{_verseStart}-{_verseEnd}";
        else
            return $"{_book} {_chapter}:{_verseStart}";
    }
}

public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        foreach (string word in text.Split(' '))
        {
            _words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int count)
    {
        // Only hide words that are not yet hidden
        List<Word> visibleWords = _words.FindAll(w => !w.IsHidden());
        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = _random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public string GetDisplayText()
    {
        string text = "";
        foreach (Word word in _words)
        {
            text += word.GetDisplayText() + " ";
        }
        return $"{_reference.GetDisplayText()} - {text.Trim()}";
    }

    public bool AllHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
                return false;
        }
        return true;
    }
}
