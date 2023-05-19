using MoreLinq;
using Obviously.Models;

namespace Obviously.Services
{
    public class GameService
    {
        private static List<Game> Games { get; set; } = new List<Game>();

        public static List<Game> GetAllGames() => Games;

        public Game? GetGame(string id) => Games.FirstOrDefault(g => g.Id == id);

        public static Game CreateNewGame(Player player)
        {
            var game = new Game(new() { player });
            Console.WriteLine($"Created new empty game: {game}");
            Games.Add(game);

            return game;
        }

        private void OnPlayerJoin(object sender, Player player)
        {
            this.PlayerJoined?.Invoke(this, player);
        }

        public event EventHandler<Player> PlayerJoined;
    }
}
