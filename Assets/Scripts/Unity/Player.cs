using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Game;
using Game.Actions;
using Unity.Services;
using UnityEngine;

namespace Unity
{
    public class Player : MonoBehaviour, IPlayer
    {
        private List<Game.Pieces.Piece> _pieces;
        private Board _board;
        private Game.Board _boardModel;
        
        private TaskCompletionSource<GameAction> _inputTcs = new TaskCompletionSource<GameAction>();
        private Maybe<Game.Pieces.Piece> _selectedPiece = Maybe<Game.Pieces.Piece>.No();
        private List<Game.Pieces.Piece> _playerPieces;
        private List<Game.Pieces.Piece> _enemyPieces;

        public void Construct(List<Game.Pieces.Piece> playerPieces,
            List<Game.Pieces.Piece> enemyPieces,
            Game.Board boardModel,
            Board board,
            InputService inputService, string n)
        {
            _playerPieces = playerPieces;
            _enemyPieces = enemyPieces;
            _board = board;
            _boardModel = boardModel;
            name = n;
            inputService.OnTap += TapHandler;
        }

        public Task<GameAction> GetInput()
        {
            return _inputTcs.Task;
        }

        private void TapHandler(Coordinate tapCoordinate)
        {
            if (_board.IsOnBoard(tapCoordinate) == false)
                return;

            if (_selectedPiece.Exists)
                HandleSelectionEnd(tapCoordinate);
            else
                HandleSelectionStart(tapCoordinate);
        }

        private void HandleSelectionStart(Coordinate tapCoordinate)
        {
            var selectedPiece = _boardModel.GetPiece(_board.WorldPosToCell(tapCoordinate.World.x));
            
            if(selectedPiece.Exists == false)
                return;
            
            if(_pieces.Any(piece => piece == selectedPiece.Value) == false)
                return;

            _selectedPiece = selectedPiece;
            Debug.Log($"Selected piece for {name}");
        }

        private void HandleSelectionEnd(Coordinate tapCoordinate)
        {
            var selectedTile = _board.WorldPosToCell(tapCoordinate.World.x);
            if(_selectedPiece.Value.CanMoveTo(selectedTile))
            {
                _inputTcs.SetResult(new Move(_selectedPiece.Value, selectedTile));
                _selectedPiece = Maybe<Game.Pieces.Piece>.No();
                Debug.Log($"Clear selection for {name}");
                _inputTcs = new TaskCompletionSource<GameAction>(); 
            }
        }
    }
}