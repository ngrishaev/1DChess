using System;
using System.Collections.Generic;
using Common;

namespace Model.Pieces
{
    public abstract class Piece
    {
        protected readonly List<Piece> Pieces;
        protected PathAvailabilityService PathAvailabilityService;

        protected Piece(int position, Color color, List<Piece> pieces, PathAvailabilityService pathAvailabilityService)
        {
            if (position < 0)
                throw new ArgumentOutOfRangeException(
                    $"Piece position cant be lower than 0. Trying to create piece at {position} position");
            Pieces = pieces;

            Position = Maybe<int>.Yes(position);
            Color = color;
            PathAvailabilityService = pathAvailabilityService;
        }

        public Maybe<int> Position { get; private set; }
        public Color Color { get; }
        public bool Captured => Position.Exists == false;

        public abstract bool CanMoveTo(int newPosition);

        public virtual void MoveTo(int position)
        {
            Position = Maybe<int>.Yes(position);
        }

        public void Capture()
        {
            Position = Maybe<int>.No();
        }
    }

    public enum Color
    {
        Black,
        White
    }
}