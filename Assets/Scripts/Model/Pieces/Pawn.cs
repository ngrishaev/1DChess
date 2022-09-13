using System.Collections.Generic;
using Model.Services;

namespace Model.Pieces
{
    public class Pawn : Piece
    {
        public readonly bool ForwardIsRight;
        public bool WasMoved { get; private set; }

        public Pawn(int position,
            Color color,
            List<Piece> pieces,
            PathAvailabilityService pathService,
            OccupiedPositionService occupyService
            )
            : base(position, color, pieces, pathService, occupyService)
        {
            ForwardIsRight = color == Color.White;
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathService.IsPawnPathAvailable(Position.Value, newPosition, WasMoved, ForwardIsRight, Pieces) &&
                   OccupyService.PawnOccupyTarget(this, newPosition, Pieces);
        }

        public override void MoveTo(int position)
        {
            base.MoveTo(position);
            WasMoved = true;
        }
    }
}