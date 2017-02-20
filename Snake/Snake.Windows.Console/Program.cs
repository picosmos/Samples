using System.Collections.Generic;
using System.Diagnostics;

namespace Koopakiller.Apps.Snake.Windows.Console
{
    using System;
    using System.Threading;

    using Portable;

    internal class Program
    {
        private static void Main()
        {
            /* TODO:
             * Collision detection for snakes ( is there still a bug? )
             */
            var t = new Thread(KeyboardListener);
            t.Start();
            var display = new ConsoleDisplay(20, 10)
            {
                DefaultColor = ConsoleColor.Gray,
                SnakeColors =
                {
                    [0] = ConsoleColor.Cyan,
                    [1] = ConsoleColor.Yellow,
                }
            };
            var game = new Game(20, 10, 2, display);
            game.DeathOccured += Game_DeathOccured;
            game.AddItem();
            foreach (var step in game)
            {
                (game.Snakes.Count >= 1 ? game.Snakes : null)?[0]?.TrySetDirection(newDirectionPlayer1);
                (game.Snakes.Count >= 2 ? game.Snakes : null)?[1]?.TrySetDirection(newDirectionPlayer2);
                Thread.Sleep(250);
            }
        }

        private static void Game_DeathOccured(Game game, IReadOnlyDictionary<Snake, IReadOnlyList<CauseOfDeath>> deaths)
        {
            foreach (var (snake, causes) in deaths)
            {
                Debug.WriteLine($"Snake {snake.Id} died because:");
                foreach (var cause in causes)
                {
                    Debug.WriteLine($" - {cause}");
                }
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