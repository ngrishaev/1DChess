using System;
using System.Collections.Generic;
using System.Linq;
using Model.Pieces;

namespace Model
{
    // TODO: Refactor static
    public static class PathAvailabilityService
    {
        public static bool IsStraightPathAvailable(Piece piece, int targetPosition, List<Piece> pieces) =>
            !pieces.Any(otherPiece => InBetween(otherPiece, piece.Position.Value, targetPosition));
        public static bool IsDiagonalPathAvailable(Piece piece, int targetPosition, List<Piece> pieces)
        {
            if (piece.Position.Value % 2 != targetPosition % 2)
                return false;
            return !pieces.Any(otherPiece => InBetweenDiagonal(otherPiece, piece.Position.Value, targetPosition));
        }

        private static bool InBetween(Piece piece, int firstPosition, int secondPosition)
        {
            if (piece.Captured)
                return false;

            return InBetweenChecked(piece, firstPosition, secondPosition);
        }

        private static bool InBetweenDiagonal(Piece piece, int firstPosition, int secondPosition)
        {
            if (firstPosition % 2 != secondPosition % 2)
                throw new ArgumentException("Incorrect target position for diagonal move");
            
            if (piece.Captured)
                return false;
            
            if (piece.Position.Value % 2 != firstPosition % 2)
                return false;

            return InBetweenChecked(piece, firstPosition, secondPosition);
        }

        private static bool InBetweenChecked(Piece piece, int firstPosition, int secondPosition)
        {
            var (min, max) = firstPosition < secondPosition
                ? (firstPosition, secondPosition)
                : (secondPosition, firstPosition);

            return piece.Position.Value > min && piece.Position.Value < max;
        }
    }
}