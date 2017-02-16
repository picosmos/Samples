namespace ConsoleApp1
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;

    internal class Program
    {
        private static void Main()
        {
            var t = new Thread(KeyboardListener);
            t.Start();
            var game = new Game(20, 10)
            {
                Display = new ConsoleDisplay()
            };
            game.AddItem();
            foreach (var step in game)
            {
                game.TrySetDirection(newDirection);
                Thread.Sleep(500);
            }
        }

        private static volatile Direction newDirection = Direction.Down;

        private static volatile Boolean exitProgram = false;

        private static void KeyboardListener()
        {
            while (!exitProgram)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        newDirection = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        newDirection = Direction.Right;
                        break;
                    case ConsoleKey.UpArrow:
                        newDirection = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        newDirection = Direction.Down;
                        break;
                }
            }

            // ReSharper disable once FunctionNeverReturns
        }
    }

    public class Game : IEnumerable, IEnumerator
    {
        public Game(Int32 width, Int32 height)
        {
            this.width = width;
            this.height = height;
            this.BlockStates = new BlockState[this.width, this.height];
            this.snake = new Snake(this.width, this.height);
            this.snake.BlockAdded += this.snake_BlockAdded;
            this.snake.BlockRemoved += this.snake_BlockRemoved;
        }

        private static readonly Random random = new Random();

        private readonly Int32 width;

        private readonly Int32 height;

        private readonly List<Position> items = new List<Position>();

        private readonly Snake snake;

        private void snake_BlockAdded(Object sender, Position e)
        {
            this.BlockStates[e.X, e.Y] = BlockState.Snake;
            var item = this.items.FirstOrDefault(itm => itm.X == e.X && itm.Y == e.Y);
            if (item != null)
            {
                this.items.Remove(item);
                this.AddItem();
                this.snake.Grow(1, this.CurrentDirection);
            }
        }

        private void snake_BlockRemoved(Object sender, Position e)
        {
            this.BlockStates[e.X, e.Y] = BlockState.Empty;
        }

        public BlockState[,] BlockStates { get; }

        public IGameDisplay Display { get; set; }

        public Int32 SnakeLength => this.snake.Length;

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

        public void AddItem()
        {
            var rndX = random.Next(0, this.width);
            var rndY = random.Next(0, this.height);

            this.BlockStates[rndX, rndY] = BlockState.Item;
            this.items.Add(new Position() { X = rndX, Y = rndY, });
        }

        #region IEnumerator

        public Boolean MoveNext()
        {
            this.snake.Move(this.CurrentDirection);

            this.Display.DrawBoard(this.BlockStates);

            return true;
        }

        public void Reset()
        {
            var w = this.BlockStates.GetLength(0);
            var h = this.BlockStates.GetLength(1);
            for (var cx = 0; cx < w; ++cx)
            {
                for (var cy = 0; cy < h; ++cy)
                {
                    this.BlockStates[cx, cy] = BlockState.Empty;
                }
            }

            this.snake.Reset();
            this.items.Clear();
        }

        public Object Current { get; } = null;

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        #endregion 
    }

    public class Snake
    {
        public Snake(Int32 width, Int32 height)
        {
            this.width = width;
            this.height = height;
            this.Reset();
        }

        private readonly Int32 width;

        private readonly Int32 height;

        private List<Position> positions;

        public Int32 Length => this.positions.Count;

        public void Move(Direction direction)
        {
            var end = this.Pop();
            this.BlockRemoved?.Invoke(this, end);

            this.Grow(1, direction);
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
            var result = this.positions[0];
            this.positions.RemoveAt(0);
            return result;
        }

        public event EventHandler<Position> BlockRemoved;

        public event EventHandler<Position> BlockAdded;

        public void Reset()
        {
            this.positions = new List<Position>
            {
                new Position() { X = 1, Y = 1 },
                new Position() { X = 1, Y = 2 },
            };
        }

        public void Grow(Int32 i, Direction direction)
        {
            var oldStart = this.positions.Last();
            var newStart = this.CreateFromOldStart(oldStart, direction);
            this.positions.Add(newStart);
            this.BlockAdded?.Invoke(this, newStart);
        }
    }

    public class Position
    {
        public Int32 X { get; set; }

        public Int32 Y { get; set; }
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    }

    public interface IGameDisplay
    {
        void DrawBoard(BlockState[,] blockStates);
    }

    public class ConsoleDisplay : IGameDisplay
    {
        public void DrawBoard(BlockState[,] blockStates)
        {
            Console.Clear();
            var width = blockStates.GetLength(0);
            var height = blockStates.GetLength(1);

            for (var cy = 0; cy < height; ++cy)
            {
                for (var cx = 0; cx < width; ++cx)
                {
                    Console.Write(this.BlockStateToChar(blockStates[cx, cy]));
                }

                Console.WriteLine();
            }
        }

        private Char BlockStateToChar(BlockState blockState)
        {
            switch (blockState)
            {
                case BlockState.Empty:
                    return ' ';
                case BlockState.Snake:
                    return 'X';
                case BlockState.Item:
                    return '*';
                default:
                    throw new InvalidEnumArgumentException(nameof(blockState), (Int32)blockState, typeof(BlockState));
            }
        }
    }

    public enum BlockState
    {
        Empty = 0,
        Snake,
        Item
    }
}