using System;

namespace Brubeck.Core
{
	/// <summary>
	/// Identifier for a Qit (Bit equivalent).
	/// </summary>
	public enum Qit : sbyte
	{
		A = -2,
		E,
		I,
		O,
		U
	}

	/// <summary>
	/// Wrapper for Converting to and from Qits to other data types.
	/// </summary>
	public static class QitConverter
	{
		/// <summary>
		/// Converts a char to its corresponding Qit value.
		/// </summary>
		/// <param name="value">Character to be converted (may be upper or lower case).</param>
		/// <returns>Converted char.</returns>
		public static Qit GetQitFromChar(char value)
		{
			return value switch
			{
				'A' or 'a' => Qit.A,
				'E' or 'e' => Qit.E,
				'I' or 'i' => Qit.I,
				'O' or 'o' => Qit.O,
				'U' or 'u' => Qit.U,
				_ => throw new IllegalConstructionException($"Illegal Qit constructor character '{value}', code: {(short)value}")
			};
		}

		/// <summary>
		/// Converts a Qit to its corresponding char value.
		/// </summary>
		/// <param name="value">Qit to be converted.</param>
		/// <returns>Qit converted to an upper case char.</returns>
		public static char GetCharFromQit(Qit value)
        {
			/* While this technically isn't exclusive, it doesn't
			 * matter because Qits don't marshall to values outside
			 * of this range, hence our #pragma directives.
			 */
#pragma warning disable CS8524
			return value switch
			{
				Qit.A => 'A',
				Qit.E => 'E',
				Qit.I => 'I',
				Qit.O => 'O',
				Qit.U => 'U',
			};
        }
#pragma warning restore CS8524

		/// <summary>
		/// Converts an int to its corresponding Qit value.
		/// </summary>
		/// <param name="value">Int to be converted (may be out of normal range of -2 to +2).</param>
		/// <returns>Converted int.</returns>
		public static Qit GetQitFromInt(int value)
		{
			/*
			 * This is used as an overflow and underflow safety
			 * mechanism, in the event that a number out of the
			 * normal range of a Qit is passed as value, this method
			 * can force the value into its conversion range (-2 to +2),
			 * allowing for overflow and underflow errors (i.e 3 becomes
			 * -2, -3 becomes 2, etc.)
			 */
			while (value > 2) value -= 5;	//If above 2, loop down
			while (value < -2) value += 5;	//If below 2, loop up
			return value switch
			{
				-2 => Qit.A,
				-1 => Qit.E,
				 0 => Qit.I,
				 1 => Qit.O,
				 2 => Qit.U,
				 _ => throw new IllegalConstructionException($"Illegal Qit constructor integer '{value}'")
			};
		}
	}
}
