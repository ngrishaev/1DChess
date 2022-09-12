using System.Collections.Generic;

namespace Model.Pieces
{
    public class Knight : Piece
    {
        public Knight(int position, Color color, List<Piece> pieces) : base(position, color, pieces)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathAvailabilityService.IsKnightPathAvailable(Position.Value, newPosition)
                   && OccupiedPositionStrategy.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}