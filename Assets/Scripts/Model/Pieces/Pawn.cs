using System.Collections.Generic;
using System.Linq;

namespace Model.Pieces
{
    public class Pawn : Piece
    {
        private readonly bool _forwardIsRight;
        private bool _haveMoved = false;

        public Pawn(int position, Color color, List<Piece> pieces, bool forwardIsRight) : base(position, color, pieces)
            => _forwardIsRight = forwardIsRight;

        public override bool CanMoveTo(int newPosition) =>
            PathAvailabilityService.PawnPath(Position.Value, newPosition, _haveMoved, _forwardIsRight,  Pieces);
        
        public override void MoveTo(int position)
        {
            base.MoveTo(position);
            _haveMoved = true;
        }
    }
}