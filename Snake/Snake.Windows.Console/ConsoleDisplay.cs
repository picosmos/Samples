using System.Collections.Generic;

namespace Koopakiller.Apps.Snake.Windows.Console
{
    using System;
    using Portable;

    public class ConsoleDisplay : IGameDisplay
    {
        public ConsoleDisplay(Int32 width, Int32 height)
        {
            this.Width = width;
            this.Height = height;
        }

        public Int32 Width { get; set; }

        public Int32 Height { get; set; }

        public ConsoleColor DefaultColor { get; set; } = ConsoleColor.Gray;

        public Dictionary<Snake, ConsoleColor> SnakeColors { get; } = new Dictionary<Snake, ConsoleColor>();

        public void DrawSnake(Snake snake)
        {
            Console.ForegroundColor = this.SnakeColors[snake];

            for (var i = 0; i < snake.Positions.Count; i++)
            {
                this.DrawAt(snake.Positions[i], this.GetSnakeCharFromPosition(snake, i));
            }
            this.ResetCursorPosition();

            Console.ForegroundColor = this.DefaultColor;
        }

        private Char GetSnakeCharFromPosition(Snake snake, Int32 pos)
        {
            if (pos == snake.Positions.Count - 1)
            {
                return 'O'; //Head
            }

            if (pos == 0)
            {
                return 'X'; //End
            }

            (Int32, Int32) normIntoRange(Int32 a, Int32 b, Int32 norm) => Math.Abs(a - b) <= 2 ? (a, b) : (a < b ? (a + norm, b) : (a, b + norm));
            var (nx, px) = normIntoRange(snake.Positions[pos - 1].X, snake.Positions[pos + 1].X, this.Width);
            var (ny, py) = normIntoRange(snake.Positions[pos - 1].Y, snake.Positions[pos + 1].Y, this.Height);

            if (nx == px)
            {
                return '│';
            }
            if (ny == py)
            {
                return '─';
            }
            
            var sum = (snake.Positions[pos + 1].X == snake.Positions[pos].X ? 0b001 : 0)
                    + (py > ny ? 0b100 : 0)
                    + (px > nx ? 0b010 : 0) ;
            return new[] {'┐', '└', '┌', '┘'}[sum >= 4 ? -sum + 7 : sum];
        }

        public void RemoveSnake(Snake snake)
        {
            foreach (var position in snake.Positions)
            {
                this.ResetPosition(position);
            }
            this.ResetCursorPosition();
        }

        public void DrawItem(Position position)
        {
            this.DrawAt(position, '*');
            this.ResetCursorPosition();

        }

        public void ResetPosition(Position position)
        {
            this.DrawAt(position, ' ');
            this.ResetCursorPosition();
        }

        public void Reset()
        {
            Console.Clear();
        }

        private void DrawAt(Position p, Char chr)
        {
            this.DrawAt(p.X, p.Y, chr);
        }

        private void DrawAt(Int32 x, Int32 y, Char chr)
        {
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(chr);
        }

        private void ResetCursorPosition()
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
        }
    }
}