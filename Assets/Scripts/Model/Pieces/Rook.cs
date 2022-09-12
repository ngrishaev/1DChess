using System.Collections.Generic;
using Model.Services;

namespace Model.Pieces
{
    public class Rook : Piece
    {
        public Rook(int position, Color color, List<Piece> pieces, PathAvailabilityService pathService) 
            : base(position, color, pieces, pathService)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathService.IsStraightPathAvailable(Position.Value, newPosition, Pieces) &&
                   OccupiedPositionStrategy.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}