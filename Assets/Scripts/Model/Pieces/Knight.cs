using System.Collections.Generic;
using System.Linq;

namespace Model.Pieces
{
    public class Knight : Piece
    {
        public Knight(int position, Color color, List<Piece> pieces) : base(position, color, pieces) { }

        public override bool CanMoveTo(int newPosition) =>
            PathAvailabilityService.IsKnightPathAvailable(Position.Value, newPosition);

    }
}