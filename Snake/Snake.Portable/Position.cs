namespace Koopakiller.Apps.Snake.Portable
{
    using System;

    public class Position
    {
        public Position(Int32 x, Int32 y)
        {
            this.X = x;
            this.Y = y;
        }

        public Int32 X { get; }

        public Int32 Y { get; }

        public override Int32 GetHashCode()
        {
            return this.X * 31 ^ this.Y;
        }

        public override Boolean Equals(Object obj)
        {
            var pos = obj as Position;
            return pos != null && pos.X == this.X && pos.Y == this.Y;
        }
    }
}