using System;
using System.Collections.Generic;

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
            _words.Add(new Word(word));
    }

    public void HideRandomWords(int count)
    {
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
            text += word.GetDisplayText() + " ";
        return $"{_reference.GetDisplayText()} - {text.Trim()}";
    }

    public bool AllHidden()
    {
        foreach (Word word in _words)
            if (!word.IsHidden()) return false;
        return true;
    }
}
hag
