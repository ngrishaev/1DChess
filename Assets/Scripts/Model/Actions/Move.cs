using Model.Pieces;

namespace Model.Actions
{
    public class Move : GameAction
    {
        private readonly Piece _piece;
        private readonly int _position;

        public Move(Piece piece, int position)
        {
            _piece = piece;
            _position = position;
        }
        
        public override void Do()
        {
            _piece.MoveTo(_position);
        }

        protected override string StringRepresentation() => 
            $"{_piece.Color} colored ${_piece.GetType().Name} moves to {_position} position";
    }
}