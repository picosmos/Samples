namespace Koopakiller.Apps.Snake.Windows.Console
{
    using System;
    using System.Threading;

    using Koopakiller.Apps.Snake.Portable;

    internal class Program
    {
        private static void Main()
        {
            /* TODO:
             * Different color for every player
             * Collision detection for snakes
             * Better render engine. Update only changed elements
             */
            var t = new Thread(KeyboardListener);
            t.Start();
            var game = new Game(20, 10, 2)
            {
                Display = new ConsoleDisplay()
            };
            game.AddItem();
            foreach (var step in game)
            {
                game.Snakes[0].TrySetDirection(newDirectionPlayer1);
                game.Snakes[1].TrySetDirection(newDirectionPlayer2);
                Thread.Sleep(250);
            }
        }

        private static volatile Direction newDirectionPlayer1 = Direction.Down;
        private static volatile Direction newDirectionPlayer2 = Direction.Down;

        private static volatile Boolean exitProgram = false;

        private static void KeyboardListener()
        {
            while (!exitProgram)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    // Player 1
                    case ConsoleKey.LeftArrow:
                        newDirectionPlayer1 = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        newDirectionPlayer1 = Direction.Right;
                        break;
                    case ConsoleKey.UpArrow:
                        newDirectionPlayer1 = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        newDirectionPlayer1 = Direction.Down;
                        break;

                    // Player 2
                    case ConsoleKey.A:
                        newDirectionPlayer2 = Direction.Left;
                        break;
                    case ConsoleKey.D:
                        newDirectionPlayer2 = Direction.Right;
                        break;
                    case ConsoleKey.W:
                        newDirectionPlayer2 = Direction.Up;
                        break;
                    case ConsoleKey.S:
                        newDirectionPlayer2 = Direction.Down;
                        break;
                }
            }
        }
    }
}