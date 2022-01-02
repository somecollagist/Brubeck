using System;
using System.Linq;
using System.Threading.Tasks;

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
        public static readonly int RamCeiling = 9765625;  //This is 5^10

        /// <summary>
        /// One-Dimensional array of Qytes with size RamCeiling.
        /// </summary>
        public Qyte[] Memory { get; protected set; } = new Qyte[RamCeiling];

        /// <summary>
        /// Initialises RAM as an array of null Qytes.
        /// </summary>
        public RAM()
        {
            //We use this instead of Array.Fill() so each of the memory locations point to seperate Qyte references.
            Memory = Enumerable.Range(0, RamCeiling).Select(t => new Qyte()).ToArray();
        }

        /// <summary>
        /// Gets the Qyte at the specified index.
        /// </summary>
        /// <param name="index">The index of the Qyte, must be between 0 and the RAM Ceiling.</param>
        /// <returns>Reference to the specified Qyte.</returns>
        public ref Qyte QyteAtIndex(int index)
        {
            if (index < 0 || index > RamCeiling) throw new IllegalIndexException($"No such RAM index '{index}' exists");
            return ref Memory[index];
        }

        /// <summary>
        /// Sets the current instance's Memory to the given Qyte array.
        /// </summary>
        /// <remarks>This method overwrites preexisting RAM states.</remarks>
        /// <param name="state">The Memory state to flash onto RAM.</param>
        public Task FlashRAMState(Qyte[] state)
        {
            Array.Copy(state, Memory, state.Length);
            return Task.CompletedTask;
        }
    }
}
