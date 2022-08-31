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
            while (_movesCount < 100)
            {
                Debug.Log("awaiting move");
                GameAction playerAction = await CurrentPlayer.GetInput();
                Debug.Log("Move found");
                playerAction.Do();
                _movesCount++;
                OnMoveFinished?.Invoke();
            }

            Debug.Log("Moves count > 100");
        }
    }

    public interface IPlayer
    {
        Task<GameAction> GetInput();
    }
}