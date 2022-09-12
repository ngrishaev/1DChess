﻿using System.Collections.Generic;

namespace Model.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(int position, Color color, List<Piece> pieces) : base(position, color, pieces)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return PathAvailabilityService.IsDiagonalPathAvailable(Position.Value, newPosition, Pieces) &&
                   OccupiedPositionStrategy.DefaultStrategy(this, newPosition, Pieces);
        }
    }
}