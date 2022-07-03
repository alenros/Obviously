using System;
using Obviously.Data.Model;

namespace Obviously.Data.Services
{
    public class WordsService
    {
        public static readonly Lazy<Word[]> Words = new(() => File.ReadAllLines("Data\\words-en.txt")
                                                                  .Select(text => new Word(text))
                                                                  .ToArray());
    }
}
