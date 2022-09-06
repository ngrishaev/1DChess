using System.Collections.Generic;
using System.Linq;

namespace Model.Pieces
{
    public class Knight : Piece
    {
        public Knight(int position, Color color, List<Piece> pieces) : base(position, color, pieces) { }

        public override bool CanMoveTo(int newPosition) =>
            HaveAccessTo(newPosition) && 
            Pieces.All(piece => !(piece.Position.ValueEquals(newPosition) && piece.Color == Color)); 
        
        
        private bool HaveAccessTo(int newPosition) =>
            Position.ValueEquals(newPosition - 2) ||
            Position.ValueEquals(newPosition - 3) ||
            Position.ValueEquals(newPosition + 3) ||
            Position.ValueEquals(newPosition + 2);
    }
}