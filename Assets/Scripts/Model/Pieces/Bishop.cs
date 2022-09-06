﻿using System;
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
            HaveAccessTo(newPosition) && 
            Pieces.All(piece => !(piece.Position.ValueEquals(newPosition) && piece.Color == Color));

        private bool HaveAccessTo(int newPosition)
        {
            if (newPosition == Position.Value)
                return false;
            
            if (newPosition % 2 != Position.Value % 2)
                return false;
            
            if (Pieces.Any(piece => InBetweenDiagonal(piece, Position.Value, newPosition)))
                return false;
            return true;
        }

        private bool InBetweenDiagonal(Piece piece, int firstPosition, int secondPosition)
        {
            if (firstPosition % 2 != secondPosition % 2)
                throw new ArgumentException("Incorrect target position for diagonal move");
            
            if (piece.Captured)
                return false;
            
            if (piece.Position.Value % 2 != firstPosition % 2)
                return false;
            
            var (min, max) = firstPosition < secondPosition
                ? (firstPosition, secondPosition)
                : (secondPosition, firstPosition);

            return piece.Position.Value > min && piece.Position.Value < max;
        }
    }
}