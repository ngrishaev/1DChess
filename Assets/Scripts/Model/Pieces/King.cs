using System.Collections.Generic;
using Model.Services;

namespace Model.Pieces
{
    public class King : Piece
    {
        public King(
            int position,
            Color color,
            List<Piece> pieces,
            PathAvailabilityService pathService,
            OccupiedPositionService occupyService
            ) : 
            base(position, color, pieces, pathService, occupyService)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathService.IsKingPathAvailable(Position.Value, newPosition)
                   && OccupyService.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}