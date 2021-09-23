using System;
using System.Linq;

namespace Brubeck.Core
{
    /// <summary>
    /// Primitive for a Qyte (Byte equivalent)
    /// </summary>
    public class Qyte
    {
        /// <summary>
        /// Array of Qits contained within the Qyte (3 Qits)
        /// </summary>
        public Qit[] Qits { get; set; } = new Qit[3];

        /// <summary>
        /// Creates a null Qyte (III).
        /// </summary>
        public Qyte()
        {
            Array.Fill(Qits, new());
        }

        /// <summary>
        /// Creates a Qyte with the specified value.
        /// </summary>
        /// <param name="qits">Value to assign to the Qyte. Only the first 3 Qytes will be used to assign the value.</param>
        public Qyte(Qit[] qits)
        {
            Array.Copy(qits, Qits, 3);
        }

        /// <summary>
        /// Creates a Qyte with the specified value.
        /// </summary>
        /// <param name="gen">Value to assign to the Qyte. Must be a string containing only vowels (upper or lower case)</param>
        public Qyte(string gen)
        {
            Qits = gen.ToCharArray().Select(t => QitConverter.GetQitFromChar(t)).ToArray()[0..3];
        }

        /// <summary>
        /// Returns an uppercase string that represents the object.
        /// </summary>
        public override string ToString()
        {
            return new string(Qits.Select(t => QitConverter.GetCharFromQit(t)).ToArray());
        }

        /// <summary>
        /// Gets the Qit at the specified index.
        /// </summary>
        /// <param name="index">The index of the Qit (must be between 0 and 2 inclusive).</param>
        /// <returns>Reference to the specified Qit.</returns>
        public ref Qit QitAtIndex(int index)
        {
            if (index < 0 || index > 2) throw new IllegalIndexException($"Cannot access Qit at index {index}");
            return ref Qits[index];
        }
    }
}
