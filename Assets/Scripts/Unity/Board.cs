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
        
        private int _boardSizeInCells;

        private Model.Board _boardModel;
        private Bounds _bounds;

        public void SetBoard(Model.Board board)
        {
            ClearBoard();
            _boardModel = board;

            CreateCells(_boardModel.Size);
            CreatePieces(board.Pieces);
        }

        public Maybe<Piece> GetPiece(WorldPosition tap)
        {
            if(IsOnBoard(tap) == false)
                return Maybe<Piece>.No();

            var tapTilePosition = WorldPosToCell(tap.X);
            
            if(IsAnyAt(tapTilePosition) == false)
                return Maybe<Piece>.No();
            
            return Maybe<Piece>.Yes(_pieces.First( piece => piece.PieceData.Position.ValueEquals(tapTilePosition)));
        }

        public bool IsOnBoard(WorldPosition worldPosition) => _bounds.IsOverlapInZ(worldPosition);

        public int WorldPosToCell(float xPos) => Mathf.RoundToInt(0.5f * (_boardSizeInCells - 1) + xPos / _cellPrefab.Width);
        
        public void UpdateState()
        {
            foreach (var cell in _cells) 
                cell.Highlight(false);

            foreach (var piece in _pieces)
            {
                if (piece.PieceData.Position.Exists)
                    piece.PlaceAt(CellToWorldPos(piece.PieceData.Position.Value));
                else
                    piece.Capture();
            }
        }

        public void HighlightMovesFor(Piece piece) => Highlight(_boardModel.GetAvailableMovesFor(piece.PieceData).ToList());
        
        private float CellToWorldPos(int cell) => _cellPrefab.Width * (0.5f + cell - (_boardSizeInCells / 2f));

        private bool IsAnyAt(int position) => _pieces.Any(piece => piece.IsAt(position));

        private void Highlight(List<int> positions)
        {
            foreach (var cell in _cells) 
                cell.Highlight(positions.Contains(cell.Position));
        }

        private void CreatePieces(List<Model.Pieces.Piece> boardPieces)
        {
            foreach (var pieceData in boardPieces)
            {
                var piece = Instantiate(_piecePrefab, _piecesRoot);
                piece.Construct(pieceData);
                
                piece.PlaceAt(CellToWorldPos(pieceData.Position.Value));
                
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
        
        private void ClearBoard()
        {
            foreach (var cell in _cells) 
                Destroy(cell.gameObject);
            _cells.Clear();
            
            foreach (var piece in _pieces) 
                Destroy(piece.gameObject);
            _pieces.Clear();
        }

        public void ResetHighlight()
        {
            foreach (var cell in _cells) 
                cell.Highlight(false);
        }
    }
}
