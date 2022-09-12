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
                new King(0, Color.White, Pieces, TODO),
                new Queen(1, Color.White, Pieces, TODO),
                new Rook(2, Color.White, Pieces, TODO),
                new Bishop(3, Color.White, Pieces, TODO),
                new Knight(4, Color.White, Pieces, TODO),
                new Pawn(5, Color.White, Pieces, TODO),
            };
            
            Blacks = new()
            {
                new King(Size - 1, Color.Black, Pieces, TODO),
                new Queen(Size - 2, Color.Black, Pieces, TODO),
                new Rook(Size - 3, Color.Black, Pieces, TODO),
                new Bishop(Size - 4, Color.Black, Pieces, TODO),
                new Knight(Size - 5, Color.Black, Pieces, TODO),
                new Pawn(Size - 6, Color.Black, Pieces, TODO),
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