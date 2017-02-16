namespace Koopakiller.Apps.Snake
{
    using System;
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
                game.Snake.TrySetDirection(newDirection);
                Thread.Sleep(250);
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
        }
    }
}