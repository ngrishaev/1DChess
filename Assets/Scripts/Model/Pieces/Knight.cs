using System.Collections.Generic;

namespace Model.Pieces
{
    public class Knight : Piece
    {
        public Knight(int position, Color color, List<Piece> pieces, PathAvailabilityService pathService) 
            : base(position, color, pieces, pathService)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathAvailabilityServiceStatic.IsKnightPathAvailable(Position.Value, newPosition)
                   && OccupiedPositionStrategy.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}