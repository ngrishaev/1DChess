using System.Collections.Generic;
using Model.Services;

namespace Model.Pieces
{
    public class Queen : Piece
    {
        public Queen(
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
            return (PathService.IsStraightPathAvailable(Position.Value, newPosition, Pieces) ||
                    PathService.IsDiagonalPathAvailable(Position.Value, newPosition, Pieces)) &&
                   OccupyService.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}