namespace Obviously.Data.Model
{
    public class Word
    {
        // TODO Words will probably have more state, like if they were guessed (and are now inactive), who guessed them etc
        public string Text { get; set; }

        public Word(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
