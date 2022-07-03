using MoreLinq;
using Obviously.Shared;

namespace Obviously.Data.Model
{
    public class Game
    {
        public string Id { get; set; } = new Random().NextInt64(1000, 9999).ToString();

        public List<Word> SharedWords { get; set; } = new List<Word>();

        public List<Player> Players { get; set; } = new List<Player>();

        public WordsDeck WordsDeck { get; set; } = new WordsDeck();

        public GameState.State State { get; set; } = GameState.State.Started;

        // The time the game has started.
        // TODO use StartTime to get rid of old games.
        public DateTime StartTime { get; set; } = DateTime.Now;

        private Game()
        {

        }

        public Game(List<Player> players)
        {
            var numberOfSharedWords = players.Count + 1;
            var numberOfPlayerWords = players.Count * GameConstants.WordsPerPlayer;

            if (!WordsDeck.CanTake(numberOfSharedWords + numberOfPlayerWords))
            {
                Console.Write("Failed creating game - could not take enough words.");
                return;
            }

            SharedWords = WordsDeck.TakeWords(numberOfSharedWords).ToList();

            var playerWords = WordsDeck.TakeWords(numberOfPlayerWords);

            var wordsBatchedByPlayers = playerWords.Batch(GameConstants.WordsPerPlayer)
                                                   .Select(v => v.ToList());

            players.Zip(wordsBatchedByPlayers)
                   .ForEach(p => p.First.Words = p.Second);

            Players = players;
        }

        public bool AddPlayer(Player player)
        {
            // Each player uses GameConstants.WordsPerPlayer + 1 shared word
            if (!WordsDeck.CanTake(GameConstants.WordsPerPlayer + 1))
            {
                return false;
            }

            var sharedWord = WordsDeck.TakeWord();
            SharedWords.Add(sharedWord!);

            var playerWords = WordsDeck.TakeWords(2)
                                       .ToList();

            player.Words = playerWords;
            Players.Add(player);
            return true;
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("Shared Words: ");
            foreach (var word in SharedWords)
            {
                sb.AppendLine(word.ToString());
            }

            sb.AppendLine("Players and their words: ");
            foreach (var player in Players)
            {
                sb.Append(player + " ");
                player.Words.ForEach(word => sb.Append(word + " "));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public bool NewRound()
        {
            var numberOfSharedWords = Players.Count + 1;
            var numberOfPlayerWords = Players.Count * GameConstants.WordsPerPlayer;
            this.Players.ForEach(p => p.Words.Clear());
            this.SharedWords.Clear();

            // If there are words in the deck - get them
            if (!WordsDeck.CanTake(numberOfSharedWords + numberOfPlayerWords))
            {
                // Otherwise shuffle a new words deck
                Console.Write("Shuffled deck.");
                this.WordsDeck = new WordsDeck();
                if (!WordsDeck.CanTake(numberOfSharedWords + numberOfPlayerWords))
                {
                    Console.Write("Failed crating new  round - could not take enough words.");
                    return false;
                }
            }

            SharedWords = WordsDeck.TakeWords(numberOfSharedWords).ToList();

            var playerWords = WordsDeck.TakeWords(numberOfPlayerWords);

            var wordsBatchedByPlayers = playerWords.Batch(GameConstants.WordsPerPlayer)
                                                   .Select(v => v.ToList());

            Players.Zip(wordsBatchedByPlayers)
                   .ForEach(p => p.First.Words = p.Second);

            return true;
        }
    }
}
