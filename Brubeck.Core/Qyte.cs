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
		/// Returns an uppercase string that represents the Qyte.
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

		/// <summary>
		/// Leftshifts a Qyte.
		/// </summary>
		/// <param name="input">The Qit to input into the rightmost index.</param>
		/// <returns>The original leftmost Qit.</returns>
		public Qit LeftShift(Qit input = Qit.I)
		{
			Qit ret = Qits[0];
			Qits = new Qit[] { Qits[1], Qits[2], input };
			return ret;
		}

		/// <summary>
		/// Rightshifts a Qyte.
		/// </summary>
		/// <param name="input">The Qit to input into the leftmost index.</param>
		/// <returns>the original rightmost Qit.</returns>
		public Qit RightShift(Qit input = Qit.I)
		{
			Qit ret = Qits[2];
			Qits = new Qit[] { input, Qits[0], Qits[1] };
			return ret;
		}


		/// <summary>
		/// Checks if this Qyte has the same value as another Qyte.
		/// </summary>
		public bool Equals(Qyte qyte)
		{
			//this gets overflowed because of how objects aren't equal with hashcodes
			return QitAtIndex(0) == qyte.QitAtIndex(0)
				&& QitAtIndex(1) == qyte.QitAtIndex(1)
				&& QitAtIndex(2) == qyte.QitAtIndex(2);
		}
	}
}
