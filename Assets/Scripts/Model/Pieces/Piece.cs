using System;
using System.Collections.Generic;
using Common;
using Model.Services;

namespace Model.Pieces
{
    public abstract class Piece
    {
        public readonly Color Color;
        
        protected readonly List<Piece> Pieces;
        protected readonly PathAvailabilityService PathService;
        protected readonly OccupiedPositionService OccupyService;
        
        public Maybe<int> Position { get; private set; }
        public bool Captured => Position.Exists == false;

        protected Piece(
            int position,
            Color color,
            List<Piece> pieces,
            PathAvailabilityService pathService,
            OccupiedPositionService occupyService)
        {
            if (position < 0)
                throw new ArgumentOutOfRangeException(
                    $"Piece position cant be lower than 0. Trying to create piece at {position} position");
            Pieces = pieces;

            Position = Maybe<int>.Yes(position);
            Color = color;
            OccupyService = occupyService;
            PathService = pathService;
        }

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