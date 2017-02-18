namespace Koopakiller.Apps.Snake.Portable
{
    public class Bite : CauseOfDeath
    {
        public Bite(Snake bitingSnake)
        {
            BitingSnake = bitingSnake;
        }

        public Snake BitingSnake { get; }
    }
}