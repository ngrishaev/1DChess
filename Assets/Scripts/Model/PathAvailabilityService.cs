using System;
using System.Collections.Generic;
using System.Linq;
using Model.Pieces;

namespace Model
{
    // TODO: Refactor static
    public static class PathAvailabilityService
    {
        public static bool IsStraightPathAvailable(int from, int to, List<Piece> pieces) =>
            !pieces.Any(otherPiece => InBetweenStraight(otherPiece, from, to));

        public static bool IsDiagonalPathAvailable(int from, int to, List<Piece> pieces)
        {
            if (from % 2 != to % 2)
                return false;
            
            return !pieces.Any(otherPiece => InBetweenDiagonal(otherPiece, from, to));
        }

        public static bool IsKingPathAvailable(int from, int to) =>
            to == from + 1 || to == from - 1;

        public static bool IsKnightPathAvailable(int positionValue, int newPosition) =>
            positionValue == newPosition - 2 ||
            positionValue == newPosition - 3 ||
            positionValue == newPosition + 3 ||
            positionValue == newPosition + 2;

        public static bool PawnPath(int from, int to, bool pawnAlreadyMoved, bool rightIsForward, List<Piece> pieces)
        {
            var firstPosition = rightIsForward ? from + 1 : from -1; 
            if (to == firstPosition)
                return true;
            
            var secondPosition = rightIsForward ? from + 2 : from -2;
            if (to != secondPosition || pawnAlreadyMoved)
                return false;
            return PawnPath(from, to, true, rightIsForward, pieces) ||
                   IsStraightPathAvailable(from, to, pieces);
        }

        private static bool InBetweenStraight(Piece piece, int firstPosition, int secondPosition)
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