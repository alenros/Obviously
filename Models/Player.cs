namespace Obviously.Data.Model
{
    public class Player
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<Word> Words { get; set; } = new List<Word>();

        public Word? ChosenWord { get; set; } = null;

        public Player(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }

        public override string ToString()
        {
            return $"Player Name: {Name} (Id: {Id})";
        }
    }
}
