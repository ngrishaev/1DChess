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

        private Game.Board _board;
        private Bounds _bounds;
        
        public void Construct(Game.Board board, InputService inputService)
        {
            _board = board;
            _inputService = inputService;
            
            CreateCells(_board.Size);
            CreatePieces(board.Pieces);

            _inputService.OnTap += TapHandler;
        }

        private void TapHandler(Coordinate tap)
        {
            foreach (var cell in _cells.Where(cell => cell.IsContains(tap)))
            {
                Maybe<Game.Pieces.Piece> pieceAtPosition = _board.GetPiece(cell.Position);
                
                if(pieceAtPosition.Exists == false)
                    continue;

                IEnumerable<int> movePositions = _board.GetAvailableMovesFor(pieceAtPosition.Value);
                Highlight(movePositions.ToList());
            }
        }

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
                piece.transform.localPosition = new Vector3(CellToWorldPos(pieceData.Position.Value), 0, 0);
                piece.Construct(pieceData);
                
                _pieces.Add(piece);
            }
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


        public bool IsOnBoard(Coordinate coordinate) => _bounds.IsOverlapInZ(coordinate.World);
        public int WorldPosToCell(float xPos) => Mathf.RoundToInt(0.5f * (_boardSizeInCells - 1) + xPos / _cellPrefab.Width);
        private float CellToWorldPos(int cell) => _cellPrefab.Width * (0.5f + cell - (_boardSizeInCells / 2f));
    }
}
