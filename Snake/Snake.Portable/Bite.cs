namespace Koopakiller.Apps.Snake.Portable
{
    public class Bite : CauseOfDeath
    {
        public Bite(Snake bitingSnake)
        {
            this.BitingSnake = bitingSnake;
        }

        public Snake BitingSnake { get; }
    }
}