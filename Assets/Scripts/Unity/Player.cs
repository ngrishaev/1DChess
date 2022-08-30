using System.Collections.Generic;
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
        private Board _board;

        private TaskCompletionSource<GameAction> _inputTcs = new TaskCompletionSource<GameAction>();
        private List<Game.Pieces.Piece> _pieces;
        private Maybe<Game.Pieces.Piece> _selectedPiece = Maybe<Game.Pieces.Piece>.No();

        private void Awake()
        {
            
        }

        public void Construct(
            List<Game.Pieces.Piece> pieces,
            Board board,
            InputService inputService)
        {
            _board = board;
            _pieces = pieces;
            inputService.OnTap += TapHandler;
        }

        public Task<GameAction> GetInput()
        {
            return _inputTcs.Task;
        }

        private void TapHandler(Coordinate tapCoordinate)
        {
            if (_selectedPiece.Exists)
                HandleSelectionEnd(tapCoordinate);
            else
                HandleSelectionStart(tapCoordinate);
        }

        private void HandleSelectionStart(Coordinate tapCoordinate)
        {
            if(_board.IsOnBoard(tapCoordinate))
                Debug.Log($"Tap on {_board.WorldPosToCell(tapCoordinate.World.x)} cell");
            else
            {
                Debug.Log($"Tap outside");
            }
        }

        private void HandleSelectionEnd(Coordinate tapCoordinate)
        {
            //_inputTcs?.TrySetResult(TODO);
            _inputTcs = new TaskCompletionSource<GameAction>();
        }
    }
}