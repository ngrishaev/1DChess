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
        private Board _board { get; }
        public IPlayer CurrentPlayer => _currentPlayer;

        public Game(Board board, IPlayer whitePlayer, IPlayer blackPlayer)
        {
            _board = board;
            _currentPlayer = whitePlayer;
            _nextPlayer = blackPlayer;
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
                _movesCount++;
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