using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Pieces
{
    public class Queen : Piece
    {
        public Queen(int position, Color color, List<Piece> pieces) : base(position, color, pieces)
        {
        }

        public override bool CanMoveTo(int newPosition) =>
            PathAvailabilityService.IsStraightPathAvailable(this, newPosition, Pieces) ||
            PathAvailabilityService.IsDiagonalPathAvailable(this, newPosition, Pieces);
    }
}