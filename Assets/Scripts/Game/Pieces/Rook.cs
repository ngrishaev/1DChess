﻿using System.Collections.Generic;
using System.Linq;

namespace Game.Pieces
{
    public class Rook : Piece
    {
        public Rook(int position, Color color, List<Piece> pieces) : base(position, color, pieces)
        {
        }

        public override bool CanMoveTo(int newPosition)
        {
            return HaveAccessTo(newPosition)
                   && Pieces.All(piece => piece.Position.Value != newPosition && piece.Position.Value != newPosition);
        }

        private bool HaveAccessTo(int newPosition)
        {
            if (newPosition == Position.Value)
                return false;
            if (Pieces.Any(piece => InBetween(piece, Position.Value, newPosition)))
                return false;
            return true;
        }

        private bool InBetween(Piece piece, int firstPosition, int secondPosition)
        {
            var (min, max) = firstPosition < secondPosition
                ? (firstPosition, secondPosition)
                : (secondPosition, firstPosition);

            return piece.Position.Value > min && piece.Position.Value < max;
        }
    }
}
