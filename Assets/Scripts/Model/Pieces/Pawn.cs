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
            HaveAccessTo(newPosition) && 
            Pieces.All(piece => !(piece.Position.ValueEquals(newPosition) && piece.Color == Color));

        public override void MoveTo(int position)
        {
            base.MoveTo(position);
            _haveMoved = true;
        }

        private bool HaveAccessTo(int newPosition) =>
            HaveAccessTo(newPosition, _forwardIsRight? 1 : -1);

        private bool HaveAccessTo(int newPosition, int direction) =>
            Position.ValueEquals(newPosition - direction * 1) ||
            (!_haveMoved && Position.ValueEquals(newPosition - direction * 2));


    }
}