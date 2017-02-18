namespace Koopakiller.Apps.Snake.Portable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Future;

    public class Snake
    {
        public Snake(Int32 width, Int32 height, Int32 id)
        {
            this.width = width;
            this.height = height;
            Id = id;
            Reset();
        }

        public Int32 Id { get; }

        private readonly Int32 width;

        private readonly Int32 height;

        public IReadOnlyList<Position> Positions => WritablePositions;

        private List<Position> WritablePositions { get; } = new List<Position>();

        public Position Head => Positions.FirstOrDefault();

        public Int32 Length => Positions.Count;

        public Direction CurrentDirection { get; private set; } = Direction.Down;


        public Boolean TrySetDirection(Direction? newDirection)
        {
            if (newDirection == null)
            {
                return false;
            }

            if (newDirection == CurrentDirection)
            {
                return false;
            }

            if ((CurrentDirection == Direction.Down && newDirection.Value == Direction.Up)
                || (CurrentDirection == Direction.Up && newDirection.Value == Direction.Down)
                || (CurrentDirection == Direction.Left && newDirection.Value == Direction.Right)
                || (CurrentDirection == Direction.Right && newDirection.Value == Direction.Left))
            {
                return false;
            }

            CurrentDirection = newDirection.Value;
            return true;
        }

        public void Move()
        {
            var end = Pop();
            BlockRemoved?.Invoke(this, end);

            Grow(1);
        }

        private Position CreateFromOldStart(Position position, Direction direction)
        {
            Int32 x = position.X, y = position.Y;
            switch (direction)
            {
                case Direction.Left:
                    x = CycleChange(x, width, -1);
                    break;
                case Direction.Right:
                    x = CycleChange(x, width, 1);
                    break;
                case Direction.Up:
                    y = CycleChange(y, height, -1);
                    break;
                case Direction.Down:
                    y = CycleChange(y, height, 1);
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(direction), (Int32)direction, typeof(Direction));
            }

            return new Position(x, y);
        }

        private Int32 CycleChange(Int32 current, Int32 max, Int32 change)
        {
            current += change;
            while (current < 0)
            {
                current += max;
            }

            while (current >= max)
            {
                current -= max;
            }

            return current;
        }

        private Position Pop()
        {
            var result = WritablePositions[0];
            WritablePositions.RemoveAt(0);
            return result;
        }

        public event Action<Snake, Position> BlockRemoved;

        public event Action<Snake, Position> BlockAdded;

        public void Reset()
        {
            WritablePositions.Clear();
            WritablePositions.Add(new Position(1 + Id * 2, 1));
            WritablePositions.Add(new Position(1 + Id * 2, 2));
        }

        public void Grow(Int32 i)
        {
            var oldStart = WritablePositions.Last();
            var newStart = CreateFromOldStart(oldStart, CurrentDirection);
            WritablePositions.Add(newStart);
            BlockAdded?.Invoke(this, newStart);
        }
    }
}