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
            this.Snakes = new List<Snake>();
            for (Int32 i = 0; i < snakeCount; ++i)
            {
                var snake = new Snake(this.width, this.height, i);
                snake.BlockAdded += this.Snake_BlockAdded;
                snake.BlockRemoved += this.Snake_BlockRemoved;
                this.Snakes.Add(snake);
            }
        }

        private static readonly Random random = new Random();

        private readonly Int32 width;

        private readonly Int32 height;

        private readonly List<Position> items = new List<Position>();

        public IList<Snake> Snakes { get; private set; }

        private void Snake_BlockAdded(Snake snake, Position p)
        {
            var item = this.items.FirstOrDefault(itm => itm.X == p.X && itm.Y == p.Y);
            if (item != null)
            {
                this.items.Remove(item);
                this.AddItem();
                snake.Grow(1);
                this.Display.ResetPosition(item);
            }
            this.Display.DrawSnake(snake);
        }

        public event Action<Game, IReadOnlyDictionary<Snake, IReadOnlyList<CauseOfDeath>>> DeathOccured;

        private void Snake_BlockRemoved(Snake snake, Position p)
        {
            this.Display.ResetPosition(p);
        }

        public IGameDisplay Display { get; set; }

        public void AddItem()
        {
            Int32 rndX, rndY;
            do
            {
                rndX = random.Next(0, this.width);
                rndY = random.Next(0, this.height);
            } while (this.Snakes.SelectMany(snake => snake.Positions).Any(pos => pos.X == rndX && pos.Y == rndY));
            
            var position = new Position(rndX, rndY);
            this.items.Add(position);
            this.Display.DrawItem(position);
        }

        public Boolean MoveNext()
        {
            foreach (var snake in this.Snakes)
            {
                snake.Move();
            }

            this.CheckForDeadSnakes();

            return true;
        }

        private void CheckForDeadSnakes()
        {
            var killedSnakes = new Dictionary<Snake, IReadOnlyList<CauseOfDeath>>();
            if (this.Snakes.Count >= 2)
            {
                for (var i = 0; i < this.Snakes.Count; ++i)
                {
                    for (var j = 0; j < this.Snakes.Count; ++j)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        if (this.Snakes[i].Positions.Any(pos => this.Snakes[j].Head.Equals(pos)))
                        {
                            if (!killedSnakes.TryGetValue(this.Snakes[i], out IReadOnlyList<CauseOfDeath> list))
                            {
                                list = new List<CauseOfDeath>();
                                killedSnakes.Add(this.Snakes[i], list);
                            }

                            ((List<CauseOfDeath>)killedSnakes[this.Snakes[i]]).Add(new Bite(this.Snakes[j]));
                        }
                    }
                }
            }

            foreach (var snake in this.Snakes)
            {
                for (var i = 1; i < snake.Positions.Count; i++)
                {
                    var pos = snake.Positions[i];
                    if (!snake.Head.Equals(pos))
                    {
                        continue;
                    }

                    if (!killedSnakes.TryGetValue(snake, out IReadOnlyList<CauseOfDeath> list))
                    {
                        list = new List<CauseOfDeath>();
                        killedSnakes.Add(snake, list);
                    }

                    ((List<CauseOfDeath>)killedSnakes[snake]).Add(new SelfBite());
                    break;
                }
            }

            if (killedSnakes.Any())
            {
                this.DeathOccured?.Invoke(this, killedSnakes);
            }

            foreach (var (killedSnake, _) in killedSnakes)
            {
                this.Snakes.Remove(killedSnake);
                this.Display.RemoveSnake(killedSnake);
            }
        }

        public void Reset()
        {
            this.Display.Reset();

            foreach (var snake in this.Snakes)
            {
                snake.Reset();
                this.Display.DrawSnake(snake);
            }

            this.items.Clear();
        }

        public Object Current { get; } = null;

        public IEnumerator GetEnumerator()
        {
            return this;
        }
    }
}