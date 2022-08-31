using System;
using System.Collections.Generic;
using Common;

namespace Game.Pieces
{
    public abstract class Piece
    {
        public Maybe<int> Position { get; private set; }
        public Color Color { get; }
        
        protected readonly List<Piece> Pieces;

        protected Piece(int position, Color color, List<Piece> pieces)
        {
            if (position < 0)
                throw new ArgumentOutOfRangeException(
                    $"Piece position cant be lower than 0. Trying to create piece at {position} position");
            Pieces = pieces;

            Position = Maybe<int>.Yes(position);
            Color = color;
        }

        public abstract bool CanMoveTo(int newPosition);

        public void MoveTo(int position) => Position = Maybe<int>.Yes(position);

        public void Capture() => Position = Maybe<int>.No();
    }
    
    public enum Color
    {
        Black,
        White
    }
}