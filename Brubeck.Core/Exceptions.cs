using System;

namespace Brubeck.Core
{
    /// <summary>
    /// Thrown when a constructor is passed an illegal argument.
    /// </summary>
    public class IllegalConstructionException : Exception
    {
        public IllegalConstructionException() : base() { }
        public IllegalConstructionException(string message) : base(message) { }
        public IllegalConstructionException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Thrown when attempting to access an item at a non-existent index.
    /// </summary>
    public class IllegalIndexException : Exception
    {
        public IllegalIndexException() : base() { }
        public IllegalIndexException(string message) : base(message) { }
        public IllegalIndexException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Thrown when an illegal operation is attempted.
    /// </summary>
    public class IllegalOperationException : Exception
    {
        public IllegalOperationException() : base() { }
        public IllegalOperationException(string message) : base(message) { }
        public IllegalOperationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Thrown when attepmting to access a non-existent component.
    /// </summary>
    public class ComponentNonExistentException : Exception
    {
        public ComponentNonExistentException() : base() { }
        public ComponentNonExistentException(string message) : base(message) { }
        public ComponentNonExistentException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Thrown when an unknown opcode is passed.
    /// </summary>
    public class UnknownOpcodeException : Exception
    {
        public UnknownOpcodeException() : base() { }
        public UnknownOpcodeException(string message) : base(message) { }
        public UnknownOpcodeException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Thrown when a data stream cannot be properly segmented.
    /// </summary>
    public class SegmentationFaultException : Exception
    {
        public SegmentationFaultException() : base() { }
        public SegmentationFaultException(string message) : base(message) { }
        public SegmentationFaultException(string message, Exception inner) : base(message, inner) { }
    }

    public class ALUOperationException : Exception
    {
        public ALUOperationException() : base() { }
        public ALUOperationException(string message) : base(message) { }
        public ALUOperationException(string message, Exception inner) : base(message, inner) { }
    }
}
