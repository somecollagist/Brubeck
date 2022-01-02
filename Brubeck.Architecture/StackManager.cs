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
		/// Allocates a section of memory to be used as the stack.
		/// </summary>
		/// <param name="stackStartAddr"></param>
		/// <param name="stackSize"></param>
		public void AllocStack(int stackStartAddr, int stackSize)
		{
			Console.WriteLine($"Stack Start Address: %{string.Join("", QitConverter.GetQitArrayFromInt(stackStartAddr-(RAM.RamCeiling / 2)).Select(t => t.ToString()))} == {stackStartAddr}");
			StackStartAddr = stackStartAddr;
			StackSize = stackSize;
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
		public void Push(Qyte qyte, ref RAM DataMem)
		{
			if (StackPointer >= StackSize) throw new SystemOutOfMemoryException("Stack Overflow");
			DataMem.Memory[StackStartAddr - StackPointer].Qits = qyte.Qits;
			StackPointer++;
		}

		/// <summary>
		/// Pops a qyte from the stack.
		/// </summary>
		/// <returns>The top item from the stack, as a Qit array.</returns>
		/// <exception cref="SystemOutOfMemoryException">Thrown if attempting to pop from an empty stack.</exception>
		public Qit[] Pop(ref RAM DataMem)
		{
			StackPointer--;
			if (StackPointer < 0) throw new SystemOutOfMemoryException("Stack Underflow");
			return DataMem.Memory[StackStartAddr - StackPointer].Qits;
		}

		/// <summary>
		/// Pushes all register values and the current instruction memory address to the stack.
		/// </summary>
		public void PushAll(ref RAM DataMem)
		{
			Push(R0, ref DataMem);
			Push(R1, ref DataMem);
			Push(R2, ref DataMem);
			Push(R3, ref DataMem);
			Push(R4, ref DataMem);
			Push(R5, ref DataMem);
			Push(R6, ref DataMem);
			Push(R7, ref DataMem);
			Push(R8, ref DataMem);
			Push(R9, ref DataMem);
			Qit[] instmemaddrqits = QitConverter.GetQitArrayFromInt(InstMemAddr.GetAddr() - (RAM.RamCeiling / 2), 12);
			instmemaddrqits[0] = ZFlag;
			instmemaddrqits[1] = CFlag;
			Qyte[] instmemaddr = Enumerable
									.Range(0, 4).Select(t => new Qyte(instmemaddrqits[(t * 3)..((t + 1) * 3)]))
									.ToArray();
			foreach(Qyte qyte in instmemaddr) Push(qyte, ref DataMem);
		}

		/// <summary>
		/// Pops the next 14 items from the stack and loads the corresponding values into the current instruction memory address and registers.
		/// </summary>
		public void PopAll(ref RAM DataMem)
		{
			List<Qit> instmemaddrqits = new();
			for(int x = 0; x < 4; x++)
			{
				instmemaddrqits.AddRange(Pop(ref DataMem).Reverse());
			}
			instmemaddrqits = instmemaddrqits.Reverse<Qit>().ToList();
			ZFlag = instmemaddrqits[0];
			CFlag = instmemaddrqits[1];
			InstMemAddr.SetAddr(instmemaddrqits.ToArray()[2..]);
			R9.Qits = Pop(ref DataMem);
			R8.Qits = Pop(ref DataMem);
			R7.Qits = Pop(ref DataMem);
			R6.Qits	= Pop(ref DataMem);
			R5.Qits = Pop(ref DataMem);
			R4.Qits = Pop(ref DataMem);
			R3.Qits = Pop(ref DataMem);
			R2.Qits = Pop(ref DataMem);
			R1.Qits = Pop(ref DataMem);
			R0.Qits = Pop(ref DataMem);
		}
	}
}
