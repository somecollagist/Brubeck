using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Brubeck.Core;
using static Brubeck.Architecture.CPU;

namespace Brubeck.Architecture
{
	/// <summary>
	/// Brubeck Information Encoding System, Brubeck's equivalent of ASCII.
	/// </summary>
	/// <remarks>It's pronounced bean lol</remarks>
	public static class BIEn
	{
		public static Qyte Decode(char c)
		{
			return new(c switch
			{
				'A' => "IIO",
				'B' => "IIU",
				'C' => "IOA",
				'D' => "IOE",
				'E' => "IOI",
				'F' => "IOO",
				'G' => "IOU",
				'H' => "IUA",
				'I' => "IUE",
				'J' => "IUI",
				'K' => "IUO",
				'L' => "IUU",
				'M' => "OAA",
				'N' => "OAE",
				'O' => "OAI",
				'P' => "OAO",
				'Q' => "OAU",
				'R' => "OEA",
				'S' => "OEE",
				'T' => "OEI",
				'U' => "OEO",
				'V' => "OEU",
				'W' => "OIA",
				'X' => "OIE",
				'Y' => "OII",
				'Z' => "OIO",

				'a' => "IIE",
				'b' => "IIA",
				'c' => "IEU",
				'd' => "IEO",
				'e' => "IEI",
				'f' => "IEE",
				'g' => "IEA",
				'h' => "IAU",
				'i' => "IAO",
				'j' => "IAI",
				'k' => "IAE",
				'l' => "IAA",
				'm' => "EUU",
				'n' => "EUO",
				'o' => "EUI",
				'p' => "EUE",
				'q' => "EUA",
				'r' => "EOU",
				's' => "EOO",
				't' => "EOI",
				'u' => "EOE",
				'v' => "EOA",
				'w' => "EIU",
				'x' => "EIO",
				'y' => "EII",
				'z' => "EIE",

				')' => "OIU",
				']' => "OOA",
				'}' => "OOE",
				'>' => "OOI",

				'(' => "EIA",
				'[' => "EEU",
				'{' => "EEO",
				'<' => "EEI",

				'0' => "OOO",
				'1' => "OOU",
				'2' => "OUA",
				'3' => "OUE",
				'4' => "OUI",
				'5' => "OUO",
				'6' => "OUU",
				'7' => "UAA",
				'8' => "UAE",
				'9' => "UAI",

				' ' => "EEE",
				'.' => "EEA",
				',' => "EAU",
				'?' => "EAO",
				'!' => "EAI",
				':' => "EAE",
				';' => "EAA",
				'\'' => "AUU",
				'"' => "AUO",
				'°' => "AUI",

				'+' => "UAO",
				'*' => "UAU",
				'&' => "UEA",
				'/' => "UEE",
				'@' => "UEI",
				'|' => "UEO",
				'^' => "UEU",
				'£' => "UIA",

				'-' => "AUE",
				'=' => "AUA",
				'#' => "AOU",
				'\\' => "AOO",
				'%' => "AOI",
				'_' => "AOE",
				'~' => "AOA",
				'$' => "AIU",

				'\u0001' => "AAA", //Control, typically invoked by CTRL+A or CTRL+SHIFT+A

				_ => throw new UnknownOpcodeException("The given character is not encoded.")
			});
		}

		public static Qit[] GetMapFromCode(Qyte code)
		{
			return code.ToString() switch
			{
				"IIO" => QChar.A,		//1
				"IIU" => QChar.B,
				"IOA" => QChar.C,
				"IOE" => QChar.D,
				"IOI" => QChar.E,
				"IOO" => QChar.F,
				"IOU" => QChar.G,
				"IUA" => QChar.H,
				"IUE" => QChar.I,
				"IUI" => QChar.J,
				"IUO" => QChar.K,
				"IUU" => QChar.L,
				"OAA" => QChar.M,
				"OAE" => QChar.N,
				"OAI" => QChar.O,
				"OAO" => QChar.P,
				"OAU" => QChar.Q,
				"OEA" => QChar.R,
				"OEE" => QChar.S,
				"OEI" => QChar.T,
				"OEO" => QChar.U,
				"OEU" => QChar.V,
				"OIA" => QChar.W,
				"OIE" => QChar.X,
				"OII" => QChar.Y,
				"OIO" => QChar.Z,		//26

				"IIE" => QChar.a,		//-1
				"IIA" => QChar.b,
				"IEU" => QChar.c,
				"IEO" => QChar.d,
				"IEI" => QChar.e,
				"IEE" => QChar.f,
				"IEA" => QChar.g,
				"IAU" => QChar.h,
				"IAO" => QChar.i,
				"IAI" => QChar.j,
				"IAE" => QChar.k,
				"IAA" => QChar.l,
				"EUU" => QChar.m,
				"EUO" => QChar.n,
				"EUI" => QChar.o,
				"EUE" => QChar.p,
				"EUA" => QChar.q,
				"EOU" => QChar.r,
				"EOO" => QChar.s,
				"EOI" => QChar.t,
				"EOE" => QChar.u,
				"EOA" => QChar.v,
				"EIU" => QChar.w,
				"EIO" => QChar.x,
				"EII" => QChar.y,
				"EIE" => QChar.z,       //-26

				"OIU" => QChar.CLBR,    //27
				"OOA" => QChar.CLSQ,
				"OOE" => QChar.CLBR,
				"OOI" => QChar.GT,

				"EIA" => QChar.OPPR,	//-27
				"EEU" => QChar.OPSQ,
				"EEO" => QChar.OPBR,
				"EEI" => QChar.LT,

				"OOO" => QChar._0,		//31
				"OOU" => QChar._1,
				"OUA" => QChar._2,
				"OUE" => QChar._3,
				"OUI" => QChar._4,
				"OUO" => QChar._5,
				"OUU" => QChar._6,
				"UAA" => QChar._7,
				"UAE" => QChar._8,
				"UAI" => QChar._9,

				"EEE" => QChar.SPC,		//-31
				"EEA" => QChar.STOP,
				"EAU" => QChar.COMMA,
				"EAO" => QChar.QMARK,
				"EAI" => QChar.EMARK,
				"EAE" => QChar.COLON,
				"EAA" => QChar.SCOLON,
				"AUU" => QChar.APOS,
				"AUO" => QChar.SMARK,
				"AUI" => QChar.DEG,

				"UAO" => QChar.PLUS,	//41
				"UAU" => QChar.ASTERX,
				"UEA" => QChar.AMP,
				"UEE" => QChar.FSLASH,
				"UEI" => QChar.ATSYM,
				"UEO" => QChar.VBAR,
				"UEU" => QChar.CARET,
				"UIA" => QChar.PND,

				"AUE" => QChar.MINUS,	//-41
				"AUA" => QChar.EQUALS,
				"AOU" => QChar.HASH,
				"AOO" => QChar.BSLASH,
				"AOI" => QChar.PCENT,
				"AOE" => QChar.USCR,
				"AOA" => QChar.TILDE,
				"AIU" => QChar.DOL,

				_ => throw new UnknownOpcodeException("The given code does not have an associated glyph Qitmap.")
			};
		}

		public enum ControlCodes
		{
			/// <summary>
			/// Null Code
			/// </summary>
			NUL = 0,

			/// <summary>
			/// Line Feed
			/// </summary>
			LF  = 49,
			/// <summary>
			/// Start of Text
			/// </summary>
			STX,
			/// <summary>
			/// Horizontal Tab
			/// </summary>
			HT,
			/// <summary>
			/// Cancel
			/// </summary>
			CAN,
			/// <summary>
			/// Positive Acknowledge
			/// </summary>
			ACK,
			/// <summary>
			/// Escape
			/// </summary>
			ESC,
			/// <summary>
			/// Copy
			/// </summary>
			CPY,
			/// <summary>
			/// Cut
			/// </summary>
			CUT,
			/// <summary>
			/// Shift In
			/// </summary>
			SI,
			/// <summary>
			/// Control In
			/// </summary>
			CI,
			/// <summary>
			/// Move Up
			/// </summary>
			UP,
			/// <summary>
			/// Move Left
			/// </summary>
			LFT,

			/// <summary>
			/// Move Right
			/// </summary>
			RHT = -60,
			/// <summary>
			/// Move Down
			/// </summary>
			DWN,
			/// <summary>
			/// Control Out
			/// </summary>
			CO,
			/// <summary>
			/// Shift Out
			/// </summary>
			SO,
			/// <summary>
			/// Delete
			/// </summary>
			DEL,
			/// <summary>
			/// Paste
			/// </summary>
			PST,
			/// <summary>
			/// Stop
			/// </summary>
			/// <remarks>May be used as a control key.</remarks>
			STP,
			/// <summary>
			/// Negative Acknowledge
			/// </summary>
			NAK,
			/// <summary>
			/// Substitute
			/// </summary>
			/// <remarks>A mistake was transmitted</remarks>
			SUB,
			/// <summary>
			/// Vertical Tab
			/// </summary>
			VT,
			/// <summary>
			/// End of Text
			/// </summary>
			ETX,
			/// <summary>
			/// Bell
			/// </summary>
			BEL
		}

		public static ControlCodes GetControlCodeFromCode(Qyte code)
		{
			return code.ToString() switch
			{
				"III" => ControlCodes.NUL,	//0

				"UIE" => ControlCodes.LF,	//49
				"UII" => ControlCodes.STX,
				"UIO" => ControlCodes.HT,
				"UIU" => ControlCodes.CAN,
				"UOA" => ControlCodes.ACK,
				"UOE" => ControlCodes.ESC,
				"UOI" => ControlCodes.CPY,
				"UOO" => ControlCodes.CUT,
				"UOU" => ControlCodes.SI,
				"UUA" => ControlCodes.CI,
				"UUE" => ControlCodes.UP,
				"UUI" => ControlCodes.LFT,

				"AIO" => ControlCodes.BEL,	//-49
				"AII" => ControlCodes.ETX,
				"AIE" => ControlCodes.VT,
				"AIA" => ControlCodes.SUB,
				"AEU" => ControlCodes.NAK,
				"AEO" => ControlCodes.STP,
				"AEI" => ControlCodes.PST,
				"AEE" => ControlCodes.DEL,
				"AEA" => ControlCodes.SO,
				"AAU" => ControlCodes.CO,
				"AAO" => ControlCodes.DWN,
				"AAI" => ControlCodes.RHT,

				_ => throw new UnknownOpcodeException("The given code is not associated with a control code.")
			};
		}
	}
}
