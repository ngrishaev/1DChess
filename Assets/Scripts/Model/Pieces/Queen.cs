using System.Collections.Generic;

namespace Model.Pieces
{
    public class Queen : Piece
    {
        public Queen(int position, Color color, List<Piece> pieces) : base(position, color, pieces)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return (PathAvailabilityService.IsStraightPathAvailable(Position.Value, newPosition, Pieces) ||
                    PathAvailabilityService.IsDiagonalPathAvailable(Position.Value, newPosition, Pieces)) &&
                   OccupiedPositionStrategy.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}