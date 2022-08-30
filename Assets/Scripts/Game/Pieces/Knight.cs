using System.Collections.Generic;
using System.Linq;

namespace Game.Pieces
{
    public class Knight : Piece
    {
        public Knight(int position, Color color, List<Piece> pieces) : base(position, color, pieces) { }

        public override bool CanMoveTo(int newPosition) =>
            HaveAccessTo(newPosition) && Pieces.All(piece => piece.Position.Value != newPosition && piece.Position.Value != newPosition);
        
        private bool HaveAccessTo(int newPosition) =>
            (newPosition == Position.Value + 2 ||
             newPosition == Position.Value + 3 ||
             newPosition == Position.Value - 2 ||
             newPosition == Position.Value - 3);
    }
}