namespace Koopakiller.Apps.Snake.Portable
{
    public interface IGameDisplay
    {
        void DrawBoard(BlockState[,] blockStates);
    }
}