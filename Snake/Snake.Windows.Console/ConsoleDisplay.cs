namespace Koopakiller.Apps.Snake.Windows.Console
{
    using System;
    using System.ComponentModel;

    using Koopakiller.Apps.Snake.Portable;

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