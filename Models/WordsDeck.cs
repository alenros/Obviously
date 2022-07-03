using Obviously.Data.Services;
using Obviously.Data.Model;

namespace Obviously.Data.Model
{
    public class WordsDeck
    {
        public WordsDeck()
        {
            var w = WordsService.Words.Value;
            var shuffled = Shared.GetShuffledSubArray(w);
            Words = new Stack<Word>(shuffled);
        }

        public Stack<Word> Words { get; set; } = new Stack<Word>();

        public void Shuffle()
        {
            var shuffledWords = Shared.GetShuffledSubArray(Words.ToArray());
            Words = new Stack<Word>(shuffledWords);
        }

        public bool CanTake(int number = 1)
        {
            return Words.Count >= number;
        }

        public Word? TakeWord()
        {
            if (!CanTake())
            {
                return null;
            }

            return Words.Pop();
        }

        public IEnumerable<Word> TakeWords(int numberOfWords)
        {
            if (!CanTake(numberOfWords))
            {
                return Enumerable.Empty<Word>();
            }

            var chosenWords = Enumerable.Range(0, numberOfWords)
                                        .Select(_ => Words.Pop());
            return chosenWords;
        }
    }
}
