using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Brubeck.Core;

namespace Brubeck.Peripheral
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class Monitor : Window
	{
		/// <summary>
		/// Width of the monitor in pixels.
		/// </summary>
		public static readonly int ResWidth = 240;

		/// <summary>
		/// Height of the monitor in pixels.
		/// </summary>
		public static readonly int ResHeight = 184;

		/// <summary>
		/// Scale factor of the monitor.
		/// </summary>
		/// <remarks>How many pixels on a real monitor make up a pixel on the emulated monitor.</remarks>
		private const int ScaleFactor = 4;

		/// <summary>
		/// Constructor for a monitor.
		/// </summary>
		public Monitor()
		{
			InitializeComponent();

			//Set the grid's size to the size in "real pixels" i.e. number of pixels on a real monitor
			Screen.Height = ResHeight * ScaleFactor;
			Screen.Width = ResWidth * ScaleFactor;

			//Dynamically create cells in the uniformgrid
			Pixels.Rows = ResHeight;
			Pixels.Columns = ResWidth;

			//Make each cell of the uniformgrid a rectangle which can be coloured (i.e. a pixel)
			for(int y = 0; y < ResHeight; y++)
			{
				for(int x = 0; x < ResWidth; x++)
				{
					Pixels.Children.Insert((y * ResWidth) + x, new Rectangle());
				}
			}
		}

		/// <summary>
		/// Displays the given video signal.
		/// </summary>
		/// <param name="input">Qyte array of the visual signal to display.</param>
		public void Display(Qyte[] input)
		{
			//Console.WriteLine("Before");
			//Qit[] VisualQitMap = string.Join("", input.Select(t => t.ToString())).Select(t => QitConverter.GetQitFromChar(t)).ToArray();
			//Console.WriteLine("After");
			//for (int y = 0; y < ResHeight; y++)
			//{
			//	Dispatcher.Invoke(() =>
			//	{
			//		for(int x = 0; x < ResWidth; x++)
			//		{
			//			Dispatcher.Invoke(() =>
			//			{
			//				Pixels.Children.SetPixel(x, y, ResWidth, QitColourMapping[VisualQitMap[y * ResWidth + x]]);
			//			});
			//		}
			//	});
			//}
			//Console.WriteLine("VRAM Written");

			Qit[] VisualQitMap = string.Join("", input.Select(t => t.ToString())).Select(t => QitConverter.GetQitFromChar(t)).ToArray();

			WriteableBitmap bmp = new(ResWidth, ResHeight, 96 / ScaleFactor, 96 / ScaleFactor, PixelFormats.Bgr24, null);
			bmp.WritePixels(VisualQitMap.Select(t => ))
		}

		/// <summary>
		/// Dictionary pairing Qits to colours.
		/// </summary>
		private static readonly Dictionary<Qit, byte[]> QitColourMapping = new()
		{
			//BGR
			{ Qit.A, new byte[] { 0, 0, 0 } },
			{ Qit.E, new byte[] { 0, 0, 0 } },
			{ Qit.I, new byte[] { 0, 0, 0 } },
			{ Qit.O, new byte[] { 0, 0, 0 } },
			{ Qit.U, new byte[] { 0, 255, 0 } },
		};
	}

	internal static class PixelGridExtensionMethods
	{
		/// <summary>
		/// Sets a pixel at a given coordinate to a certain colour.
		/// </summary>
		/// <param name="x">Pixel's altitude from the top.</param>
		/// <param name="y">Pixel's offset from the left.</param>
		/// <param name="colour">The colour to be assigned.</param>
		public static void SetPixel(this UIElementCollection pixels, int x, int y, int ResWidth, SolidColorBrush colour)
		{
			pixels.SetPixel(y * ResWidth + x, colour);
		}

		/// <summary>
		/// Sets a pixel at a given index to a certain colour. 
		/// </summary>
		/// <param name="index">The pixels index, computed left to right, top to bottom.</param>
		/// <param name="colour">the colour to be assigned.</param>
		/// <returns></returns>
		public static void SetPixel(this UIElementCollection pixels, int index, SolidColorBrush colour)
		{
			//Task.Run(() => ((Rectangle)pixels[index]).Fill = colour);
			//lock(pixels)
			//{
			//    ((Rectangle)pixels[index]).Fill = colour;
			//}
			((Rectangle)pixels[index]).Fill = colour;
		}
	}
}
