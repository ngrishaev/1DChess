using System;

namespace Common
{
    public class Maybe<T>
    {
        private T _value;
        
        public T Value
        {
            get => Exists ? _value : throw new InvalidOperationException("Can't get non existing value");
            private set => _value = value;
        }
        public bool Exists { get; }
        
        private Maybe(T value, bool exists)
        {
            Value = value;
            Exists = exists;
        }
        
        public static Maybe<T> No() => new Maybe<T>(default(T), false);
        public static Maybe<T> Yes(T value) => new Maybe<T>(value, true);
        public bool ValueEquals(Maybe<T> other) => Exists && other.Exists && other.Value.Equals(Value);
        public bool ValueEquals(T value) => Exists && value.Equals(Value);
        
        public override string ToString()
        {
            if (Exists)
                return Value.ToString();
            return "Doesnt exists";
        }
    }
}