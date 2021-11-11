using System;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        /// <summary>
        /// An extension of Qyte with specific methods for rapid temporary memory storage.
        /// </summary>
        public class Register : Qyte
        {
            /// <summary>
            /// Gets a register with the specified Qyte Alias.
            /// </summary>
            /// <param name="addr">Qyte alias of the Register (between IEA and IEU or IOA and IOU).</param>
            /// <returns>Reference to the specified Register.</returns>
            public static ref Register GetRegisterFromQyte(Qyte addr)
            {
                /* Hi there future Johann! There's a neat little thing where we can use return switch x {...};,
                 * and we could maybe use it here, but we gotta consider these refs, a=so it might not work.
                 * Since it's almost 21:00 and I've bene putting off this assembler work for ages, maybe look
                 * into it for me sometime? Cheers commander. P.S. they're called ref switch expressions
                 */

                //This is just a map of qyte values to references to respective Registers, what more d'you want?
                switch (addr.ToString())
                {
                    case "IEA":
                        return ref R0;
                    case "IEE":
                        return ref R1;
                    case "IEI":
                        return ref R2;
                    case "IEO":
                        return ref R3;
                    case "IEU":
                        return ref R4;
                    case "IOA":
                        return ref R5;
                    case "IOE":
                        return ref R6;
                    case "IOI":
                        return ref R7;
                    case "IOO":
                        return ref R8;
                    case "IOU":
                        return ref R9;
                    default:
                        throw new ComponentNonExistentException($"No Register can be accessed from Qyte '{addr}'");
                }
            }

            /// <summary>
            /// Adds a value to the Register.
            /// </summary>
            /// <param name="alpha">Value to add.</param>
            /// <param name="carry">Carry Qit.</param>
            public void Add(Qyte alpha, Qit carry = Qit.I) => Qits = ALU.Add(this, alpha, carry).Item1.Qits;

            /// <summary>
            /// Subtracts a value from the Register.
            /// </summary>
            /// <param name="alpha">Value to subtract by.</param>
            /// <param name="carry">Carry Qit.</param>
            public void Sub(Qyte alpha, Qit carry = Qit.I) => Qits = ALU.Add(this, Logic.NOT(alpha), carry).Item1.Qits;

            /// <summary>
            /// Multiplies the Register by a given value.
            /// </summary>
            /// <param name="alpha">Value to multiply by.</param>
            public void Mul(Qyte alpha) => Qits = ALU.Multiply(this, alpha, Qit.I).Item1.Qits;

            /// <summary>
            /// Divides the Register by a given value.
            /// </summary>
            /// <param name="alpha">Value to divide by.</param>
            public void Div(Qyte alpha) => Qits = ALU.Divide(this, alpha).Item1.Qits;
        }
    }
}
