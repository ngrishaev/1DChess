using System.Collections.Generic;
using Model.Services;

namespace Model.Pieces
{
    public class King : Piece
    {
        public King(int position, Color color, List<Piece> pieces, PathAvailabilityService pathService) : 
            base(position, color, pieces, pathService)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathService.IsKingPathAvailable(Position.Value, newPosition)
                   && OccupiedPositionStrategy.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}