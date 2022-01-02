using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Brubeck;
using Brubeck.Core;

namespace Brubeck.Architecture
{
	public partial class CPU
	{
		/// <summary>
		/// Zero equalilty flag.
		/// </summary>
		public Qit ZFlag { get; private set; } = Qit.I;

		/// <summary>
		/// Comparator flag.
		/// </summary>
		public Qit CFlag { get; private set; } = Qit.I;

		private static partial class ALU
		{
			/// <summary>
			/// Returns if the given Qyte is zero (III).
			/// </summary>
			public static bool IsZero(Qyte a)
			{
				return new Qyte("III").Equals(a);
			}

			/// <summary>
			/// Returns if the given Qyte is greater than zero (III).
			/// </summary>
			/// <param name="a">Qyte to compare against zero.</param>
			public static bool IsGreaterThanZero(Qyte a)
			{
				return IsAGreaterThanB(a, new Qyte());
			}

			/// <summary>
			/// Returns if one Qyte is greater than the other.
			/// </summary>
			/// <returns>True if the first qyte is greater than the second, false otherwise.</returns>
			public static bool IsAGreaterThanB(Qyte a, Qyte b)
			{
				return QitConverter.GetIntFromQitArray(a.Qits) > QitConverter.GetIntFromQitArray(b.Qits);
			}
		}

		/// <summary>
		/// Compares two qytes and sets the Z and C flags to reflect their relationship.
		/// </summary>
		/// <param name="a">The first qyte to compare.</param>
		/// <param name="b">The second qyte to compare.</param>
		public void SetFlags(Qyte a, Qyte b)
		{
			//ZFlag logic

			if(!a.Equals(b))
			{
				if (!ALU.IsZero(a)) ZFlag = Qit.A;				//a != b, a != 0
				else ZFlag = Qit.E;								//a != b, a == 0
			}
			else
			{
				if (!ALU.IsZero(a)) ZFlag = Qit.O;				//a == b, a != 0
				else ZFlag = Qit.U;								//a == b, a == 0
			}

			//CFlag logic

			if(a.Equals(Logic.NOT(b)))
			{
				if(!ALU.IsGreaterThanZero(a)) CFlag = Qit.A;	//a = -1 * b, a < 0
				else CFlag = Qit.U;								//a = -1 * b, a > 0
			}
			else
			{
				//These states are overridden by the above two
				if (!ALU.IsAGreaterThanB(a, b)) CFlag = Qit.E;	//a < b
				else CFlag = Qit.O;								//a > b
			}
		}

		/// <summary>
		/// Clears the values of the Z and C flags.
		/// </summary>
		public void ClearFlags()
		{
			ZFlag = CFlag = Qit.I;
		}

		/// <summary>
		/// Enum of all possible types of comparison.
		/// </summary>
		private enum ComparisonMethods
		{
			Equal,
			NotEqual,
			MagEqual,
			MagNotEqual,
			LessThan,
			GreaterThan,
			LessThanOrEqual,
			GreaterThanOrEqual,
			Zero,
			NotZero
		}

		/// <summary>
		/// Checks the cpu flags and returns the truth of a comparison.
		/// </summary>
		/// <param name="method">Method to use in comparison.</param>
		/// <returns>If a comparison is true or not.</returns>
		/// <exception cref="ComparisonException">Thrown if the flags haven't been set or if an illegal comparison method is used.</exception>
		private bool Compare(ComparisonMethods method)
		{
			if (ZFlag == Qit.I || CFlag == Qit.I) throw new ComparisonException("Z and C flags are set to I (cannot be compared)");

			return method switch
			{
				ComparisonMethods.Equal =>
					(ZFlag is Qit.O or Qit.U),
				ComparisonMethods.NotEqual =>
					(ZFlag is Qit.A or Qit.E),
				ComparisonMethods.MagEqual =>
					(ZFlag is Qit.O or Qit.U) || (CFlag is Qit.A or Qit.U),
				ComparisonMethods.MagNotEqual =>
					(ZFlag is Qit.A or Qit.E) || (CFlag is Qit.E or Qit.O),
				ComparisonMethods.LessThan =>
					(ZFlag is Qit.A or Qit.E) && (CFlag is Qit.A or Qit.E),
				ComparisonMethods.GreaterThan =>
					(ZFlag is Qit.A or Qit.E) && (CFlag is Qit.O or Qit.U),
				ComparisonMethods.LessThanOrEqual =>
					(ZFlag is Qit.O or Qit.U) || (CFlag is Qit.A or Qit.E),
				ComparisonMethods.GreaterThanOrEqual =>
					(ZFlag is Qit.O or Qit.U) || (CFlag is Qit.O or Qit.U),
				ComparisonMethods.Zero =>
					(ZFlag is Qit.A or Qit.U),
				ComparisonMethods.NotZero =>
					(ZFlag is Qit.E or Qit.O),
				_ => throw new ComparisonException($"Illegal comparison method {method}")
			};
		}

		private void RunConditionalJump(ComparisonMethods method, ref RAM InstMem)
		{
			Qyte[] addr = GetNextQytes(4, ref InstMem);

			if(Compare(method))
			{
				InstMemAddr.SetAddr(ReadAddress(addr));
				if (InstMem.QyteAtIndex(InstMemAddr.GetAddr()).Equals(new("UOU"))) return;
				throw new IllegalIndexException($"Jump to location {InstMemAddr} is not permitted in this context.");
			}
		}
		
		private void RunUnconditionalJumpToLabel(ref RAM InstMem)
		{
			InstMemAddr.SetAddr(ReadAddress(GetNextQytes(4, ref InstMem)));
			if (InstMem.QyteAtIndex(InstMemAddr.GetAddr()).Equals(new("UOU"))) return;
			throw new IllegalIndexException($"Jump to location {InstMemAddr} is not permitted in this context.");
		}

		private void RunUnconditionalJumpToSubroutine(ref RAM InstMem)
		{
			InstMemAddr.SetAddr(ReadAddress(GetNextQytes(4, ref InstMem)));
			if (InstMem.QyteAtIndex(InstMemAddr.GetAddr()).Equals(new("UUA"))) return;
			throw new IllegalIndexException($"Jump to location {InstMemAddr} is not permitted in this context.");
		}
	}
}
