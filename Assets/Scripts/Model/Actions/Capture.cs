using Model.Pieces;

namespace Model.Actions
{
    public class Capture : GameAction
    {
        private readonly Piece _actor;
        private readonly Piece _forCapturing;

        public Capture(Piece actor, Piece forCapturing)
        {
            _actor = actor;
            _forCapturing = forCapturing;
        }
        
        public override void Do()
        {
            var newPosition = _forCapturing.Position.Value;
            _forCapturing.Capture();
            _actor.MoveTo(newPosition);
        }

        protected override string StringRepresentation() =>
            $"{_actor.Color} colored ${_actor.GetType().Name}" +
            $" capture {_forCapturing.GetType().Name}";
    }
}