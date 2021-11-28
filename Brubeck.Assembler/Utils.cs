using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

			public static string ConvertCharToCode(char c)
			{
				return c switch
				{
					'A' => "IIO",
					'B' => "IIU",
					'C' => "IOA",
					'D' => "IOE",
					'E' => "IOI",
					'F' => "IOO",
					'G' => "IOU",
					'H' => "IUA",
					'I' => "IUE",
					'J' => "IUI",
					'K' => "IUO",
					'L' => "IUU",
					'M' => "OAA",
					'N' => "OAE",
					'O' => "OAI",
					'P' => "OAO",
					'Q' => "OAU",
					'R' => "OEA",
					'S' => "OEE",
					'T' => "OEI",
					'U' => "OEO",
					'V' => "OEU",
					'W' => "OIA",
					'X' => "OIE",
					'Y' => "OII",
					'Z' => "OIO",

					'a' => "IIE",
					'b' => "IIA",
					'c' => "IEU",
					'd' => "IEO",
					'e' => "IEI",
					'f' => "IEE",
					'g' => "IEA",
					'h' => "IAU",
					'i' => "IAO",
					'j' => "IAI",
					'k' => "IAE",
					'l' => "IAA",
					'm' => "EUU",
					'n' => "EUO",
					'o' => "EUI",
					'p' => "EUE",
					'q' => "EUA",
					'r' => "EOU",
					's' => "EOO",
					't' => "EOI",
					'u' => "EOE",
					'v' => "EOA",
					'w' => "EIU",
					'x' => "EIO",
					'y' => "EII",
					'z' => "EIE",

					')' => "OIU",
					']' => "OOA",
					'}' => "OOE",
					'>' => "OOI",

					'(' => "EIA",
					'[' => "EEU",
					'{' => "EEO",
					'<' => "EEI",

					'0' => "OOO",
					'1' => "OOU",
					'2' => "OUA",
					'3' => "OUE",
					'4' => "OUI",
					'5' => "OUO",
					'6' => "OUU",
					'7' => "UAA",
					'8' => "UAE",
					'9' => "UAI",

					' ' => "EEE",
					'.' => "EEA",
					',' => "EAU",
					'?' => "EAO",
					'!' => "EAI",
					':' => "EAE",
					';' => "EAA",
					'\''=> "AUU",
					'"' => "AUO",
					'°' => "AUI",

					'+' => "UAO",
					'*' => "UAU",
					'&' => "UEA",
					'/' => "UEE",
					'@' => "UEI",
					'|' => "UEO",
					'^' => "UEU",
					'£' => "UIA",

					'-' => "AUE",
					'=' => "AUA",
					'#' => "AOU",
					'\\'=> "AOO",
					'%' => "AOI",
					'_' => "AOE",
					'~' => "AOA",
					'$' => "AIU",

					_ => throw new AssemblySegmentationFault()
				};
			}
		}
	}
}
