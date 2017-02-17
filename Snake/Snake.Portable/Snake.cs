namespace Koopakiller.Apps.Snake.Portable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Koopakiller.Apps.Snake.Portable.Future;

    public class Snake
    {
        public Snake(Int32 width, Int32 height, Int32 id)
        {
            this.width = width;
            this.height = height;
            this.Id = id;
            this.Reset();
        }

        public Int32 Id { get; }

        private readonly Int32 width;

        private readonly Int32 height;

        public IReadOnlyList<Position> Positions => this.WritablePositions;

        private List<Position> WritablePositions { get; } = new List<Position>();

        public Int32 Length => this.Positions.Count;

        public Direction CurrentDirection { get; private set; } = Direction.Down;


        public Boolean TrySetDirection(Direction? newDirection)
        {
            if (newDirection == null)
            {
                return false;
            }

            if (newDirection == this.CurrentDirection)
            {
                return false;
            }

            if ((this.CurrentDirection == Direction.Down && newDirection.Value == Direction.Up)
                || (this.CurrentDirection == Direction.Up && newDirection.Value == Direction.Down)
                || (this.CurrentDirection == Direction.Left && newDirection.Value == Direction.Right)
                || (this.CurrentDirection == Direction.Right && newDirection.Value == Direction.Left))
            {
                return false;
            }

            this.CurrentDirection = newDirection.Value;
            return true;
        }

        public void Move()
        {
            var end = this.Pop();
            this.BlockRemoved?.Invoke(this, end);

            this.Grow(1);
        }

        private Position CreateFromOldStart(Position position, Direction direction)
        {
            Int32 x = position.X, y = position.Y;
            switch (direction)
            {
                case Direction.Left:
                    x = this.CycleChange(x, this.width, -1);
                    break;
                case Direction.Right:
                    x = this.CycleChange(x, this.width, 1);
                    break;
                case Direction.Up:
                    y = this.CycleChange(y, this.height, -1);
                    break;
                case Direction.Down:
                    y = this.CycleChange(y, this.height, 1);
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(direction), (Int32)direction, typeof(Direction));
            }

            return new Position() { X = x, Y = y };
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
            var result = this.WritablePositions[0];
            this.WritablePositions.RemoveAt(0);
            return result;
        }

        public event Action<Snake, Position> BlockRemoved;

        public event Action<Snake, Position> BlockAdded;

        public void Reset()
        {
            this.WritablePositions.Clear();
            this.WritablePositions.Add(new Position() { X = 1 + this.Id * 2, Y = 1 });
            this.WritablePositions.Add(new Position() { X = 1 + this.Id * 2, Y = 2 });
        }

        public void Grow(Int32 i)
        {
            var oldStart = this.WritablePositions.Last();
            var newStart = this.CreateFromOldStart(oldStart, this.CurrentDirection);
            this.WritablePositions.Add(newStart);
            this.BlockAdded?.Invoke(this, newStart);
        }
    }
}