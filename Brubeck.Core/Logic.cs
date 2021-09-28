using System;
using System.Linq;

namespace Brubeck.Core
{
    public class Logic
    {
        /// <summary>
        /// Logical NOT Gate.
        /// </summary>
        public static Qit NOT(Qit a) => (Qit)(-(sbyte)a);

        /// <summary>
        /// Qitwise logical NOT Gate.
        /// </summary>
        public static Qyte NOT(Qyte a) => new(a.Qits.Select(t => NOT(t)).ToArray());

        /// <summary>
        /// Logical AND Gate.
        /// </summary>
        public static Qit AND(Qit a, Qit b) => ((sbyte)a < (sbyte)b) ? a : b;

        /// <summary>
        /// Qitwise logical AND Gate.
        /// </summary>
        public static Qyte AND(Qyte a, Qyte b) => new(new Qit[]
        {
            AND(a.QitAtIndex(0), b.QitAtIndex(0)),
            AND(a.QitAtIndex(1), b.QitAtIndex(1)),
            AND(a.QitAtIndex(2), b.QitAtIndex(2))
        });

        /// <summary>
        /// Logical OR Gate.
        /// </summary>
        public static Qit OR(Qit a, Qit b) => ((sbyte)a > (sbyte)b) ? a : b;

        /// <summary>
        /// Qitwise logical OR Gate.
        /// </summary>
        public static Qyte OR(Qyte a, Qyte b) => new(new Qit[]
        {
            OR(a.QitAtIndex(0), b.QitAtIndex(0)),
            OR(a.QitAtIndex(1), b.QitAtIndex(1)),
            OR(a.QitAtIndex(2), b.QitAtIndex(2))
        });

        /// <summary>
        /// Logical XOR Gate.
        /// </summary>
        public static Qit XOR(Qit a, Qit b) => (Qit)(-1 * (sbyte)a * (sbyte)b / 2);

        /// <summary>
        /// Qitwise logical XOR Gate.
        /// </summary>
        public static Qyte XOR(Qyte a, Qyte b) => new(new Qit[]
        {
            XOR(a.QitAtIndex(0), b.QitAtIndex(0)),
            XOR(a.QitAtIndex(1), b.QitAtIndex(1)),
            XOR(a.QitAtIndex(2), b.QitAtIndex(2))
        });
    }
}
