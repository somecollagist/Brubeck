using System;
using System.Linq;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        //public Qit[] GetOperandsAsQitStream()
        //{
        //    /* Here's our method for converting the Qyte[] into a Qit[]:
        //     * - Convert each Qyte of operands into a string
        //     * - Turn this string[] into a string by joining with empties
        //     * - Convert each char of this string into a Qit by means of the Core converter.
        //     */
        //    return string.Join(
        //        string.Empty,
        //        Operands.Select(t => t.ToString()))
        //        .Select(t => QitConverter.GetQitFromChar(t))
        //        .ToArray();
        //}
    }
}
