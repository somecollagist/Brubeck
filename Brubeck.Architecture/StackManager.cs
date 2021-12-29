using System;
using System.Collections.Generic;
using System.Linq;

using Brubeck.Core;

namespace Brubeck.Architecture
{
	public partial class CPU
	{
		/// <summary>
		/// Start Address of the stack.
		/// </summary>
		/// <remarks>Stack memory works backwards, so the end address will precede the start address. It is recommended that the start address immediately precede VRAM.</remarks>
		public int StackStartAddr { get; private set; } = 0;

		/// <summary>
		/// Size of the stack in qytes/addresses.
		/// </summary>
		private int StackSize = 0;

		/// <summary>
		/// A reference array of the current stack.
		/// </summary>
		private Qyte[] Stack;

		/// <summary>
		/// Allocates a section of memory to be used as the stack.
		/// </summary>
		/// <param name="stackStartAddr"></param>
		/// <param name="stackSize"></param>
		public void AllocStack(int stackStartAddr, int stackSize, ref RAM DataMem)
		{
			StackStartAddr = stackStartAddr;
			StackSize = stackSize;
			Stack = DataMem.Memory[(StackStartAddr - StackSize)..StackStartAddr];
			Array.Reverse(Stack);
		}

		/// <summary>
		/// Pointer to the top item on the stack.
		/// </summary>
		private int StackPointer = 0;

		/// <summary>
		/// Pushes a Qyte to the stack.
		/// </summary>
		/// <param name="qyte">Value to push to the stack.</param>
		/// <exception cref="SystemOutOfMemoryException">Thrown if attempting to push beyond allocated stack memory.</exception>
		public void Push(Qyte qyte)
		{
			if (StackPointer >= StackSize) throw new SystemOutOfMemoryException("Stack Overflow");
			Stack[StackPointer].Qits = qyte.Qits;
			StackPointer++;
		}

		/// <summary>
		/// Pops a qyte from the stack.
		/// </summary>
		/// <returns>The top item from the stack, as a Qit array.</returns>
		/// <exception cref="SystemOutOfMemoryException">Thrown if attempting to pop from an empty stack.</exception>
		public Qit[] Pop()
		{
			StackPointer--;
			if (StackPointer < 0) throw new SystemOutOfMemoryException("Stack Underflow");
			return Stack[StackPointer].Qits;
		}

		/// <summary>
		/// Pushes all register values and the current instruction memory address to the stack.
		/// </summary>
		public void PushAll()
		{
			Push(R0);
			Push(R1);
			Push(R2);
			Push(R3);
			Push(R4);
			Push(R5);
			Push(R6);
			Push(R7);
			Push(R8);
			Push(R9);
			Qit[] instmemaddrqits = QitConverter.GetQitArrayFromInt(InstMemAddr, 12);
			instmemaddrqits[0] = ZFlag;
			instmemaddrqits[1] = CFlag;
			Qyte[] instmemaddr = Enumerable
									.Range(0, 4).Select(t => new Qyte(instmemaddrqits[(t * 3)..((t + 1) * 3)]))
									.ToArray();
			foreach(Qyte qyte in instmemaddr) Push(qyte);
		}

		/// <summary>
		/// Pops the next 14 items from the stack and loads the corresponding values into the current instruction memory address and registers.
		/// </summary>
		public void PopAll()
		{
			List<Qit> instmemaddrqits = new();
			for(int x = 0; x < 4; x++)
			{
				instmemaddrqits.AddRange(Pop().Reverse());
			}
			instmemaddrqits = instmemaddrqits.Reverse<Qit>().ToList();
			ZFlag = instmemaddrqits[0];
			CFlag = instmemaddrqits[1];
			InstMemAddr = QitConverter.GetIntFromQitArray(instmemaddrqits.ToArray()[2..]);
			R9.Qits = Pop();
			R8.Qits = Pop();
			R7.Qits = Pop();
			R6.Qits	= Pop();
			R5.Qits = Pop();
			R4.Qits = Pop();
			R3.Qits = Pop();
			R2.Qits = Pop();
			R1.Qits = Pop();
			R0.Qits = Pop();
		}
	}
}
