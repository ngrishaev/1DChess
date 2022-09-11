using System.Collections.Generic;

namespace Model.Pieces
{
    public class Rook : Piece
    {
        public Rook(int position, Color color, List<Piece> pieces) : base(position, color, pieces)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathAvailabilityService.IsStraightPathAvailable(this, newPosition, Pieces);
        }
    }
}
