using System.Collections.Generic;

namespace Model.Pieces
{
    public class King : Piece
    {
        public King(int position, Color color, List<Piece> pieces) : base(position, color, pieces) { }

        public override bool CanMoveTo(int newPosition) =>
            PathAvailabilityService.IsKingPathAvailable(Position.Value, newPosition);
    }
}