using System;

namespace Brubeck.Core
{
    public class IllegalConstructionException : Exception
    {
        public IllegalConstructionException() : base() { }
        public IllegalConstructionException(string message) : base(message) { }
        public IllegalConstructionException(string message, Exception inner) : base(message, inner) { }
    }

    public class IllegalIndexException : Exception
    {
        public IllegalIndexException() : base() { }
        public IllegalIndexException(string message) : base(message) { }
        public IllegalIndexException(string message, Exception inner) : base(message, inner) { }
    }

    public class ComponentNonExistentException : Exception
    {
        public ComponentNonExistentException() : base() { }
        public ComponentNonExistentException(string message) : base(message) { }
        public ComponentNonExistentException(string message, Exception inner) : base(message, inner) { }
    }
}
