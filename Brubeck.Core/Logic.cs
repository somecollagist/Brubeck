using System;

namespace Brubeck.Core
{
    public class Logic
    {
        public static Qit NOT(Qit a) => (Qit)(-(sbyte)a);

        public static Qit AND(Qit a, Qit b) => ((sbyte)a < (sbyte)b) ? a : b;

        public static Qit OR(Qit a, Qit b) => ((sbyte)a > (sbyte)b) ? a : b;

        public static Qit XOR(Qit a, Qit b) => (Qit)(-1 * (sbyte)a * (sbyte)b / 2);
    }
}
