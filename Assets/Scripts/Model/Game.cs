using Model.Actions;
using Model.Players;
using UnityEngine;

namespace Model
{
    public class Game
    {
        private readonly IGameObserver _gameObserver;
        private IPlayer _currentPlayer;
        private IPlayer _nextPlayer;
        private Board _board;
        public IPlayer CurrentPlayer => _currentPlayer;

        private int _movesCount = 0;

        public Game(Board board, IPlayer whitePlayer, IPlayer blackPlayer, IGameObserver gameObserver)
        {
            _board = board;
            _currentPlayer = whitePlayer;
            _nextPlayer = blackPlayer;
            _gameObserver = gameObserver;
        }

        public async void Run()
        {
            while (_movesCount < 100 && !_currentPlayer.KingCaptured())
            {
                GameAction playerAction = await _currentPlayer.GetInput();
                playerAction.Do();
                Debug.Log(playerAction.ToString());
                
                _gameObserver.OnMoveEnd();
                
                SwitchPlayers();
                _movesCount++;
            }
            _gameObserver.OnGameFinished(_nextPlayer);

            Debug.Log("Moves count > 100");
        }

        private void SwitchPlayers() => 
            (_currentPlayer, _nextPlayer) = (_nextPlayer, _currentPlayer);
    }
}