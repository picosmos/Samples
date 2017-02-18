namespace Koopakiller.Apps.Snake.Portable
{
    public interface IGameDisplay
    {
        void DrawSnake(Snake newSnake);

        void RemoveSnake(Snake snake);

        void DrawItem(Position position);

        void ResetPosition(Position position);

        void Reset();
    }
}