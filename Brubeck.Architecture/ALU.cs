﻿using System;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        /// <summary>
        /// Wrapper for ALU circuitry.
        /// </summary>
        private static class ALU
        {
            /// <summary>
            /// Adds two inputs, returns their sum without carry out.
            /// </summary>
            public static Qit Sum(Qit a, Qit b)
            {
                return QitConverter.GetQitFromInt((int)Logic.OR(a, b) + (int)Logic.AND(a, b));
            }

            /// <summary>
            /// Adds two inputs, returns their sum with carry out.
            /// </summary>
            /// <returns>Sum of a and b, and their carryover.</returns>
            public static (Qit, Qit) HalfAdder(Qit a, Qit b)
            {
                Qit s = Sum(a, b);
                return (s, QitConverter.GetQitFromInt(((int)s) / 3));
            }

            /// <summary>
            /// Adds two numbers with a carry input, returns their sum with carry out.
            /// </summary>
            /// <param name="a">An input Qit.</param>
            /// <param name="b">An input Qit.</param>
            /// <param name="cin">Carry in.</param>
            /// <returns>Sum of a and b with carry in, and resulting carryover.</returns>
            public static (Qit, Qit) Add(Qit a, Qit b, Qit cin)
            {
                Qit x, y;
                (x, y) = HalfAdder(a, b);

                Qit s, n;
                (s, n) = HalfAdder(cin, x);

                return (s, Logic.OR(y, n));
            }
        }
    }
}
