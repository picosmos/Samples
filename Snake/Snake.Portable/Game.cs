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
            Snakes = new List<Snake>();
            for (Int32 i = 0; i < snakeCount; ++i)
            {
                var snake = new Snake(this.width, this.height, i);
                snake.BlockAdded += Snake_BlockAdded;
                snake.BlockRemoved += Snake_BlockRemoved;
                Snakes.Add(snake);
            }
        }

        private static readonly Random random = new Random();

        private readonly Int32 width;

        private readonly Int32 height;

        private readonly List<Position> items = new List<Position>();

        public IList<Snake> Snakes { get; private set; }

        private void Snake_BlockAdded(Snake snake, Position p)
        {
            var item = items.FirstOrDefault(itm => itm.X == p.X && itm.Y == p.Y);
            if (item != null)
            {
                items.Remove(item);
                AddItem();
                snake.Grow(1);
                Display.ResetPosition(item);
            }
            Display.DrawSnake(snake);
        }

        public event Action<Game, IReadOnlyDictionary<Snake, IReadOnlyList<CauseOfDeath>>> DeathOccured;

        private void Snake_BlockRemoved(Snake snake, Position p)
        {
            Display.ResetPosition(p);
        }

        public IGameDisplay Display { get; set; }

        public void AddItem()
        {
            Int32 rndX, rndY;
            do
            {
                rndX = random.Next(0, width);
                rndY = random.Next(0, height);
            } while (Snakes.SelectMany(snake => snake.Positions).Any(pos => pos.X == rndX && pos.Y == rndY));
            
            var position = new Position(rndX, rndY);
            items.Add(position);
            Display.DrawItem(position);
        }

        public Boolean MoveNext()
        {
            foreach (var snake in Snakes)
            {
                snake.Move();
            }

            CheckForDeadSnakes();

            return true;
        }

        private void CheckForDeadSnakes()
        {
            var killedSnakes = new Dictionary<Snake, IReadOnlyList<CauseOfDeath>>();
            if (Snakes.Count >= 2)
            {
                for (var i = 0; i < Snakes.Count; ++i)
                {
                    for (var j = 0; j < Snakes.Count; ++j)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        if (Snakes[i].Positions.Any(pos => Snakes[j].Head.Equals(pos)))
                        {
                            if (!killedSnakes.TryGetValue(Snakes[i], out IReadOnlyList<CauseOfDeath> list))
                            {
                                list = new List<CauseOfDeath>();
                                killedSnakes.Add(Snakes[i], list);
                            }

                            ((List<CauseOfDeath>)killedSnakes[Snakes[i]]).Add(new Bite(Snakes[j]));
                        }
                    }
                }
            }

            foreach (var snake in Snakes)
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
                DeathOccured?.Invoke(this, killedSnakes);
            }

            foreach (var (killedSnake, _) in killedSnakes)
            {
                Snakes.Remove(killedSnake);
                this.Display.RemoveSnake(killedSnake);
            }
        }

        public void Reset()
        {
            Display.Reset();

            foreach (var snake in Snakes)
            {
                snake.Reset();
                Display.DrawSnake(snake);
            }

            items.Clear();
        }

        public Object Current { get; } = null;

        public IEnumerator GetEnumerator()
        {
            return this;
        }
    }
}