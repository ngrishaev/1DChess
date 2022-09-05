using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Game;
using Game.Actions;
using Game.Pieces;
using Unity.Services;
using UnityEngine;

namespace Unity
{
    public class Player : MonoBehaviour, IPlayer
    {
        public string Name { get; private set; }
        private Board _board;
        private Game.Board _boardModel;

        private TaskCompletionSource<GameAction> _inputTcs = new TaskCompletionSource<GameAction>();
        private Maybe<Piece> _selectedPiece = Maybe<Piece>.No();
        private List<Game.Pieces.Piece> _playerPieces; // TODO: наверное это Set? Возможно, вообще отдельный класс
        private List<Game.Pieces.Piece> _enemyPieces;
        private bool _waitingForInput = false;

        public void Construct(List<Game.Pieces.Piece> playerPieces,
            List<Game.Pieces.Piece> enemyPieces,
            Game.Board boardModel,
            Board board,
            InputService inputService,
            string name)
        {
            _playerPieces = playerPieces;
            _enemyPieces = enemyPieces;
            _board = board;
            _boardModel = boardModel;
            Name = name;
            inputService.OnTap += TapHandler;
        }

        public Task<GameAction> GetInput()
        {
            _waitingForInput = true;
            _selectedPiece = Maybe<Piece>.No();
            _inputTcs = new TaskCompletionSource<GameAction>(); 
            return _inputTcs.Task;
        }

        // TODO: Дубляж кода
        // TODO: в любом наборе фигур должен быть король. В любом ли?
        public bool KingCaptured() => _playerPieces.Any(piece => piece is King && piece.Captured);

        private void TapHandler(Coordinate tapCoordinate)
        {
            if (_waitingForInput == false)
                return;
            
            if (_board.IsOnBoard(tapCoordinate) == false)
                return;

            if (_selectedPiece.Exists)
                HandleSelectionEnd(tapCoordinate);
            else
                HandleSelectionStart(tapCoordinate);
        }

        private void HandleSelectionStart(Coordinate tapCoordinate)
        {
            Maybe<Piece> selectedPiece = _board.GetPiece(tapCoordinate);
            
            if(selectedPiece.Exists == false)
                return;
            
            if(_playerPieces.Any(piece => piece == selectedPiece.Value.PieceData) == false)
                return;
            
            _selectedPiece = selectedPiece;
            _selectedPiece.Value.Select();
            Debug.Log($"Selected piece for {Name}");
        }

        private void HandleSelectionEnd(Coordinate tapCoordinate)
        {
            var selectedTile = _board.WorldPosToCell(tapCoordinate.World.x);
            var pieceOnTile = _board.GetPiece(tapCoordinate);
            
            if(_selectedPiece.Value.PieceData.CanMoveTo(selectedTile))
            {
                _waitingForInput = false;

                GameAction move = pieceOnTile.Exists
                    ? new Capture(_selectedPiece.Value.PieceData, pieceOnTile.Value.PieceData)
                    : new Move(_selectedPiece.Value.PieceData, selectedTile);
                _inputTcs.SetResult(move);
                Debug.Log($"Clear selection for {name}");
            }
        }
    }
}