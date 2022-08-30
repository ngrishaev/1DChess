using System.Threading.Tasks;
using Game.Actions;

namespace Game
{
    public class Game
    {
        private IPlayer[] _players = new IPlayer[2];
        private int _movesCount = 0;

        private IPlayer CurrentPlayer => _players[_movesCount % _players.Length];
        public Board Board { get; }

        public Game(int size, IPlayer p1, IPlayer p2)
        {
            Board = new Board(size);
            _players[0] = p1;
            _players[1] = p2;
        }

        public async void Run()
        {
            while (true)
            {
                GameAction gameInput = await CurrentPlayer.GetInput();
                gameInput.Process();
                _movesCount++;
            }
        }
    }

    public interface IPlayer
    {
        Task<GameAction> GetInput();
    }
}