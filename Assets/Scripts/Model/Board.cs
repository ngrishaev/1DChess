using System.Collections.Generic;
using Model.Pieces;
using Model.Services;

namespace Model
{
    public class Board
    {
        public List<Piece> Blacks { get; } = new List<Piece>();
        public List<Piece> Whites { get; } = new List<Piece>();
        public List<Piece> Pieces { get; } = new List<Piece>();
        public int Size { get; }
        public Board(int size)
        {
            Size = size;

            var pathService = new PathAvailabilityService();
            
            //TODO: factory?
            Whites = new()
            {
                new King(0, Color.White, Pieces, pathService),
                new Queen(1, Color.White, Pieces, pathService),
                new Rook(2, Color.White, Pieces, pathService),
                new Bishop(3, Color.White, Pieces, pathService),
                new Knight(4, Color.White, Pieces, pathService),
                new Pawn(5, Color.White, Pieces, pathService),
            };
            
            Blacks = new()
            {
                new King(Size - 1, Color.Black, Pieces, pathService),
                new Queen(Size - 2, Color.Black, Pieces, pathService),
                new Rook(Size - 3, Color.Black, Pieces, pathService),
                new Bishop(Size - 4, Color.Black, Pieces, pathService),
                new Knight(Size - 5, Color.Black, Pieces, pathService),
                new Pawn(Size - 6, Color.Black, Pieces, pathService),
            };
            
            Pieces.AddRange(Whites);
            Pieces.AddRange(Blacks);
        }

        public IEnumerable<int> GetAvailableMovesFor(Piece piece)
        {
            for (int i = 0; i < Size; i++)
            {
                if (piece.CanMoveTo(i))
                    yield return i;
            }
        }
    }
}