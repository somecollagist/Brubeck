using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

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
		/// Refresh rate of the emulated monitor in hertz.
		/// </summary>
		private const int RefreshRate = 2;

		internal Bitmap bmp = new(ResWidth, ResHeight);

		public Qyte[] CachedVideoFeed = new Qyte[ResHeight * ResWidth / 3];

		/// <summary>
		/// Constructor for a monitor.
		/// </summary>
		public Monitor()
		{
			InitializeComponent();

			//Set the grid's size to the size in "real pixels" i.e. number of pixels on a real monitor
			Screen.Height = ResHeight * ScaleFactor;
			Screen.Width = ResWidth * ScaleFactor;

			Array.Fill(CachedVideoFeed, new("AAA"));

			DispatcherTimer Refresher = new();
			Refresher.Interval = TimeSpan.FromMilliseconds(1000 / RefreshRate);
			Refresher.Tick += Display;
			Refresher.Start();
		}

		/// <summary>
		/// Displays the given video signal.
		/// </summary>
		void Display(object sender, EventArgs e)
		{
			Qit[] VisualQitMap =
				string.Join("", CachedVideoFeed.Select(t => t.ToString()))
				.Select(t => QitConverter.GetQitFromChar(t))
				.ToArray();

			WriteableBitmap bmp = new(ResWidth, ResHeight, 24, 24, PixelFormats.Bgr32, null);
			uint[] pixels = new uint[bmp.PixelWidth * bmp.PixelHeight];
			for (int x = 0; x < VisualQitMap.Length; x++)
			{
				byte[] colour = QitColourMapping[VisualQitMap[x]];
				pixels[x] = (uint)((colour[0] << 16) + (colour[1] << 8) + (colour[2] << 0));
			}

			bmp.WritePixels(
				new Int32Rect(0, 0, bmp.PixelWidth, bmp.PixelHeight),
				pixels,
				bmp.PixelWidth * (bmp.Format.BitsPerPixel / 8),
				0
			);

			int sumcolourpixels = pixels.Where(t => t != 0x00000000).Count();

			using(FileStream fs = new("img.bmp", FileMode.Create))
            {
				PngBitmapEncoder enc = new();
				enc.Frames.Add(BitmapFrame.Create(bmp.Clone()));
				enc.Save(fs);
            }

			PixelMap.Source = bmp;
		}

		/// <summary>
		/// Dictionary pairing Qits to colours.
		/// </summary>
		private static readonly Dictionary<Qit, byte[]> QitColourMapping = new()
		{
								// B     G     R
			{ Qit.A, new byte[] { 0x00, 0x00, 0x00} },
			{ Qit.E, new byte[] { 0x00, 0x00, 0x00 } },
			{ Qit.I, new byte[] { 0x00, 0x00, 0x00 } },
			{ Qit.O, new byte[] { 0x00, 0x00, 0x00 } },
			{ Qit.U, new byte[] { 0x00, 0xff, 0x00 } },
		};
	}
}
