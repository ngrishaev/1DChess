using System.Collections.Generic;

namespace Model.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(int position, Color color, List<Piece> pieces, PathAvailabilityService pathService)
            : base(position, color, pieces, pathService)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathAvailabilityServiceStatic.IsDiagonalPathAvailable(Position.Value, newPosition, Pieces) &&
                   OccupiedPositionStrategy.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}