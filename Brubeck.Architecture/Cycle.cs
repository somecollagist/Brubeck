using System;
using System.Linq;

using Brubeck.Core;

namespace Brubeck.Architecture
{
	public partial class CPU
	{
		public ExecutionState Run(ref RAM InstMem, ref RAM DataMem,ref Qyte[] VideoFeed)
		{
			ExecutionState es;
			while ((es = Cycle(ref InstMem, ref DataMem, ref VideoFeed)) == ExecutionState.OK);
			return es;
		}

		private ExecutionState Cycle(ref RAM InstMem, ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			Qyte opcode = GetNextQyte(ref InstMem);

			try
			{
				//Raw opcodes
				if (opcode.QitAtIndex(0) == Qit.U)
				{
					switch (new string(opcode.Qits[1..3].Select(t => QitConverter.GetCharFromQit(t)).ToArray()))
					{
						case "AA": //JEQ
							RunConditionalJump(ComparisonMethods.Equal, ref InstMem);
							break;

						case "AE": //JNEQ
							RunConditionalJump(ComparisonMethods.NotEqual, ref InstMem);
							break;

						case "AI": //JMAGEQ
							RunConditionalJump(ComparisonMethods.MagEqual, ref InstMem);
							break;

						case "AO": //JMAGNEQ
							RunConditionalJump(ComparisonMethods.MagNotEqual, ref InstMem);
							break;

						case "AU": //JLT
							RunConditionalJump(ComparisonMethods.LessThan, ref InstMem);
							break;

						case "EA": //JGT
							RunConditionalJump(ComparisonMethods.GreaterThan, ref InstMem);
							break;

						case "EE": //JLTEQ
							RunConditionalJump(ComparisonMethods.LessThanOrEqual, ref InstMem);
							break;

						case "EI": //JGTEQ
							RunConditionalJump(ComparisonMethods.GreaterThanOrEqual, ref InstMem);
							break;

						case "EO": //JZ
							RunConditionalJump(ComparisonMethods.Zero, ref InstMem);
							break;

						case "EU": //JNZ
							RunConditionalJump(ComparisonMethods.NotZero, ref InstMem);
							break;

						case "IA": //JU
							RunUnconditionalJumpToLabel(ref InstMem);
							break;

						case "IE": //NOT
							Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)).NOT();
							break;

						case "II": //PUSH
							Push(Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)));
							break;

						case "IO": //PUSHALL
							PushAll();
							break;

						case "IU": //POP
							Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)).Qits = Pop();
							break;

						case "OA": //POPALL
							PopAll();
							break;

						case "OE": //CLEARFLAGS
							ClearFlags();
							break;

						case "OI": //INC
							Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)).Add(new("IIO"));
							break;

						case "OO": //DEC
							Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)).Add(new("IIE"));
							break;

						case "OU": //LABEL
							//This is a no-op, but is checked when jumped to
							break;

						case "UA": //SUBR
							//This is a no-op, but is checked when jumped to
							break;

						case "UE": //RETURN
							PopAll();
							InstMemAddr += 4;
							break;

						case "UI": //CALL
							PushAll();
							RunUnconditionalJumpToSubroutine(ref InstMem);
							break;

						case "UO": //HALT
							return ExecutionState.HLT;

						case "UU": //INT
							Console.Write("Interrupt: ");
							char chr = Console.ReadKey().KeyChar;
							Console.Write("\n");
							Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)).Qits = BIEn.Decode(chr).Qits;
							break;

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
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							Register.GetRegisterFromQyte(ops.Item1).Add(ops.Item2);
							break;

						case "AE": //SUB
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							Register.GetRegisterFromQyte(ops.Item1).Sub(ops.Item2);
							break;

						case "AI": //MUL
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							Register.GetRegisterFromQyte(ops.Item1).Mul(ops.Item2);
							break;

						case "AO": //DIV
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							Register.GetRegisterFromQyte(ops.Item1).Div(ops.Item2);
							break;

						case "AU": //MOD
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							Register.GetRegisterFromQyte(ops.Item1).Mod(ops.Item2);
							break;

						case "EA": //MOVLOC
							Qyte[] MOVLOCaddr = GetNextQytes(4, ref InstMem);
							SetDataMemAddr(ReadAddress(MOVLOCaddr));
							//Get a reference to the memory location to write to
							Qit[] MOVLOCdest = ReadDataMemoryLocation(MOVLOCaddr, ref DataMem).Qits;

							//Switch the adverb to find what should be written to the memory location
							//We use copy to so array references don't get shared between memory locations
							try
							{
								Qyte MOVLOCval = GetNextOperandRaw(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
								MOVLOCval.Qits.CopyTo(MOVLOCdest, 0);
							}
							catch (SegmentationFaultException) { return ExecutionState.ERR; }
							break;

						case "EE": //MOV
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							Register.GetRegisterFromQyte(ops.Item1).Qits = ops.Item2.Qits;
							break;

						case "EI": //AND
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							Register.GetRegisterFromQyte(ops.Item1).AND(ops.Item2);
							break;

						case "EO": //OR
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							Register.GetRegisterFromQyte(ops.Item1).OR(ops.Item2);
							break;

						case "EU": //XOR
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							Register.GetRegisterFromQyte(ops.Item1).XOR(ops.Item2);
							break;

						case "IA": //CMP
							
							//SetFlags(GetNextQyte(ref InstMem), GetNextQyte(ref InstMem));
							Qyte CMPreg = Register.GetRegisterFromQyte(GetNextQyte(ref InstMem));
							Qyte CMPop2;
							try
							{
								CMPop2 = GetNextOperandRaw(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							}
							catch (SegmentationFaultException) { return ExecutionState.ERR; }

							SetFlags(CMPreg, CMPop2);
							break;

						case "IE": //VRAMADD
							Qyte VRAMADDchr;
							try
							{
								VRAMADDchr = GetNextOperandRaw(opcode.QitAtIndex(0), ref InstMem, ref DataMem);
							}
							catch (SegmentationFaultException) { return ExecutionState.ERR; }

							WriteCharToVRAM(BIEn.GetMapFromCode(VRAMADDchr), ref DataMem, ref VideoFeed);
							break;

						case "II": //VRAMSUB
							Qit VRAMSUBflag = opcode.QitAtIndex(0);                //Prevents a double access
							if (VRAMSUBflag == Qit.I) return ExecutionState.OK;    //Allow continuation if qyte is null
							else
							{
								int max = QitConverter.GetIntFromQitArray(ALU.Abs(GetNextOperandRaw(VRAMSUBflag, ref InstMem, ref DataMem)).Qits); //Evaluated seperately to avoid overreading
								//Loops through for the value of the operand. It uses absolute value so negatives are ok.
								for (int x = 0; x < max; x++)
								{
									RemoveCharFromVRAM(ref DataMem, ref VideoFeed);
								}
							}
							break;

						case "IO": //SHIFT
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);

							if (!ALU.IsZero(ops.Item2))
							{
								for (int x = 0; x < QitConverter.GetIntFromQitArray(ops.Item2.Qits); x++)
								{
									Qit[] SHIFToriginal = Register.GetRegisterFromQyte(ops.Item1).Qits;
									if (ALU.IsGreaterThanZero(ops.Item2)) SHIFToriginal = new Qit[] { SHIFToriginal[1], SHIFToriginal[2], Qit.I };
									else SHIFToriginal = new Qit[] { Qit.I, SHIFToriginal[0], SHIFToriginal[1] };
								}
							}
							break;

						case "IU": //ROTATE
							ops = GetStandardOperands(opcode.QitAtIndex(0), ref InstMem, ref DataMem);

							if (!ALU.IsZero(ops.Item2))
							{
								for (int x = 0; x < QitConverter.GetIntFromQitArray(ops.Item2.Qits); x++)
								{
									Qit[] ROTATEoriginal = Register.GetRegisterFromQyte(ops.Item1).Qits;
									if (ALU.IsGreaterThanZero(ops.Item2)) ROTATEoriginal = new Qit[] { ROTATEoriginal[1], ROTATEoriginal[2], ROTATEoriginal[0] };
									else ROTATEoriginal = new Qit[] { ROTATEoriginal[2], ROTATEoriginal[0], ROTATEoriginal[1] };
								}
							}
							break;

						case "UA": //DWRITE
							switch (opcode.QitAtIndex(0))
							{
								case Qit.O:
									DataMem.QyteAtIndex(GetDataMemAddr()).Qits = Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)).Qits;
									break;

								default:
									break;
							}
							break;

						case "UE": //DREAD
							switch(opcode.QitAtIndex(0))
							{
								case Qit.O:
									Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)).Qits = DataMem.QyteAtIndex(GetDataMemAddr()).Qits;
									break;

								default:
									break;
							}
							break;

						case "UI": //DPSET
							switch (opcode.QitAtIndex(0))
							{
								case Qit.O:
									SetDataMemAddr(ReadAddress(GetNextQytes(4, ref InstMem)));
									break;

								default:
									break;
							}
							break;

						case "UO": //DPINC
							switch (opcode.QitAtIndex(0))
							{
								case Qit.O:
									IncDataMemAddr();
									break;

								default:
									break;
							}
							break;

						case "UU": //DPDEC
							switch (opcode.QitAtIndex(0))
							{
								case Qit.O:
									DecDataMemAddr();
									break;

								default:
									break;
							}
							break;

						default:
							return ExecutionState.ERR;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Emulator enountered a runtime error: {ex.GetType()}");
				Console.WriteLine($"\tMessage: {ex.Message}");
				Console.WriteLine($"\tSource: {ex.Source}");
				Console.WriteLine($"\tStack Trace: \n{string.Join('\n', ex.StackTrace.Split("\n").Select(t => "\t\t" + t.Trim()))}");
				return ExecutionState.ERR;
			}
			return ExecutionState.OK;
		}

		/// <summary>
		/// Obtains the two operands required for most operations.
		/// </summary>
		/// <param name="OptionFlag">First Qit of the opcode, which specifies if a register, memory location, or constant is used.</param>
		/// <param name="InstMem">Reference to Instruction Memory.</param>
		/// <param name="DataMem">Reference to Data Memory.</param>
		/// <returns>Qyte alias of target register, and the value of the second operand.</returns>
		private (Qyte, Qyte) GetStandardOperands(Qit OptionFlag, ref RAM InstMem, ref RAM DataMem)
		{
			Qyte[] operands;
			switch(OptionFlag)
			{
				case Qit.A: //Register
					operands = GetNextQytes(2, ref InstMem);
					return (operands[0], Register.GetRegisterFromQyte(operands[1]));

				case Qit.E: //Mem Loc
					operands = GetNextQytes(5, ref InstMem);
					return (operands[0], ReadDataMemoryLocation(operands[1..], ref DataMem));

				case Qit.O: //Const
					operands = GetNextQytes(2, ref InstMem);
					return (operands[0], operands[1]);

				default:
					return (null, null);
			}
		}

		/// <summary>
		/// Obtains the value of the next operand.
		/// </summary>
		/// <param name="OptionFlag">Opcode flag used to determine the nature of the operand.</param>
		/// <param name="InstMem">Reference to Instruction Memory.</param>
		/// <param name="DataMem">Reference to Data Memory.</param>
		/// <returns>The value of the next operand.</returns>
		private Qyte GetNextOperandRaw(Qit OptionFlag, ref RAM InstMem, ref RAM DataMem)
		{
			return OptionFlag switch
			{
				//Register
				Qit.A => Register.GetRegisterFromQyte(GetNextQyte(ref InstMem)),
				//Mem Loc
				Qit.E => ReadDataMemoryLocation(GetNextQytes(4, ref InstMem), ref DataMem),
				//Const
				Qit.O => GetNextQyte(ref InstMem),
				_ => throw new SegmentationFaultException(),
			};
		}

		/// <summary>
		/// Takes in an address and converts it to a RAM index.
		/// </summary>
		private static int ReadAddress(Qyte[] address)
		{
			return QitConverter.GetIntFromQitArray(
				new Qit[] { address[0].QitAtIndex(2) }
				.Concat(address[1].Qits)
				.Concat(address[2].Qits)
				.Concat(address[3].Qits)
				.ToArray()
				) + (RAM.RamCeiling / 2);
		}

		/// <summary>
		/// Reads the value at the specified address of data memory.
		/// </summary>
		/// <param name="address">Address to read from.</param>
		/// <param name="InstMem">Reference to Instruction Memory.</param>
		/// <param name="DataMem">Reference to Data Memory.</param>
		/// <returns>A reference to the Qyte at the specified address.</returns>
		private static ref Qyte ReadDataMemoryLocation(Qyte[] address, ref RAM DataMem)
		{
			return ref DataMem.QyteAtIndex(ReadAddress(address));
		}
	}
}
