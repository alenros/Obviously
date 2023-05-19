using System;
using Obviously.Models;

namespace Obviously.Services
{
    public class WordsService
    {
        public static readonly Lazy<Word[]> Words = new(() => File.ReadAllLines("Data\\words-en.txt")
                                                                  .Select(text => new Word(text))
                                                                  .ToArray());
    }
}
