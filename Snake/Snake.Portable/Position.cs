namespace Koopakiller.Apps.Snake.Portable
{
    using System;

    public class Position
    {
        public Position(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }

        public Int32 X { get; }

        public Int32 Y { get; }

        public override Int32 GetHashCode()
        {
            return X * 31 ^ Y;
        }

        public override Boolean Equals(Object obj)
        {
            var pos = obj as Position;
            return pos != null && pos.X == X && pos.Y == Y;
        }
    }
}