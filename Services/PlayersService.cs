using Obviously.Models;

namespace Obviously.Services
{
    public class PlayersService
    {
        private static List<Player> Players { get; set; } = new List<Player>();

        public static Player CreatePlayer(string playerName)
        {
            var player = new Player(playerName);
            Players.Add(player);

            return player;
        }

        public static List<Player> GetPlayers()
        {
            return Players;
        }

        public static Player GetPlayer(string id)
        {
            return Players.FirstOrDefault(p => p.Id == id);
        }
    }
}
