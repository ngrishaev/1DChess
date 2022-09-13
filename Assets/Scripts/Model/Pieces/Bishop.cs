using System.Collections.Generic;
using Model.Services;

namespace Model.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(
            int position,
            Color color,
            List<Piece> pieces,
            PathAvailabilityService pathService,
            OccupiedPositionService occupyService
            )
            : base(position, color, pieces, pathService, occupyService)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathService.IsDiagonalPathAvailable(Position.Value, newPosition, Pieces) &&
                   OccupyService.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}