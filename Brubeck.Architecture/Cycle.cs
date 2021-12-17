using System;
using System.Linq;

using Brubeck.Core;

#pragma warning disable CS8509
namespace Brubeck.Architecture
{
	public partial class CPU
	{
		public ExecutionState Run(ref RAM InstMem, ref RAM DataMem,ref Qyte[] VideoFeed)
		{
			ExecutionState es;
			while ((es = Cycle(ref InstMem, ref DataMem, ref VideoFeed)) == ExecutionState.OK) ;
			return es;
		}

		private ExecutionState Cycle(ref RAM InstMem, ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			Qyte opcode = GetNextQyte(ref InstMem);
			Console.WriteLine(opcode.ToString());

			//Raw opcodes
			if (opcode.QitAtIndex(0) == Qit.U)
			{
				switch(new string(opcode.Qits[1..3].Select(t => QitConverter.GetCharFromQit(t)).ToArray()))
				{
					case "EA": //NOT
						Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)).NOT();
						break;

					case "II": //HALT
						return ExecutionState.HLT;

					default:
						return ExecutionState.ERR;
				}
			}
			else
			{
				(Qyte, Qyte) ops; //We define ops inside the cases because not every opcode uses ops in the defualt way and the method alters the state of DataMem
				switch (new string(opcode.Qits[1..3].Select(t => QitConverter.GetCharFromQit(t)).ToArray()))
				{
					case "AA": //ADD
						ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
						Register.GetRegisterFromQyte(ops.Item1).Add(ops.Item2);
						break;

					case "AE": //SUB
						ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
						Register.GetRegisterFromQyte(ops.Item1).Sub(ops.Item2);
						break;

					case "AI": //MUL
						ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
						Register.GetRegisterFromQyte(ops.Item1).Mul(ops.Item2);
						break;

					case "AO": //DIV
						ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
						Register.GetRegisterFromQyte(ops.Item1).Div(ops.Item2);
						break;

					case "AU": //MOD
						ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
						Register.GetRegisterFromQyte(ops.Item1).Mod(ops.Item2);
						break;

					case "EE": //AND
						ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
						Register.GetRegisterFromQyte(ops.Item1).AND(ops.Item2);
						break;

					case "EI": //OR
						ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
						Register.GetRegisterFromQyte(ops.Item1).OR(ops.Item2);
						break;

					case "EO": //XOR
						ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
						Register.GetRegisterFromQyte(ops.Item1).XOR(ops.Item2);
						break;

					case "EU": //MOVLOC
						//Get a reference to the memory location to write to
						Qit[] dest = DataMem.QyteAtIndex(
							QitConverter.GetIntFromQitArray(
								new Qit[] { GetNextQyte(ref InstMem).QitAtIndex(2) }
								.Concat(GetNextQyte(ref InstMem).Qits)
								.Concat(GetNextQyte(ref InstMem).Qits)
								.Concat(GetNextQyte(ref InstMem).Qits)
								.ToArray()
								) + (RAM.RamCeiling / 2)
							).Qits;

						//Switch the adverb to find what should be written to the memory location
						//We use copy to so array references don't get shared between memory locations
						switch(opcode.QitAtIndex(0))
						{
							case Qit.A:	//Another register
								Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)).Qits.CopyTo(dest, 0);
								break;

							case Qit.E:	//Another memory location
								DataMem.QyteAtIndex(
										QitConverter.GetIntFromQitArray(
											new Qit[] { GetNextQyte(ref InstMem).QitAtIndex(2) }
											.Concat(GetNextQyte(ref InstMem).Qits)
											.Concat(GetNextQyte(ref InstMem).Qits)
											.Concat(GetNextQyte(ref InstMem).Qits)
											.ToArray()
										) + (RAM.RamCeiling / 2)).Qits.CopyTo(dest, 0);
								break;

							case Qit.O:	//A constant
								GetNextQyte(ref InstMem).Qits.CopyTo(dest, 0);
								break;
						};
						break;

					case "IA": //MOV
						ops = GetOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
						Register.GetRegisterFromQyte(ops.Item1).Qits = ops.Item2.Qits;
						break;

					case "II":
						if (opcode.QitAtIndex(0) == Qit.I) return ExecutionState.OK;    //Allow continuation if qyte is null
						else return ExecutionState.ERR;                                 //Otherwise it's a segmentation fault

					case "OO": //VRAMADD
						Qyte chr = GetNextQyte(ref InstMem);
						switch(opcode.QitAtIndex(0))
						{
							case Qit.A: //Register
								chr = Register.GetRegisterFromQyte(chr);
								break;

							case Qit.E: //Mem Loc
								chr = DataMem.QyteAtIndex(
									QitConverter.GetIntFromQitArray(
										new Qit[] { chr.QitAtIndex(2) }
										.Concat(GetNextQyte(ref InstMem).Qits)
										.Concat(GetNextQyte(ref InstMem).Qits)
										.Concat(GetNextQyte(ref InstMem).Qits)
										.ToArray()
										) + (RAM.RamCeiling / 2)
									);
								break;
						}
						WriteCharToVRAM(BIEn.GetMapFromCode(chr), ref DataMem, ref VideoFeed);
						break;

					case "OU": //VRAMSUB
						RemoveCharFromVRAM(ref DataMem, ref VideoFeed);
						break;

					default:
						return ExecutionState.ERR;
				}
			}
			return ExecutionState.OK;
		}

		/// <summary>
		/// Obtains the two operands required for most operations.
		/// </summary>
		/// <param name="OptionFlag">First Qit of the opcode, which specifies if a register, memory location, or constant is used.</param>
		/// <param name="Memory">Reference to current memory.</param>
		/// <returns>Qyte alias of target register, and the value of the second operand.</returns>
		private (Qyte, Qyte) GetOperands(Qit OptionFlag, ref RAM InstMem, ref RAM DataMem)
		{
			Qyte[] operands;
			switch(OptionFlag)
			{
				case Qit.A: //Register
					operands = GetNextQytes(2, ref InstMem);
					return (operands[0], Register.GetRegisterFromQyte(operands[1]));

				case Qit.E: //Mem Loc
					operands = GetNextQytes(5, ref InstMem);
					return (operands[0],
							DataMem.QyteAtIndex(
									QitConverter.GetIntFromQitArray(
										new Qit[] { operands[1].QitAtIndex(2) }
										.Concat(operands[2].Qits)
										.Concat(operands[3].Qits)
										.Concat(operands[4].Qits)
										.ToArray()) + (RAM.RamCeiling / 2)
									));

				case Qit.O: //Const
					operands = GetNextQytes(2, ref InstMem);
					return (operands[0], operands[1]);

				default:
					return (null, null);
			}
		}
	}
}
