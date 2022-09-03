using System;
using System.Threading.Tasks;
using Game.Actions;
using UnityEngine;

namespace Game
{
    public class Game
    {
        public event Action OnMoveFinished;
        
        private IPlayer[] _players = new IPlayer[2];
        private int _movesCount = 0;

        // TODO_L: Do not liek
        public IPlayer CurrentPlayer => _players[_movesCount % _players.Length];
        public Board Board { get; }

        public Game(Board board, IPlayer p1, IPlayer p2)
        {
            Board = board;
            _players[0] = p1;
            _players[1] = p2;
        }

        public async void Run()
        {
            while (_movesCount < 100)
            {
                GameAction playerAction = await CurrentPlayer.GetInput();
                playerAction.Do();
                Debug.Log(playerAction.ToString());
                _movesCount++;
                OnMoveFinished?.Invoke();
            }

            Debug.Log("Moves count > 100");
        }
    }

    public interface IPlayer
    {
        string Name { get; }
        
        Task<GameAction> GetInput();
    }
}