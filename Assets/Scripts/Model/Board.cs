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
            var occupyService = new OccupiedPositionService();
            
            //TODO: factory?
            Whites = new()
            {
                new King(0, Color.White, Pieces, pathService, occupyService),
                new Queen(1, Color.White, Pieces, pathService, occupyService),
                new Rook(2, Color.White, Pieces, pathService, occupyService),
                new Bishop(3, Color.White, Pieces, pathService, occupyService),
                new Knight(4, Color.White, Pieces, pathService, occupyService),
                new Pawn(5, Color.White, Pieces, pathService, occupyService),
            };
            
            Blacks = new()
            {
                new King(Size - 1, Color.Black, Pieces, pathService, occupyService),
                new Queen(Size - 2, Color.Black, Pieces, pathService, occupyService),
                new Rook(Size - 3, Color.Black, Pieces, pathService, occupyService),
                new Bishop(Size - 4, Color.Black, Pieces, pathService, occupyService),
                new Knight(Size - 5, Color.Black, Pieces, pathService, occupyService),
                new Pawn(Size - 6, Color.Black, Pieces, pathService, occupyService),
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