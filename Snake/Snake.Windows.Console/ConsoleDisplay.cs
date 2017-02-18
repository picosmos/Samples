namespace Koopakiller.Apps.Snake.Windows.Console
{
    using System;
    using Portable;

    public class ConsoleDisplay : IGameDisplay
    {
        public void DrawSnake(Snake newSnake)
        {
            foreach (var position in newSnake.Positions)
            {
                DrawAt(position, 'X');
            }
            ResetCursorPosition();
        }

        public void RemoveSnake(Snake snake)
        {
            foreach (var position in snake.Positions)
            {
                this.ResetPosition(position);
            }
            ResetCursorPosition();
        }

        public void DrawItem(Position position)
        {
            DrawAt(position, '*');
            ResetCursorPosition();

        }

        public void ResetPosition(Position position)
        {
            DrawAt(position, ' ');
            ResetCursorPosition();
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