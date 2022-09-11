using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(int position, Color color, List<Piece> pieces) : base(position, color, pieces)
        {
            
        }

        public override bool CanMoveTo(int newPosition) =>
         PathAvailabilityService.IsDiagonalPathAvailable(this, newPosition, Pieces);
    }
}