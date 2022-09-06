using System.Collections.Generic;
using System.Linq;

namespace Model.Pieces
{
    public class Pawn : Piece
    {
        private readonly bool _forwardIsRight;

        public Pawn(int position, Color color, List<Piece> pieces, bool forwardIsRight) : base(position, color, pieces)
            => _forwardIsRight = forwardIsRight;

        public override bool CanMoveTo(int newPosition) =>
            HaveAccessTo(newPosition) && 
            Pieces.All(piece => !(piece.Position.ValueEquals(newPosition) && piece.Color == Color));

        private bool HaveAccessTo(int newPosition) =>
            _forwardIsRight ? Position.ValueEquals(newPosition - 1) : Position.ValueEquals(newPosition + 1);
    }
}