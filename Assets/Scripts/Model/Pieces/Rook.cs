﻿using System.Collections.Generic;

namespace Model.Pieces
{
    public class Rook : Piece
    {
        public Rook(int position, Color color, List<Piece> pieces) : base(position, color, pieces)
        {
        }

        public override bool CanMoveTo(int newPosition) => 
            PathAvailabilityService.IsStraightPathAvailable(Position.Value, newPosition, Pieces);
    }
}
