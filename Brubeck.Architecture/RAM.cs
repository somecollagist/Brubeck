using System;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    /// <summary>
    /// Wrapper for RAM.
    /// </summary>
    public class RAM
    {
        /// <summary>
        /// Maximum number of RAM Addresses. Each RAM address contains one Qyte.
        /// </summary>
        public const int RamCeiling = 9765625;

#pragma warning disable IDE0044
        /// <summary>
        /// One-Dimensional array of Qytes with size RamCeiling.
        /// </summary>
        private Qyte[] Memory = new Qyte[RamCeiling];
#pragma warning restore IDE0044

        /// <summary>
        /// Initialises RAM as an array of null Qytes.
        /// </summary>
        public RAM()
        {
            Array.Fill(Memory, new());
        }

        /// <summary>
        /// Gets the Qyte at the specified index.
        /// </summary>
        /// <param name="index">The index of the Qyte (must be between 0 and RamCeiling inclusive).</param>
        /// <returns>Reference to the specified Qyte.</returns>
        public ref Qyte QyteAtIndex(int index)
        {
            if (index < 0 || index > RamCeiling) throw new IllegalIndexException($"No such RAM index '{index}' exists");
            return ref Memory[index];
        }
    }
}
