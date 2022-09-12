using System.Collections.Generic;

namespace Model.Pieces
{
    public class King : Piece
    {
        public King(int position, Color color, List<Piece> pieces) : base(position, color, pieces)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathAvailabilityService.IsKingPathAvailable(Position.Value, newPosition)
                   && OccupiedPositionStrategy.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}