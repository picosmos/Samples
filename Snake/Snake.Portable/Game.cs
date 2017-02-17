namespace Koopakiller.Apps.Snake.Portable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Game : IEnumerable, IEnumerator
    {
        public Game(Int32 width, Int32 height, Int32 snakeCount)
        {
            this.width = width;
            this.height = height;
            this.BlockStates = new BlockState[this.width, this.height];
            this.Snakes = new List<Snake>();
            for (Int32 i = 0; i < snakeCount; ++i)
            {
                var snake = new Snake(this.width, this.height, i);
                snake.BlockAdded += this.snake_BlockAdded;
                snake.BlockRemoved += this.snake_BlockRemoved;
                this.Snakes.Add(snake);
            }
        }

        private static readonly Random random = new Random();

        private readonly Int32 width;

        private readonly Int32 height;

        private readonly List<Position> items = new List<Position>();

        public IList<Snake> Snakes { get; private set; }

        private void snake_BlockAdded(Snake snake, Position p)
        {
            this.BlockStates[p.X, p.Y] = BlockState.Snake;
            var item = this.items.FirstOrDefault(itm => itm.X == p.X && itm.Y == p.Y);
            if (item != null)
            {
                this.items.Remove(item);
                this.AddItem();
                snake.Grow(1);
            }
        }

        private void snake_BlockRemoved(Snake snake, Position p)
        {
            this.BlockStates[p.X, p.Y] = BlockState.Empty;
        }

        public BlockState[,] BlockStates { get; }

        public IGameDisplay Display { get; set; }

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
            foreach (var snake in this.Snakes)
            {
                snake.Move();
            }

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

            foreach (var snake in this.Snakes)
            {
                snake.Reset();
            }

            this.items.Clear();
        }

        public Object Current { get; } = null;

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        #endregion 
    }
}