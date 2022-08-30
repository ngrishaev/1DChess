using System.Collections.Generic;
using System.Linq;

namespace Game.Pieces
{
    public class King : Piece
    {
        public King(int position, Color color, List<Piece> pieces) : base(position, color, pieces) { }

        public override bool CanMoveTo(int newPosition) => 
            HaveAccessTo(newPosition) && Pieces.All(piece => piece.Position.Value != newPosition && piece.Position.Value != newPosition);

        private bool HaveAccessTo(int newPosition)
        {
            return (newPosition == Position.Value + 1 || newPosition == Position.Value - 1);
        }
    }
}