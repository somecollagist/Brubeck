using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Brubeck.Architecture;
using Brubeck.Core;

namespace Brubeck.Assembler
{
	public partial class Assembler
	{
		private static class Utils
		{
			public static string UppercaseByRegex(string t, Regex regex)
			{
				char[] output = t.ToCharArray();
				MatchCollection matches = regex.Matches(t);
				foreach(Match match in matches)
				{
					for(int x = match.Index; x < match.Index + match.Length; x++)
					{
						output[x] = char.ToUpper(output[x]);
					}
				}
				return new string(output);
			}

			/// <summary>
			/// Returns if the given string contains only A,E,I,O,U.
			/// </summary>
			public static bool IsVowelRestricted(string str)
			{
				return str.All(t => new char[] { 'A', 'E', 'I', 'O', 'U' }.Contains(t));
			}

			/// <summary>
			/// Returns the three-qit alias of the given register number.
			/// </summary>
			/// <param name="num">The number of the corresponding register in assembly.</param>
			public static string GetRegisterAlias(int num)
			{
				return num switch
				{
					0 => "IEA",
					1 => "IEE",
					2 => "IEI",
					3 => "IEO",
					4 => "IEU",
					5 => "IOA",
					6 => "IOE",
					7 => "IOI",
					8 => "IOO",
					9 => "IOU",
					_ => throw new AssemblySegmentationFault()
				};
			}

			/// <summary>
			/// Computes the address of an instruction given how many Qytes precede it.
			/// </summary>
			/// <param name="offset">Number of Qytes preceding an instruction.</param>
			/// <returns>The Address of an instruction.</returns>
			public static string GetAddress(int offset)
			{
				return new string(QitConverter.GetQitArrayFromInt(offset - (RAM.RamCeiling / 2), 12).Select(x => QitConverter.GetCharFromQit(x)).ToArray());
			}
		}
	}
}
