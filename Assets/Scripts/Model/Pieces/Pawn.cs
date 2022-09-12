using System.Collections.Generic;

namespace Model.Pieces
{
    public class Pawn : Piece
    {
        private readonly bool _forwardIsRight;
        private bool _haveMoved;

        public Pawn(
            int position,
            Color color,
            List<Piece> pieces,
            PathAvailabilityService pathService 
        )
            : base(position, color, pieces, pathService)
        {
            _forwardIsRight = color == Color.White;
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathAvailabilityServiceStatic.PawnPath(Position.Value, newPosition, _haveMoved, _forwardIsRight, Pieces) &&
                   OccupiedPositionStrategy.DefaultStrategy(this, newPosition, Pieces);
        }

        public override void MoveTo(int position)
        {
            base.MoveTo(position);
            _haveMoved = true;
        }
    }
}