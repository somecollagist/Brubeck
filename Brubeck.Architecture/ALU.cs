using System;

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
            /// Calculates the carryover of the addition of two inputs. 
            /// </summary>
            public static Qit SumOverflow(Qit a, Qit b)
            {
                return QitConverter.GetQitFromInt(((int)Logic.OR(a, b) + (int)Logic.AND(a, b)) / 3);
            }
            
            /// <summary>
            /// Adds two inputs, returns their sum with carry out.
            /// </summary>
            /// <returns>Sum of a and b, and their carryover.</returns>
            public static (Qit, Qit) HalfAdder(Qit a, Qit b)
            {
                return (Sum(a,b), SumOverflow(a,b));
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

            /// <summary>
            /// Adds two numbers with a carry input, returns their sum with carry out.
            /// </summary>
            /// <param name="a">An input Qyte.</param>
            /// <param name="b">An input Qyte.</param>
            /// <param name="cin">Carry in.</param>
            /// <returns>Sum of a and b with carry in, and resulting carryover.</returns>
            public static (Qyte, Qit) Add(Qyte a, Qyte b, Qit cin)
            {
                Qit s = cin;
                Qit[] gen = new Qit[3];
                for(int x = 2; x >= 0; x--)
                {
                    (gen[x], s) = Add(a.QitAtIndex(x), b.QitAtIndex(x), s);
                }
                return (new Qyte(gen), s);
            }

            /// <summary>
            /// Multiplies two inputs, returns their product wihtout carry out.
            /// </summary>
            public static Qit Prod(Qit a, Qit b)
            {
                return QitConverter.GetQitFromInt((int)Logic.OR(a, b) * (int)Logic.AND(a, b));
            }

            /// <summary>
            /// Calculates the carryover of the multiplicatoin of two inputs.
            /// </summary>
            public static Qit ProdOverflow(Qit a, Qit b)
            {
                return QitConverter.GetQitFromInt(((int)Logic.OR(a, b) * (int)Logic.AND(a, b)) / 3);
            }

            /// <summary>
            /// Multiplies two inputs, returns their product with carry out.
            /// </summary>
            /// <returns>Product of a and b, and thier carryover.</returns>
            public static (Qit, Qit) HalfMultiplier(Qit a, Qit b)
            {
                return (Prod(a, b), ProdOverflow(a, b));
            }

            /// <summary>
            /// Multiplies two numbers with a carry input, returns thier product with carry out.
            /// </summary>
            /// <param name="a">An input Qit.</param>
            /// <param name="b">An input Qit.</param>
            /// <param name="cin">Carry in.</param>
            /// <returns>Product of a md b with carry in, and resulting carryover.</returns>
            public static (Qit, Qit) Multiply(Qit a, Qit b, Qit cin)
            {
                Qit x, y;
                (x, y) = HalfMultiplier(a, b);

                Qit s, n;
                (s, n) = HalfAdder(cin, x);

                return (s, Add(y, n, Qit.I).Item1);
            }

            /// <summary>
            /// Multiplies two numbers with a carry input, returns their product with carry out.
            /// </summary>
            /// <param name="a">An input Qyte.</param>
            /// <param name="b">An input Qyte.</param>
            /// <param name="cin">Carry in.</param>
            /// <returns>Product of a and b with carry in, and resulting carryover.</returns>
            public static (Qyte, Qit) Multiply(Qyte a, Qyte b, Qit cin)
            {
                Qit s = cin;
                Qit[,] gen = new Qit[3, 5] {
                    { Qit.I, Qit.I, Qit.I, Qit.I, Qit.I },
                    { Qit.I, Qit.I, Qit.I, Qit.I, Qit.I },
                    { Qit.I, Qit.I, Qit.I, Qit.I, Qit.I }
                };

                for(int x = 0; x <= 2; x++)
                {
                    for(int y = 2; y >= 0; y--)
                    {
                        (gen[x, y-x+2], s) = Multiply(a.QitAtIndex(y), b.QitAtIndex(2-x), s);
                    }
                    s = Qit.I;
                }

                Qit[] newgen = new Qit[3];
                for(int x = 4; x >= 2; x--)
                {
                    (newgen[x - 2], s) = Add(gen[0, x], gen[1, x], gen[2, x]);
                    (newgen[x - 2], s) = Add(newgen[x - 2], s, Qit.I);
                }

                return (new Qyte(newgen), s);
            }
        }
    }
}
