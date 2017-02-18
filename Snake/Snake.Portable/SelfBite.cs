using System;

namespace Koopakiller.Apps.Snake.Portable
{
    public class SelfBite : CauseOfDeath
    {
        public override String ToString()
        {
            return "The snake has biten it self.";
        }
    }
}