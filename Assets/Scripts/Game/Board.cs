using System.Collections.Generic;
using System.Linq;
using Common;
using Game.Pieces;

namespace Game
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
                new Knight(1, Color.White, Pieces),
                new Rook(2, Color.White, Pieces),
            };
            
            Blacks = new()
            {
                new King(Size - 1, Color.Black, Pieces),
                new Knight(Size - 2, Color.Black, Pieces),
                new Rook(Size - 3, Color.Black, Pieces),
            };
            
            Pieces.AddRange(Whites);
            Pieces.AddRange(Blacks);
        }

        public bool IsAnyAt(int position) => 
            Pieces.Any(piece => piece.Position.Value == position);

        public Maybe<Piece> GetPiece(int position) =>
            IsAnyAt(position)
                ? Maybe<Piece>.Yes(Pieces.First(piece => piece.Position.Value == position))
                : Maybe<Piece>.No();

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