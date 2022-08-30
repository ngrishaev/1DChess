using Game.Pieces;

namespace Game.Actions
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
        
        public override void Process()
        {
            _piece.MoveTo(_position);
        }
    }

    public class Capture : GameAction
    {
        private readonly Piece _actor;
        private readonly Piece _forCapturing;

        public Capture(Piece actor, Piece forCapturing)
        {
            _actor = actor;
            _forCapturing = forCapturing;
        }
        
        public override void Process()
        {
            _forCapturing.Capture();
        }
    }

    public abstract class GameAction
    {
        public abstract void Process();
    }
}