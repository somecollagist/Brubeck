using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brubeck.Assembler
{
    public partial class Assembler
    {
        private static class Utils
        {
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
        }
    }
}
