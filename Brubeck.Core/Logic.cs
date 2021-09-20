using System;

namespace Brubeck.Core
{
    public class Logic
    {
        /// <summary>
        /// Logical NOT Gate.
        /// </summary>
        /// <returns>NOT(a).</returns>
        public static Qit NOT(Qit a) => (Qit)(-(sbyte)a);

        /// <summary>
        /// Logical AND Gate.
        /// </summary>
        /// <returns>AND(a,b).</returns>
        public static Qit AND(Qit a, Qit b) => ((sbyte)a < (sbyte)b) ? a : b;

        /// <summary>
        /// Logical OR Gate.
        /// </summary>
        /// <returns>OR(a,b).</returns>
        public static Qit OR(Qit a, Qit b) => ((sbyte)a > (sbyte)b) ? a : b;

        /// <summary>
        /// Logical XOR Gate.
        /// </summary>
        /// <returns>XOR(a,b).</returns>
        public static Qit XOR(Qit a, Qit b) => (Qit)(-1 * (sbyte)a * (sbyte)b / 2);
    }
}
