using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Brubeck.Core;

namespace Brubeck.Architecture
{
    public partial class CPU
    {
        /// <summary>
        /// An extension of Register with specific modifications to handle 10-qit words relating to RAM indices.
        /// </summary>
        public class AddressRegister : Register
        {
            /// <summary>
            /// Sets the address regsister's value to null.
            /// </summary>
            public AddressRegister() { }

            /// <summary>
            /// Sets the address register's value to the given state.
            /// </summary>
            /// <exception cref="IllegalConstructionException">Thrown if the state is not a 10-qit array.</exception>
            public AddressRegister(Qit[] state)
            {
                if (state.Length != 10)
                {
                    throw new IllegalConstructionException("An address register can only be constructed with a state of length 10 Qits.");
                }
                else Qits = state;
            }

            /// <summary>
            /// Array of Qits contained within the Register (10 Qits)
            /// </summary>
            public override Qit[] Qits { get; set; } = new Qit[10];

            /// <summary>
            /// Integer representation of the index.
            /// </summary>
            /// <remarks>This is zero based, and is not truly reflective of the state of the Qits property.</remarks>
            private int Addr;

            /// <summary>
            /// Getter for the integer memory address.
            /// </summary>
            public int GetAddr() => Addr;

            /// <summary>
            /// Setter for the memory address.
            /// </summary>
            /// <param name="value">Integer value to set the memory address to.</param>
            /// <exception cref="IllegalIndexException">Thrown if the index is less than zero or greater than RAM.RamCeiling</exception>
            public void SetAddr(int value)
            {
                if (value < 0 || value >= RAM.RamCeiling)
                    throw new IllegalIndexException($"A Memory Address cannot be set to value {value} because it is either less than 0 or greater than the established RAM Ceiling.");
                else
                {
                    Addr = value;
                    Qits = QitConverter.GetQitArrayFromInt(value-(RAM.RamCeiling / 2), 10);
                }
            }

            /// <summary>
            /// Setter for the memory address.
            /// </summary>
            /// <param name="value">Qit array to set the memory address to.</param>
            /// <exception cref="IllegalConstructionException">Thrown if the qit array does not have a length of 10.</exception>
            public void SetAddr(Qit[] value)
            {
                if (value.Length != 10)
                {
                    throw new IllegalConstructionException("An address register can only be constructed with a state of length 10 Qits.");
                }
                else
                {
                    Addr = QitConverter.GetIntFromQitArray(value)+(RAM.RamCeiling / 2);
                    Qits = value;
                }
            }

            /// <summary>
            /// Increments the memory Address. Exceeding the RAM Ceiling sets the address to 0.
            /// </summary>
            public void IncAddr()
            {
                Addr = ++Addr == RAM.RamCeiling ? 0 : Addr;
                Qits = QitConverter.GetQitArrayFromInt(Addr - (RAM.RamCeiling / 2), 10);
            }

            /// <summary>
            /// Decrements the memory Address. Going below 0 sets the address to the RAM Ceiling.
            /// </summary>
            public void DecAddr()
            {
                Addr = Addr-- == 0 ? RAM.RamCeiling - 1 : Addr;
                Qits = QitConverter.GetQitArrayFromInt(Addr - (RAM.RamCeiling / 2), 10);
            }

            /// <summary>
            /// Leftshifts an address register.
            /// </summary>
            /// <param name="input">The Qit to input into the rightmost index.</param>
            /// <returns>The original leftmost Qit.</returns>
            public override Qit LeftShift(Qit input = Qit.I)
            {
                Qit ret = Qits[0];
                Qits = new Qit[] { Qits[1], Qits[2], Qits[3], Qits[4], Qits[5], Qits[6], Qits[7], Qits[8], Qits[9], input };
                return ret;
            }

            /// <summary>
            /// Rightshifts an address register.
            /// </summary>
            /// <param name="input">The Qit to input into the leftmost index.</param>
            /// <returns>the original rightmost Qit.</returns>
            public override Qit RightShift(Qit input = Qit.I)
            {
                Qit ret = Qits[9];
                Qits = new Qit[] { input, Qits[0], Qits[1], Qits[2], Qits[3], Qits[4], Qits[5], Qits[6], Qits[7], Qits[8] };
                return ret;
            }
        }
    }
}
