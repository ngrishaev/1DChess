﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Model;
using Model.Actions;
using Model.Pieces;
using Unity.Services;
using UnityEngine;

namespace Unity
{
    public class Player : MonoBehaviour, IPlayer
    {
        public string Name { get; private set; }
        public event Action<Unity.Piece> OnPieceSelect; 
        public event Action<Unity.Piece> OnPieceDeselect; 
        private Board _board;

        private TaskCompletionSource<GameAction> _inputTcs = new TaskCompletionSource<GameAction>();
        private Maybe<Piece> _selectedPiece = Maybe<Piece>.No();
        private List<Model.Pieces.Piece> _playerPieces;
        private bool WaitingForInput => !_inputTcs.Task?.IsCompleted ?? false;

        public void Construct(
            Board board,
            InputService inputService,
            string name)
        {
            _board = board;
            Name = name;
            inputService.OnTap += TapHandler;
        }

        public void JoinGame(
            List<Model.Pieces.Piece> playerPieces
        )
        {
            _playerPieces = playerPieces;
        }

        public Task<GameAction> GetInput()
        {
            _selectedPiece = Maybe<Piece>.No();
            _inputTcs = new TaskCompletionSource<GameAction>(); 
            return _inputTcs.Task;
        }
        
        public bool KingCaptured() => _playerPieces.Any(piece => piece is King && piece.Captured);

        private void TapHandler(Coordinate tapCoordinate)
        {
            if (WaitingForInput == false)
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
            
            SelectPiece(selectedPiece);
        }

        private void SelectPiece(Maybe<Piece> selectedPiece)
        {
            _selectedPiece = selectedPiece;
            OnPieceSelect?.Invoke(_selectedPiece.Value);
            Debug.Log($"Selected piece for {Name}");
        }

        private void HandleSelectionEnd(Coordinate tapCoordinate)
        {
            var selectedTile = _board.WorldPosToCell(tapCoordinate.World.x);
            var pieceOnTile = _board.GetPiece(tapCoordinate);

            if (pieceOnTile.ValueEquals(_selectedPiece))
            {
                OnPieceDeselect?.Invoke(_selectedPiece.Value);
                _selectedPiece = Maybe<Piece>.No();
                return;
            }
            
            if(pieceOnTile.Exists && pieceOnTile.Value.PieceData.Color == _selectedPiece.Value.Color)
            {
                SelectPiece(pieceOnTile);
                return;
            }
            
            if(_selectedPiece.Value.PieceData.CanMoveTo(selectedTile))
            {
                GameAction move = pieceOnTile.Exists
                    ? new Capture(_selectedPiece.Value.PieceData, pieceOnTile.Value.PieceData)
                    : new Move(_selectedPiece.Value.PieceData, selectedTile);
                _inputTcs.SetResult(move);
                Debug.Log($"Clear selection for {name}");
            }
        }
    }
}