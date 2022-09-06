namespace Model
{
    public interface IGameObserver
    {
        public void OnMoveEnd();
        public void OnGameFinished(IPlayer winner);
    }
}