using System;

namespace Koopakiller.Apps.Snake.Portable
{
    public class Bite : CauseOfDeath
    {
        public Bite(Snake bitingSnake)
        {
            this.BitingSnake = bitingSnake;
        }

        public Snake BitingSnake { get; }

        public override String ToString()
        {
            return $"The snake was biten by snake {this.BitingSnake.Id}";
        }
    }
}