using System;
using System.Collections.Generic;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    /// <summary>
    /// Base CPU Class
    /// </summary>
    public partial class CPU
    {
        /// <summary>
        /// Current Memory Address of RAM being read.
        /// </summary>
        private int MemAddr;

        /// <summary>
        /// Getter for Current Memory Address.
        /// </summary>
        public int GetMemAddr() => MemAddr;
        /// <summary>
        /// Setter for Current Memory Address.
        /// </summary>
        public void SetMemAddr(int value) => MemAddr = value is < 0 or >= RAM.RamCeiling
            ? throw new IllegalIndexException($"Memory Address cannot be set to value {value} because it is either less than 0 or greater than the established RAM Ceiling.")
            : value;

        /// <summary>
        /// Increments Current Memory Address. Exceeding the RAM Ceiling sets the address to 0.
        /// </summary>
        public void IncMemAddr() => MemAddr = ++MemAddr == RAM.RamCeiling ? 0 : MemAddr;
        /// <summary>
        /// Decrements Current Memory Address. Going below 0 sets the address to the RAM Ceiling.
        /// </summary>
        public void DecMemAddr() => MemAddr = MemAddr-- == 0 ? RAM.RamCeiling - 1 : MemAddr;

        /// <summary>
        /// Returns the value of the next Qyte in Memory.
        /// </summary>
        /// <param name="Memory">Reference to the current memory being used.</param>
        public Qyte GetNextQyte(ref RAM Memory)
        {
            IncMemAddr();
            return Memory.QyteAtIndex(MemAddr - 1);
        }

        /// <summary>
        /// Returns the values of the next number of Qytes in memory;
        /// </summary>
        /// <param name="count">Number of qytes to read.</param>
        /// <param name="Memory">Reference to the current memory being used.</param>
        public Qyte[] GetNextQytes(int count, ref RAM Memory)
        {
            Qyte[] qytes = new Qyte[count];
            for (int x = 0; x < count; x++) qytes[x] = GetNextQyte(ref Memory);
            return qytes;
        }

#pragma warning disable CA2211
        /// <summary>
        /// A general purpose register.
        /// </summary>
        public static Register R0, R1, R2, R3, R4, R5, R6, R7, R8, R9;
#pragma warning restore CA2211

        private enum ExecutionState
        {
            /// <summary>
            /// No CPU fault, execution was successful.
            /// </summary>
            OK,
            /// <summary>
            /// CPU encountered an error, execution was unsuccessful.
            /// </summary>
            ERR,
            /// <summary>
            /// CPU was instructed to stop.
            /// </summary>
            HLT
        }
    }
}
