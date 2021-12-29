using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Brubeck.Core;
using Brubeck.Peripheral;

namespace Brubeck.Architecture
{
	public partial class CPU
	{
		/// <summary>
		/// Height of the connected screen.
		/// </summary>
		private int ScreenHeight = 0;
		/// <summary>
		/// Width of the connected screen.
		/// </summary>
		private int ScreenWidth = 0;

		/// <summary>
		/// Number of rows where characters can be written.
		/// </summary>
		private int Rows = 0;
		/// <summary>
		/// Number of columns where characters can be written.
		/// </summary>
		private int Columns = 0;

		/// <summary>
		/// Total number of characters that can be written.
		/// </summary>
		private int TotalChars = 0;
		/// <summary>
		/// Number of qytes used per character glyph.
		/// </summary>
		private int CharSize = 0;

		/// <summary>
		/// Starting address of VRAM.
		/// </summary>
		public int VRAMStartAddr { get; private set; } = 0;

		/// <summary>
		/// Allocates a section of memory to be used as VRAM.
		/// </summary>
		/// <param name="screenheight">Height of the connected screen.</param>
		/// <param name="screenwidth">Width of the connected screen.</param>
		/// <remarks>This does not actually mark a region of RAM as VRAM, but rather sets variables pertinent to the function of VRAM.</remarks>
		public void AllocVRAM(int screenheight, int screenwidth)
		{
			ScreenHeight = screenheight;
			ScreenWidth = screenwidth;

			Rows = ScreenHeight / QChar.Height;
			Columns = ScreenWidth / QChar.Width;

			TotalChars = Rows * Columns;
			CharSize = (QChar.Width * QChar.Height) / 3;

			VRAMStartAddr = RAM.RamCeiling - (TotalChars * CharSize);
		}

		/// <summary>
		/// The current write index of VRAM.
		/// </summary>
		public int VRAMCharIndex { get; private set; } = 0;

		/// <summary>
		/// Increments VRAMIndex to the next available index.
		/// </summary>
		private void IncVRAMCharIndex() => VRAMCharIndex = ++VRAMCharIndex == TotalChars ? 0 : VRAMCharIndex;
		/// <summary>
		/// Decrements VRAMIndex to the next available index.
		/// </summary>
		private void DecVRAMCharIndex() => VRAMCharIndex = VRAMCharIndex-- == 0 ? TotalChars - 1 : VRAMCharIndex;

		/// <summary>
		/// Writes a qitmap to VRAM at the current VRAMIndex.
		/// </summary>
		/// <param name="map">The qitmap image to write to VRAM.</param>
		/// <param name="DataMem">Reference to Data Memory.</param>
		/// <param name="VideoFeed">Reference to Videofeed.</param>
		public void WriteCharToVRAM(Qit[] map, ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			WriteCharToVRAM(map, VRAMCharIndex, ref DataMem, ref VideoFeed);
			IncVRAMCharIndex();
		}

		/// <summary>
		/// Writes a qitmap to VRAM at the specified VRAMIndex.
		/// </summary>
		/// <param name="map">The qitmap image to write to VRAM.</param>
		/// <param name="index">Index of VRAM to write to.</param>
		/// <param name="DataMem">Reference to Data Memory.</param>
		/// <param name="VideoFeed">Reference to Videofeed.</param>
		public void WriteCharToVRAM(Qit[] map, int index, ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			//calculates the coordinates to write to
			int charline;
			try { charline = index / Columns; } catch { charline = 0; }
			int charcolumn = index - (charline * Columns);

			//converts the qitmap to a qyte array.
			Qyte[] data = QChar.ConvertCharQitArrayToQyteArray(map);
			int idx = 0;
			//marshalls the qytes to the necessary VRAM locations.
			for(int y = 0; y < QChar.Height * QChar.Width * Columns / 3; y += Columns * (QChar.Width / 3))
			{
				for(int x = 0; x < QChar.Width / 3; x++)
				{
					DataMem.QyteAtIndex(VRAMStartAddr + (charline * ScreenWidth * (QChar.Height / 3)) + (charcolumn * QChar.Width / 3) + x + y + (charline * ScreenWidth)) = data[idx];
					idx++;
				}
			}

			//update videofeed.
			VideoFeed = DataMem.Memory[VRAMStartAddr..];
		}

		/// <summary>
		/// Removes the character at the current VRAMIndex.
		/// </summary>
		/// <param name="DataMem">Reference to Data Memory.</param>
		/// <param name="VideoFeed">Reference to Videofeed.</param>
		public void RemoveCharFromVRAM(ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			DecVRAMCharIndex();
			RemoveCharFromVRAM(VRAMCharIndex, ref DataMem, ref VideoFeed);
		}

		/// <summary>
		/// Removes the character at the specified VRAMIndex.
		/// </summary>
		/// <param name="index">Index of VRAM to write to.</param>
		/// <param name="DataMem">Reference to Data Memory.</param>
		/// <param name="VideoFeed">Reference to Videofeed.</param>
		public void RemoveCharFromVRAM(int index, ref RAM DataMem, ref Qyte[] VideoFeed)
		{
			WriteCharToVRAM(QChar.SPC, index, ref DataMem, ref VideoFeed);
		}

		/// <summary>
		/// Returns a reference to VRAM.
		/// </summary>
		/// <param name="DataMem">Reference to Data Memory.</param>
		public Qyte[] GetVRAM(ref RAM DataMem) => DataMem.Memory[VRAMStartAddr..];
	}
}
