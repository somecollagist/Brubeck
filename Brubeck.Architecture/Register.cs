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
        }
    }
}
