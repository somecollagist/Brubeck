using System;

namespace Brubeck.Core
{
	public enum Qit : sbyte
	{
		A = -2,
		E,
		I,
		O,
		U
	}

	public static class QitConverter
	{
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

		public static char GetCharFromQit(Qit value)
        {
			//While this technically isn't exclusive, it doesn't matter because Qits don't marshall to values outside of this range.
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

		public static Qit GetQitFromInt(int value)
		{
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
