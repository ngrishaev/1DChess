namespace Model.Actions
{
    public abstract class GameAction
    {
        public abstract void Do();

        public sealed override string ToString()
        {
            return StringRepresentation();
        }

        protected abstract string StringRepresentation();
    }
}