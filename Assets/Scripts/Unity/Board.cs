using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Unity.Services;
using UnityEngine;

namespace Unity
{
    public class Board : MonoBehaviour
    {

        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private Transform _piecesRoot;
        
        [Header("Resources")]
        [SerializeField] private Cell _cellPrefab;
        [SerializeField] private Piece _piecePrefab;

        private List<Cell> _cells = new List<Cell>();
        private List<Piece> _pieces = new List<Piece>();

        private InputService _inputService;
        private int _boardSizeInCells;

        private Game.Board _boardModel;
        private Bounds _bounds;
        
        public void Construct(Game.Board board, InputService inputService)
        {
            _boardModel = board;
            _inputService = inputService;
            
            CreateCells(_boardModel.Size);
            CreatePieces(board.Pieces);
        }

        private void HighlightMovesFor(Piece piece) => Highlight(_boardModel.GetAvailableMovesFor(piece.PieceData).ToList());

        private void Highlight(List<int> positions)
        {
            foreach (var cell in _cells) 
                cell.Highlight(positions.Contains(cell.Position));
        }

        private void CreatePieces(List<Game.Pieces.Piece> boardPieces)
        {
            foreach (var pieceData in boardPieces)
            {
                var piece = Instantiate(_piecePrefab, _piecesRoot);
                piece.Construct(pieceData);
                
                piece.PlaceAt(CellToWorldPos(pieceData.Position.Value));

                piece.OnSelect += PieceSelectionHandler;
                
                _pieces.Add(piece);
            }
        }

        private void PieceSelectionHandler(Piece selectedPiece)
        {
            HighlightMovesFor(selectedPiece);
        }

        private void CreateCells(int size)
        {
            _boardSizeInCells = size;
            for (int i = 0; i < size; i++)
            {
                Cell cell = Instantiate(_cellPrefab, _cellsRoot);
                cell.transform.localPosition = new Vector3(CellToWorldPos(i), 0, 0);
                cell.Construct(i);

                _cells.Add(cell);
            }

            _bounds = new Bounds(transform.position, new Vector3(_boardSizeInCells * _cellPrefab.Width, _cellPrefab.Height, 0));
        }


        public Maybe<Piece> GetPiece(Coordinate tap)
        {
            if(IsOnBoard(tap) == false)
                return Maybe<Piece>.No();

            var tapTilePosition = WorldPosToCell(tap.World.x);
            
            if(IsAnyAt(tapTilePosition) == false)
                return Maybe<Piece>.No();
            
            return Maybe<Piece>.Yes(_pieces.First( piece => piece.PieceData.Position.ValueEquals(tapTilePosition)));
        }

        public bool IsOnBoard(Coordinate coordinate) => _bounds.IsOverlapInZ(coordinate.World);

        public int WorldPosToCell(float xPos) => Mathf.RoundToInt(0.5f * (_boardSizeInCells - 1) + xPos / _cellPrefab.Width);

        private float CellToWorldPos(int cell) => _cellPrefab.Width * (0.5f + cell - (_boardSizeInCells / 2f));

        private bool IsAnyAt(int position) => _pieces.Any(piece => piece.IsAt(position));

        public void UpdateState()
        {
            foreach (var piece in _pieces)
            {
                if (piece.PieceData.Position.Exists)
                    piece.PlaceAt(CellToWorldPos(piece.PieceData.Position.Value));
                else
                    piece.Capture();
            }
        }
    }
}
