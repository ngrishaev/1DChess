using System.Collections.Generic;
using Model.Pieces;

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
            
            Whites = new()
            {
                new King(0, Color.White, Pieces),
                new Queen(1, Color.White, Pieces),
                new Rook(2, Color.White, Pieces),
                new Bishop(3, Color.White, Pieces),
                new Knight(4, Color.White, Pieces),
                new Pawn(5, Color.White, Pieces, true),
            };
            
            Blacks = new()
            {
                new King(Size - 1, Color.Black, Pieces),
                new Queen(Size - 2, Color.Black, Pieces),
                new Rook(Size - 3, Color.Black, Pieces),
                new Bishop(Size - 4, Color.Black, Pieces),
                new Knight(Size - 5, Color.Black, Pieces),
                new Pawn(Size - 6, Color.Black, Pieces, false),
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