using System;
using System.Threading.Tasks;
using Game.Actions;
using UnityEngine;

namespace Game
{
    public class Game
    {
        public event Action OnMoveFinished;
        public event Action<IPlayer> OnGameFinished;
        
        private IPlayer _currentPlayer;
        private IPlayer _nextPlayer;
        private int _movesCount = 0;
        public Board Board { get; }
        public IPlayer CurrentPlayer => _currentPlayer;

        public Game(Board board, IPlayer p1, IPlayer p2)
        {
            Board = board;
            _currentPlayer = p1;
            _nextPlayer = p2;
        }

        public async void Run()
        {
            while (_movesCount < 100 && !_currentPlayer.KingCaptured())
            {
                GameAction playerAction = await _currentPlayer.GetInput();
                playerAction.Do();
                Debug.Log(playerAction.ToString());
                
                OnMoveFinished?.Invoke();
                
                SwitchPlayers();
            }
            OnGameFinished?.Invoke(_nextPlayer);

            Debug.Log("Moves count > 100");
        }

        private void SwitchPlayers() => 
            (_currentPlayer, _nextPlayer) = (_nextPlayer, _currentPlayer);
    }

    public interface IPlayer
    {
        string Name { get; }
        Task<GameAction> GetInput();
        bool KingCaptured();
    }
}