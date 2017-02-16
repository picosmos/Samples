namespace ConsoleApp1
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class Game : IEnumerable, IEnumerator
    {
        public Game(Int32 width, Int32 height)
        {
            this.width = width;
            this.height = height;
            this.BlockStates = new BlockState[this.width, this.height];
            this.Snake = new Snake(this.width, this.height);
            this.Snake.BlockAdded += this.snake_BlockAdded;
            this.Snake.BlockRemoved += this.snake_BlockRemoved;
        }

        private static readonly Random random = new Random();

        private readonly Int32 width;

        private readonly Int32 height;

        private readonly List<Position> items = new List<Position>();

        public Snake Snake { get; private set; }

        private void snake_BlockAdded(Object sender, Position e)
        {
            this.BlockStates[e.X, e.Y] = BlockState.Snake;
            var item = this.items.FirstOrDefault(itm => itm.X == e.X && itm.Y == e.Y);
            if (item != null)
            {
                this.items.Remove(item);
                this.AddItem();
                this.Snake.Grow(1);
            }
        }

        private void snake_BlockRemoved(Object sender, Position e)
        {
            this.BlockStates[e.X, e.Y] = BlockState.Empty;
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
            this.Snake.Move();

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

            this.Snake.Reset();
            this.items.Clear();
        }

        public Object Current { get; } = null;

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        #endregion 
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
}